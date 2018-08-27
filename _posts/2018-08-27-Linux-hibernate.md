---
layout: post
title: Linux 命令行关机，重启，待机，休眠
categories: [linux]
tags: [linux]
fullview: false
comments: true
---

### 关机
{% highlight shell %}
sudo halt
sudo init 0
sudo shutdown -h now
sudo shutdown -h 0
{% endhighlight %}

### 定时/延时关机
{% highlight shell %}
sudo shutdown -h 19:00
sudo shutdown -h +30    ## 单位为分钟
{% endhighlight %}

### 重启
{% highlight shell %}
sudo reboot
sudo init 6
sudo shutdown -r now
{% endhighlight %}

### 待机
{% highlight shell %}
sudo pm-suspend
sudo pm-suspend-hybrid
sudo echo "mem" >/sys/power/state
sudo hibernate-ram
{% endhighlight %}

### 休眠
{% highlight shell %}
sudo pm-hibernate
sudo echo "disk" >/sys/power/state
sudo hibernate-disk
{% endhighlight %}

