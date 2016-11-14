---
layout: post
title: EFI 引导的 GPT 分区如何安装 Linux 
categories: [linux, efi, gpt, grub2]
tags: [linux, install]
fullview: false
comments: true
---

自从拆下一块硬盘给 Raspberry PI 做数据共享后, 一直想把咱的 openSUSE 装到固态硬盘中.但由于机器里有一块4T的硬盘分区表就全搞成 GPT 的了.那么也就只能使用 UEFI 来引导计算机了.
Linux使用了十几年了,从来没有这么折腾过.

## 第一波 -- 黑屏
从把 openSUSE Leap ISO 写到U盘, 从SSD硬盘划出60G的空间给将来的 Linux 系统, 开始了这次奇幻之旅.

从U盘启动计算机, 选择开始安装, 滚动了几行加载文字后, 黑屏....循环....黑屏!!!

开始以为是发行版的问题 换 Debian, Linux mint, Ubuntu 均是类似问题.

这看起来是个通病了, 由于一直以为是4T硬盘和GPT分区搞怪, 把所有的硬盘都拔了,问题依旧(当然了问题出在了显卡)

为什么以前在安装Linux的时候那么顺利,现在就这种样的问题呢?

开始回忆到底都有哪些硬件改动.

>1. 之前用的是 AMD 的 HD5770, 去年烧掉了,换了AMD R9.但后来重装了显卡驱动后,当时的系统也能正常工作.
>2. 加了4T的硬盘, 把所有分区表都升级成 GPT, 并使用 EFI 引导.

实在没有什么办法, 找Google帮忙吧.

啊哈~有类似的问题 需要在 Grub 的时候添加参数  **nomodeset** 来禁用加载显卡驱动

尝试 ... 

果然有变化. 看起来禁止加载 radeon 驱动后黑屏问题是初步解决了.

但是引导文字滚动了几行后显示一些USB失败的提示, 并且找不到U盘的安装盘了.

## 第二波 -- USB设备丢失

## 第三波 -- 光盘安装终于成功启动后USB设备无效

## Happy Func
在用 aticonfig 生成了双显的配置后,稍加修改写到 openSUSE 的配置文件中. 重启 - **十秒开机**. 超赞!
后面开始装各种库,继续我在 openSUSE 上的开发之路.

