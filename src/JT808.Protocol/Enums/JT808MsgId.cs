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
        /// 0x0001
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x0001))]
        [JT808MsgIdDescription("0x0001", "终端通用应答")]
        终端通用应答 = 0x0001,
        /// <summary>
        /// 终端心跳
        /// 0x0002
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x0002))]
        [JT808MsgIdDescription("0x0002", "终端心跳")]
        终端心跳 = 0x0002,
        /// <summary>
        /// 终端注册
        /// 0x0100
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x0100))]
        [JT808MsgIdDescription("0x0100", "终端注册")]
        终端注册 = 0x0100,
        /// <summary>
        /// 终端注销
        /// 0x0003
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x0003))]
        [JT808MsgIdDescription("0x0003", "终端注销")]
        终端注销 = 0x0003,
        /// <summary>
        /// 终端鉴权
        /// 0x0102
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x0102))]
        [JT808MsgIdDescription("0x0102", "终端鉴权")]
        终端鉴权 = 0x0102,
        /// <summary>
        /// 位置信息汇报
        /// 0x0200
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x0200))]
        [JT808MsgIdDescription("0x0200", "位置信息汇报")]
        位置信息汇报 = 0x0200,
        /// <summary>
        ///  终端RSA公钥【0A00】 
        ///  0x0A00
        /// </summary>
        [JT808MsgIdDescription("0x0A00", "终端RSA公钥")]
        终端RSA公钥 = 0x0A00,
        /// <summary>
        /// 平台通用应答
        /// 0x8001
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x8001))]
        [JT808MsgIdDescription("0x8001", "平台通用应答")]
        平台通用应答 = 0x8001,
        /// <summary>
        /// 补传分包请求
        /// 0x8003
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x8003))]
        [JT808MsgIdDescription("0x8003", "补传分包请求")]
        补传分包请求 = 0x8003,
        /// <summary>
        /// 终端注册应答
        /// 0x8100
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x8100))]
        [JT808MsgIdDescription("0x8100", "终端注册应答")]
        终端注册应答 = 0x8100,
        /// <summary>
        /// 文本信息下发
        /// 0x8300
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x8300))]
        [JT808MsgIdDescription("0x8300", "文本信息下发")]
        文本信息下发 = 0x8300,
        /// <summary>
        /// 定位数据批量上传
        /// 0x0704
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x0704))]
        [JT808MsgIdDescription("0x0704", "定位数据批量上传")]
        定位数据批量上传 = 0x0704,
        /// <summary>
        /// 多媒体数据上传
        /// 0x0801
        /// </summary>
        //[JT808BodiesType(typeof(JT808_0x0801))]
        [JT808MsgIdDescription("0x0801", "多媒体数据上传")]
        多媒体数据上传 = 0x0801,
        /// <summary>
        /// 位置信息查询
        /// 0x8201
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x8201))]
        [JT808MsgIdDescription("0x8201", "位置信息查询")]
        位置信息查询 =0x8201,
        /// <summary>
        /// 位置信息查询应答
        /// 0x0201
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x0201))]
        [JT808MsgIdDescription("0x0201", "位置信息查询应答")]
        位置信息查询应答 = 0x0201,
        /// <summary>
        /// 设置终端参数
        /// 0x8103
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x8103))]
        [JT808MsgIdDescription("0x8103", "设置终端参数")]
        设置终端参数 = 0x8103,
        /// <summary>
        /// 查询终端参数
        /// 0x8104
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x8104))]
        [JT808MsgIdDescription("0x8104", "查询终端参数")]
        查询终端参数 = 0x8104,
        /// <summary>
        /// 终端控制
        /// 0x8105
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x8105))]
        [JT808MsgIdDescription("0x8105", "终端控制")]
        终端控制 = 0x8105,
        /// <summary>
        /// 查询指定终端参数
        /// 0x8106
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x8106))]
        [JT808MsgIdDescription("0x8106", "查询指定终端参数")]
        查询指定终端参数 = 0x8106,
        /// <summary>
        /// 查询终端参数应答
        /// 0x0104
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x0104))]
        [JT808MsgIdDescription("0x0104", "查询终端参数应答")]
        查询终端参数应答 = 0x0104,

        /// <summary>
        /// 查询终端属性
        /// 0x8107
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x8107))]
        [JT808MsgIdDescription("0x8107", "查询终端属性")]
        查询终端属性 = 0x8107,

        /// <summary>
        /// 查询终端属性应答
        /// 0x0107
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x0107))]
        [JT808MsgIdDescription("0x0107", "查询终端属性应答")]
        查询终端属性应答 =0x0107,
        /// <summary>
        /// 下发终端升级包
        /// 0x8108
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x8108))]
        [JT808MsgIdDescription("0x8108", "下发终端升级包")]
        下发终端升级包 = 0x8108,
        /// <summary>
        /// 终端升级结果通知
        /// 0x0108
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x0108))]
        [JT808MsgIdDescription("0x0108", "终端升级结果通知")]
        终端升级结果通知 = 0x0108,
        /// <summary>
        /// 临时位置跟踪控制
        /// 0x8202
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x8202))]
        [JT808MsgIdDescription("0x8202", "临时位置跟踪控制")]
        临时位置跟踪控制 = 0x8202,
        /// <summary>
        /// 人工确认报警消息
        /// 0x8203
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x8203))]
        [JT808MsgIdDescription("0x8203", "人工确认报警消息")]
        人工确认报警消息 = 0x8203,
        /// <summary>
        /// 车辆控制
        /// 0x8500
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x8500))]
        [JT808MsgIdDescription("0x8500", "车辆控制")]
        车辆控制 = 0x8500,
        /// <summary>
        /// 车辆控制应答
        /// 0x0500
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x0500))]
        [JT808MsgIdDescription("0x0500", "车辆控制应答")]
        车辆控制应答 = 0x0500,
        /// <summary>
        /// 设置圆形区域
        /// 0x8600
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x8600))]
        [JT808MsgIdDescription("0x8600", "设置圆形区域")]
        设置圆形区域 =0x8600,
        /// <summary>
        /// 删除圆形区域
        /// 0x8601
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x8601))]
        [JT808MsgIdDescription("0x8601", "删除圆形区域")]
        删除圆形区域 = 0x8601,
        /// <summary>
        /// 上报驾驶员身份信息请求
        /// 0x8702
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x8702))]
        [JT808MsgIdDescription("0x8702", "上报驾驶员身份信息请求")]
        上报驾驶员身份信息请求 = 0x8702,
        /// <summary>
        /// 电话回拨
        /// 0x8400
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x8400))]
        [JT808MsgIdDescription("0x8400", "电话回拨")]
        电话回拨 =0x8400,
        /// <summary>
        /// 设置电话本
        /// 0x8401
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x8401))]
        [JT808MsgIdDescription("0x8401", "设置电话本")]
        设置电话本 = 0x8401,
        /// <summary>
        /// 录音开始命令
        /// 0x8804
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x8804))]
        [JT808MsgIdDescription("0x8804", "录音开始命令")]
        录音开始命令 = 0x8804,
        /// <summary>
        /// 单条存储多媒体数据检索上传命令
        /// 0x8805
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x8805))]
        [JT808MsgIdDescription("0x8805", "单条存储多媒体数据检索上传命令")]
        单条存储多媒体数据检索上传命令 = 0x8805,
        /// <summary>
        /// 数据上行透传
        /// 0x0900
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x0900))]
        [JT808MsgIdDescription("0x0900", "数据上行透传")]
        数据上行透传 = 0x0900,
    }
}
