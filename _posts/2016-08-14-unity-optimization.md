---
layout: post
title: Unity 优化之奇技淫巧
categories: [game, unity3d, optimization]
tags: [unity3d, optimization]
fullview: false
comments: true
---

自从 Unity3d **免费** 以来，非常多的公司个人搭上这只贼船。由于易上手，太多的项目粗枝滥造，不槛入目。经过几个项目的历练，我们来总结一下Unity3D使用过程中应该注意的一些地方吧。希望能起到点作用。

### 优化的目的：

> 1. 保证 FPS
> 2. 在满足第一点的情况下 画质,工时,效率 这三点进行权衡

### 优化的步骤

> 1. 真机测试看 FPS
> 2. 看Profiler看CPU运行时间 如果CPU耗时高就是 CPU Bound 否则就是 GPU Bound
> 3. 如果是 CPU Bound 就打各种 Profiler 细查耗时的函数
> 4. 如果是 GPU Bound 就用各种厂家提供的检查工具 比如
* IOS 上可用 XCode GPU capture frame 
* Android 上可以用 Adreno Profiler 或者 Mali develop tools
> 5. 如果即不是CPU也不是GPU,那就好玩了,检查一下带宽,尝试打开 mipmap

### 优化的目标

* FPS : 30 电影一般为24帧, 游戏个别时候可能耗时较多,但不要低于20帧
* MEM : 内存 Reserved Total：150MB。其中：纹理资源：50 MB；网格资源：20 MB；动画片段：15 MB；音频片段：15 MB；Mono堆内存：40 MB；其他：10 MB
* Load: 场景加载时间 5s

## CPU
做游戏 80% 的代码都是跑在 CPU 上的，除了游戏中的逻辑代码需要在CPU做相关计算之外，还有很多图形相关的计算是由CPU驱动的。

所以写每一个函数的时候都应该走脑子，一个不留神这些代码就造成我们背锅，加班的罪魁祸首。

### Script
Unity3d 为我们提供了一种非常简捷的方式 **MonoBehaviour** 来让大家方便地实现自己的功能,但是有不少的开发者认为只要把功能做在 MonoBehaviour 中，再挂到需要的 GameObject 上就是**组件式编程**了，殊不知这样并不是组件式编程，少量的挂接并不会造成太大的问题，但当这些 GameObject 被代码大量的实例化以后，情况就不是那么乐观了。

好，为什么会有问题捏？

Unity3d 是C++写成的，我们在使用 Unity3d 时，所写的代码是 C# 写成的。C++ 是自己分配并释放内存 我们称其为 **Native堆**，而C#是将所有的动态内存对象存放在一个称做**托管堆**的地方。托管堆是 CLR 划出的地址空间，并且由CLR做垃圾回收。C# 做为一种 C++ 的脚本语言，当有 MonoBehaviour 挂在 GameObject 上时C++需要为这些脚本准备相应的Native堆，并为它们准备执行序列，轮流调用。在C++调用C#的时候需要做一些准备工作，而这些准备工作是有开销的。所以这种调用远比 C# 直接调用 C# 要慢得多，而且内存占用也大的多。

### DrawCall

#### DrawCall 是什么？
OpenGL ES 的函数调用中 glDrawElements 负责将三角形的绘制发送给显卡驱动程序，每一次调用就是一个 DrawCall。这个函数可以一次将非常多的三角形下发。

打个比方说，一个模型有 1000 个三角形组成，如果场景中有10个这样的模型。

>1. 把1000个三角形画10次，得到了场景画面A；
>1. 把10000个三角形画1次，得到了场景画面B；

在精心准备的情况下，这个画面A和画面B是可以相同的。

#### Static Batching

>1. **相同材质**
>2. **不能移动**

唯一的条件使得静态合并更容易使用，当然它是有副作用的。不能移动的原因猜测是由于每个模型的世界坐标的不同，合并后有些矩阵没法分别给单个模型单独设置。就只好把顶点的位置预先计算好，存在一个Vertex Buffer中，就这样带来了顶点内存使用量x2 如果顶点数量太多的时候，这里需要小心。

尽量多的静态批次，DrawCall一定是最大化合并的。因为它很容易获得。

另外在代码中 StaticBatchingUtility.Combine 也可以用来对没有勾Static的节点进行统一合并。


**主要用途** 场景模型

#### Dynamic Batching

>1. **材质相同**
>2. **模型总顶点数<900**
>3. **用法线单个模型顶点数<300**
>4. **用UV0, UV1 或Tangent时单模型顶点数不超<180**
>5. **相同的缩放值**
>6. **没有使用 lightmap**
>7. **Shader 没有使用多 Pass**

只要满足上面的条件，这些模型会被自动 Dynamic Batching 合并 DrawCall

**主要用途** 特效粒子动画

#### Culling

经过精心对 Static batch 和 Dynamic batch 的安排大部分的游戏项目 DrawCall 数已经能够进入到一个可接受的范围。如果依然很高，说明你的项目内容是比较复杂的了。

剔除操作很重要，Unity3d 提供的 Occlusion Culling （遮挡剔除）是一个非常有用的功能。它会耗用CPU时间，如果你只是想通过该功能来减少DrawCall来使CPU使用更高效的话，大多数情况下就可以放弃了。

方便且可靠的剔除方式是视矩剔除 Camera.layerCullDistances 在场景内有大量模型该功能虽然很粗暴但确实可以有效降低 DrawCall，但是太大的场景在模型贴图使用量上必然会更多，Unity自身的场景管理目前还不足以良好地支撑这种方式，大场景如不做特殊处理无法避免内存占用过大。

#### LOD

经常我们要对游戏做远近景的处理，也需要对不同性能的机型做适配处理。这时就应该想到 LOD 的相关使用。

LOD : Level of Detail 的简称, 意思是多细节层次。根据物体模型的节点在显示环境中所处的位置和重要程度，决定物体渲染的资源分配，降低次要垂体的面数和细节，从而提高渲染运算效率。

>1. 模型LOD
>2. 纹理LOD
>3. Shader LOD

### Animation

UWA 有一篇文章写得很详细了[详细了解](https://blog.uwa4d.com/archives/Optimization_Animation.html)

### Physics

2017.4 之前的版本还是别用了 Destroy 时会产生不可预料的 Crash

如果你的游戏在CPU使用上已经有些卡顿了就别使用这种效果了

## IO

网络IO、磁盘IO 都可以称为IO访问，对于CPU来说从发起访问到等待结果送到中间是一个非常慢长的过程，所以可以通过多线程或者协程来减少对CPU时间的浪费。

## Mem

一般存在于下面几种可能的情况

>1. 数据表太大
>2. 纹理格式错误使用
>3. 内存泄漏

### 托管堆

### GC

### Lua

[Lua 内存快照](2018-01-24-lua-high-performance.md)
[Lua 控制Garbage](2018-04-16-lua-garbage.md)

## GPU

### OverDraw

每一段代码都会有它的输入/输出 做为GPU的输出最终就体现在画在屏幕上的像素数量

如果在一帧中大量重复绘制某些像素，就会给GPU造成超负荷劳动。


### Shader

对 Shader 的优化其实极其类似于CPU的优化过程，不外乎几个方面：

>1. 浮点数精度
>2. 访问的数据是否已经在GPU的缓存中
>3. 太过复杂的光照计算

#### if 到底怎么啦

GPU对代码是并行处理的, 在一部分GPU上会把 if/else 都做计算，然后取其一个分支的结果做为最后的输出。

### Texture

#### Tex2D 的那些事儿


### Bus

GPU 和 CPU 虽然可能共享同一块内存芯片，也可能会使用统一的地址空间，但终归它们是两个计算设备。

>1. 不要在同一帧中频繁访问 FrameBuffer 否则会造非常太的总线带宽压力
>2. 排除第1项的可能性后，如果Bus依然有压力尝试打开 Mipmap


