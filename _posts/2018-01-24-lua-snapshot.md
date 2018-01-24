---
layout: post
title: Lua 内存快照
categories: [unity, lua, memleak, optimization]
tags: [unity, lua, memleak, optimization]
fullview: false
comments: true
---


### Lua 内存快照

使用云风的方法

https://github.com/cloudwu/lua-snapshot

1. 生成lua内存snapshot，可通过两个snapshot比较出新创建但未清除引用的内存
1. 但是查不到清除引用且没被回收的内存，比如大量的临时对象

简易用法如下

{% highlight lua %}
local S1 = snapshot()

local tmp = {}
tmp.xx = { 1, 2, 3 }
tmp.yy = "dfgsj";

local S2 = snapshot()

for k, v in pairs(S2) do
	if S1[k] == nil then
		print(k, "-- ", v)
	end
end

{% endhighlight %}


