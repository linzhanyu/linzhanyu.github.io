---
layout: post
title: PandoraBox 常用配置
categories: [linux]
tags: [linux, openwrt]
fullview: false
comments: true
---

本来想直接用起来OpenWRT的，折腾了一下还是放弃了，技术不行还是有难度。

刷了个 PandoraBox 老固件，听说新版本名字已经改了。刷这个固件是因为它的IPv6默认支持得比较好。

---

### IPv6 配置

#### 让子网得到IPv6地址地址

![IPv6-LAN](/assets/image/IPv6Setting.png)

#### 添加IPv6的数据包转发
{% highlight shell %}
ip6tables -t nat -A POSTROUTING -o eth0.2 -j MASQUERADE
{% endhighlight %}

---

### IPTV 配置

{% highlight shell %}
opkg update
opkg install mcproxy
opkg install udpxy
{% endhighlight %}


#### 配置 mcproxy

{% highlight conf %}
######################################
##-- mcproxy configuration script --##
######################################

# Erase or comment out the following line when configured
# disable;

# Protocol: IGMPv1|IGMPv2|IGMPv3 (IPv4) - MLDv1|MLDv2 (IPv6)
protocol IGMPv3;

# Proxy Instance: upstream ==> downstream
pinstance proxy1: "eth0.2" ==> "br-lan";
# pinstance proxy2: "eth0.2" ==> "br-lan";
# pinstance proxy3: eth0 ==> eth1 eth2;
# 

{% endhighlight %}


#### 配置 udpxy

![IPv6-LAN](/assets/image/udpxySetting.png)

#### 查看 udpxy 状态

[udpxy-status](http://192.168.10.1:8012/status)

#### 配置防火墙

{% highlight conf %}
config rule                             
        option name 'Allow-IGMP'                        
        option src 'wan'                                   
        option proto 'igmp'        
        option family 'ipv4'                             
        option target 'ACCEPT'         

config rule                                     
        option name 'Allow-UDP-mcproxy' 
        option src 'wan'                  
        option proto 'udp'           
        option dest 'lan'                   
        option dest_ip '224.0.0.0/4'                     
        option target 'ACCEPT'                           
                                   
config rule                                     
        option name 'Allow-UDP-udpxy' 
        option src 'wan'               
        option proto 'udp'            
        option dest_ip '224.0.0.0/4'                       
        option target 'ACCEPT'                             
                               
config forwarding                    
        option src 'wan'                
        option dest 'lan'               
{% endhighlight %}

#### VLC 播放测试：

可以通过VLC(Linux) / PotPlayer(Windows) 打开类似如下地址播放
http://192.168.10.1:8012/udp/239.2.1.129:8000/

![IPv6-LAN](/assets/image/vlc-iptv.png)

---

### Transparent over the wall

未完待续
