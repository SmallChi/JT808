# JT/T808 Protocol

[![MIT Licence](https://img.shields.io/github/license/mashape/apistatus.svg)](https://github.com/SmallChi/JT808/blob/master/LICENSE)![.NET Core](https://github.com/SmallChi/JT808/workflows/.NET%20Core/badge.svg?branch=master)

<p>
    <span>English</span> |  
    <a href="README.md">中文</a>
</p>

## Precondition

1. Master base conversion: binary to hexadecimal;
2. Master BCD and Hex coding；
3. Master all kinds of displacement, xor;
4. Master common reflexes;
5. Master the use of JObject;  
6. Command fast CTRL + C, CTRL + V;
7. Master the basic usage of Span\<T\>;
8. Master the above skills, you can begin to move bricks.

## JT808 Data structure parsing

### Packet[JT808Package]

| Head logo | Header   | Data volume/Subcontracted data volume | Checksum   | tail logo |
| :----: | :---------: |  :---------: | :----------: | :----: |
| Begin  | JT808Header |  JT808Bodies/JT808SubDataBodies |CheckCode | End    |
| 7E     | -           | -           | -         | 7E     |

### Header[JT808Header]

| Message Id | Message body properties| Protocol Version (2019 version)|Terminal Phone Number| Message sequence number | Total number of message packages (dependent on subcontracting)  | Package Number (depends on subcontracting or not)  |
| :----: | :----------------------------: | :-------------: |:-------------: | :--------: |:---------: | :-------:|
| MsgId  | JT808HeaderMessageBodyProperty | ProtocolVersion|TerminalPhoneNo | MsgNum     |PackgeCount | PackageIndex |

#### Header Message Body Property[JT808HeaderMessageBodyProperty]

|Protocol Version (2019 version)| Whether the subcontract | Encryption identification | Body length |
|:------:| :------: | :------: | :--------: |
|VersionFlag| IsPackge | Encrypt  | DataLength |

#### Message body properties[JT808Bodies]

> According to the corresponding message ID：MsgId

***Notic:Data content (excluding header and tail identifiers) is escaped***

The escape rules are as follows:

1. If the data contains character 0x7e, replace it with character 0x7d followed by character 0x02;
2. If character 0x7d exists in the data content, replace it with character 0x7d followed by character 0x01.

The cause of the anti-escape：Verify the TCP message boundary for the JT808 protocol.

### For example 1

#### 1.Create Package:

> MsgId 0x0200:Location information reporting

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

// output result Hex:
// 7E 02 00 00 26 12 34 56 78 90 12 00 7D 02 00 00 00 01 00 00 00 02 00 BA 7F 0E 07 E4 F1 1C 00 28 00 3C 00 00 18 10 15 10 10 10 01 04 00 00 00 64 02 02 00 7D 01 13 7E
```

#### 2.Manual unpack:

``` text
1.original package:
7E 02 00 00 26 12 34 56 78 90 12 00 (7D 02) 00 00 00 01 00 00 00 02 00 BA 7F 0E 07 E4 F1 1C 00 28 00 3C 00 00 18 10 15 10 10 10 01 04 00 00 00 64 02 02 00 (7D 01) 13 7E

2.Reverse escape
7D 02 ->7E
7D 01 ->7D
After the escape
7E 02 00 00 26 12 34 56 78 90 12 00 7E 00 00 00 01 00 00 00 02 00 BA 7F 0E 07 E4 F1 1C 00 28 00 3C 00 00 18 10 15 10 10 10 01 04 00 00 00 64 02 02 00 7D 13 7E

3.disassembly
7E                  --Head logo 
02 00               --data head->message id
00 26               --data head->message body properties
12 34 56 78 90 12   --data head->terminal phone number
00 7E               --data head->message sequence number
00 00 00 01         --message body->alarm flag
00 00 00 02         --message body->status flag
00 BA 7F 0E         --message body->latitude
07 E4 F1 1C         --message body->longitude
00 28               --message body->elevation
00 3C               --message body->speed
00 00               --message body->direction
18 10 15 10 10 10   --message body->gps time
01                  --message body->additional information->mileage
04                  --message body->additional information->length
00 00 00 64         --message body->additional information->mileage value
02                  --message body->additional information->oil
02                  --message body->additional information->length
00 7D               --message body->additional information->oil value
13                  --Checksum
7E                  --tail logo
```

#### 3.Program to unpack:

``` csharp
//1.Convert to a byte array
byte[] bytes = "7E 02 00 00 26 12 34 56 78 90 12 00 7D 02 00 00 00 01 00 00 00 02 00 BA 7F 0E 07 E4 F1 1C 00 28 00 3C 00 00 18 10 15 10 10 10 01 04 00 00 00 64 02 02 00 7D 01 13 7E".ToHexBytes();

//2.Deserialize the array
var jT808Package = JT808Serializer.Deserialize(bytes);

//3.packet header
Assert.Equal(Enums.JT808MsgId._0x0200, jT808Package.Header.MsgId);
Assert.Equal(38, jT808Package.Header.MessageBodyProperty.DataLength);
Assert.Equal(126, jT808Package.Header.MsgNum);
Assert.Equal("123456789012", jT808Package.Header.TerminalPhoneNo);
Assert.False(jT808Package.Header.MessageBodyProperty.IsPackge);
Assert.Equal(0, jT808Package.Header.PackageIndex);
Assert.Equal(0, jT808Package.Header.PackgeCount);
Assert.Equal(JT808EncryptMethod.None, jT808Package.Header.MessageBodyProperty.Encrypt);

//4.The packet body
JT808_0x0200 jT808_0x0200 = (JT808_0x0200)jT808Package.Bodies;
Assert.Equal((uint)1, jT808_0x0200.AlarmFlag);
Assert.Equal((uint)40, jT808_0x0200.Altitude);
Assert.Equal(DateTime.Parse("2018-10-15 10:10:10"), jT808_0x0200.GPSTime);
Assert.Equal(12222222, jT808_0x0200.Lat);
Assert.Equal(132444444, jT808_0x0200.Lng);
Assert.Equal(60, jT808_0x0200.Speed);
Assert.Equal(0, jT808_0x0200.Direction);
Assert.Equal((uint)2, jT808_0x0200.StatusFlag);
//4.1.Additional information 1
Assert.Equal(100, ((JT808_0x0200_0x01)jT808_0x0200.BasicLocationAttachData[JT808Constants.JT808_0x0200_0x01]).Mileage);
//4.2.Additional information 2
Assert.Equal(125, ((JT808_0x0200_0x02)jT808_0x0200.BasicLocationAttachData[JT808Constants.JT808_0x0200_0x02]).Oil);
```

### For example 2

``` csharp
// Create the JT808Package package using the extension method of the message Id
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
//output result hex：
//7E 02 00 00 26 12 34 56 78 90 12 00 01 00 00 00 01 00 00 00 02 00 BA 7F 0E 07 E4 F1 1C 00 28 00 3C 00 00 18 10 15 10 10 10 01 04 00 00 00 64 02 02 00 7D 01 6C 7E
```

### For example 3

``` csharp
// Initial Configuration
IJT808Config DT1JT808Config = new DT1Config();
IJT808Config DT2JT808Config = new DT2Config();
// Register custom message external assemblies
DT1JT808Config.Register(Assembly.GetExecutingAssembly());
// Skip checksum validation
DT1JT808Config.SkipCRCCode = true;
// Add a user-defined message Id based on the device terminal Id
DT1JT808Config.MsgIdFactory.SetMap<DT1Demo6>();
DT1JT808Config.FormatterFactory.SetMap<DT1Demo6>();
DT2JT808Config.MsgIdFactory.SetMap<DT2Demo6>();
DT2JT808Config.FormatterFactory.SetMap<DT2Demo6>();
// Initializes the serialization instance
JT808Serializer DT1JT808Serializer = new JT808Serializer(DT1JT808Config);
JT808Serializer DT2JT808Serializer = new JT808Serializer(DT2JT808Config);
```

[See Demo6 for Simples](https://github.com/SmallChi/JT808/blob/master/src/JT808.Protocol.Test/Simples/Demo6.cs)

### For example 4

#### Problems encountered - Additional information about custom locations for multiple devices and protocols

***scene:***
One device vendor corresponds to multiple device types. Different device types may have the same Id of the user-defined location additional information. As a result, the ids of the user-defined additional information conflict and cannot be resolved.  

***solution:***
1. You can make a factory based on the device type to decouple the dependency on the common serializer.

2. You can configure (GlobalConfigBase) based on the device type and bind the corresponding protocol parsers to different GlobalConfigBase instances.

[See Demo4 for Simples](https://github.com/SmallChi/JT808/blob/master/src/JT808.Protocol.Test/Simples/Demo4.cs)

[See Demo6 for Simples](https://github.com/SmallChi/JT808/blob/master/src/JT808.Protocol.Test/Simples/Demo6.cs)

> If anyone has another solution, please let me know. Thank you.  

### For example 5

#### Problems encountered - multimedia data upload for subcontracting processing

***scene:***
When the device uploads multimedia data, due to the large amount of data, it cannot upload at one time, so it adopts the subcontracting method.  

***solution:***

1. The first packet of data is parsed in the usual way;

2. When the second packet comes up, it merges with the SubDataBodies of the first packet;

3. When N packet data comes in, continue as in Step 2.

> General Knowledge 1:Since the maximum length of the message body is 10bits (1023 bytes), there is a hard condition that the maximum length cannot be exceeded.

> General Knowledge 2:General industry subcontracting is an integer multiple of 256, too much is not good, too little is not good, must be just right.  

[See Demo5 for Simples](https://github.com/SmallChi/JT808/blob/master/src/JT808.Protocol.Test/Simples/Demo5.cs)

### For example 6

#### Problems encountered - Message ids conflict on multiple devices and protocols

***scene:***
Because each device vendor is different, the private protocol of the device may use the same message ID as the instruction, causing the platform to fail to resolve the message.

***solution:***
You can configure (GlobalConfigBase) according to the device type and bind the corresponding protocol parser according to the GlobalConfigBase instance.

[See Demo6 for Simples](https://github.com/SmallChi/JT808/blob/master/src/JT808.Protocol.Test/Simples/Demo6.cs)

### For example 7

How to compatibility with the 2019 version.

> The latest protocol documentation has been written for compatibility, which is the version identifier at bit 14 in the message body attribute.  

1. If the 14th bit is 0, the protocol is the 2011 version.

2. When bit 14 is 1, the protocol is the 2019 version.

[See Demo7 for Simples](https://github.com/SmallChi/JT808/blob/master/src/JT808.Protocol.Test/Simples/Demo7.cs)

### For example 8

Protocol analyzer is also very useful in data anomalies and error correction, always can not rely on 24K krypton eye to observe the data, so you can develop the protocol at the same time to write the protocol analyzer, so that it is convenient for technology or technical support troubleshooting problems, improve efficiency.

[See Demo8 for Simples](https://github.com/SmallChi/JT808/blob/master/src/JT808.Protocol.Test/Simples/Demo8.cs)

### For example 9

Add dashcam serializer, which can either exist alone or be assembled into 808 data packets.

[See Demo9 for Simples](https://github.com/SmallChi/JT808/blob/master/src/JT808.Protocol.Test/Simples/Demo9.cs)

### For example 10

***scene1:***
Some devices, not in accordance with the NATIONAL standard additional information Id, the additional information Id is two bytes, so that there will be repeated additional Id in the reported data, resulting in platform parsing errors.

***scene2:***
Since the length of additional information customized by the equipment manufacturer of Guangdong Standard can be four or four bytes, it needs to be compatible.

***scene3:***
Some devices report two ids with the same additional information. In this case, only one Id can be parsed and the other Id can be discarded in the exception additional information.

|Additional Information| Number of additional ID bytes | Number of additional length bytes | Remarks |
| :--- | :---: | :---: | :---:|
| JT808_0x0200_CustomBodyBase  | 1 BYTE | 1 BYTE | standard|
| JT808_0x0200_CustomBodyBase2 | 2 BYTE | 1 BYTE | custom|
| JT808_0x0200_CustomBodyBase3 | 2 BYTE | 2 BYTE | custom|
| JT808_0x0200_CustomBodyBase4 | 1 BYTE | 4 BYTE | custom|

[See Demo10 for Simples](https://github.com/SmallChi/JT808/blob/master/src/JT808.Protocol.Test/Simples/Demo10.cs)

>Notice:The default is the **standard** to parse, if there is unknown add-on, may not be the correct resolution, it is better to develop according to the protocol document and then register the parser to parse.

### For example 11

***scene:***
On some devices, abnormal positioning data reported by the supplement is inconsistent with the length of the original content, resulting in errors in data parsing of the whole packet. If the device is not upgraded and cannot be changed, the amount of data reported by the supplement can be resolved as much as possible.

[See Demo11 for Simples](https://github.com/SmallChi/JT808/blob/master/src/JT808.Protocol.Test/Simples/Demo11.cs)

### For example 12

***scene:***
Since the device of Yue Biao has extended the 0x8105 terminal control message command parameter of the 2019 version, it needs to be compatible.

[See Demo12 for Simples](https://github.com/SmallChi/JT808/blob/master/src/JT808.Protocol.Test/Simples/Demo12.cs)

### For example 13

***scene:***
Because the protocol library itself may have a message parsing error, or put up PR, but may not be released in time, at this time you need to copy the original message parsing, transform, and then re-register.

[See Demo13 for Simples](https://github.com/SmallChi/JT808/blob/master/src/JT808.Protocol.Test/Simples/Demo13.cs)

### For example 14

***scene:***
Because some vendors do not comply with the requirements, the application uses the 2013 protocol but the version identifier position in the message header is 1, causing the application to believe that the 2019 protocol is used.  Parse the error.  At this point, you can force parsing to 2013, then fix the version identity and reserialize the message for use by downstream services.

[See Demo14 for Simples](https://github.com/SmallChi/JT808/blob/master/src/JT808.Protocol.Test/Simples/Demo14.cs)

### For example 15

***scene:***
A registration message compatible with the 2011 protocol.

[See Demo15 for Simples](https://github.com/SmallChi/JT808/blob/master/src/JT808.Protocol.Test/Simples/Demo15.cs)

### For example 16

***scene:***
The platform delivers subcontracted data to the equipment.

We can refer to the subcontracting data structure of the equipment in [Example 5], and then to achieve it.  

[See Demo16 for Simples](https://github.com/SmallChi/JT808/blob/master/src/JT808.Protocol.Test/Simples/Demo16.cs)

### 举个栗子17

***scene:***
The additional data of the JT/T808 0x0200 standard protocol is explicitly reserved for some additional information ids. Therefore, according to the standard, some device manufacturers use the additional information ids of the standard. Therefore, to resolve the problem, you can refer to the external custom additional information registration method.

[see Demo10 for Simples](https://github.com/SmallChi/JT808/blob/master/src/JT808.Protocol.Test/Simples/Demo10.cs).

### 举个栗子18

***scene:***
Due to access many different equipment vendor's agreement, but each protocol docking and less, think at the same time in a class library under the unified management, then in each manufacturer not conflict using the assembly way of registration is no problem, once has the conflict, then use the assembly way registered will quote Id conflict, The library does not support this way of isolation, so I use [SetMap] to manage this situation.

## NuGet Install

| Package Name| Version| Preview  Version |Downloads|Remark|
| --- | --- | --- | ---| --- |
| Install-Package JT808 | ![JT808](https://img.shields.io/nuget/v/JT808.svg) | ![JT808](https://img.shields.io/nuget/vpre/JT808.svg)|![JT808](https://img.shields.io/nuget/dt/JT808.svg) |JT808|
| Install-Package JT808.Protocol.Extensions.JT1078 | ![JT808.Protocol.Extensions.JT1078](https://img.shields.io/nuget/v/JT808.Protocol.Extensions.JT1078.svg) | ![JT808.Protocol.Extensions.JT1078](https://img.shields.io/nuget/vpre/JT808.Protocol.Extensions.JT1078.svg)|![JT808](https://img.shields.io/nuget/dt/JT808.Protocol.Extensions.JT1078.svg) |JT1078 extension JT808|
| Install-Package JT808.Protocol.Extensions.SuBiao| ![JT808.Protocol.Extensions.SuBiao](https://img.shields.io/nuget/v/JT808.Protocol.Extensions.SuBiao.svg) | ![JT808.Protocol.Extensions.SuBiao](https://img.shields.io/nuget/vpre/JT808.Protocol.Extensions.SuBiao.svg)|![JT808](https://img.shields.io/nuget/dt/JT808.Protocol.Extensions.SuBiao.svg) |Active Safety (Su Biao) extension JT808|
| Install-Package JT808.Protocol.Extensions.YueBiao| ![JT808.Protocol.Extensions.YueBiao](https://img.shields.io/nuget/v/JT808.Protocol.Extensions.YueBiao.svg) | ![JT808.Protocol.Extensions.YueBiao](https://img.shields.io/nuget/vpre/JT808.Protocol.Extensions.YueBiao.svg)|![JT808](https://img.shields.io/nuget/dt/JT808.Protocol.Extensions.YueBiao.svg) |Active Safety (Yue Biao) extension JT808|
| Install-Package JT808.Protocol.DependencyInjection| ![JT808.Protocol.DependencyInjection](https://img.shields.io/nuget/v/JT808.Protocol.DependencyInjection.svg) | ![JT808.Protocol.DependencyInjection](https://img.shields.io/nuget/vpre/JT808.Protocol.DependencyInjection.svg)|![JT808.Protocol.DependencyInjection](https://img.shields.io/nuget/dt/JT808.Protocol.DependencyInjection.svg) |JT808 DependencyInjection|

## Using BenchmarkDotNet performance test reports (just for fun, not to be taken seriously)

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


## JT808 Comparison table of terminal communication protocol messages

| SN  | Message Id      | Completion | Unit Test | Message body name  |2019 version | 2011 version
| :---: | :-----------: | :------: | :------: | :---------------------------- |:----------------------------:|:----------------------------:|
| 1     | 0x0001        | √        | √        | Terminal universal reply       |
| 2     | 0x8001        | √        | √        | Platform Universal response    |
| 3     | 0x0002        | √        | √        | Terminal heart                 |
| 4     | 0x8003        | √        | √        | Forwarding subcontract request |                 |Is the new|
| 5     | 0x0100        | √        | √        | Terminal registration                       |changed             |be modified
| 6     | 0x8100        | √        | √        | Terminal registration reply  |
| 7     | 0x0003        | √        | √        | Terminal logout              |
| 8     | 0x0102        | √        | √        | Terminal authentication      |changed|
| 9     | 0x8103        | √        | √        | Setting Terminal Parameters   |changedAndNew        |be modified
| 10    | 0x8104        | √        | √        | Querying Terminal Parameters   |
| 11    | 0x0104        | √        | √        | Query terminal parameter response |
| 12    | 0x8105        | √        | √        | Terminal control              |
| 13    | 0x8106        | √        | √        | Example Query specified terminal parameters |                  |Is the new
| 14    | 0x8107        | √        | The message body is empty| Querying Terminal Properties  |                  |Is the new
| 15    | 0x0107        | √        | √        | Query the response of the terminal properties |                  |Is the new
| 16    | 0x8108        | √        | √        | Query terminal properties reply Deliver the terminal upgrade package                 |                  |Is the new
| 17    | 0x0108        | √        | √        | Terminal upgrade result notification               |                  |Is the new
| 18    | 0x0200        | √        | √        | Location information reporting                   |Add Additional information       |be modified
| 19    | 0x8201        | √        | √        | Location information query                   |
| 20    | 0x0201        | √        | √        | Location information query response               |
| 21    | 0x8202        | √        | √        | Temporary position tracking control               |
| 22    | 0x8203        | √        | √        | Manually confirm the alarm message               |                  |Is the new
| 23    | 0x8300        | √        | √        | Text message delivery|changed              |be modified
| 24    | 0x8301        | √        | √        | Event set                       |delete|
| 25    | 0x0301        | √        | √        | Event report                       |delete|
| 26    | 0x8302        | √        | √        | Questions issued                       |delete|
| 27    | 0x0302        | √        | √        | Question answering                       |delete|
| 28    | 0x8303        | √        | √        | Information on demand menu Settings               |delete|
| 29    | 0x0303        | √        | √        | Information on demand/Information cancel                  |delete|
| 30    | 0x8304        | √        | √        | Information service                       |delete|
| 31    | 0x8400        | √        | √        | Back to the dial                       |
| 32    | 0x8401        | √        | √        | Set up a phone book                     |
| 33    | 0x8500        | √        | √        | Vehicle control                       |changed|
| 34    | 0x0500        | √        | √        | Vehicle control response                   |
| 35    | 0x8600        | √        | √        | Set the circular area                   |changed                |be modified
| 36    | 0x8601        | √        | √        | Delete circular area                   |
| 37    | 0x8602        | √        | √        | Set rectangle area|changed|
| 38    | 0x8603        | √        | √        | Delete rectangular area                   |
| 39    | 0x8604        | √        | √        | Set polygon region                 |changed|
| 40    | 0x8605        | √        | √        | Delete polygon area                 |
| 41    | 0x8606        | √        | √        | Set the route                       |changed|
| 42    | 0x8607        | √        | √        | Delete the route                       |
| 43    | 0x8700        | √        | √      | Drive recorder data acquisition command         |                       |be modified
| 44    | 0x0700        | √        | √      | Data upload from driving recorder             |
| 45    | 0x8701        | √        | √      | Driving recorder parameters down command         |                        |be modified
| 46    | 0x0701        | √        | √        |  Electronic waybill reporting                   |
| 47    | 0x0702        | √        | √        | Collect and report driver identity information         |changed                  |be modified
| 48    | 0x8702        | √        | The message body is empty| Report driver identification request         |                      |Is the new
| 49    | 0x0704        | √        | √        | Upload location data in batches               |changed|                 |Is the new
| 50    | 0x0705        | √        | √        | CAN bus data upload|changed|                 |Is the new
| 51    | 0x0800        | √        | √        | Upload multimedia event information|                      |be modified
| 52    | 0x0801        | √        | √        | Multimedia Data upload|changed                  |be modified
| 53    | 0x8800        | √        | √        | Reply to multimedia data upload |                      |be modified
| 54    | 0x8801        | √        | √        | The camera immediately shoots the command |changed|
| 55    | 0x0805        | √        | √        | The camera immediately shoots the command reply         |changed|                  |Is the new
| 56    | 0x8802        | √        | √        | Store multimedia data retrieval             |
| 57    | 0x0802        | √        | √        | Store multimedia data retrieval replies   |be modified
| 58    | 0x8803        | √        | √        | Store multimedia data upload             |
| 59    | 0x8804        | √        | √        | Recording Start Command                   |
| 60    | 0x8805        | √        | √        | Single storage multimedia data retrieval upload command |changed|                   |Is the new
| 61    | 0x8900        | √        | √        | Data is transmitted through downlink   |changed                  |be modified
| 62    | 0x0900        | √        | √        | Data is transparently transmitted upstream  |changed                  |be modified
| 63    | 0x0901        | √        | √        | Data compression reporting     |
| 64    | 0x8A00        | √        | √        | Platform RSA Public Key        |
| 65    | 0x0A00        | √        | √        | Terminal RSA Public Key        |
| 66    | 0x8F00~0x8FFF | reserved     | reserved     | Platform downlink message reserved  |
| 67    | 0x0F00~0x0FFF | reserved     | reserved     | The uplink message on the terminal is reserved |
| 68    | 0x0004 | √     | √     | Queries server time requests             |new|
| 69    | 0x8004 | √     | √     | Query the server time response             |new|
| 70    | 0x0005 | √     | √     | The terminal sends the subcontract request  |new|
| 71    | 0x8204 | √     | √     | Link detection               |new|
| 72    | 0x8608 | √     | √     | Example Query area or line data      |new|
| 73    | 0x0608 | √     | √     | Query area or line data reply  |new|
| 74    | 0xE000~0xEFFF | reserved     | reserved     | The vendor defines the uplink message  |new|
| 75    | 0xF000~0xFFFF | reserved     | reserved     | Vendor defined downlink messages  |new|

## Extend JT808 discussion message comparison table

| SN  | Message Id      | Completion | Unit Test | Message body name  |
| :---: | :-----------: | :------: | :------: | :----------------------------              |
| 1     | 0x0200_0x14        | √        | √        | Video related alarm                            |
| 2     | 0x0200_0x15        | √        | √        | Video signal loss alarm status                     |
| 3     | 0x0200_0x16        | √        | √        | Video signal occlusion alarm status                     |
| 4     | 0x0200_0x17        | √        | √        | Memory fault alarm status                       |
| 5     | 0x0200_0x18        | √        | √        | Abnormal driving behavior alarm detailed description                  |
| 6     | 0x8103_0x0075        | √        | √        | Audio and video parameter Settings                   |
| 7     | 0x8103_0x0076        | √        | √        | Set the audio and video channel list                       |
| 8     | 0x8103_0x0077        | √        | √        | Separate video channel parameter Settings                       |
| 9     | 0x8103_0x0079        | √        | √        | Special alarm video parameter setting                   |
| 10     | 0x8103_0x007A        | √        | √        | Video related alarm masking word                       |
| 11     | 0x8103_0x007B        | √        | √        | Image analysis alarm parameter setting                       |
| 12     | 0x8103_0x007C        | √        | √        | Wake up in hibernation mode                   |
| 13     | 0x1003        | √        | √        | The terminal uploads audio and video properties                            |
| 14     | 0x1005        | √        | √        | Terminal uploads passenger flow                     |
| 15     | 0x1205        | √        | √        | The terminal uploads the audio and video resource list                     |
| 16     | 0x1206        | √        | √        | Notification of file upload completion                       |
| 17     | 0x9003        | √        | √        | Example Query audio and video properties of terminals                  |
| 18     | 0x9101        | √        | √        | Real-time audio and video transmission request                   |
| 19     | 0x9102        | √        | √        | Audio and video real-time transmission control                       |
| 20     | 0x9105        | √        | √        | Real-time audio and video transmission status notification                       |
| 21     | 0x9201        | √        | √        | The platform delivers a remote video playback request                   |
| 22     | 0x9202        | √        | √        |  Platform delivers remote video playback control                       |
| 23     | 0x9205        | √        | √        | Querying a Resource List                       |
| 24     | 0x9206        | √        | √        | File upload command                   |
| 25     | 0x9207        | √        | √        | File Upload Control                            |
| 26     | 0x9301        | √        | √        | PTZ rotation                     |
| 27     | 0x9302        | √        | √        | PTZ Adjust focal length control                     |
| 28     | 0x9303        | √        | √        | PTZ Adjust aperture control                       |
| 29     | 0x9304        | √        | √        | PTZ Wiper control                  |
| 30     | 0x9305        | √        | √        | Infrared light filling control                   |
| 31     | 0x9306        | √        | √        | Head doubling control                       |

## usage

```csharp
Use DI:
IServiceCollection serviceDescriptors1 = new ServiceCollection();
serviceDescriptors1.AddJT808Configure()
                   .AddJT1078Configure();

Use Global:
JT808Serializer.Instance.Register(JT808_JT1078_Constants.GetCurrentAssembly());
```

## Active Security (SuBiao) extended JT808 protocol message comparison table

| SN  | Message Id      | Completion | Unit Test | Message body name  |
| :---: | :---: | :---: | :---: | :---|
| 1 | 0x1210 | √ | √ | Alarm attachment information message |
| 2 | 0x1211 | √ | √ | Uploading File Information |
| 3 | 0x1212 | √ | √ | Message indicating that file uploading is complete |
| 4 | 0x9208 | √ | √ | Alarm attachment upload instruction |
| 5 | 0x9212 | √ | √ | File upload complete reply message |
| 6 | 0x0200_0x64 | √ | √ | Advanced driver assistance system alarm information |
| 7 | 0x0200_0x65 | √ | √ | Driver status monitoring system alarm information |
| 8 | 0x0200_0x66 | √ | √ | Tire pressure monitoring system alarm information |
| 9 | 0x0200_0x67 | √ | √ | Blind area monitoring system alarm information |
| 10 | 0x8103_0xF364 | √ | √ | Advanced driver assistance system parameters |
| 11 | 0x8103_0xF365 | √ | √ | Driver condition monitoring system parameters |
| 12 | 0x8103_0xF366 | √ | √ | Tire pressure monitoring system parameters |
| 13 | 0x8103_0xF367 | √ | √ | Blind area monitoring system parameters |
| 14 | 0x0900 | √ | √ | Upload basic Information |
| 15 | 0x0900_0xF7 | √ | √ | Operating status of peripheral |
| 16 | 0x0900_0xF8 | √ | √ | Peripheral system information |
| 17 | 0x8900 | √ | √ | Querying Basic Information |
| 18 | 0x8900_0xF7 | √ | √ | Operating status of peripheral |
| 19 | 0x8900_0xF8 | √ | √ | Peripheral system information |

## usage

```csharp
Use DI:
IServiceCollection serviceDescriptors1 = new ServiceCollection();
serviceDescriptors1.AddJT808Configure()
                   .AddSuBiaoConfigure();

Use Global:
JT808Serializer.Instance.Register(JT808_SuBiao_Constants.GetCurrentAssembly());
```

## Active Security (Yue Biao) extended JT808 protocol message comparison table

> Notice:Based on JT/T808 version 2019

| SN  | Message Id      | Completion | Unit Test | Message body name  |
| :---: | :---: | :---: | :---: | :---|
| 1 | 0x1210 | √ | √ | Alarm attachment information message |
| 2 | 0x1211 | √ | √ | Uploading File Information |
| 3 | 0x1212 | √ |  √ | Message indicating that file uploading is complete |
| 4 | 0x9208 | √ | √ | Alarm attachment upload instruction |
| 5 | 0x9212 | √ | √ | File upload complete reply message |
| 6 | 0x1FC4 | √ | √ | Terminal upgrade progress reported |
| 7 | 0x0200_0x64 | √ | √ | Advanced driver assistance system alarm information |
| 8 | 0x0200_0x65 | √ | √ | Driver status monitoring system alarm information |
| 9 | 0x0200_0x66 | √ | √ | Tire pressure monitoring system alarm information |
| 10 | 0x0200_0x67 | √ | √ | Blind area monitoring system alarm information |
| 11 | 0x8103_0xF364 | √ | √ | Advanced driver assistance system parameters |
| 12 | 0x8103_0xF365 | √ | √ | Driver condition monitoring system parameters |
| 13 | 0x8103_0xF366 | √ | √ | Tire pressure monitoring system parameters |
| 14 | 0x8103_0xF367 | √ | √ | Blind area monitoring system parameters |
| 15 | 0x8103_0xF370 | √ | √ | Smart video protocol version information |
| 16 | 0x0900 | √ | √ | Upload basic Information |
| 17 | 0x0900_0xF7 | √ | √ | Operating status of peripheral |
| 18 | 0x0900_0xF8 | √ | √ | Peripheral system information |
| 19 | 0x8900 | √ | √ | Querying Basic Information |
| 20 | 0x8900_0xF7 | √ | √ | Operating status of peripheral |
| 21 | 0x8900_0xF8 | √ | √ | Peripheral system information |

## usage

```csharp
Use DI:
IServiceCollection serviceDescriptors1 = new ServiceCollection();
serviceDescriptors1.AddJT808Configure()
                   .AddYueBiaoConfigure();

Use Global:
JT808Serializer.Instance.Register(JT808_YueBiao_Constants.GetCurrentAssembly());
```
