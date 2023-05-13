---
layout: post
title: OpenWRT 拒绝DNS污染
categories: [linux]
tags: [linux, openwrt]
fullview: false
comments: true
---


### DNS 污染

一些手机会在DHCP获取网络配置时,自动添加 114.114.114.114 来做DNS服务器,这样就在局域网中加入了被国内DNS污染的风险.

与其这样,不如我们先把它劫持了. 转发给自己搭建的防劫持DNS服务器.

防支持的DNS服务器抢建方法参考v2ray等梯子的搭建

{% highlight shell %}
iptables -t nat -A PREROUTING -p udp -d 114.114.114.114--dport 53 -j DNAT --to-destination 127.0.0.1:53
{% endhighlight %}


