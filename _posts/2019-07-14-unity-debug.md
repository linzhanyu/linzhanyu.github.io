---
layout: post
title: Unity Debug 技巧
categories: [linux]
tags: [linux]
fullview: false
comments: true
---

死循环定位技巧

有时候一口气码了一堆代码，但运行时偶发的死循环一定让你头皮发麻。从哪儿开始查呢？

其实要定位这种问题并不算难，请出Visual Studio 挂上Unity以后，让Unity进入到死循环状态，然后在VS中“暂停”当前调试进程。然后某个线程中就会有你的代码栈正处于死循环状态。

