---
layout: post
title: Ubuntu22.04 设置无法打开
categories: [ubuntu]
tags: [gnome]
fullview: false
comments: true
---
### 记录一次Ubuntu下设置打不开的奇怪问题
``` shell
$ echo $DISPLAY
echo $XDG_SESSION_TYPE
gnome-control-center --verbose
:0
wayland
总线错误 (核心已转储)
```

尝试重新安装 gnome-control-center, gnome-desktop 之类均未能修复, 只好挂上GDB看看了

```shell
$ gdb gnome-control-center
GNU gdb (Ubuntu 12.1-0ubuntu1~22.04.2) 12.1
Copyright (C) 2022 Free Software Foundation, Inc.
License GPLv3+: GNU GPL version 3 or later <http://gnu.org/licenses/gpl.html>
This is free software: you are free to change and redistribute it.
There is NO WARRANTY, to the extent permitted by law.
Type "show copying" and "show warranty" for details.
This GDB was configured as "x86_64-linux-gnu".
Type "show configuration" for configuration details.
For bug reporting instructions, please see:
<https://www.gnu.org/software/gdb/bugs/>.
Find the GDB manual and other documentation resources online at:
    <http://www.gnu.org/software/gdb/documentation/>.

For help, type "help".
Type "apropos word" to search for commands related to "word"...
Registered pretty printers for UE classes
Registered pretty printers for UE classes
Registered pretty printers for UE classes
Reading symbols from gnome-control-center...
(No debugging symbols found in gnome-control-center)
(gdb) r
Starting program: /usr/bin/gnome-control-center 

Program received signal SIGBUS, Bus error.
memset () at ../sysdeps/x86_64/multiarch/memset-vec-unaligned-erms.S:283
283	../sysdeps/x86_64/multiarch/memset-vec-unaligned-erms.S: 没有那个文件或目录.
(gdb) l
278	in ../sysdeps/x86_64/multiarch/memset-vec-unaligned-erms.S
(gdb) c
Continuing.

Program terminated with signal SIGBUS, Bus error.
The program no longer exists.
```

那么我们发现 memset 这个函数 Crash 了. 我们检查更底层的gnome依赖的文件是否出现了损坏或异常.


``` shell
sudo apt update
sudo apt install debsums
sudo debsums -s
```

### 图形驱动和渲染相关库损坏：
``` shell
$ sudo debsums -s
debsums: changed file /usr/lib/firefox/libxul.so (from firefox package)
debsums: changed file /usr/lib/x86_64-linux-gnu/dri/kms_swrast_dri.so (from libgl1-mesa-dri:amd64 package)
debsums: changed file /usr/lib/x86_64-linux-gnu/dri/r600_dri.so (from libgl1-mesa-dri:amd64 package)
debsums: changed file /usr/lib/x86_64-linux-gnu/dri/radeonsi_dri.so (from libgl1-mesa-dri:amd64 package)
debsums: changed file /usr/lib/x86_64-linux-gnu/dri/swrast_dri.so (from libgl1-mesa-dri:amd64 package)
debsums: changed file /usr/lib/x86_64-linux-gnu/dri/virtio_gpu_dri.so (from libgl1-mesa-dri:amd64 package)
debsums: changed file /usr/lib/x86_64-linux-gnu/dri/vmwgfx_dri.so (from libgl1-mesa-dri:amd64 package)
debsums: changed file /usr/lib/i386-linux-gnu/dri/kms_swrast_dri.so (from libgl1-mesa-dri:i386 package)
debsums: changed file /usr/lib/i386-linux-gnu/dri/r600_dri.so (from libgl1-mesa-dri:i386 package)
debsums: changed file /usr/lib/i386-linux-gnu/dri/radeonsi_dri.so (from libgl1-mesa-dri:i386 package)
debsums: changed file /usr/lib/i386-linux-gnu/dri/swrast_dri.so (from libgl1-mesa-dri:i386 package)
debsums: changed file /usr/lib/i386-linux-gnu/dri/virtio_gpu_dri.so (from libgl1-mesa-dri:i386 package)
debsums: changed file /usr/lib/i386-linux-gnu/dri/vmwgfx_dri.so (from libgl1-mesa-dri:i386 package)
debsums: changed file /usr/lib/x86_64-linux-gnu/libLLVM-13.so.1 (from libllvm13:amd64 package)
debsums: changed file /usr/lib/x86_64-linux-gnu/libLLVM-14.so.1 (from libllvm14:amd64 package)
debsums: changed file /usr/lib/x86_64-linux-gnu/libLLVM-15.so.1 (from libllvm15:amd64 package)
debsums: changed file /usr/lib/x86_64-linux-gnu/libQt5WebEngineCore.so.5.15.9 (from libqt5webenginecore5:amd64 package)
debsums: changed file /usr/lib/x86_64-linux-gnu/libwebkit2gtk-4.0.so.37.72.6 (from libwebkit2gtk-4.0-37:amd64 package)
debsums: changed file /lib/systemd/system/logrotate.timer (from logrotate package)
debsums: changed file /usr/lib/x86_64-linux-gnu/dri/r600_drv_video.so (from mesa-va-drivers:amd64 package)
debsums: changed file /usr/lib/x86_64-linux-gnu/dri/radeonsi_drv_video.so (from mesa-va-drivers:amd64 package)
debsums: changed file /usr/lib/x86_64-linux-gnu/dri/virtio_gpu_drv_video.so (from mesa-va-drivers:amd64 package)
debsums: changed file /usr/lib/x86_64-linux-gnu/vdpau/libvdpau_r600.so.1.0.0 (from mesa-vdpau-drivers:amd64 package)
debsums: changed file /usr/lib/x86_64-linux-gnu/vdpau/libvdpau_radeonsi.so.1.0.0 (from mesa-vdpau-drivers:amd64 package)
debsums: changed file /usr/lib/x86_64-linux-gnu/vdpau/libvdpau_virtio_gpu.so.1.0.0 (from mesa-vdpau-drivers:amd64 package)
debsums: changed file /usr/share/misc/pci.ids (from pci.ids package)
debsums: changed file /usr/lib/thunderbird/libxul.so (from thunderbird package)
debsums: changed file /usr/lib/thunderbird/omni.ja (from thunderbird package)
```

### 重装受损核心库
``` shell
sudo apt install --reinstall \
libgl1-mesa-dri:amd64 \
libgl1-mesa-dri:i386 \
mesa-va-drivers:amd64 \
mesa-vdpau-drivers:amd64 \
libllvm13:amd64 \
libllvm14:amd64 \
libllvm15:amd64 \
libwebkit2gtk-4.0-37:amd64 \
libqt5webenginecore5:amd64 \
logrotate \
firefox \
thunderbird \
pci.ids
```
### 再次尝试
``` shell
$ gdb gnome-control-center
run
```

成功了!

