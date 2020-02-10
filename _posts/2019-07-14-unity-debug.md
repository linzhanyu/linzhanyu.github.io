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

VSCode 挂上Unity调试以后点暂停也可以

---

当然在Editor中出现的问题总是容易处理的.有时候你的APK安装到真机后会出现这样的情况:

![Stack Overflow](/assets/image/StackOverflow.jpg)

并且该堆栈信息不能用下面的命令来看到堆栈的调用函数

{% highlight shell %}
ndk-stack -sym AndroidStudioProject/wonderland/src/main/jniLibs/armabi-v7a -dump crash.log
{% endhighlight %}

对于这样生硬的堆栈信息,首先注意到报出来的异常的是 Signal 11. 然后寄存器信息中没有明显的可导致 Crash 的地址. 再往下就得看 backtrace 了.

backtrace 中出现了大量的 0117bb04 的地址. 很明显就是一个递归调用的函数. 想想代码中哪里写了递归的"深度遍历"计算呢? 找到它去做覆盖测试,一定能还原问题的本质.


