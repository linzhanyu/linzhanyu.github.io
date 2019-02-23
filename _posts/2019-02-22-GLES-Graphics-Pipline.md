---
layout: post
title: GLES 渲染管线
categories: [unity3d]
tags: [unity3d]
fullview: false
comments: true
---


#### GPU 渲染管线

![GLES2.0GraphicsPipeline](/assets/image/GLES2_0GraphicsPipline.png)

> 1. Vertex Array / Buffer Object : 顶点数据来源, 这是渲染管线的顶点输入, 通常使用Buffer Object效率更好
> 1. Vertex Shader : **顶点着色器** 通过可编程方式实现对顶点的操作,如进行坐标转换,计算 per-vertex color以及纹理坐标
> 1. Primitive Assembly：**图元装配**，经过着色器处理之后的顶点在图片装配阶段被装配为基本图元。OpenGL ES 支持三种基本图元：点，线和三角形，它们是可被 OpenGL ES 渲染的。接着对装配好的图元进行**裁剪**（clip）：保留完全在视锥体中的图元，丢弃完全不在视锥体中的图元，对一半在一半不在的图元进行裁剪；接着再对在视锥体中的图元进行**剔除**处理（cull）：这个过程可编码来决定是剔除正面，背面还是全部剔除。
> 1. Rasterization：**光栅化**。在光栅化阶段，基本图元被转换为二维的片元(fragment)，fragment 表示可以被渲染到屏幕上的像素，它包含位置，颜色，纹理坐标等信息，这些值是由图元的顶点信息进行插值计算得到的。这些片元接着被送到片元着色器中处理。这是从顶点数据到可渲染在显示设备上的像素的质变过程。
> 1. Fragment Shader：**片元着色器**通过可编程的方式实现对片元的操作。在这一阶段它接受光栅化处理之后的fragment，color，深度值，模版值作为输入。
> 1. Per-Fragment Operation：在这一阶段对片元着色器输出的每一个片元进行一系列测试与处理，从而决定最终用于渲染的像素。这一系列处理过程如下：

![GLES2.0Per-Fragment Operation](/assets/image/PerFragmentOperation.png)

> 1. Pixel ownership test：该测试决定像素在 framebuffer 中的位置是不是为当前 OpenGL ES 所有。也就是说测试某个像素是否对用户可见或者被重叠窗口所阻挡
> 1. Scissor Test：**剪裁测试**，判断像素是否在由 glScissor 定义的剪裁矩形内，不在该剪裁区域内的像素就会被剪裁掉
> 1. Stencil Test：**模版测试**，将模版缓存中的值与一个参考值进行比较，从而进行相应的处理
> 1. Depth Test：**深度测试**，比较下一个片段与帧缓冲区中的片段的深度，从而决定哪一个像素在前面，哪一个像素被遮挡
> 1. Blending：**混合**，混合是将片段的颜色和帧缓冲区中已有的颜色值进行混合，并将混合所得的新值写入帧缓冲；
> 1. Dithering：**抖动**，抖动是使用有限的色彩让你看到比实际图象更多色彩的显示方式，以缓解表示颜色的值的精度不够大而导致的颜色剧变的问题。
> 1. Framebuffer：这是流水线的最后一个阶段，Framebuffer 中存储这可以用于渲染到屏幕或纹理中的像素值，也可以从Framebuffer 中读回像素值，但不能读取其他值（如深度值，模版值等）。

