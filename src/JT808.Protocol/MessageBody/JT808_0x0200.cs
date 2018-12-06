using System;
using System.Collections.Generic;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters;
using JT808.Protocol.Attributes;
using System.Runtime.Serialization;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 位置信息汇报
    /// </summary>
    [JT808Formatter(typeof(JT808_0x0200Formatter))]
    public class JT808_0x0200 : JT808Bodies
    {
        /// <summary>
        /// 报警标志 
        /// </summary>
        public uint AlarmFlag { get; set; }
        /// <summary>
        /// 状态位标志
        /// </summary>
        public uint StatusFlag { get; set; }
        /// <summary>
        /// 纬度
        /// 以度为单位的纬度值乘以 10 的 6 次方，精确到百万分之一度
        /// </summary>
        public int Lat { get; set; }
        /// <summary>
        /// 经度
        /// 以度为单位的经度值乘以 10 的 6 次方，精确到百万分之一度
        /// </summary>
        public int Lng { get; set; }
        /// <summary>
        /// 高程
        /// 海拔高度，单位为米（m）
        /// </summary>
        public ushort Altitude { get; set; }
        /// <summary>
        /// 速度 1/10km/h
        /// </summary>
        public ushort Speed { get; set; }
        /// <summary>
        /// 方向 0-359，正北为 0，顺时针
        /// </summary>
        public ushort Direction { get; set; }
        /// <summary>
        /// YY-MM-DD-hh-mm-ss（GMT+8 时间，本标准中之后涉及的时间均采用此时区）
        /// </summary>
        public DateTime GPSTime { get; set; }
        /// <summary>
        /// 位置附加信息
        /// </summary>
        public Dictionary<byte, JT808_0x0200_BodyBase> JT808LocationAttachData { get; set; }
        /// <summary>
        /// 存储自定义附加信息源数据
        /// </summary>
        [IgnoreDataMember]
        public Dictionary<byte, byte[]> JT808CustomLocationAttachOriginalData { get; set; }
        /// <summary>
        /// 自定义位置附加信息
        /// 场景：
        /// 一个设备厂商对应多个设备类型，不同设备类型可能存在相同的自定义位置附加信息Id，导致自定义附加信息Id冲突，无法解析。
        /// 
        /// 解决方式1：
        /// a.根据不同的设备类型和相同的自定义附加信息Id组合(DeviceType_AttachId)形如:ab_0xD2、cd_0xD2;
        /// b.根据设备上传的自定义附加信息Id(0xD2),通过一把梭的方式调用对应自定义附加信息Id(0xD2)的所有解析器;
        /// c.根据平台录入的设备类型,通过终端号关联设备类型，查找出仅有的一个解析器解析的数据;
        /// 缺点：
        /// 1.依赖平台录入的设备类型
        /// 2.会带来一些性能损耗
        /// 一般来说：开发先解析好对应设备的协议，这时候是知道对应的设备类型，可以先死在程序里，然后录入到对应的平台中。
        /// 
        /// 解决方式2：
        /// 根据不同的设备对应不同的端口，通过端口解析不同设备;
        /// 缺点：需要维护端口和设备之间的关联及对应的解析服务
        ///
        /// 解决方式3：一和二结合
        /// 在检查到对应的协议之后，下发消息让设备换到对应的端口
        /// 1.对外先有一个统一的端口
        /// 2.根据设备上报的数据通过类似路由的方式，找到对应该协议和端口，然后往设备下发更改对应的ip和端口
        /// 
        /// 解决方式4：
        /// 1.凡是解析自定义附加信息Id协议的，先进行分割存储，然后在根据外部的设备类型进行统一处理。
        /// 2.可以根据设备类型做个工厂，解耦对公共序列化器的依赖。
        /// 缺点：
        /// 依赖平台录入的设备类型
        /// </summary>
        public Dictionary<byte, JT808_0x0200_CustomBodyBase> JT808CustomLocationAttachData { get; set; }
    }
}
