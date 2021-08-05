---
layout: post
title: 手动控制 AMD RX5700XT 的风扇转速
categories: [hardware]
tags: [hardware]
fullview: false
comments: true
---

#### 第一次频繁死机

这一代显卡真安静啊,温度达不到一定的高度风扇完全是不转的.但是主板把M.2插槽设计在了显卡的下方,三星的硬盘承受不了这么高的温度.太热了会诱发死机.

用下面的命令会可以手动控制风扇的转速

/etc/rc.local

{% highlight shell %}
cd /sys/class/drm/card0/device/hwmon/hwmon1
echo 1 | sudo tee fan1_enable
echo 90 | sudo tee pwm1
{% endhighlight %}


---

#### 第二次频繁死机

到底是哪里出了问题? 最近系统更新后,又出现了频繁的死机

dmsg 查看发现有一处 Crash 和 edac 驱动有关系

这个驱动是服务器内存ECC校验用的,咱的内存也没有这个功能,先屏蔽了它试试:

{% highlight shell %}
sudo vim /etc/modprobe.d/backlist.conf
{% endhighlight %}

中加入以下内容:
{% highlight conf %}
blacklist amd64_edac_mod
{% endhighlight %}

目前为止已经稳定的运行了几天了,甚好!


---

#### 第三次偶发死机

经过前两次的死机原因探索,对这台机器基本也有了一个较全面的认识,这几天夏天到了,天气越来越热,这个机器又会偶发死机,开盖检查显卡依然烫手.可怜的三星nvme硬盘,怕是要被这个显卡烤坏了.之前已经把风扇转速控制写在 rc.local 中了,那么就把怀疑点集中在待机环节上.

经过一翻检查,待机后显卡的风扇确实又停了!!!

那么就需要找到一个唤醒后执行的脚本,把显卡风扇转速控制写进去.

待机相关命令:pm-suspend

查找帮助写入相关配置: /etc/pm/sleep.d/20_MyGraphicDevice

{% highlight shell %}
#!/bin/sh

# 让显卡风扇自动转着点儿
GRAPHIC_DIR=/sys/class/drm/card0/device/hwmon/hwmon1

case "${1}" in
        hibernate)
# nothing
            ;;
        resume|thaw)
            echo 1 | tee $GRAPHIC_DIR/fan1_enable
            echo 90 | tee $GRAPHIC_DIR/pwm1
            ;;
esac

{% endhighlight %}

这次在唤醒之后显卡风扇也能如期工作.

