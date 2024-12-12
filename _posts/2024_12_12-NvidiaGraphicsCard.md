---
layout: post
title: Nvidia Graphics Card
categories: [hardware]
tags: [hardware]
fullview: false
comments: true
---

# Nvidia 显卡在Ubuntu22.04下调节风扇转速

- 方法1
```bash
sudo vim /etc/X11/Xwrapper.config
# 添加
needs_root_rights=yes
```

- 方法2
```bash
sudo vim /usr/share/X11/xorg.conf.d/10-nvidia.conf
# 添加
Option     "Coolbits" "4"
```

```bash
nvidia-settings  -a "[fan:0]/GPUTargetFanSpeed=50"
```


