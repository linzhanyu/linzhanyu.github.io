---
layout: post
title: openSUSE 进行大版本升级
categories: [openSUSE, zypper, dup]
tags: [openSUSE, zypper, dup]
fullview: false
comments: true
---

## 如何快速的对OpenSUSE进行大版本升级

{% highlight shell %}
> sed -i s/42.2/42.3/ /etc/zypp/repos.d/*
> zypper dup
{% endhighlight %}


