---
layout: post
title: Python 中安装使用 Tensorflow
categories: [linux, python3, pip, tensorflow]
tags: [linux, tensorflow]
fullview: false
comments: true
---

看了好长时间的机器学习，终于有点上道的感觉。
笔记本上有NVIDIA的显卡，刚好可以用来学习一下 Tensorflow 。其实就是想跑一下它的GPU加速版本，

## 平台与环境
openSUSE Leap 42.1 (x86_64)
	小技巧：查看发行版本 lsb_release -a
Python 3.4.5

## 准备知识
>1. Tensorflow 的 GPU 版本需要 CUDA 的支持
>2. CUDA 需要 NVIDIA 官方驱动的支持
>3. 在集显和独显共存的笔记本上，需要 Bumblebee 的支持

## 安装 Bumblebee
https://en.opensuse.org/SDB:NVIDIA_Bumblebee
注意添加源的时候要和自己的系统版本号匹配

## 安装 CUDA
https://developer.nvidia.com/cuda-downloads
安装完成后需要设置 PATH 和 LD_LIBRARY_PATH 环境变量
或者更改 bashrc 和 /etc/ld.so.conf 记得执行 ldconfig

## 安装 Tensorflow
安装 CPU 版本 tensorflow (有N卡的机型不推荐)
sudo pip3 install tensorflow
安装 GPU 版本 tensorflow
sudo pip3 install tensorflow-gpu
如果有问题的话可能是依赖的 numpy 版本低
sudo pip3 install --upgrade numpy

## 测试
optirun python3
# 
