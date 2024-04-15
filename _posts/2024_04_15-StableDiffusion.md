---
layout: post
title: AMD 5700XT 显卡安装使用 Stable Diffusion
categories: [vscode]
tags: [vscode]
fullview: false
comments: true
---

#### 必要的选择

rocm5.2 是必要的 如果使用了更高的版本就跑不起来了

pip3 install torch==1.13.1+rocm5.2 torchvision==0.14.1+rocm5.2 torchaudio==0.13.1+rocm5.2 --extra-index-url https://download.pytorch.org/whl/rocm5.2

