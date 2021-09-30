---
layout: post
title: Raspberry PI 3B+ 支持 OpenCL 1.2
categories: [opencl]
tags: [opencl, rspi3]
fullview: false
comments: true
---

{% highlight shell %}
$ sudo clinfo
Number of platforms                               1
  Platform Name                                   OpenCL for the Raspberry Pi VideoCore IV GPU
  Platform Vendor                                 doe300
  Platform Version                                OpenCL 1.2 VC4CL 0.4.9999
  Platform Profile                                EMBEDDED_PROFILE
  Platform Extensions                             cl_khr_il_program cl_khr_spir cl_khr_create_command_queue cl_altera_device_temperature cl_altera_live_object_tracking cl_khr_icd cl_vc4cl_performance_counters
  Platform Extensions function suffix             VC4CL
 
  Platform Name                                   OpenCL for the Raspberry Pi VideoCore IV GPU
Number of devices                                 1
  Device Name                                     VideoCore IV GPU
  Device Vendor                                   Broadcom
  Device Vendor ID                                0xa5c
  Device Version                                  OpenCL 1.2 VC4CL 0.4.9999
  Driver Version                                  0.4.9999
  Device OpenCL C Version                         OpenCL C 1.2 
  Device Type                                     GPU
  Device Profile                                  EMBEDDED_PROFILE
  Device Available                                Yes
  Compiler Available                              Yes
  Linker Available                                No
  Max compute units                               1
  Max clock frequency                             300MHz
  Core Temperature (Altera)                       53 C
  Device Partition                                (core)
    Max number of sub-devices                     0
    Supported partition types                     None
    Supported affinity domains                    (n/a)
  Max work item dimensions                        3
  Max work item sizes                             12x12x12
  Max work group size                             12
 
  Preferred work group size multiple              1
  Preferred / native vector sizes                 
    char                                                16 / 16      
    short                                               16 / 16      
    int                                                 16 / 16      
    long                                                 0 / 0       
    half                                                 0 / 0        (n/a)
    float                                               16 / 16      
    double                                               0 / 0        (n/a)
  Half-precision Floating-point support           (n/a)
  Single-precision Floating-point support         (core)
    Denormals                                     No
    Infinity and NANs                             No
    Round to nearest                              No
    Round to zero                                 Yes
    Round to infinity                             No
    IEEE754-2008 fused multiply-add               No
    Support is emulated in software               No
    Correctly-rounded divide and sqrt operations  No
  Double-precision Floating-point support         (n/a)
  Address bits                                    32, Little-Endian
  Global memory size                              268435456 (256MiB)
  Error Correction support                        No
  Max memory allocation                           268435456 (256MiB)
  Unified memory for Host and Device              Yes
  Minimum alignment for any data type             64 bytes
  Alignment of base address                       512 bits (64 bytes)
  Global Memory cache type                        Read/Write
  Global Memory cache size                        32768 (32KiB)
  Global Memory cache line size                   64 bytes
  Image support                                   No
  Local memory type                               Global
  Local memory size                               268435456 (256MiB)
  Max number of constant args                     64
  Max constant buffer size                        268435456 (256MiB)
  Max size of kernel argument                     256
  Queue properties                                
    Out-of-order execution                        No
    Profiling                                     Yes
  Prefer user sync for interop                    Yes
  Profiling timer resolution                      1ns
  Execution capabilities                          
    Run OpenCL kernels                            Yes
    Run native kernels                            No
    IL version                                    SPIR-V_1.2 SPIR_1.2
    SPIR versions                                 1.2
  printf() buffer size                            0
  Built-in kernels                                (n/a)
  Device Extensions                               cl_khr_global_int32_base_atomics cl_khr_global_int32_extended_atomics cl_khr_local_int32_base_atomics cl_khr_local_int32_extended_atomics cl_khr_byte_addressable_store cl_nv_pragma_unroll cl_arm_core_id cl_ext_atomic_counters_32 cl_khr_initialize_memory cl_arm_integer_dot_product_int8 cl_arm_integer_dot_product_accumulate_int8 cl_arm_integer_dot_product_accumulate_int16 cl_arm_integer_dot_product_accumulate_saturate_int8 cl_khr_il_program cl_khr_spir cl_khr_create_command_queue cl_altera_device_temperature cl_altera_live_object_tracking cl_khr_icd cl_vc4cl_performance_counters
 
NULL platform behavior
  clGetPlatformInfo(NULL, CL_PLATFORM_NAME, ...)  OpenCL for the Raspberry Pi VideoCore IV GPU
  clGetDeviceIDs(NULL, CL_DEVICE_TYPE_ALL, ...)   Success [VC4CL]
  clCreateContext(NULL, ...) [default]            Success [VC4CL]
  clCreateContextFromType(NULL, CL_DEVICE_TYPE_DEFAULT)  Success (1)
    Platform Name                                 OpenCL for the Raspberry Pi VideoCore IV GPU
    Device Name                                   VideoCore IV GPU
  clCreateContextFromType(NULL, CL_DEVICE_TYPE_CPU)  No devices found in platform
  clCreateContextFromType(NULL, CL_DEVICE_TYPE_GPU)  Success (1)
    Platform Name                                 OpenCL for the Raspberry Pi VideoCore IV GPU
    Device Name                                   VideoCore IV GPU
  clCreateContextFromType(NULL, CL_DEVICE_TYPE_ACCELERATOR)  No devices found in platform
  clCreateContextFromType(NULL, CL_DEVICE_TYPE_CUSTOM)  No devices found in platform
  clCreateContextFromType(NULL, CL_DEVICE_TYPE_ALL)  Success (1)
    Platform Name                                 OpenCL for the Raspberry Pi VideoCore IV GPU
    Device Name                                   VideoCore IV GPU
 
ICD loader properties
  ICD loader Name                                 OpenCL ICD Loader
  ICD loader Vendor                               OCL Icd free software
  ICD loader Version                              2.2.12
  ICD loader Profile                              OpenCL 2.2
{% endhighlight %}

