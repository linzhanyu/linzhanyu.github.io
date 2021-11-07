---
layout: post
title: Linux 开发中使用VSCode连接进程PID调试
categories: [vscode]
tags: [linux, vscode]
fullview: false
comments: true
---

VScode连接 Python 进程调试时出现 "timed out waiting for debug server to connect" 提示, 连接失败.


---


#### 设置为许可模式

修改 /etc/sysctl.d/10-ptrace.conf 文件中的 ptrace 的值


{% highlight conf %}
kernel.yama.ptrace_scope = 0
{% endhighlight %}


重新加载该文件

{% highlight shell %}
sysctl -p /etc/sysctl.d/10-ptrace.conf
{% endhighlight %}

这样操作后 F5 启动调试即可通过PID连接相应进程进行调试了.


