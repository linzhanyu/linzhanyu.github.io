---
layout: post
title: openSUSE 进行大版本升级
categories: [openSUSE, zypper]
tags: [openSUSE, zypper, dup]
fullview: false
comments: true
---

现代的发行版大都包含有方便的大版本升级方式

## 如何快速的对OpenSUSE进行大版本升级

{% highlight shell %}
> sed -i s/42.2/42.3/ /etc/zypp/repos.d/*
> zypper dup
{% endhighlight %}


