---
layout: post
title: VIM 删除重复行
categories: [vim]
tags: [vim]
fullview: false
comments: true
---

在处理一些LOG文件内容的时候，有些重复的行需要只保留其中一行


1. 先排序

:sort

2. 处理该删除的行 （可用）

:sor ur /^/

3. 或者已序文本

{% highlight vim %}
:g/^\(.*\)$\n\1$/d              //去除重复行
:g/\%(^\1$\n\)\@<=\(.*\)$/d     //功能同上，也是去除重复行
:g/\%(^\1\>.*$\n\)\@<=\(\k\+\).*$/d  //功能同上，也是去除重复行 
{% endhighlight %}

4. 外部处理
   1. sort uniq 命令组合
   sort file | uniq 
   2. 使用awk
   awk ‘!a[$0]++’ file

