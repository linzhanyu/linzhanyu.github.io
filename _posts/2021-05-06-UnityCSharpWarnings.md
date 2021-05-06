---
layout: post
title: Unity C# Warnings as Errors
categories: [unity3d]
tags: [CShape, BUG]
fullview: false
comments: true
---

泛型代码的错误书写会导致原有的Warning都变成Error

Example:

{% highlight cshape %}
private static StringBuilder Append<T>( this StringBuilder sb, Action<T> action ) where T : System.Enum
{
    ...
}

sb.Append<>( format => {} )

{% endhighlight %}

Append<> 即可令你所有的 Warning 都变成 Error. 编译器BUG?
