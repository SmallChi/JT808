# JT/T808协议

[![MIT Licence](https://img.shields.io/github/license/mashape/apistatus.svg)](https://github.com/SmallChi/JT808/blob/master/LICENSE)![.NET Core](https://github.com/SmallChi/JT808/workflows/.NET%20Core/badge.svg?branch=master)

<p>
    <span>中文</span> |  
    <a href="README.en.md">English</a>
</p>

## 前提条件

1. 掌握进制转换：二进制转十六进制；
2. 掌握BCD编码、Hex编码；
3. 掌握各种位移、异或；
4. 掌握常用反射；
5. 掌握JObject的用法；
6. 掌握快速ctrl+c、ctrl+v；
7. 掌握Span\<T\>的基本用法
8. 掌握以上装逼技能，就可以开始搬砖了。

## JT808数据结构解析

### 数据包[JT808Package]

| 头标识 | 数据头       | 数据体/分包数据体      | 校验码    | 尾标识 |
| :----: | :---------: |  :---------: | :----------: | :----: |
| Begin  | JT808Header |  JT808Bodies/JT808SubDataBodies |CheckCode | End    |
| 7E     | -           | -           | -         | 7E     |

### 数据头[JT808Header]

| 消息ID | 消息体属性                     | 协议版本号(2019版本)|终端手机号      | 消息流水号 | 消息总包数(依赖是否分包)  | 包序号(依赖是否分包)   |
| :----: | :----------------------------: | :-------------: |:-------------: | :--------: |:---------: | :-------:|
| MsgId  | JT808HeaderMessageBodyProperty | ProtocolVersion|TerminalPhoneNo | MsgNum     |PackgeCount | PackageIndex |

#### 数据头-消息体属性[JT808HeaderMessageBodyProperty]

|版本标识(2019版本)| 是否分包 | 加密标识 | 消息体长度 |
|:------:| :------: | :------: | :--------: |
|VersionFlag| IsPackge | Encrypt  | DataLength |

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

``` csharp

JT808Package jT808Package = new JT808Package();

jT808Package.Header = new JT808Header
{
    MsgId = Enums.JT808MsgId._0x0200,
    ManualMsgNum = 126,
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
jT808_0x0200.BasicLocationAttachData = new Dictionary<byte, JT808_0x0200_BodyBase>();

jT808_0x0200.BasicLocationAttachData.Add(JT808Constants.JT808_0x0200_0x01, new JT808_0x0200_0x01
{
    Mileage = 100
});

jT808_0x0200.BasicLocationAttachData.Add(JT808Constants.JT808_0x0200_0x02, new JT808_0x0200_0x02
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

``` text
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

``` csharp
//1.转成byte数组
byte[] bytes = "7E 02 00 00 26 12 34 56 78 90 12 00 7D 02 00 00 00 01 00 00 00 02 00 BA 7F 0E 07 E4 F1 1C 00 28 00 3C 00 00 18 10 15 10 10 10 01 04 00 00 00 64 02 02 00 7D 01 13 7E".ToHexBytes();

//2.将数组反序列化
var jT808Package = JT808Serializer.Deserialize(bytes);

//3.数据包头
Assert.Equal(Enums.JT808MsgId._0x0200, jT808Package.Header.MsgId);
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
Assert.Equal(100, ((JT808_0x0200_0x01)jT808_0x0200.BasicLocationAttachData[JT808Constants.JT808_0x0200_0x01]).Mileage);
//4.2.附加信息2
Assert.Equal(125, ((JT808_0x0200_0x02)jT808_0x0200.BasicLocationAttachData[JT808Constants.JT808_0x0200_0x02]).Oil);
```

### 举个栗子2

``` csharp
// 使用消息Id的扩展方法创建JT808Package包
JT808Package jT808Package = Enums.JT808MsgId._0x0200.Create("123456789012",
    new JT808_0x0200 {
        AlarmFlag = 1,
        Altitude = 40,
        GPSTime = DateTime.Parse("2018-10-15 10:10:10"),
        Lat = 12222222,
        Lng = 132444444,
        Speed = 60,
        Direction = 0,
        StatusFlag = 2,
        BasicLocationAttachData = new Dictionary<byte, JT808LocationAttachBase>
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

``` csharp
// 初始化配置
IJT808Config DT1JT808Config = new DT1Config();
IJT808Config DT2JT808Config = new DT2Config();
// 注册自定义消息外部程序集
DT1JT808Config.Register(Assembly.GetExecutingAssembly());
// 跳过校验和验证
DT1JT808Config.SkipCRCCode = true;
// 根据不同的设备终端号，添加自定义消息Id
DT1JT808Config.MsgIdFactory.SetMap<DT1Demo6>();
DT1JT808Config.FormatterFactory.SetMap<DT1Demo6>();
DT2JT808Config.MsgIdFactory.SetMap<DT2Demo6>();
DT2JT808Config.FormatterFactory.SetMap<DT2Demo6>();
// 初始化序列化实例
JT808Serializer DT1JT808Serializer = new JT808Serializer(DT1JT808Config);
JT808Serializer DT2JT808Serializer = new JT808Serializer(DT2JT808Config);
```

[可以参考Simples的Demo6](https://github.com/SmallChi/JT808/blob/master/src/JT808.Protocol.Test/Simples/Demo6.cs)

### 举个栗子4

#### 遇到的问题-多设备多协议的自定义位置附加信息

场景：
一个设备厂商对应多个设备类型，不同设备类型可能存在相同的自定义位置附加信息Id，导致自定义附加信息Id冲突，无法解析。

***解决方式：***

1.可以根据设备类型做个工厂，解耦对公共序列化器的依赖。

2.可以根据设备类型去实现(GlobalConfigBase)对应的配置，根据不同的GlobalConfigBase实例去绑定对应协议解析器。

[可以参考Simples的Demo4](https://github.com/SmallChi/JT808/blob/master/src/JT808.Protocol.Test/Simples/Demo4.cs)

[可以参考Simples的Demo6](https://github.com/SmallChi/JT808/blob/master/src/JT808.Protocol.Test/Simples/Demo6.cs)

> 要是哪位大佬还有其他的解决方式，请您告知我下，谢谢您了。

### 举个栗子5

#### 遇到的问题-多媒体数据上传进行分包处理

场景:
设备在上传多媒体数据的时候，由于数据比较多，一次上传不了，所以采用分包方式处理。

***解决方式：***

1. 第一包数据上来采用平常的方式去解析数据；

2. 当第二包上来跟第一包的分包数据体(SubDataBodies)进行合并

3. 当N包数据上来，延续步骤2的方式。

> 普及知识点1：由于消息体长度最大为10bit也就是1023的字节，所以这边就有个硬性条件不能超过最大长度。

> 普及知识点2：一般行业分包是按256的整数倍，太多不行，太少也不行，必须刚刚好。

[可以参考Simples的Demo5](https://github.com/SmallChi/JT808/blob/master/src/JT808.Protocol.Test/Simples/Demo5.cs)

### 举个栗子6

#### 遇到的问题-多设备多协议的消息ID冲突

场景:
由于每个设备厂商不同，导致设备的私有协议可能使用相同的消息ID作为指令，导致平台解析不了。

***解决方式：***

可以根据设备类型去实现(GlobalConfigBase)对应的配置，根据不同的GlobalConfigBase实例去绑定对应协议解析器。

[可以参考Simples的Demo6](https://github.com/SmallChi/JT808/blob/master/src/JT808.Protocol.Test/Simples/Demo6.cs)

### 举个栗子7

如何兼容2019版本

> 最新协议文档已经写好了如何做兼容，就是在消息体属性中第14位为版本标识。

1. 当第14位为0时，标识协议为2011年的版本；

2. 当第14位为1时，标识协议为2019年的版本。

[可以参考Simples的Demo7](https://github.com/SmallChi/JT808/blob/master/src/JT808.Protocol.Test/Simples/Demo7.cs)

### 举个栗子8

协议分析器在数据出现异常和纠错的时候也是挺有用的，总不能凭借24K氪金眼去观察数据，那么可以在开发协议的同时就把协议分析器给写好，这样方便技术或者技术支持排查问题，提高效率。

[可以参考Simples的Demo8](https://github.com/SmallChi/JT808/blob/master/src/JT808.Protocol.Test/Simples/Demo8.cs)

### 举个栗子9

增加行车记录仪序列化器，既可以单独的存在，也可以组装在808的数据包当中。

[可以参考Simples的Demo9](https://github.com/SmallChi/JT808/blob/master/src/JT808.Protocol.Test/Simples/Demo9.cs)

### 举个栗子10

场景1:
有些设备，不会按照国标的附加信息Id来搞，把附加信息Id搞为两个字节，这样在上报上来的数据就会存在重复的附加Id，导致平台解析出错。

场景2：
由于粤标的设备厂家自定义的附加信息长度可以为四4个字节的，所以需要兼容。

场景3：
有些设备上报会出现两个相同的附加信息Id,那么只能解析一个，另一个只能丢在异常附加信息里面去处理。

|附加信息类说明| 附加ID字节数 | 附加长度字节数 | 备注 |
| :--- | :---: | :---: | :---:|
| JT808_0x0200_CustomBodyBase  | 1 BYTE | 1 BYTE | 标准|
| JT808_0x0200_CustomBodyBase2 | 2 BYTE | 1 BYTE | 自定义|
| JT808_0x0200_CustomBodyBase3 | 2 BYTE | 2 BYTE | 自定义|
| JT808_0x0200_CustomBodyBase4 | 1 BYTE | 4 BYTE | 自定义|

[可以参考Simples的Demo10](https://github.com/SmallChi/JT808/blob/master/src/JT808.Protocol.Test/Simples/Demo10.cs)

>注意：默认都是以**标准**的去解析，要是出现未知的附加，不一定解析就是正确，最好还是需要依照协议文档去开发然后自行注册解析器去解析。

### 举个栗子11

场景:
有些设备，补报的定位数据有异常数据包内容长度跟原始的内容长度不一致导致整包的数据的解析出错，再设备不升级，改不了的情况下，尽量能解析多少补报的数据量，就解析多少。

[可以参考Simples的Demo11](https://github.com/SmallChi/JT808/blob/master/src/JT808.Protocol.Test/Simples/Demo11.cs)

### 举个栗子12

场景:
由于粤标的设备把2019版本的0x8105终端控制消息命令参数做了扩展，所以需要兼容。

[可以参考Simples的Demo12](https://github.com/SmallChi/JT808/blob/master/src/JT808.Protocol.Test/Simples/Demo12.cs)

### 举个栗子13

场景:
由于协议库本身可能存在消息解析出错的情况，要么就提PR上来，但是不一定会及时发布，这时候就需要自己把原有的消息解析复制出来，改造好，然后重新注册。

[可以参考Simples的Demo13](https://github.com/SmallChi/JT808/blob/master/src/JT808.Protocol.Test/Simples/Demo13.cs)

### 举个栗子14

场景:
由于某些厂商不按要求去做，明明使用的2013的协议但是在消息头部的版本标识位置为1，导致程序认为是2019协议。从而解析报错。此时可以强制解析成2013后，然后修正版本标识，重新序列化消息，以供下游服务使用

[可以参考Simples的Demo14](https://github.com/SmallChi/JT808/blob/master/src/JT808.Protocol.Test/Simples/Demo14.cs)

### 举个栗子15

场景:
兼容2011协议的注册消息

[可以参考Simples的Demo15](https://github.com/SmallChi/JT808/blob/master/src/JT808.Protocol.Test/Simples/Demo15.cs)

### 举个栗子16

场景:
平台下发分包数据到设备

可以参考【举个栗子5】中，设备上来的分包数据结构，然后举一反三的去实现。

### 举个栗子17

场景:
由于808的0x0200标准协议的附加数据是有明确的表示对部分附加信息Id进行保留的，所以按照标准，有些设备厂商把标准的附加信息Id占用，所以需要解析这部分的数据，可以参考外部自定义附加信息注册的方式来解决解析问题。

可以参考[举个栗子10](https://github.com/SmallChi/JT808/blob/master/src/JT808.Protocol.Test/Simples/Demo10.cs)中的程序。

### 举个栗子18

场景:
由于接入很多不同设备厂商的协议，但是每个协议对接又比较少，想同时放在一个类库下面进行统一管理，那么在各个厂家不冲突的情况下使用程序集方式的注册是没有问题的，一旦有冲突，那么使用程序集的方式进行注册会报Id冲突，本身库不支持这种方式进行隔离的，所以遇到这种情况自己使用SetMap的方式进行管理。

## NuGet安装

| Package Name| Version| Preview  Version |Downloads|Remark|
| --- | --- | --- | ---| --- |
| Install-Package JT808 | ![JT808](https://img.shields.io/nuget/v/JT808.svg) | ![JT808](https://img.shields.io/nuget/vpre/JT808.svg)|![JT808](https://img.shields.io/nuget/dt/JT808.svg) |JT808|
| Install-Package JT808.Protocol.Extensions.JT1078 | ![JT808.Protocol.Extensions.JT1078](https://img.shields.io/nuget/v/JT808.Protocol.Extensions.JT1078.svg) | ![JT808.Protocol.Extensions.JT1078](https://img.shields.io/nuget/vpre/JT808.Protocol.Extensions.JT1078.svg)|![JT808](https://img.shields.io/nuget/dt/JT808.Protocol.Extensions.JT1078.svg) |JT1078扩展JT808|
| Install-Package JT808.Protocol.Extensions.SuBiao| ![JT808.Protocol.Extensions.SuBiao](https://img.shields.io/nuget/v/JT808.Protocol.Extensions.SuBiao.svg) | ![JT808.Protocol.Extensions.SuBiao](https://img.shields.io/nuget/vpre/JT808.Protocol.Extensions.SuBiao.svg)|![JT808](https://img.shields.io/nuget/dt/JT808.Protocol.Extensions.SuBiao.svg) |主动安全（苏标）扩展JT808|
| Install-Package JT808.Protocol.Extensions.YueBiao| ![JT808.Protocol.Extensions.YueBiao](https://img.shields.io/nuget/v/JT808.Protocol.Extensions.YueBiao.svg) | ![JT808.Protocol.Extensions.YueBiao](https://img.shields.io/nuget/vpre/JT808.Protocol.Extensions.YueBiao.svg)|![JT808](https://img.shields.io/nuget/dt/JT808.Protocol.Extensions.YueBiao.svg) |主动安全（粤标）扩展JT808|
| Install-Package JT808.Protocol.DependencyInjection| ![JT808.Protocol.DependencyInjection](https://img.shields.io/nuget/v/JT808.Protocol.DependencyInjection.svg) | ![JT808.Protocol.DependencyInjection](https://img.shields.io/nuget/vpre/JT808.Protocol.DependencyInjection.svg)|![JT808.Protocol.DependencyInjection](https://img.shields.io/nuget/dt/JT808.Protocol.DependencyInjection.svg) |JT808依赖注入扩展|

## 使用BenchmarkDotNet性能测试报告（只是玩玩，不能当真）

``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 11 (10.0.22621.1105)
Intel Core i7-8700K CPU 3.70GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET SDK=7.0.102
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  Job-OIKLWD : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2

Platform=AnyCpu  Server=False  Toolchain=.NET 7.0  

```
|                          Method |       Categories |      N |          Mean |        Error |       StdDev |       Gen0 |     Gen1 |    Allocated |
|-------------------------------- |----------------- |------- |--------------:|-------------:|-------------:|-----------:|---------:|-------------:|
|                 **0x0100Serialize** | **0x0100Serializer** |    **100** |      **75.25 μs** |     **0.519 μs** |     **0.433 μs** |    **10.7422** |        **-** |     **66.41 KB** |
|               0x0100Deserialize | 0x0100Serializer |    100 |      66.56 μs |     0.412 μs |     0.385 μs |    15.7471 |        - |     96.88 KB |
|                 **0x0100Serialize** | **0x0100Serializer** |  **10000** |   **7,581.60 μs** |   **108.729 μs** |   **101.705 μs** |  **1078.1250** |        **-** |   **6640.63 KB** |
|               0x0100Deserialize | 0x0100Serializer |  10000 |   6,609.91 μs |    58.293 μs |    51.675 μs |  1578.1250 |        - |    9687.5 KB |
|                 **0x0100Serialize** | **0x0100Serializer** | **100000** |  **74,221.22 μs** |   **514.498 μs** |   **456.089 μs** | **10714.2857** |        **-** |  **66406.32 KB** |
|               0x0100Deserialize | 0x0100Serializer | 100000 |  65,918.35 μs |   173.702 μs |   162.481 μs | 15750.0000 |        - |  96875.06 KB |
|                                 |                  |        |               |              |              |            |          |              |
|   **0x0200_All_AttachId_Serialize** | **0x0200Serializer** |    **100** |     **133.71 μs** |     **1.003 μs** |     **0.889 μs** |    **29.2969** |        **-** |    **180.47 KB** |
| 0x0200_All_AttachId_Deserialize | 0x0200Serializer |    100 |     137.71 μs |     1.125 μs |     0.997 μs |    38.0859 |   0.2441 |    234.38 KB |
|   **0x0200_All_AttachId_Serialize** | **0x0200Serializer** |  **10000** |  **13,317.92 μs** |    **82.257 μs** |    **68.688 μs** |  **2937.5000** |        **-** |  **18046.88 KB** |
| 0x0200_All_AttachId_Deserialize | 0x0200Serializer |  10000 |  14,040.86 μs |   242.740 μs |   227.060 μs |  3812.5000 |  15.6250 |  23437.51 KB |
|   **0x0200_All_AttachId_Serialize** | **0x0200Serializer** | **100000** | **131,292.10 μs** |   **871.653 μs** |   **815.344 μs** | **29250.0000** |        **-** | **180468.87 KB** |
| 0x0200_All_AttachId_Deserialize | 0x0200Serializer | 100000 | 137,063.75 μs | 1,301.430 μs | 1,086.753 μs | 38250.0000 | 250.0000 | 234375.12 KB |


## JT808终端通讯协议消息对照表

| 序号  | 消息ID        | 完成情况 | 测试情况 | 消息体名称                     |2019版本            | 2011版本
| :---: | :-----------: | :------: | :------: | :---------------------------- |:----------------------------:|:----------------------------:|
| 1     | 0x0001        | √        | √        | 终端通用应答                   |
| 2     | 0x8001        | √        | √        | 平台通用应答                   |
| 3     | 0x0002        | √        | √        | 终端心跳                       |
| 4     | 0x8003        | √        | √        | 补传分包请求                   |                 |被新增|
| 5     | 0x0100        | √        | √        | 终端注册                       |修改             |被修改
| 6     | 0x8100        | √        | √        | 终端注册应答                   |
| 7     | 0x0003        | √        | √        | 终端注销                       |
| 8     | 0x0102        | √        | √        | 终端鉴权                       |修改|
| 9     | 0x8103        | √        | √        | 设置终端参数                   |修改且增加        |被修改
| 10    | 0x8104        | √        | √        | 查询终端参数                   |
| 11    | 0x0104        | √        | √        | 查询终端参数应答               |
| 12    | 0x8105        | √        | √        | 终端控制                       |
| 13    | 0x8106        | √        | √        | 查询指定终端参数               |                  |被新增
| 14    | 0x8107        | √        | 消息体为空| 查询终端属性                   |                  |被新增
| 15    | 0x0107        | √        | √        | 查询终端属性应答               |                  |被新增
| 16    | 0x8108        | √        | √        | 下发终端升级包                 |                  |被新增
| 17    | 0x0108        | √        | √        | 终端升级结果通知               |                  |被新增
| 18    | 0x0200        | √        | √        | 位置信息汇报                   |增加附加信息       |被修改
| 19    | 0x8201        | √        | √        | 位置信息查询                   |
| 20    | 0x0201        | √        | √        | 位置信息查询应答               |
| 21    | 0x8202        | √        | √        | 临时位置跟踪控制               |
| 22    | 0x8203        | √        | √        | 人工确认报警消息               |                  |被新增
| 23    | 0x8300        | √        | √        | 文本信息下发                   |修改              |被修改
| 24    | 0x8301        | √        | √        | 事件设置                       |删除|
| 25    | 0x0301        | √        | √        | 事件报告                       |删除|
| 26    | 0x8302        | √        | √        | 提问下发                       |删除|
| 27    | 0x0302        | √        | √        | 提问应答                       |删除|
| 28    | 0x8303        | √        | √        | 信息点播菜单设置               |删除|
| 29    | 0x0303        | √        | √        | 信息点播/取消                  |删除|
| 30    | 0x8304        | √        | √        | 信息服务                       |删除|
| 31    | 0x8400        | √        | √        | 电话回拨                       |
| 32    | 0x8401        | √        | √        | 设置电话本                     |
| 33    | 0x8500        | √        | √        | 车辆控制                       |修改|
| 34    | 0x0500        | √        | √        | 车辆控制应答                   |
| 35    | 0x8600        | √        | √        | 设置圆形区域                   |修改                |被修改
| 36    | 0x8601        | √        | √        | 删除圆形区域                   |
| 37    | 0x8602        | √        | √        | 设置矩形区域                   |修改|
| 38    | 0x8603        | √        | √        | 删除矩形区域                   |
| 39    | 0x8604        | √        | √        | 设置多边形区域                 |修改|
| 40    | 0x8605        | √        | √        | 删除多边形区域                 |
| 41    | 0x8606        | √        | √        | 设置路线                       |修改|
| 42    | 0x8607        | √        | √        | 删除路线                       |
| 43    | 0x8700        | √        | √      | 行驶记录仪数据采集命令         |                       |被修改
| 44    | 0x0700        | √        | √      | 行驶记录仪数据上传             |
| 45    | 0x8701        | √        | √      | 行驶记录仪参数下传命令         |                        |被修改
| 46    | 0x0701        | √        | √        | 电子运单上报                   |
| 47    | 0x0702        | √        | √        | 驾驶员身份信息采集上报         |修改                  |被修改
| 48    | 0x8702        | √        | 消息体为空| 上报驾驶员身份信息请求         |                      |被新增
| 49    | 0x0704        | √        | √        | 定位数据批量上传               |修改|                 |被新增
| 50    | 0x0705        | √        | √        | CAN 总线数据上传               |修改|                 |被新增
| 51    | 0x0800        | √        | √        | 多媒体事件信息上传             |                      |被修改
| 52    | 0x0801        | √        | √        | 多媒体数据上传                 |修改                  |被修改
| 53    | 0x8800        | √        | √        | 多媒体数据上传应答             |                      |被修改
| 54    | 0x8801        | √        | √        | 摄像头立即拍摄命令             |修改|
| 55    | 0x0805        | √        | √        | 摄像头立即拍摄命令应答         |修改|                  |被新增
| 56    | 0x8802        | √        | √        | 存储多媒体数据检索             |
| 57    | 0x0802        | √        | √        | 存储多媒体数据检索应答         |                      |被修改
| 58    | 0x8803        | √        | √        | 存储多媒体数据上传             |
| 59    | 0x8804        | √        | √        | 录音开始命令                   |
| 60    | 0x8805        | √        | √        | 单条存储多媒体数据检索上传命令 |修改|                   |被新增
| 61    | 0x8900        | √        | √        | 数据下行透传                   |修改                  |被修改
| 62    | 0x0900        | √        | √        | 数据上行透传                   |修改                  |被修改
| 63    | 0x0901        | √        | √        | 数据压缩上报                   |
| 64    | 0x8A00        | √        | √        | 平台 RSA 公钥                  |
| 65    | 0x0A00        | √        | √        | 终端 RSA 公钥                  |
| 66    | 0x8F00~0x8FFF | 保留     | 保留     | 平台下行消息保留               |
| 67    | 0x0F00~0x0FFF | 保留     | 保留     | 终端上行消息保留               |
| 68    | 0x0004 | √     | √     | 查询服务器时间请求             |新增|
| 69    | 0x8004 | √     | √     | 查询服务器时间应答             |新增|
| 70    | 0x0005 | √     | √     | 终端补传分包请求               |新增|
| 71    | 0x8204 | √     | √     | 链路检测               |新增|
| 72    | 0x8608 | √     | √     | 查询区域或线路数据      |新增|
| 73    | 0x0608 | √     | √     | 查询区域或线路数据应答  |新增|
| 74    | 0xE000~0xEFFF | 保留     | 保留     | 厂商自定义上行消息      |新增|
| 75    | 0xF000~0xFFFF | 保留     | 保留     | 厂商自定义下行消息  |新增|

## JT1078扩展JT808议消息对照表

| 序号  | 消息ID        | 完成情况 | 测试情况 | 消息体名称                                     |
| :---: | :-----------: | :------: | :------: | :----------------------------              |
| 1     | 0x0200_0x14        | √        | √        | 视频相关报警                            |
| 2     | 0x0200_0x15        | √        | √        | 视频信号丢失报警状态                     |
| 3     | 0x0200_0x16        | √        | √        | 视频信号遮挡报警状态                     |
| 4     | 0x0200_0x17        | √        | √        | 存储器故障报警状态                       |
| 5     | 0x0200_0x18        | √        | √        | 异常驾驶行为报警详细描述                  |
| 6     | 0x8103_0x0075        | √        | √        | 音视频参数设置                   |
| 7     | 0x8103_0x0076        | √        | √        | 音视频通道列表设置                       |
| 8     | 0x8103_0x0077        | √        | √        | 单独视频通道参数设置                       |
| 9     | 0x8103_0x0079        | √        | √        | 特殊报警录像参数设置                   |
| 10     | 0x8103_0x007A        | √        | √        | 视频相关报警屏蔽字                       |
| 11     | 0x8103_0x007B        | √        | √        | 图像分析报警参数设置                       |
| 12     | 0x8103_0x007C        | √        | √        | 终端休眠模式唤醒设置                   |
| 13     | 0x1003        | √        | √        | 终端上传音视频属性                            |
| 14     | 0x1005        | √        | √        | 终端上传乘客流量                     |
| 15     | 0x1205        | √        | √        | 终端上传音视频资源列表                     |
| 16     | 0x1206        | √        | √        | 文件上传完成通知                       |
| 17     | 0x9003        | √        | √        | 查询终端音视频属性                  |
| 18     | 0x9101        | √        | √        | 实时音视频传输请求                   |
| 19     | 0x9102        | √        | √        | 音视频实时传输控制                       |
| 20     | 0x9105        | √        | √        | 实时音视频传输状态通知                       |
| 21     | 0x9201        | √        | √        | 平台下发远程录像回放请求                   |
| 22     | 0x9202        | √        | √        | 平台下发远程录像回放控制                       |
| 23     | 0x9205        | √        | √        | 查询资源列表                       |
| 24     | 0x9206        | √        | √        | 文件上传指令                   |
| 25     | 0x9207        | √        | √        | 文件上传控制                            |
| 26     | 0x9301        | √        | √        | 云台旋转                     |
| 27     | 0x9302        | √        | √        | 云台调整焦距控制                     |
| 28     | 0x9303        | √        | √        | 云台调整光圈控制                       |
| 29     | 0x9304        | √        | √        | 云台雨刷控制                  |
| 30     | 0x9305        | √        | √        | 红外补光控制                   |
| 31     | 0x9306        | √        | √        | 云台变倍控制                       |

## 使用方法

```csharp
DI:
IServiceCollection serviceDescriptors1 = new ServiceCollection();
serviceDescriptors1.AddJT808Configure()
                   .AddJT1078Configure();
全局注册：
JT808Serializer.Instance.Register(JT808_JT1078_Constants.GetCurrentAssembly());
```

## 主动安全（苏标）扩展JT808协议消息对照表

| 序号  | 消息ID | 完成情况 | 测试情况 | 消息体名称 |
| :---: | :---: | :---: | :---: | :---|
| 1 | 0x1210 | √ | √ | 报警附件信息消息 |
| 2 | 0x1211 | √ | √ | 文件信息上传 |
| 3 | 0x1212 | √ | √ | 文件上传完成消息 |
| 4 | 0x9208 | √ | √ | 报警附件上传指令 |
| 5 | 0x9212 | √ | √ | 文件上传完成消息应答 |
| 6 | 0x0200_0x64 | √ | √ | 高级驾驶辅助系统报警信息 |
| 7 | 0x0200_0x65 | √ | √ | 驾驶员状态监测系统报警信息 |
| 8 | 0x0200_0x66 | √ | √ | 胎压监测系统报警信息 |
| 9 | 0x0200_0x67 | √ | √ | 盲区监测系统报警信息 |
| 10 | 0x8103_0xF364 | √ | √ | 高级驾驶辅助系统参数 |
| 11 | 0x8103_0xF365 | √ | √ | 驾驶员状态监测系统参数 |
| 12 | 0x8103_0xF366 | √ | √ | 胎压监测系统参数 |
| 13 | 0x8103_0xF367 | √ | √ | 盲区监测系统参数 |
| 14 | 0x0900 | √ | √ | 上传基本信息 |
| 15 | 0x0900_0xF7 | √ | √ | 外设工作状态 |
| 16 | 0x0900_0xF8 | √ | √ | 外设系统信息 |
| 17 | 0x8900 | √ | √ | 查询基本信息 |
| 18 | 0x8900_0xF7 | √ | √ | 外设工作状态 |
| 19 | 0x8900_0xF8 | √ | √ | 外设系统信息 |

## 使用方法

```csharp
DI:
IServiceCollection serviceDescriptors1 = new ServiceCollection();
serviceDescriptors1.AddJT808Configure()
                   .AddSuBiaoConfigure();

全局注册:
JT808Serializer.Instance.Register(JT808_SuBiao_Constants.GetCurrentAssembly());
```

## 主动安全（粤标）扩展JT808协议消息对照表

> 注意：基于JT/T808 2019版本

| 序号  | 消息ID | 完成情况 | 测试情况 | 消息体名称 |
| :---: | :---: | :---: | :---: | :---|
| 1 | 0x1210 | √ | √ | 报警附件信息消息 |
| 2 | 0x1211 | √ | √ | 文件信息上传 |
| 3 | 0x1212 | √ |  √ | 文件上传完成消息 |
| 4 | 0x9208 | √ | √ | 报警附件上传指令 |
| 5 | 0x9212 | √ | √ | 文件上传完成消息应答 |
| 6 | 0x1FC4 | √ | √ | 终端升级进度上报 |
| 7 | 0x0200_0x64 | √ | √ | 高级驾驶辅助系统报警信息 |
| 8 | 0x0200_0x65 | √ | √ | 驾驶员状态监测系统报警信息 |
| 9 | 0x0200_0x66 | √ | √ | 胎压监测系统报警信息 |
| 10 | 0x0200_0x67 | √ | √ | 盲区监测系统报警信息 |
| 11 | 0x8103_0xF364 | √ | √ | 高级驾驶辅助系统参数 |
| 12 | 0x8103_0xF365 | √ | √ | 驾驶员状态监测系统参数 |
| 13 | 0x8103_0xF366 | √ | √ | 胎压监测系统参数 |
| 14 | 0x8103_0xF367 | √ | √ | 盲区监测系统参数 |
| 15 | 0x8103_0xF370 | √ | √ | 智能视频协议版本信息 |
| 16 | 0x0900 | √ | √ | 上传基本信息 |
| 17 | 0x0900_0xF7 | √ | √ | 外设工作状态 |
| 18 | 0x0900_0xF8 | √ | √ | 外设系统信息 |
| 19 | 0x8900 | √ | √ | 查询基本信息 |
| 20 | 0x8900_0xF7 | √ | √ | 外设工作状态 |
| 21 | 0x8900_0xF8 | √ | √ | 外设系统信息 |

## 使用方法

```csharp
DI:
IServiceCollection serviceDescriptors1 = new ServiceCollection();
serviceDescriptors1.AddJT808Configure()
                   .AddYueBiaoConfigure();

全局注册:
JT808Serializer.Instance.Register(JT808_YueBiao_Constants.GetCurrentAssembly());
```
