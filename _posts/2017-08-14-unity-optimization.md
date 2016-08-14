---
layout: post
title: Unity 优化之奇技淫巧
categories: [game, unity3d, optimization]
tags: [unity3d, optimization]
fullview: false
comments: true
---

自从 Unity3d **免费** 以来，非常多的公司个人搭上这只贼船。由于易上手，太多的项目粗枝滥造，不槛入目。经过几个项目的历练，我们来总结一下Unity3D使用过程中应该注意的一些地方吧。希望能起到点作用。

## CPU
做游戏 80% 的代码都是跑在 CPU 上的，除了游戏中的逻辑代码需要在CPU做相关计算之外，还有很多图形相关的计算是由CPU驱动的。

所以写每一个函数的时候都应该走脑子，一个不留神这些代码就造成我们背锅，加班的罪魁祸首。

### Script
Unity3d 为我们提供了一种非常简捷的方式 **MonoBehaviour** 来让大家方便地实现自己的功能,但是有不少的开发者认为只要把功能做在 MonoBehaviour 中，再挂到需要的 GameObject 上就是**组件式编程**了，殊不知这样并不是组件式编程，少量的挂接并不会造成太大的问题，但当这些 GameObject 被代码大量的实例化以后，情况就不是那么乐观了。

好，为什么会有问题捏？

Unity3d 是C++写成的，我们在使用 Unity3d 时，所写的代码是 C# 写成的。C++ 是自己分配并释放内存 我们称其为 **Native堆**，而C#是将所有的动态内存对象存放在一个称做托管堆的地方。托管堆是 CLR 划出的地址空间，并且由CLR做垃圾回收。C# 做为一种 C++ 的脚本语言，当有 MonoBehaviour 挂在 GameObject 上时C++需要为这些脚本准备相应的Native堆，并为它们准备执行序列，轮流调用。在C++调用C#的时候需要做一些准备工作，而这些准备工作是有开销的。所以这种调用远比 C# 直接调用 C# 要慢得多，而且内存占用也大的多。

### DrawCall

#### Dynamic Batching

#### Static Batching

#### LOD

## GPU

### Shader

#### if 到底怎么啦

### Texture

#### Tex2D 的那些事儿


### Bus

## IO

## Mem

### 托管堆

### GC



