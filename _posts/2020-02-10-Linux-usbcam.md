---
layout: post
title: Linux 下使用USB-Cam的方法
categories: [linux]
tags: [linux]
fullview: false
comments: true
---

在Linux下可以很方便高效地使用你的USB-Camera，我来记录一下使用方法。

---

#### 安装要使用的工具

{% highlight shell %}
sudo apt-get install v4l-utils guvcview
{% endhighlight %}

#### 查看可用的设备列表

{% highlight shell %}
v4l2-ctl --list-devices
{% endhighlight %}

#### 打开视频察看窗口

{% highlight shell %}
guvcview -d /dev/video0 -u h264 -F 30 -x 1920x1080 -f RGB3 -m full
{% endhighlight %}

#### 扩展用法

guvcview 支持命令行截图，视频录制。并且可以选择硬件所支持的不同的分辨率，视频格式，编码，帧率。
