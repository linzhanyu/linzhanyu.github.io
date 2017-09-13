---
layout: post
title: Unity3D + tolua 项目中的内存控制
categories: [unity, tolua, optimization]
tags: [unity, tolua, memory, leak]
fullview: false
comments: true
---

Unity3D 项目如果出现了内存泄漏，这可是个令人头疼的问题。
尤其是 C# + LUA 同时存在的项目

## 起因
由于IOS平台无法简单地实现代码层面的功能热更新, 大家想了各种各样的方法来做了不同的方案.
比如早先的 xLua, uLua 虽性能较差但也已经可用.
而后的 LSharp 它最初的想法很好,代码都使用C#语言编写,让IL直接变成脚本解释器的资源,但由于引入了 il2cpp 后这种实现方式出现了问题.后来也没有什么更新了.
这时又有tolua诞生直接使用C形式的Lua接口提高了LUA的运行时性能.LUA语言小巧简单易编写,对从业人员要求低.提供了代码热更新的一种良好的实现方案.
手机游戏中代码需要热更新的部分主要集中于 UI / 网络协议 / 数据表

## 实际问题
为什么会占用太多的内存呢? 主要原因是由于 C# / Lua 这一类语言的内存释放是基于GC的.而GC又是基于变量的引用计数.那么程序代码中必要要做的事情就是解开相互之间关联的引用.

内存问题主要集中于两个方面:
1. 内存占用太多
代码编写不合理功能模块使用了太多的内存
该释放的部分在工作时间段结束后依然没有明确的释放

2. 内存泄露 看起来释放了但其实没有
比如 GameObject 已经不存在了,但是 Component / Material / Texture 还依然存在, 而且再没有代码可以访问并且释放它们.

## 详述
Unity 项目中的内存占用主要由 Native Heap, Managed Heap 两部分组成.本地堆和托管堆的主要区别就是是否受C#的内存垃圾收集器自动管理.

代码中大块的对像主要是这么几个部分:
#### C# 部分:
Asset Bundle			资源包
Asset Resource			资源包里的资源文件
Asset Instantiate		实例化以后的 GameObject 以及上面挂着的各种 Component / Material / Texture 
![AssetBundle](/assets/image/assetbundle.jpg)

#### LUA 部分:
tolua 的 Lua 是C的, 是不受托管堆管理的,也就是说是在 NativeHeap 中的.但是它的逻辑最终主要是操控某个 GameObject 或者是某个 C# 的 Class Object ,但是 Object 是存在于托管堆中的, 那么也就是说只要 Lua 需要控制, C# 托管堆中的 Object 就得活着. 不能被垃圾回收器自动回收掉.

由于代码写在了Lua中还有一种需要做的事情是C#回调Lua中的函数,比如要在等等用户点击某处的时候进行一个操作 Lua 需要提供一个 Delegate / Action 样的东西给C#层来使用.

C#语言由于没有明显的析构函数,为了使用非托管资源做了一个颇具争议的IDisposeable接口.继承这个接口的对象在不使用的时候要明确的调用以释放非托管资源.

### tolua 中有一些关键点:
C# 给Lua用的对象为了保持它的存活tolua使用了一个 ObjectTranslator 来为它们建立引用以保障不被垃圾回收.
Lua 给C#中使用回调函数之类会集中在 funcRefMap / delegateMap 中.以保持引用数据不被Lua回收

那么只要这些地方的引用计数在维护过程中出现问题就会导致内存资源被持续持有


## 分析方法
### 打印C#堆内实例
使用第三方插件 UnityHeapDump. 由于该代码在输出的过程中会撑大 mono heap 经常在还没有输出完成的时候就发生无法分配内存.即使完成了打印操作,游戏逻辑也会出现一些奇怪的问题,而无法继续.

但是这个插件是可以打印出那些关联的比较大的对象或者某个容器中的东西比较多.

### 打印Lua堆内实例 并做对比
使用第三方插件 LuaMemorySnapshotDump. 该代码由Lua书写, 在打印Lua内存的过程中可能会造成Lua无法分配内存的问题.不过不常见.

## 解决
>1. 代码BUG
>1.1. 通过观察我们发现C#的 Struct 会在 ObjectTranslator 中产生大量的复制品,如果是每帧会访问多次的Struct建议改为访问Class
>1.2. 需要在特定的时刻调用 LuaState.RefreshDelegateMap

>2. 框架失误
>2.1. 在 Delegate 的执行函数中试图从链中删除自己的 Delegate 这是办不到的.
>2.2. 框架在设计时没有在恰当的时候明确的调用LuaFunction / LuaTable 之类的对象的 Dispose 函数 导致该LUA对象引用的其它的C#对象也不会释放.造成大量的内存泄漏.
>2.3. 恰当频繁的手动执行LuaGC.

