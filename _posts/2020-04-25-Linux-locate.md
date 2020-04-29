---
layout: post
title: Linux 下快速查找文件
categories: [linux]
tags: [linux]
fullview: false
comments: true
---

用习惯了 Windows 下的 everything，换到Linux时还真有点不太习惯，还好找到了 locate 来弥补这个功能。

### 工作依赖

查询系统上需要预构建文件索引数据库（如果将来的文件系统可以支持统一的快速查询方式那将多么美好）

系统中有自动任务 /etc/cron.daily/mlocate 自动构建

手动构建可以使用：

{% highlight shell %}
updatedb
{% endhighlight %}

构建文件索引数据库需要遍历整个根文件系统，极其消耗资源


### 正式使用

1. 查找速度快
1. 全路径模糊查找
1. 权限安全

#### 命令参数：

-i   不区分大小写

-n # 只列举#个匹配项

-r   支持正则表达式
