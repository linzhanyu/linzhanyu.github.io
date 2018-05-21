---
layout: post
title: 让Lua内存更稳定
categories: [unity, lua, garbage, optimization]
tags: [unity, lua, garbage, optimization]
fullview: false
comments: true
---

LUA 做为一门带有GC的脚本语言稍不留神就可能导致内存不足的代码产生，这里帮你解决这个问题。

### 编写内存稳定的Lua代码

### 由于忒简单先上能够解决问题的LUA代码

初始化的时候，找个地方调用一次就可以了

{% highlight lua %}
collectgarbage("setpause",100)
collectgarbage("setstepmul",5000)
{% endhighlight %}

### 再来说说原理

垃圾回收做为一个



