---
layout: post
title: UE5 中编译使用 Protobuf
categories: [Unreal]
tags: [protobuf]
fullview: false
comments: true
---

### UE 中第三方库的编译

编译主要得有 -fPIC 参数, 生编译器产生位置无关代码(Positio Independent Code)

产生的代码没有绝对地址, 全部使用相对地址, 故代码可以被加载到内存的任意位置, 都可以正确执行.

#### Windows 编译



#### Linux 编译

编译器的选择:

Linux下的编译器有GCC和clang可以选择, 由于UE使用的是自带的 clang 编译器, 所以我们也在系统中安装 clang 吧!

直接编译放入UE工程后, 编译链接时会报错如下:
```log
[25/114] Link (lld) libUnrealEditor-MineNet.so                                                                                                                                                                                                                                               [453/1851]
ld.lld: error: undefined symbol: std::allocator<char>::allocator()                                                                                                                                                                                                                                     
>>> referenced by arenastring.cc                                                                                                                                                                                                                                                                       
>>>               arenastring.cc.o:(google::protobuf::internal::LazyString::Init[abi:cxx11]() const) in archive /mnt/disk/jenkins/workspace/Dev-MineMind-Linux/Source/ThirdParty/Protobuf/3.21.7/lib/libprotobuf.a                                                                                     
>>> referenced by arenastring.cc                                                                                                                                                                                                                                                                       
>>>               arenastring.cc.o:(google::protobuf::internal::(anonymous namespace)::CreateString(std::__cxx11::basic_string<char, std::char_traits<char>, std::allocator<char>> const&)) in archive /mnt/disk/jenkins/workspace/Dev-MineMind-Linux/Source/ThirdParty/Protobuf/3.21.7/lib/libprotobuf
.a                                                                                                                                                                                                                                                                                                     
>>> referenced by arenastring.cc                                                                                                                   
>>>               arenastring.cc.o:(std::__cxx11::basic_string<char, std::char_traits<char>, std::allocator<char>>* google::protobuf::Arena::Create<std::__cxx11::basic_string<char, std::char_traits<char>, std::allocator<char>>, char const*, unsigned long>(google::protobuf::Arena*, char const*&&
, unsigned long&&)) in archive /mnt/disk/jenkins/workspace/Dev-MineMind-Linux/Source/ThirdParty/Protobuf/3.21.7/lib/libprotobuf.a                                                                                                                                                                      
>>> referenced 180 more times                                                                                                                                                                                                                                                                          
>>> did you mean: std::allocator<int>::allocator()                                                                                                                                                                                                                                                     
>>> defined in: /mnt/disk/jenkins/workspace/Dev-MineMind-Linux/Source/ThirdParty/Protobuf/3.21.7/lib/libprotobuf.a(descriptor.cc.o)                                                                                                                                                                    
ld.lld: error: undefined symbol: std::string::empty() const                                                                                                                                                                                                                                            
>>> referenced by miniodata.pb.cc                                                                                                                                                                                                                                                                      
>>>               miniodata.pb.cc.o:(FNetMSG::FNetMSG(FNetMSG const&)) in archive /mnt/disk/jenkins/workspace/Dev-MineMind-Linux/Source/ThirdParty/Protocol/lib/libProtocol.a
>>> referenced by miniodata.pb.cc
>>>               miniodata.pb.cc.o:(FNetMSG::_InternalSerialize(unsigned char*, google::protobuf::io::EpsCopyOutputStream*) const) in archive /mnt/disk/jenkins/workspace/Dev-MineMind-Linux/Source/ThirdParty/Protocol/lib/libProtocol.a
>>> referenced by miniodata.pb.cc
>>>               miniodata.pb.cc.o:(FNetMSG::ByteSizeLong() const) in archive /mnt/disk/jenkins/workspace/Dev-MineMind-Linux/Source/ThirdParty/Protocol/lib/libProtocol.a
>>> referenced 73 more times
ld.lld: error: undefined symbol: std::ios_base::Init::Init()
>>> referenced by arena.cc
>>>               arena.cc.o:(__static_initialization_and_destruction_0(int, int)) in archive /mnt/disk/jenkins/workspace/Dev-MineMind-Linux/Source/ThirdParty/Protobuf/3.21.7/lib/libprotobuf.a
>>> referenced by generated_message_util.cc
>>>               generated_message_util.cc.o:(__static_initialization_and_destruction_0(int, int)) in archive /mnt/disk/jenkins/workspace/Dev-MineMind-Linux/Source/ThirdParty/Protobuf/3.21.7/lib/libprotobuf.a
>>> referenced by repeated_field.cc
>>>               repeated_field.cc.o:(__static_initialization_and_destruction_0(int, int)) in archive /mnt/disk/jenkins/workspace/Dev-MineMind-Linux/Source/ThirdParty/Protobuf/3.21.7/lib/libprotobuf.a
>>> referenced 35 more times
ld.lld: error: undefined symbol: std::ios_base::Init::~Init()
>>> referenced by arena.cc
>>>               arena.cc.o:(__static_initialization_and_destruction_0(int, int)) in archive /mnt/disk/jenkins/workspace/Dev-MineMind-Linux/Source/ThirdParty/Protobuf/3.21.7/lib/libprotobuf.a
>>> referenced by generated_message_util.cc
>>>               generated_message_util.cc.o:(__static_initialization_and_destruction_0(int, int)) in archive /mnt/disk/jenkins/workspace/Dev-MineMind-Linux/Source/ThirdParty/Protobuf/3.21.7/lib/libprotobuf.a
>>> referenced by repeated_field.cc
>>>               repeated_field.cc.o:(__static_initialization_and_destruction_0(int, int)) in archive /mnt/disk/jenkins/workspace/Dev-MineMind-Linux/Source/ThirdParty/Protobuf/3.21.7/lib/libprotobuf.a
>>> referenced 35 more times
ld.lld: error: undefined symbol: google::protobuf::internal::ArenaStringPtr::Set(std::__1::basic_string<char, std::__1::char_traits<char>, std::__1::allocator<char>> const&, google::protobuf::Arena*)
>>> referenced by arenastring.h:409 (/mnt/disk/jenkins/workspace/Dev-MineMind-Linux/Source/ThirdParty/Protobuf/3.21.7/include/google/protobuf/arenastring.h:409)
>>>               /mnt/disk/jenkins/workspace/Dev-MineMind-Linux/Intermediate/Build/Linux/x64/UnrealEditor/Development/MineNet/NetworkMngr.cpp.o:(UNetworkMngr::SendMSG(MineUIData&, TDelegate<void (MineUIData const&), FDefaultDelegateUserPolicy> const&))
>>> referenced by arenastring.h:409 (/mnt/disk/jenkins/workspace/Dev-MineMind-Linux/Source/ThirdParty/Protobuf/3.21.7/include/google/protobuf/arenastring.h:409)
>>>               /mnt/disk/jenkins/workspace/Dev-MineMind-Linux/Intermediate/Build/Linux/x64/UnrealEditor/Development/MineNet/NetworkMngr.cpp.o:(UNetworkMngr::SendRequest(FString const&, TDelegate<void (bool, MineInfo const&), FDefaultDelegateUserPolicy> const&))
>>> referenced by arenastring.h:409 (/mnt/disk/jenkins/workspace/Dev-MineMind-Linux/Source/ThirdParty/Protobuf/3.21.7/include/google/protobuf/arenastring.h:409)
>>>               /mnt/disk/jenkins/workspace/Dev-MineMind-Linux/Intermediate/Build/Linux/x64/UnrealEditor/Development/MineNet/NetworkMngr.cpp.o:(UNetworkMngr::SendRequest(FString const&, TDelegate<void (bool, FString const&), FDefaultDelegateUserPolicy> const&))
>>> referenced 5 more times
ld.lld: error: undefined symbol: std::__cxx11::basic_string<char, std::char_traits<char>, std::allocator<char>>::~basic_string()
>>> referenced by generated_message_util.cc
>>>               generated_message_util.cc.o:(google::protobuf::internal::DestroyString(void const*)) in archive /mnt/disk/jenkins/workspace/Dev-MineMind-Linux/Source/ThirdParty/Protobuf/3.21.7/lib/libprotobuf.a
>>> referenced by extension_set.cc
>>>               extension_set.cc.o:(google::protobuf::internal::(anonymous namespace)::Register(google::protobuf::internal::ExtensionInfo const&)) in archive /mnt/disk/jenkins/workspace/Dev-MineMind-Linux/Source/ThirdParty/Protobuf/3.21.7/lib/libprotobuf.a
>>> referenced by extension_set.cc
>>>               extension_set.cc.o:(google::protobuf::internal::(anonymous namespace)::Register(google::protobuf::internal::ExtensionInfo const&)) in archive /mnt/disk/jenkins/workspace/Dev-MineMind-Linux/Source/ThirdParty/Protobuf/3.21.7/lib/libprotobuf.a
>>> referenced 1497 more times
ld.lld: error: undefined symbol: google::protobuf::internal::ArenaStringPtr::Set(std::string const&, google::protobuf::Arena*)
>>> referenced by miniodata.pb.cc
>>>               miniodata.pb.cc.o:(FNetMSG::FNetMSG(FNetMSG const&)) in archive /mnt/disk/jenkins/workspace/Dev-MineMind-Linux/Source/ThirdParty/Protocol/lib/libProtocol.a
>>> referenced by miniodata.pb.cc
>>>               miniodata.pb.cc.o:(FNetMSG::MergeImpl(google::protobuf::Message&, google::protobuf::Message const&)) in archive /mnt/disk/jenkins/workspace/Dev-MineMind-Linux/Source/ThirdParty/Protocol/lib/libProtocol.a
>>> referenced by miniodata.pb.cc
>>>               miniodata.pb.cc.o:(FNetCMD::FNetCMD(FNetCMD const&)) in archive /mnt/disk/jenkins/workspace/Dev-MineMind-Linux/Source/ThirdParty/Protocol/lib/libProtocol.a
>>> referenced 35 more times

```

如何来分析这个问题?

1. 首先要观察 libprotobuf.a 和 引用该静态库的 MineNet/XXX.o 

```shell
# 
nm -C Source/ThirdParty/Protobuf/3.21.7/lib/libprotobuf.a | grep "basic_string" | head
# 得到结果表示使用 libc++ 编译的
nm -C Source/ThirdParty/Protobuf/3.21.7/lib/libprotobuf.a | c++filt | grep "std::__1" | head
# 得到结果表示使用 libstdc++ 编译的
nm -C Source/ThirdParty/Protobuf/3.21.7/lib/libprotobuf.a | c++filt | grep "std::__cxx11" | head
# 检查 UE5 编译后用的是哪种方式
nm -C Intermediate/Build/Linux/x64/MineMindClient/Development/MineNet/MineNet.cpp.o | c++filt | grep "std::__1" | head
```

通过以上命令我确定了我自编的UE5是使用了 clang + libc++ 的方式.

所以只需要让 Protobuf 编译的和 UE 的方式相同了即可.


```shell
sudo apt install clang
# 使用 libc++ 
sudo apt install libc++-dev libc++abi-dev

export CC=clang
export CXX=clang++

cmake -B build \
 -DCMAKE_POSITION_INDEPENDENT_CODE=ON \
 -Dprotobuf_BUILD_TESTS=OFF \
 -DCMAKE_CXX_FLAGS="-stdlib=libc++ -fPIC -DGOOGLE_PROTOBUF_INTERNAL_DONATE_STEAL_INLINE=1" \
 -DCMAKE_CXX_COMPILER=clang++ \
 -DCMAKE_C_COMPILER=clang \
 -DCMAKE_INSTALL_PREFIX=<YourWorkspace>/Source/ThirdParty/Protobuf/3.21.7

cmake --build build --config Release --target install -j8

```


