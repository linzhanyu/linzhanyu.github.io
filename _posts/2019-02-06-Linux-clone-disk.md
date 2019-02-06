---
layout: post
title: Linux 分区压缩备份
categories: [linux]
tags: [linux]
fullview: false
comments: true
---

在日常操作中偶尔会需要用U盘安装一下操作系统，装完后原U盘还希望将U盘中的文件恢复为原来的结构。

遇到这种问题就可以先用下面的方法备份原U盘中的内容，然后将系统盘镜像写入U盘，用毕再还原U盘镜像。

### 备份

{% highlight shell %}
$ sudo dd if=/dev/sdf |gzip >win10_ryzen2.gz
$ sudo dd if=/dev/sdf |pigz -9 -c >win10_ryzen2_2.gz
$ sudo dd if=/dev/sdf |pxz -T 8 -z -9 -
$ sudo dd if=/dev/sdf |7z a -si win10_ryzen2.7z
{% endhighlight %}

### 还原

{% highlight shell %}
$ gzip -dc win10_ryzen2.gz |sudo dd of=/dev/sdf
$ pxz -T 8 -d -c win10_ryzen2.xz |sudo dd of=/dev/sdf
$ 7z x -so win10_ryzen2.7z |sudo dd of=/dev/sdf
{% endhighlight %}

### 多线程优化

* pigz 是 gzip 的多线程版本
* pxz 是 xz 的多线程版本

### 总结

* gzip / pigz 压缩速度和内存占用都比较低，但是压缩率也不如 xz 高。低性能配置可以使用。
* xz 格式和 7z 都是使用 lzma 压缩算法，压缩率接近。7z 暂无多线程版本，速度略慢。
* pxz 有多线程支持,可以利用多线程，加快压缩速度。同时也会使用大量内存。高性能配置可以使用。

### 不足

* 由于U盘中有部分空间并未使用，但使用该备份命令依然会对其进行记录，使得备份文件体积比较大。
* 如果U盘中的内容不包含引导，直接用压缩命令备份文件目录，备份文件会更小。
* 当然对于包含引导的磁盘分区，可以单独备份引导扇区，分区表。然后再对需要的分区内文件进行压缩打包备份。

还原的时候还原引导扇区，分区表，挂载分区，解压文件目录压缩包。
这个方式就有点类似构建一个大名鼎鼎的 Ghost 了。


