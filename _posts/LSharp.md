---
layout: post
title: Test test test!
categories: [general, setup, demo]
tags: [demo, linzhanyu, setup]
fullview: true
comments: true
---

### LSharp

LSharp 项目工程配置

Unity 项目中使用 LSharp 需要注意的问题
  *  不要在 L# 中定义模板代码
  * 委托不要在 L# 代码中定义
  * 不要在 GameObject 上直接挂 Core 中的代码
  * L# 和 C# 代码中类型的不同 使用反射时要注意 比如涉及到 typeof 的代码(尽量少用吧.)
  * func(i++) 这样的代码，i++ 会被先计算，谨慎使用。

