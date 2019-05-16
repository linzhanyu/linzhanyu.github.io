---
layout: post
title: VIM 常用操作
categories: [vim]
tags: [vim]
fullview: false
comments: true
---
### 如何翻转文本行

{% highlight vim %}
:g/.*/mo0 
# 或者 
:g/^/mo0
{% endhighlight %}

### 如何删除符合某种匹配情况的整行

{% highlight vim %}
:g/xxxx/d
{% endhighlight %}


### 如何删除不匹配某种情况的整行

{% highlight vim %}
:v/xxxx/d
{% endhighlight %}



