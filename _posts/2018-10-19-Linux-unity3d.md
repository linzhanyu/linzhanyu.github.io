---
layout: post
title: Linux Unity3D 安装
categories: [linux]
tags: [linux, unity3d]
fullview: false
comments: true
---

Unity3D 自从有了Linux版本, 就不用再把开发环境往Windows平台下切换了.毕竟Linux才是最适合做开发的集成环境.

### 下载

[下载Linux版本](https://forum.unity.com/threads/unity-on-linux-release-notes-and-known-issues.350256/page-2)

[获取各版本完整安装包下载地址](https://public-cdn.cloud.unity3d.com/hub/prod/releases-linux.json)

### 安装

有一系列前置依赖包,按首页提示检查自己的系统中是否安装过.

{% highlight shell %}
> chmod +x UnitySetup-xxxx.xx.xxx
> sudo ./UnitySetup-xxxx.xx.xxx
{% endhighlight %}

### 手动安装 不用Hub

目前发布的 2018.3.0f2 无明显BUG使用起来已经非常顺手了。

{% highlight shell %}
> tar -Jxf Unity.tar.xz
> tar -Jxf UnitySetup-iOS-Support-for-Editor-2018.3.0f2.tar.xz
> mkdir Editor/Data/PlaybackEngines/AndroidPlayer
> cd Editor/Data/PlaybackEngines/AndroidPlayer
> xar -xf ../../../../UnitySetup-Android-Support-for-Editor-2018.3.0f2.pkg
> sudo apt install monodevelop
> Editor/Unity
{% endhighlight %}


