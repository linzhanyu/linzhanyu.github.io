---
layout: post
title: VIM 常用操作
categories: [vim]
tags: [vim]
fullview: false
comments: true
---

介绍各种常用的VIM操作指令
### 如何翻转文本行

{% highlight vim %}
:g/.*/mo0 
# 或者 
:g/^/mo0
{% endhighlight %}

解释: 首先是 g 命令, '/' 间的是查找体, 对于找到的结果执行 mo 命令(move), 0是行号

### 如何删除符合某种匹配情况的整行

{% highlight vim %}
:g/xxxx/d
{% endhighlight %}


### 如何删除不匹配某种情况的整行

{% highlight vim %}
:v/xxxx/d
{% endhighlight %}



