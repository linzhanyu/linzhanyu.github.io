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

#### 翻转方法一
{% highlight vim %}
:g/.*/mo0 
# 或者 
:g/^/mo0
{% endhighlight %}

解释: 首先是 g 命令, '/' 间的是查找体, 对于找到的结果执行 mo 命令(move), 0是行号

#### 翻转方法二

{% highlight vim %}
" 可以只翻转选中的部分
:!tac
{% endhighlight %}

#### 翻转方法三

{% highlight vim %}
" 只能翻转一整行 选中行中一部分无效
:!rev
{% endhighlight %}

### 如何删除符合某种匹配情况的整行

{% highlight vim %}
:g/xxxx/d
{% endhighlight %}


### 如何删除不匹配某种情况的整行

{% highlight vim %}
:v/xxxx/d
{% endhighlight %}

### 将文本列对齐

{% highlight vim %}
:n,m!column -t
{% endhighlight %}



