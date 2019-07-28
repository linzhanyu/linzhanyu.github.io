---
layout: post
title: HDF5 支持的 Select 与存盘方式
categories: [python]
tags: [python, pandas, HDF5]
fullview: false
comments: true
---

HDF5 是一种高性能轻量化小型数据库

Pandas 中的 HDF5Store 可以便捷地将数据存取入HDF5文件

如果想要直接通过 HDF5Store.select 来读取少量数据 在数据表存盘的时候就必须得用 

db.append( path, dataframe, ... )

的形式存入。如果使用的是 put 形式存入的数据是不能使用 select 来读取的。

