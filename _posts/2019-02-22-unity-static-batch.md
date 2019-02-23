---
layout: post
title: Unity Static Batch 原理分析
categories: [unity3d]
tags: [unity3d]
fullview: false
mermaid: true
comments: true
---

### 深入分析一下 Unity3D 的 Static Batch

#### 为什么要使用 Static Batch

[如前文所说]({% post_url 2016-08-14-unity-optimization %}):降低 DrawCall 减轻 CPU 负担 
>


#### Static Batch 工作时间点

> 1. **在执行Build的时候**, 如果你打开 PlayerSettings.StaticBatching 的话, 副作用增加AssetBundle体积
> 1. **调用Mesh.CombineMeshes**, 需要场景中的物体选择了 Static 选项, 并且FBX的打开 Read/Write Enabled. 副作用顶点内存空间占用x2

#### 静态合批原理

{% mermaid %}
graph TD;
	模型A-->合批模型1;
	...  -->合批模型1;
	模型N-->合批模型1;
	合批模型1-->渲染A;
	合批模型1-->渲染A+N;
	合批模型1-->Vertex_Buffer;
	渲染A-->MeshA_Vertex;
	渲染A+N-->MeshA+N_Vertex;
	Vertex_Buffer-->GPU;
	MeshA_Vertex-->Rebuild_Index_Buffer;
	MeshA+N_Vertex-->Rebuild_Index_Buffer;
	Rebuild_Index_Buffer-->GPU;
	GPU-->视锥顶点裁剪;
{% endmermaid %}

由于静态合批后,各个模型顶点数据被合并成一个大Buffer,但是在场景中某些合批模型并不显示,如何减少这部分三角形的数量?由于是静态合批VertexBuffer是不能变动的.这时就是由Rebuild_Index_Buffer来实现.这样就可以控制可显示模型的三角面在一帧中被绘制.


剩余的三角形依然很多,有些并不在摄像机范围内.如果绘制必造成对GPU资源的浪费.在GPU中进行视锥裁剪,视锥顶点裁剪的工作原理请看 [GLES2.0渲染管线]({% post_url 2019-02-22-GLES-Graphics-Pipline %})



