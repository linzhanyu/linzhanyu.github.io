---
layout: post
title: 数学统计的实际意义
categories: [math]
tags: [math, statistics]
fullview: false
comments: true
---

## 标准差

标准差是指一组数据的离散程度或变化量的一种度量，通常用符号 σ 表示。标准差越大，说明数据的离散程度越高，反之则说明数据越接近平均值。标准差的计算公式如下：

$$\sigma=\sqrt{\frac{1}{N}\sum_{i=1}^{N}(x_i-\mu)^2}$$

其中，$\sigma$ 表示标准差，$x_i$ 表示第 $i$ 个数据点，$\mu$ 表示数据的均值，$n$ 表示数据点的数量。

在实际应用中，标准差经常用于衡量数据的稳定性和风险程度，例如在金融领域中，投资者经常使用标准差来评估股票或基金的风险水平。标准差还可以用于判断数据是否符合正态分布。如果一组数据符合正态分布，那么其约 68% 的数据会落在平均值的一个标准差内，约 95% 的数据会落在平均值的两个标准差内，约 99.7% 的数据会落在平均值的三个标准差内。


## 残差

在统计学中，残差指的是一个实际观测值与对应的模型预测值之间的差异。也就是说，它是实际值与预测值之间的偏差，表示了模型未能解释的部分。如果一个模型能够完全解释实际观测值的变异，那么其残差将为零。

在机器学习中，残差可以用来评估模型的性能和预测精度。更准确地说，残差是在训练模型时用来计算误差的。模型的目标是最小化残差的平方和，这个值也被称为损失函数。最小化损失函数的过程就是模型的训练过程，即找到最优的模型参数，使得预测值和实际值之间的差异最小化。

在回归分析中，残差可以用来检验回归模型的适当性和是否符合假设。如果残差的方差不均匀或存在自相关性，那么就可能需要对模型进行调整。