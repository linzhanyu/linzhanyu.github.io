---
layout: post
title: 转移整个系统到新硬盘上不重装
categories: [linux]
tags: [linux]
fullview: false
comments: true
---

情景是这样的：我有一台老式的台式机电脑，新添了一个固态硬盘，我想把装在原来的机械硬盘上的操作系统转移到新的固态硬盘上。原来的操作系统是Debian 8 (Jessie)，机械硬盘容量是250G；新的固态硬盘是120G的。

我想要达成的目的是：

操作系统和应用软件装在固态硬盘中（固态硬盘寿命到了，文件找不回来也没有太大损失）；
主目录分区/home 还用原来机械硬盘中的主目录分区；
swap分区也是原来机械键盘上旧系统的swap分区（减少固态硬盘的写入操作）。
经过调研（就是狂搜）和失败的尝试，我在这里总结一个可行的流程。往新盘上转移操作系统，其实可以简单的分成三步：新硬盘分区，复制文件，设置引导程序和文件系统。至于固态硬盘的优化，或者4K对齐等等问题，网上的教程已经足够详细了，不是重点，所以我不打算写这方面的内容。

完成以下步骤所需要的主要工具是：grub, gparted, rsync.

新硬盘分区
在我的系统上，固态硬盘对应设备/dev/sdb。首先，用普通用户身份启动 gparted, 命令是

{% highlight shell %}
$ gksu gparted
{% endhighlight %}

---

输入超级用户密码，即可使用gparted的图形界面。选择设备/dev/sdb, 然后执行菜单 Device -> Create Partition Table 命令，在分区表类型中选择 msdos。这种分区表类型就是使用传统的MBR空隙记录引导程序的文件信息。为什么不选用更新的GPT？如果是很老的台式机，BIOS可能不不支持EFI设备，也就不支持从GPT分区启动了。

在创建了分区表后，为简单起见，我只分了一个区，文件系统的类型设置为对SSD支持还算好的ext4。至此新硬盘分区这一步骤完成。

复制文件
复制文件最关键的是保持文件的属主、权限和属性等。网传的dd方式有一些限制，例如要求源盘比目标盘小，否则就要指定写入数据的量。也有人提出要tar命令，我没有验证。更直接的方式是使用rsync命令。

首先把新盘挂载在/mnt目录。执行下面的命令都需要超级用户。

{% highlight shell %}
mount -t ext4 -o defaults,noatime,discard /dev/sdb1 /mnt
{% endhighlight %}

暂不解释文件系统挂载选项。接下来执行rsync命令：

{% highlight shell %}
rsync -aAXv --exclude='/dev/*' --exclude='/proc/*' --exclude='/sys/*' --exclude='/tmp/*' --exclude='/run/*' --exclude='/mnt/*' --exclude='/media/*' --exclude='/lost+found/*' --exclude='/home/*' / /mnt
sudo rsync -aAXv --exclude='/dev/*' --exclude='/proc/*' --exclude='/sys/*' --exclude='/boot/efi/*' --exclude='/tmp/*' --exclude='/run/*' --exclude='/mnt/*' --exclude='/media/*' --exclude='/lost+found/*' / /mnt/newsys
{% endhighlight %}

这里的选项 A 表示在复制过程中保留文件的ACLs信息， X表示保留文件的扩展信息， a 是常用选项的组合，相当于 rlptgoD, 而v表示在复制过程中显示当前正在复制的文件名。被排除在外的目录中，/dev, /proc, /sys, /run是在启动过程中生成的；/tmp中的文件不重要；/lost+found目录只适用特定的分区，复制到其他分区是无用的；/home目录不要复制，它将挂载在新系统中。注意，一定要用单引号，禁止shell把符号*作展开。

取决了固态硬盘的写入速度和系统文件的大小，这个过程可能需要几分钟的时间。

设置引导程序和文件系统
接下来修改 /mnt/etc/fstab文件，修改新系统的文件系统挂载选项。这默认是一个只读文件，所做的修改需要强行写入才能保持。现在最可靠的办法是使用UUID标识分区。获取各分区的UUID的一个简便方法是使用命令blkid, 不带参数即可在屏幕显示所有分区的UUID。

设置引导程序的方法，自动化程度最高的一种是chroot到新系统所在的目录，用grub的工具自动完成。这需要绑定挂载一些目录：

{% highlight shell %}
mount --bind /proc /mnt/proc
mount --bind /sys  /mnt/sys
mount --bind /dev  /mnt/dev
mount --bind /run  /mnt/run
mount /dev/sda1 /mnt/boot/efi
{% endhighlight %}
然后执行chroot命令

{% highlight shell %}
chroot /mnt
{% endhighlight %}
把引导程序安装到新硬盘上

{% highlight shell %}
grub-install /dev/sdb
{% endhighlight %}
最后自动配置grub引导程序

{% highlight shell %}
update-grub
{% endhighlight %}
至此，所有的步骤都已完成，接下来就是重启系统，在BIOS中修改启动媒介的顺序，从新硬盘优先启动。

上述内容是我在参考以下两个网页，并实际操作后写总结的。
