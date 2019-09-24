# JT808协议

[![MIT Licence](https://img.shields.io/github/license/mashape/apistatus.svg)](https://github.com/SmallChi/JT808/blob/master/LICENSE)[![FOSSA Status](https://app.fossa.io/api/projects/git%2Bgithub.com%2FSmallChi%2FJT808.svg?type=shield)](https://app.fossa.io/projects/git%2Bgithub.com%2FSmallChi%2FJT808?ref=badge_shield)[![Build Status](https://travis-ci.org/SmallChi/JT808.svg?branch=master)](https://travis-ci.org/SmallChi/JT808)

## 前提条件

1. 掌握进制转换：二进制转十六进制；
2. 掌握BCD编码、Hex编码；
3. 掌握各种位移、异或；
4. 掌握常用反射；
5. 掌握快速ctrl+c、ctrl+v；
6. 掌握Span\<T\>的基本用法
7. 掌握以上装逼技能，就可以开始搬砖了。

## JT808数据结构解析

### 数据包[JT808Package]

| 头标识 | 数据头       | 数据体      | 校验码    | 尾标识 |
| :----: | :---------: |  :---------: | :----------: | :----: |
| Begin  | JT808Header |  JT808Bodies |CheckCode | End    |
| 7E     | -           | -           | -         | 7E     |

### 数据头[JT808Header]

| 消息ID | 消息体属性                     | 终端手机号      | 消息流水号 | 消息总包数(依赖是否分包)  | 包序号(依赖是否分包)   |
| :----: | :----------------------------: | :-------------: | :--------: |:---------: | :-------:|
| MsgId  | JT808HeaderMessageBodyProperty | TerminalPhoneNo | MsgNum     |PackgeCount | PackageIndex |

#### 数据头-消息体属性[JT808HeaderMessageBodyProperty]

| 是否分包 | 加密标识 | 消息体长度 |
| :------: | :------: | :--------: |
| IsPackge | Encrypt  | DataLength |

#### 消息体属性[JT808Bodies]

> 根据对应消息ID：MsgId

***注意：数据内容(除去头和尾标识)进行转义判断***

转义规则如下:

1. 若数据内容中有出现字符 0x7e 的，需替换为字符 0x7d 紧跟字符 0x02;
2. 若数据内容中有出现字符 0x7d 的，需替换为字符 0x7d 紧跟字符 0x01;

反转义的原因：确认JT808协议的TCP消息边界。

### 举个栗子1

#### 1.组包：

> MsgId 0x0200:位置信息汇报

``` package

JT808Package jT808Package = new JT808Package();

jT808Package.Header = new JT808Header
{
    MsgId = Enums.JT808MsgId.位置信息汇报,
    MsgNum = 126,
    TerminalPhoneNo = "123456789012"
};

JT808_0x0200 jT808_0x0200 = new JT808_0x0200();
jT808_0x0200.AlarmFlag = 1;
jT808_0x0200.Altitude = 40;
jT808_0x0200.GPSTime = DateTime.Parse("2018-10-15 10:10:10");
jT808_0x0200.Lat = 12222222;
jT808_0x0200.Lng = 132444444;
jT808_0x0200.Speed = 60;
jT808_0x0200.Direction = 0;
jT808_0x0200.StatusFlag = 2;
jT808_0x0200.JT808LocationAttachData = new Dictionary<byte, JT808_0x0200_BodyBase>();

jT808_0x0200.JT808LocationAttachData.Add(JT808Constants.JT808_0x0200_0x01, new JT808_0x0200_0x01
{
    Mileage = 100
});

jT808_0x0200.JT808LocationAttachData.Add(JT808Constants.JT808_0x0200_0x02, new JT808_0x0200_0x02
{
    Oil = 125
});

jT808Package.Bodies = jT808_0x0200;

byte[] data = JT808Serializer.Serialize(jT808Package);

var hex = data.ToHexString();

// 输出结果Hex：
// 7E 02 00 00 26 12 34 56 78 90 12 00 7D 02 00 00 00 01 00 00 00 02 00 BA 7F 0E 07 E4 F1 1C 00 28 00 3C 00 00 18 10 15 10 10 10 01 04 00 00 00 64 02 02 00 7D 01 13 7E
```

#### 2.手动解包：

``` unpackage
1.原包：
7E 02 00 00 26 12 34 56 78 90 12 00 (7D 02) 00 00 00 01 00 00 00 02 00 BA 7F 0E 07 E4 F1 1C 00 28 00 3C 00 00 18 10 15 10 10 10 01 04 00 00 00 64 02 02 00 (7D 01) 13 7E

2.进行反转义
7D 02 ->7E
7D 01 ->7D
反转义后
7E 02 00 00 26 12 34 56 78 90 12 00 7E 00 00 00 01 00 00 00 02 00 BA 7F 0E 07 E4 F1 1C 00 28 00 3C 00 00 18 10 15 10 10 10 01 04 00 00 00 64 02 02 00 7D 13 7E

3.拆解
7E                  --头标识
02 00               --数据头->消息ID
00 26               --数据头->消息体属性
12 34 56 78 90 12   --数据头->终端手机号
00 7E               --数据头->消息流水号
00 00 00 01         --消息体->报警标志
00 00 00 02         --消息体->状态位标志
00 BA 7F 0E         --消息体->纬度
07 E4 F1 1C         --消息体->经度
00 28               --消息体->海拔高度
00 3C               --消息体->速度
00 00               --消息体->方向
18 10 15 10 10 10   --消息体->GPS时间
01                  --消息体->附加信息->里程
04                  --消息体->附加信息->长度
00 00 00 64         --消息体->附加信息->数据
02                  --消息体->附加信息->油量
02                  --消息体->附加信息->长度
00 7D               --消息体->附加信息->数据
13                  --检验码
7E                  --尾标识
```

#### 3.程序解包：

``` unpackage2
//1.转成byte数组
byte[] bytes = "7E 02 00 00 26 12 34 56 78 90 12 00 7D 02 00 00 00 01 00 00 00 02 00 BA 7F 0E 07 E4 F1 1C 00 28 00 3C 00 00 18 10 15 10 10 10 01 04 00 00 00 64 02 02 00 7D 01 13 7E".ToHexBytes();

//2.将数组反序列化
var jT808Package = JT808Serializer.Deserialize(bytes);

//3.数据包头
Assert.Equal(Enums.JT808MsgId.位置信息汇报, jT808Package.Header.MsgId);
Assert.Equal(38, jT808Package.Header.MessageBodyProperty.DataLength);
Assert.Equal(126, jT808Package.Header.MsgNum);
Assert.Equal("123456789012", jT808Package.Header.TerminalPhoneNo);
Assert.False(jT808Package.Header.MessageBodyProperty.IsPackge);
Assert.Equal(0, jT808Package.Header.PackageIndex);
Assert.Equal(0, jT808Package.Header.PackgeCount);
Assert.Equal(JT808EncryptMethod.None, jT808Package.Header.MessageBodyProperty.Encrypt);

//4.数据包体
JT808_0x0200 jT808_0x0200 = (JT808_0x0200)jT808Package.Bodies;
Assert.Equal((uint)1, jT808_0x0200.AlarmFlag);
Assert.Equal((uint)40, jT808_0x0200.Altitude);
Assert.Equal(DateTime.Parse("2018-10-15 10:10:10"), jT808_0x0200.GPSTime);
Assert.Equal(12222222, jT808_0x0200.Lat);
Assert.Equal(132444444, jT808_0x0200.Lng);
Assert.Equal(60, jT808_0x0200.Speed);
Assert.Equal(0, jT808_0x0200.Direction);
Assert.Equal((uint)2, jT808_0x0200.StatusFlag);
//4.1.附加信息1
Assert.Equal(100, ((JT808_0x0200_0x01)jT808_0x0200.JT808LocationAttachData[JT808Constants.JT808_0x0200_0x01]).Mileage);
//4.2.附加信息2
Assert.Equal(125, ((JT808_0x0200_0x02)jT808_0x0200.JT808LocationAttachData[JT808Constants.JT808_0x0200_0x02]).Oil);
```

### 举个栗子2

``` create package
// 使用消息Id的扩展方法创建JT808Package包
JT808Package jT808Package = Enums.JT808MsgId.位置信息汇报.Create("123456789012",
    new JT808_0x0200 {
        AlarmFlag = 1,
        Altitude = 40,
        GPSTime = DateTime.Parse("2018-10-15 10:10:10"),
        Lat = 12222222,
        Lng = 132444444,
        Speed = 60,
        Direction = 0,
        StatusFlag = 2,
        JT808LocationAttachData = new Dictionary<byte, JT808LocationAttachBase>
        {
            { JT808Constants.JT808_0x0200_0x01,new JT808_0x0200_0x01{Mileage = 100}},
            { JT808Constants.JT808_0x0200_0x02,new JT808_0x0200_0x02{Oil = 125}}
        }
});

byte[] data = JT808Serializer.Serialize(jT808Package);

var hex = data.ToHexString();
//输出结果Hex：
//7E 02 00 00 26 12 34 56 78 90 12 00 01 00 00 00 01 00 00 00 02 00 BA 7F 0E 07 E4 F1 1C 00 28 00 3C 00 00 18 10 15 10 10 10 01 04 00 00 00 64 02 02 00 7D 01 6C 7E
```

### 举个栗子3

``` config
// 初始化配置
IJT808Config jT808Config = DefaultGlobalConfig.Create();
// 注册自定义消息外部程序集
jT808Config.Register(Assembly.GetExecutingAssembly());
// 跳过校验和验证
jT808Config.SkipCRCCode = true;
// 根据不同的设备终端号，添加自定义消息Id
jT808Config.MsgIdFactory.CustomSetMap<DT1Demo6>(0x91, "1234567891");
jT808Config.MsgIdFactory.CustomSetMap<DT2Demo6>(0x91, "1234567892");
// 初始化序列化实例
JT808Serializer JT808Serializer = new JT808Serializer(jT808Coonfig);
```

### 举个栗子4

#### 遇到的问题-多设备多协议的自定义位置附加信息

场景：
一个设备厂商对应多个设备类型，不同设备类型可能存在相同的自定义位置附加信息Id，导致自定义附加信息Id冲突，无法解析。

***解决方式：***

1.凡是解析自定义附加信息Id协议的，先进行分割存储，然后在根据外部的设备类型进行统一处理;

2.可以根据设备类型做个工厂，解耦对公共序列化器的依赖。

[可以参考Simples的Demo4](https://github.com/SmallChi/JT808/blob/master/src/JT808.Protocol.Test/Simples/Demo4.cs)

> 要是哪位大佬还有其他的解决方式，请您告知我下，谢谢您了。

### 举个栗子5

#### 遇到的问题-多媒体数据上传进行分包处理

场景:
设备在上传多媒体数据的时候，由于数据比较多，一次上传不了，所以采用分包方式处理。

***解决方式：***

1. 第一包数据上来采用平常的方式去解析数据；

2. 当N包数据上来，采用统一分包消息体去接收数据，最后在合并成一条。

> 普及知识点：一般行业分包是按256的整数倍，太多不行，太少也不行，必须刚刚好。

[可以参考Simples的Demo5](https://github.com/SmallChi/JT808/blob/master/src/JT808.Protocol.Test/Simples/Demo5.cs)

### 举个栗子6

#### 遇到的问题-多设备多协议的消息ID冲突

场景:
由于每个设备厂商不同，导致设备的私有协议可能使用相同的消息ID作为指令，导致平台解析不了。

***解决方式：***

对于设备来说，设备终端号是唯一标识，可以通过使用设备终端号和消息ID去查询对应的序列化器。

[可以参考Simples的Demo6](https://github.com/SmallChi/JT808/blob/master/src/JT808.Protocol.Test/Simples/Demo6.cs)

## NuGet安装

| Package Name          | Version                                            | Downloads                                           |
| --------------------- | -------------------------------------------------- | --------------------------------------------------- |
| Install-Package JT808 | ![JT808](https://img.shields.io/nuget/v/JT808.svg) | ![JT808](https://img.shields.io/nuget/dt/JT808.svg) |
| Install-Package JT808.Protocol.Extensions.JT1078 | ![JT808.Protocol.Extensions.JT1078](https://img.shields.io/nuget/v/JT808.Protocol.Extensions.JT1078.svg) | ![JT808](https://img.shields.io/nuget/dt/JT808.Protocol.Extensions.JT1078.svg) |

## 使用BenchmarkDotNet性能测试报告（只是玩玩，不能当真）

``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-8700K CPU 3.70GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.8.4010.0
  Job-LGLQDK : .NET Core 2.2.7 (CoreCLR 4.6.28008.02, CoreFX 4.6.28008.03), 64bit RyuJIT
  Job-ZHZJMS : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), 64bit RyuJIT

Platform=AnyCpu  Server=False  

```

|                          Method |     Toolchain |       Categories |      N |           Mean |         Error |        StdDev |      Gen 0 | Gen 1 | Gen 2 |    Allocated |
|-------------------------------- |-------------- |----------------- |------- |---------------:|--------------:|--------------:|-----------:|------:|------:|-------------:|
|   **0x0200_All_AttachId_Serialize** | **.NET Core 2.2** | **0x0200Serializer** |    **100** |     **2,481.6 us** |     **48.900 us** |     **48.026 us** |    **31.2500** |     **-** |     **-** |    **193.75 KB** |
| 0x0200_All_AttachId_Deserialize | .NET Core 2.2 | 0x0200Serializer |    100 |     3,325.1 us |     51.965 us |     48.608 us |    78.1250 |     - |     - |    493.75 KB |
|   0x0200_All_AttachId_Serialize | .NET Core 3.0 | 0x0200Serializer |    100 |     2,385.7 us |     46.285 us |     67.843 us |    31.2500 |     - |     - |    192.97 KB |
| 0x0200_All_AttachId_Deserialize | .NET Core 3.0 | 0x0200Serializer |    100 |     3,054.6 us |     25.435 us |     21.239 us |    78.1250 |     - |     - |     487.5 KB |
|   **0x0200_All_AttachId_Serialize** | **.NET Core 2.2** | **0x0200Serializer** |  **10000** |   **243,678.2 us** |  **4,693.810 us** |  **4,160.941 us** |  **3000.0000** |     **-** |     **-** |     **19375 KB** |
| 0x0200_All_AttachId_Deserialize | .NET Core 2.2 | 0x0200Serializer |  10000 |   326,335.0 us |  3,298.498 us |  2,754.396 us |  8000.0000 |     - |     - |     49375 KB |
|   0x0200_All_AttachId_Serialize | .NET Core 3.0 | 0x0200Serializer |  10000 |   230,732.4 us |  2,339.207 us |  2,073.646 us |  3000.0000 |     - |     - |  19296.88 KB |
| 0x0200_All_AttachId_Deserialize | .NET Core 3.0 | 0x0200Serializer |  10000 |   301,096.7 us |  5,182.683 us |  4,847.885 us |  7000.0000 |     - |     - |     48750 KB |
|   **0x0200_All_AttachId_Serialize** | **.NET Core 2.2** | **0x0200Serializer** | **100000** | **2,444,504.1 us** | **44,917.512 us** | **39,818.204 us** | **31000.0000** |     **-** |     **-** |    **193750 KB** |
| 0x0200_All_AttachId_Deserialize | .NET Core 2.2 | 0x0200Serializer | 100000 | 3,312,997.6 us | 62,270.110 us | 55,200.831 us | 80000.0000 |     - |     - |    493750 KB |
|   0x0200_All_AttachId_Serialize | .NET Core 3.0 | 0x0200Serializer | 100000 | 2,325,997.7 us | 46,265.141 us | 43,276.442 us | 31000.0000 |     - |     - | 192968.75 KB |
| 0x0200_All_AttachId_Deserialize | .NET Core 3.0 | 0x0200Serializer | 100000 | 3,118,972.3 us | 60,591.451 us | 72,129.813 us | 79000.0000 |     - |     - |    487500 KB |
|                                 |               |                  |        |                |               |               |            |       |       |              |
|                 **0x0100Serialize** | **.NET Core 2.2** | **0x0100Serializer** |    **100** |       **249.1 us** |      **4.022 us** |      **3.565 us** |    **10.2539** |     **-** |     **-** |     **63.28 KB** |
|               0x0100Deserialize | .NET Core 2.2 | 0x0100Serializer |    100 |       278.2 us |      5.353 us |      7.147 us |    17.5781 |     - |     - |    108.59 KB |
|                 0x0100Serialize | .NET Core 3.0 | 0x0100Serializer |    100 |       235.4 us |      4.582 us |      4.062 us |     9.7656 |     - |     - |      62.5 KB |
|               0x0100Deserialize | .NET Core 3.0 | 0x0100Serializer |    100 |       236.1 us |      1.417 us |      1.256 us |    14.6484 |     - |     - |     89.84 KB |
|                 **0x0100Serialize** | **.NET Core 2.2** | **0x0100Serializer** |  **10000** |    **25,587.8 us** |    **795.384 us** |    **816.801 us** |  **1000.0000** |     **-** |     **-** |   **6328.13 KB** |
|               0x0100Deserialize | .NET Core 2.2 | 0x0100Serializer |  10000 |    26,166.2 us |    308.810 us |    273.752 us |  1750.0000 |     - |     - |  10859.38 KB |
|                 0x0100Serialize | .NET Core 3.0 | 0x0100Serializer |  10000 |    23,668.4 us |    483.884 us |    517.751 us |  1000.0000 |     - |     - |      6250 KB |
|               0x0100Deserialize | .NET Core 3.0 | 0x0100Serializer |  10000 |    23,899.1 us |    400.562 us |    374.686 us |  1437.5000 |     - |     - |   8984.38 KB |
|                 **0x0100Serialize** | **.NET Core 2.2** | **0x0100Serializer** | **100000** |   **250,100.0 us** |  **3,182.496 us** |  **2,821.200 us** | **10000.0000** |     **-** |     **-** |  **63281.25 KB** |
|               0x0100Deserialize | .NET Core 2.2 | 0x0100Serializer | 100000 |   258,726.8 us |  2,116.900 us |  1,876.577 us | 17000.0000 |     - |     - | 108593.75 KB |
|                 0x0100Serialize | .NET Core 3.0 | 0x0100Serializer | 100000 |   237,334.9 us |  3,793.161 us |  3,548.125 us | 10000.0000 |     - |     - |     62500 KB |
|               0x0100Deserialize | .NET Core 3.0 | 0x0100Serializer | 100000 |   242,108.4 us |  3,433.333 us |  3,211.541 us | 14000.0000 |     - |     - |  89843.75 KB |

## JT808终端通讯协议消息对照表

| 序号  | 消息ID        | 完成情况 | 测试情况 | 消息体名称                     |
| :---: | :-----------: | :------: | :------: | :----------------------------: |
| 1     | 0x0001        | √        | √        | 终端通用应答                   |
| 2     | 0x8001        | √        | √        | 平台通用应答                   |
| 3     | 0x0002        | √        | √        | 终端心跳                       |
| 4     | 0x8003        | √        | √        | 补传分包请求                   |
| 5     | 0x0100        | √        | √        | 终端注册                       |
| 6     | 0x8100        | √        | √        | 终端注册应答                   |
| 7     | 0x0003        | √        | √        | 终端注销                       |
| 8     | 0x0102        | √        | √        | 终端鉴权                       |
| 9     | 0x8103        | √        | √        | 设置终端参数                   |
| 10    | 0x8104        | √        | √        | 查询终端参数                   |
| 11    | 0x0104        | √        | √        | 查询终端参数应答               |
| 12    | 0x8105        | √        | √        | 终端控制                       |
| 13    | 0x8106        | √        | √        | 查询指定终端参数               |
| 14    | 0x8107        | √        | 消息体为空| 查询终端属性                   |
| 15    | 0x0107        | √        | √        | 查询终端属性应答               |
| 16    | 0x8108        | √        | √        | 下发终端升级包                 |
| 17    | 0x0108        | √        | √        | 终端升级结果通知               |
| 18    | 0x0200        | √        | √        | 位置信息汇报                   |
| 19    | 0x8201        | √        | √        | 位置信息查询                   |
| 20    | 0x0201        | √        | √        | 位置信息查询应答               |
| 21    | 0x8202        | √        | √        | 临时位置跟踪控制               |
| 22    | 0x8203        | √        | √        | 人工确认报警消息               |
| 23    | 0x8300        | √        | √        | 文本信息下发                   |
| 24    | 0x8301        | √        | √        | 事件设置                       |
| 25    | 0x0301        | √        | √        | 事件报告                       |
| 26    | 0x8302        | √        | √        | 提问下发                       |
| 27    | 0x0302        | √        | √        | 提问应答                       |
| 28    | 0x8303        | √        | √        | 信息点播菜单设置               |
| 29    | 0x0303        | √        | √        | 信息点播/取消                  |
| 30    | 0x8304        | √        | √        | 信息服务                       |
| 31    | 0x8400        | √        | √        | 电话回拨                       |
| 32    | 0x8401        | √        | √        | 设置电话本                     |
| 33    | 0x8500        | √        | √        | 车辆控制                       |
| 34    | 0x0500        | √        | √        | 车辆控制应答                   |
| 35    | 0x8600        | √        | √        | 设置圆形区域                   |
| 36    | 0x8601        | √        | √        | 删除圆形区域                   |
| 37    | 0x8602        | √        | √        | 设置矩形区域                   |
| 38    | 0x8603        | √        | √        | 删除矩形区域                   |
| 39    | 0x8604        | √        | √        | 设置多边形区域                 |
| 40    | 0x8605        | √        | √        | 删除多边形区域                 |
| 41    | 0x8606        | √        | √        | 设置路线                       |
| 42    | 0x8607        | √        | √        | 删除路线                       |
| 43    | 0x8700        | x        | 下个版本  | 行驶记录仪数据采集命令         |
| 44    | 0x0700        | x        | 下个版本  | 行驶记录仪数据上传             |
| 45    | 0x8701        | x        | 下个版本  | 行驶记录仪参数下传命令         |
| 46    | 0x0701        | √        | √        | 电子运单上报                   |
| 47    | 0x0702        | √        | √        | 驾驶员身份信息采集上报         |
| 48    | 0x8702        | √        | 消息体为空| 上报驾驶员身份信息请求         |
| 49    | 0x0704        | √        | √        | 定位数据批量上传               |
| 50    | 0x0705        | √        | √        | CAN 总线数据上传               |
| 51    | 0x0800        | √        | √        | 多媒体事件信息上传             |
| 52    | 0x0801        | √        | √        | 多媒体数据上传                 |
| 53    | 0x8800        | √        | √        | 多媒体数据上传应答             |
| 54    | 0x8801        | √        | √        | 摄像头立即拍摄命令             |
| 55    | 0x0805        | √        | √        | 摄像头立即拍摄命令应答         |
| 56    | 0x8802        | √        | √        | 存储多媒体数据检索             |
| 57    | 0x0802        | √        | √        | 存储多媒体数据检索应答         |
| 58    | 0x8803        | √        | √        | 存储多媒体数据上传             |
| 59    | 0x8804        | √        | √        | 录音开始命令                   |
| 60    | 0x8805        | √        | √        | 单条存储多媒体数据检索上传命令 |
| 61    | 0x8900        | √        | √        | 数据下行透传                   |
| 62    | 0x0900        | √        | √        | 数据上行透传                   |
| 63    | 0x0901        | √        | √        | 数据压缩上报                   |
| 64    | 0x8A00        | √        | √        | 平台 RSA 公钥                  |
| 65    | 0x0A00        | √        | √        | 终端 RSA 公钥                  |
| 66    | 0x8F00~0x8FFF | 保留     | 保留     | 平台下行消息保留               |
| 67    | 0x0F00~0x0FFF | 保留     | 保留     | 终端上行消息保留               |
