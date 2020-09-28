---
layout: post
title: 手动控制 AMD RX5700XT 的风扇转速
categories: [hardware]
tags: [hardware]
fullview: false
comments: true
---

这一代显卡真安静啊,温度达不到一定的高度风扇完全是不转的.但是主板把M.2插槽设计在了显卡的下方,三星的硬盘承受不了这么高的温度.太热了会诱发死机.

用下面的命令会可以手动控制风扇的转速

{% highlight shell %}
cd /sys/class/drm/card0/device/hwmon/hwmon1
echo 1 | sudo tee fan1_enable
echo 90 | sudo tee pwm1
{% endhighlight %}

#### 后记

到底是哪里出了问题? 最近系统更新后,又出现了频繁的死机

dmsg 查看发现有一处 Crash 和 edac 驱动有关系

这个驱动是服务器内存ECC校验用的,咱的内存也没有这个功能,先屏蔽了它试试:

{% highlight shell %}
sudo vim /etc/modprobe.d/backlist.conf
{% endhighlight %}

中加入以下内容:
{% highlight conf %}
blacklist amd64_edac_mod
{% endhighlight %}

目前为止已经稳定的运行了几天了,甚好!
