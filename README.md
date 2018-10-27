# JT808协议

## 瞎逼逼

1. 该JT808协议是参考[MessagePack-CSharp](https://github.com/neuecc/MessagePack-CSharp)一款二进制序列化器，借鉴其思想，不得不说站在巨人的肩膀上搬砖就是爽歪歪。
2. 搜索了很多资源没有针对.NET开源的808协议库，难道做GPS行业的.NET很少人吗？可能是藏着，掖着<(￣3￣)>  <(￣3￣)>  <(￣3￣)> 。
3. 不得不说这GB的文档，太坑了。。。
4. 现在有了[JT809](https://github.com/SmallChi/JT809/blob/master/README.md)的基础，对JT808就只剩搬砖了。

## 前提条件

1. 掌握进制转换：二进制转十六进制；
2. 掌握BCD编码、Hex编码；
3. 掌握各种位移、异或；
4. 掌握常用反射；
5. 掌握快速ctrl+c、ctrl+v；
6. 掌握以上装逼技能，就可以开始搬砖了。

## JT808数据结构解析

### 数据包[JT808Package]

|头标识|数据头|数据体|校验码|尾标识
|:------:|:------:|:------:|:------:|:------:|
|  Begin  | JT808Header  |  JT808Bodies  | CheckCode | End |
|  7E  | - | - | - | 7E |

### 数据头[JT808Header]

|消息ID|消息体属性|终端手机号|消息流水号|
|:------:|:------:|:------:|:------:|
| MsgId | JT808HeaderMessageBodyProperty | TerminalPhoneNo | MsgNum |

#### 数据头-消息体属性[JT808HeaderMessageBodyProperty]

|是否分包|加密标识|消息体长度|消息总包数|包序号
|:------:|:------:|:------:|:------:|:------:|
|  IsPackge  | Encrypt  |  DataLength  | PackgeCount | PackageIndex |

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
jT808_0x0200.JT808LocationAttachData = new Dictionary<byte, JT808LocationAttachBase>();

jT808_0x0200.JT808LocationAttachData.Add(JT808LocationAttachBase.AttachId0x01, new JT808LocationAttachImpl0x01
{
    Mileage = 100
});

jT808_0x0200.JT808LocationAttachData.Add(JT808LocationAttachBase.AttachId0x02, new JT808LocationAttachImpl0x02
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
var jT808Package = JT808Serializer.Deserialize<JT808Package>(bytes);

//3.数据包头
Assert.Equal(Enums.JT808MsgId.位置信息汇报, jT808Package.Header.MsgId);
Assert.Equal(38, jT808Package.Header.MessageBodyProperty.DataLength);
Assert.Equal(126, jT808Package.Header.MsgNum);
Assert.Equal("123456789012", jT808Package.Header.TerminalPhoneNo);
Assert.False(jT808Package.Header.MessageBodyProperty.IsPackge);
Assert.Equal(0, jT808Package.Header.MessageBodyProperty.PackageIndex);
Assert.Equal(0, jT808Package.Header.MessageBodyProperty.PackgeCount);
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
Assert.Equal(100, ((JT808LocationAttachImpl0x01)jT808_0x0200.JT808LocationAttachData[JT808LocationAttachBase.AttachId0x01]).Mileage);
//4.2.附加信息2
Assert.Equal(125, ((JT808LocationAttachImpl0x02)jT808_0x0200.JT808LocationAttachData[JT808LocationAttachBase.AttachId0x02]).Oil);
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
            { JT808LocationAttachBase.AttachId0x01,new JT808LocationAttachImpl0x01{Mileage = 100}},
            { JT808LocationAttachBase.AttachId0x02,new JT808LocationAttachImpl0x02{Oil = 125}}
        }
});

byte[] data = JT808Serializer.Serialize(jT808Package);

var hex = data.ToHexString();
//输出结果Hex：
//7E 02 00 00 26 12 34 56 78 90 12 00 01 00 00 00 01 00 00 00 02 00 BA 7F 0E 07 E4 F1 1C 00 28 00 3C 00 00 18 10 15 10 10 10 01 04 00 00 00 64 02 02 00 7D 01 6C 7E
```

### 举个栗子3

``` config
// 全局配置
JT808GlobalConfig.Instance
    // 注册自定义位置附加信息
    .Register_0x0200_Attach<JT808LocationAttachImpl0x06>(0x06)
    //.SetMsgSNDistributed(//todo 实现IMsgSNDistributed消息流水号)
    // 注册自定义数据上行透传信息
    //.Register_0x0900_Ext<>(//todo 继承自JT808_0x0900_BodyBase类)
    // 注册自定义数据下行透传信息
    //.Register_0x8900_Ext<>(//todo 继承自JT808_0x8900_BodyBase类)
    // 跳过校验码验证
    .SetSkipCRCCode(true);
```

| Package Name |  Version | Downloads
|--------------|  ------- | ----
| Install-Package JT808 | ![JT808](https://img.shields.io/nuget/v/JT808.svg) | ![JT808](https://img.shields.io/nuget/dt/JT808.svg)

## JT808终端通讯协议消息对照表

|   序号     |   消息ID  |  完成情况   |   消息体名称
|   :------: |   :------: |   :------: |   :------:
|   1   |   0x0001         |   √   |   终端通用应答
|   2   |   0x8001         |   √   |   平台通用应答
|   3   |   0x0002         |   √   |   终端心跳
|   4   |   0x8003         |   √   |   补传分包请求
|   5   |   0x0100         |   √   |   终端注册
|   6   |   0x8100         |   √   |   终端注册应答
|   7   |   0x0003         |   √   |   终端注销
|   8   |   0x0102         |   √   |   终端鉴权
|   9   |   0x8103         |   x   |   设置终端参数
|   10  |   0x8104         |   x   |   查询终端参数
|   11  |   0x0104         |   x   |   查询终端参数应答
|   12  |   0x8105         |   x   |   终端控制
|   13  |   0x8106         |   √   |   查询指定终端参数
|   14  |   0x8107         |   √   |   查询终端属性
|   15  |   0x0107         |   √   |   查询终端属性应答
|   16  |   0x8108         |   √   |   下发终端升级包
|   17  |   0x0108         |   √   |   终端升级结果通知
|   18  |   0x0200         |   √   |   位置信息汇报
|   19  |   0x8201         |   √   |   位置信息查询
|   20  |   0x0201         |   √   |   位置信息查询应答
|   21  |   0x8202         |   √   |   临时位置跟踪控制
|   22  |   0x8203         |   √   |   人工确认报警消息
|   23  |   0x8300         |   √   |   文本信息下发
|   24  |   0x8301         |   √   |   事件设置
|   25  |   0x0301         |   √   |   事件报告
|   26  |   0x8302         |   x   |   提问下发
|   27  |   0x0302         |   √   |   提问应答
|   28  |   0x8303         |   x   |   信息点播菜单设置
|   29  |   0x8203         |   x   |   信息点播/取消
|   30  |   0x8304         |   x   |   信息服务
|   31  |   0x8400         |   √   |   电话回拨
|   32  |   0x8401         |   √   |   设置电话本
|   33  |   0x8500         |   √   |   车辆控制
|   34  |   0x0500         |   √   |   车辆控制应答
|   35  |   0x8600         |   √   |   设置圆形区域
|   36  |   0x8601         |   √   |   删除圆形区域
|   37  |   0x8602         |   √   |   设置矩形区域
|   38  |   0x8603         |   √   |   删除矩形区域
|   39  |   0x8604         |   √   |   设置多边形区域
|   40  |   0x8605         |   √   |   删除多边形区域
|   41  |   0x8606         |   √   |   设置路线
|   42  |   0x8607         |   √   |   删除路线
|   43  |   0x8700         |   x   |   行驶记录仪数据采集命令
|   44  |   0x0700         |   x   |   行驶记录仪数据上传
|   45  |   0x8701         |   x   |   行驶记录仪参数下传命令
|   46  |   0x8203         |   x   |   电子运单上报
|   47  |   0x0702         |   √   |   驾驶员身份信息采集上报
|   48  |   0x8702         |   √   |   上报驾驶员身份信息请求
|   49  |   0x0704         |   √   |   定位数据批量上传
|   50  |   0x0705         |   x   |   CAN 总线数据上传
|   51  |   0x0800         |   x   |   多媒体事件信息上传
|   52  |   0x0801         |   x   |   多媒体数据上传
|   53  |   0x8800         |   x   |   多媒体数据上传应答
|   54  |   0x8801         |   x   |   摄像头立即拍摄命令
|   55  |   0x0805         |   x   |   摄像头立即拍摄命令应答
|   56  |   0x8802         |   x   |   存储多媒体数据检索
|   57  |   0x0802         |   x   |   存储多媒体数据检索应答
|   58  |   0x8803         |   x   |   存储多媒体数据上传
|   59  |   0x8804         |   √   |   录音开始命令
|   60  |   0x8805         |   √   |   单条存储多媒体数据检索上传命令
|   61  |   0x8900         |   √   |   数据下行透传
|   62  |   0x0900         |   √   |   数据上行透传
|   63  |   0x0901         |   x   |   数据压缩上报
|   64  |   0x8A00         |   x   |   平台 RSA 公钥
|   65  |   0x0A00         |   x   |   终端 RSA 公钥
|   66  |   0x8F00~0x8FFF  |   x   |   平台下行消息保留
|   67  |   0x0F00~0x0FFF  |   x   |   终端上行消息保留