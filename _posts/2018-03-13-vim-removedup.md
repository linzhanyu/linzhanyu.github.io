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

2. 处理该删除的行

:sor ur /^/

或者

:g/^.*$\n\1$/d

又或

:%s/^\(.*\)\(\n\1\)\+$/\1/
