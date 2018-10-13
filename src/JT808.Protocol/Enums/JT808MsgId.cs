using JT808.Protocol.Attributes;
using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Enums
{
    /// <summary>
    /// JT808消息
    /// </summary>
    public enum JT808MsgId : ushort
    {
        /// <summary>
        /// 终端通用应答
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x0001))]
        终端通用应答 = 0x0001,
        /// <summary>
        /// 终端心跳
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x0002))]
        终端心跳 = 0x0002,
        /// <summary>
        /// 终端注册
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x0100))]
        终端注册 = 0x0100,
        /// <summary>
        /// 终端注销
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x0003))]
        终端注销 = 0x0003,
        /// <summary>
        /// 终端鉴权
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x0102))]
        终端鉴权 = 0x0102,
        /// <summary>
        /// 位置信息汇报
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x0200))]
        位置信息汇报 = 0x0200,
        /// <summary>
        ///  终端RSA公钥【0A00】 
        /// </summary>
        终端RSA公钥 = 0x0A00,
        /// <summary>
        /// 平台通用应答
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x8001))]
        平台通用应答 = 0x8001,
        /// <summary>
        /// 终端注册应答
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x8100))]
        终端注册应答 = 0x8100,
        /// <summary>
        /// 文本信息下发
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x8300))]
        文本信息下发 = 0x8300,
        /// <summary>
        /// 定位数据批量上传
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x0704))]
        定位数据批量上传 = 0x0704,
        /// <summary>
        /// 多媒体数据上传
        /// </summary>
        //[JT808BodiesType(typeof(JT808_0x0801))]
        多媒体数据上传 = 0x0801,
        [JT808BodiesType(typeof(JT808_0x8201))]
        位置信息查询=0x8201,
        [JT808BodiesType(typeof(JT808_0x0201))]
        位置信息查询应答 = 0x0201,
        [JT808BodiesType(typeof(JT808_0x8107))]
        查询终端属性 = 0x8107,
        [JT808BodiesType(typeof(JT808_0x0107))]
        查询终端属性应答 =0x0107,
        [JT808BodiesType(typeof(JT808_0x8108))]
        下发终端升级包 = 0x8108,
        [JT808BodiesType(typeof(JT808_0x0108))]
        终端升级结果通知 = 0x0108,
        [JT808BodiesType(typeof(JT808_0x8202))]
        临时位置跟踪控制 = 0x8202,
        [JT808BodiesType(typeof(JT808_0x8500))]
        车辆控制 = 0x8500,
        [JT808BodiesType(typeof(JT808_0x0500))]
        车辆控制应答 = 0x0500,
        [JT808BodiesType(typeof(JT808_0x8702))]
        上报驾驶员身份信息请求 = 0x8702,
        [JT808BodiesType(typeof(JT808_0x8400))]
        电话回拨 =0x8400,
        [JT808BodiesType(typeof(JT808_0x8401))]
        设置电话本 = 0x8401,
        [JT808BodiesType(typeof(JT808_0x8804))]
        录音开始命令 = 0x8804,
        [JT808BodiesType(typeof(JT808_0x8805))]
        单条存储多媒体数据检索上传命令 = 0x8805,
        [JT808BodiesType(typeof(JT808_0x0900))]
        数据上行透传 = 0x0900,
    }
}
