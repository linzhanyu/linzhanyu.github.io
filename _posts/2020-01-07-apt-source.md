---
layout: post
title: apt-get 使用国内源及自己搭建镜像
categories: [linux]
tags: [apt]
fullview: false
comments: true
---

这个方法绝对会给Linux用户在使用过程中带来一个非常好的体验!

#### 给系统用上国内 apt 源

{% highlight shell %}
sudo sed -i 's/cn.archive.ubuntu.com/mirrors.aliyun.com/g' /etc/apt/sources.list
{% endhighlight %}

#### 自己搭建 APT 源镜像

{% highlight conf %}
############# config ##################
#
# set base_path    /var/spool/apt-mirror
#
# set mirror_path  $base_path/mirror
# set skel_path    $base_path/skel
# set var_path     $base_path/var
# set cleanscript $var_path/clean.sh
# set defaultarch  <running host architecture>
# set postmirror_script $var_path/postmirror.sh
# set run_postmirror 0

set base_path    /mnt/disk1/apt-mirror
set defaultarch  amd64

set nthreads     20
set _tilde 0
#
############# end config ##############

# deb http://ftp.us.debian.org/debian unstable main contrib non-free
# deb-src http://ftp.us.debian.org/debian unstable main contrib non-free

# mirror additional architectures
#deb-alpha http://ftp.us.debian.org/debian unstable main contrib non-free
#deb-amd64 http://ftp.us.debian.org/debian unstable main contrib non-free
#deb-armel http://ftp.us.debian.org/debian unstable main contrib non-free
#deb-hppa http://ftp.us.debian.org/debian unstable main contrib non-free
#deb-i386 http://ftp.us.debian.org/debian unstable main contrib non-free
#deb-ia64 http://ftp.us.debian.org/debian unstable main contrib non-free
#deb-m68k http://ftp.us.debian.org/debian unstable main contrib non-free
#deb-mips http://ftp.us.debian.org/debian unstable main contrib non-free
#deb-mipsel http://ftp.us.debian.org/debian unstable main contrib non-free
#deb-powerpc http://ftp.us.debian.org/debian unstable main contrib non-free
#deb-s390 http://ftp.us.debian.org/debian unstable main contrib non-free
#deb-sparc http://ftp.us.debian.org/debian unstable main contrib non-free

# clean http://ftp.us.debian.org/debian

deb [arch=amd64] http://repo.radeon.com/rocm/apt/debian/ xenial main


{% endhighlight %}

只要硬盘空间够大,可以享受飞一般的速度

执行命令

{% highlight shell %}
apt-mirror
{% endhighlight %}
