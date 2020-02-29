---
layout: post
title: 通过 dhcpcd 配置IP地址
categories: [linux]
tags: [linux]
fullview: false
comments: true
---

对于IP地址通常情况下是用 /etc/network/interfaces 中配置的,最近为了统一RaspberryPI的环境使用 dhcpcd 来配置.

#### 安装 dhcpcd

{% highlight shell %}
sudo apt install dhcpcd5
{% endhighlight %}

为啥是 dhcpcd5 ? 那是因为要支持IPv6的地址丫.

---


#### 网络接口改名

安装完 dhcpcd 后，系统中的网络接口名称就变了，我要把它改回熟悉的 eth0 !

{% highlight shell %}
sudo vi /etc/udev/rules.d/70-persistent-net.rules
{% endhighlight %}

添加如下内容，注意MAC地址修改成自己的：

{% highlight rules %}
SUBSYSTEM=="net", ACTION=="add", DRIVERS=="?*", ATTR{address}=="b8:ac:6f:65:31:e5", ATTR{dev_id}=="0x0", ATTR{type}=="1", KERNEL=="eth*", NAME="eth0"
{% endhighlight %}

---

#### 配置 dhcpcd

在 /etc/dhcpcd.conf 最后添加如下内容，注意修改成自己的子网IP地址。

{% highlight conf %}
interface eth0
static ip_address=192.168.10.10/24
static routers=192.168.10.1
static domain_name_servers=192.168.10.1 8.8.8.8
{% endhighlight %}

