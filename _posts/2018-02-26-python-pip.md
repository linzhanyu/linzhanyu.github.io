---
layout: post
title: Python 中 pip 一键安装依赖包
categories: [python, pip]
tags: [python, pip]
fullview: false
comments: true
---

### Python 使用 pip 一键安装所有依赖模块

经常使用一些开源项目的话，其代码会引用到一些第三方模块，逐个安装不厌其烦，而且还有版本号的匹配问题。

## 安装依赖模块
{% highlight shell %}
python -m pip install -r requirements.txt
{% endhighlight %}

## 生成依赖文件
{% highlight shell %}
python -m pip freeze > requirements.txt
{% endhighlight %}

