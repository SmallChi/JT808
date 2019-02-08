using JT808.Protocol.Attributes;
using JT808.Protocol.MessageBody;

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
        /// 平台通用应答
        /// 0x8001
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x8001))]
        [JT808MsgIdDescription("0x8001", "平台通用应答")]
        平台通用应答 = 0x8001,
        /// <summary>
        /// 终端心跳
        /// 0x0002
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x0002))]
        [JT808MsgIdDescription("0x0002", "终端心跳")]
        终端心跳 = 0x0002,
        /// <summary>
        /// 补传分包请求
        /// 0x8003
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x8003))]
        [JT808MsgIdDescription("0x8003", "补传分包请求")]
        补传分包请求 = 0x8003,
        /// <summary>
        /// 终端注册
        /// 0x0100
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x0100))]
        [JT808MsgIdDescription("0x0100", "终端注册")]
        终端注册 = 0x0100,
        /// <summary>
        /// 终端注册应答
        /// 0x8100
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x8100))]
        [JT808MsgIdDescription("0x8100", "终端注册应答")]
        终端注册应答 = 0x8100,
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
        /// 查询终端参数应答
        /// 0x0104
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x0104))]
        [JT808MsgIdDescription("0x0104", "查询终端参数应答")]
        查询终端参数应答 = 0x0104,
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
        查询终端属性应答 = 0x0107,
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
        /// 位置信息汇报
        /// 0x0200
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x0200))]
        [JT808MsgIdDescription("0x0200", "位置信息汇报")]
        位置信息汇报 = 0x0200,
        /// <summary>
        /// 位置信息查询
        /// 0x8201
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x8201))]
        [JT808MsgIdDescription("0x8201", "位置信息查询")]
        位置信息查询 = 0x8201,
        /// <summary>
        /// 位置信息查询应答
        /// 0x0201
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x0201))]
        [JT808MsgIdDescription("0x0201", "位置信息查询应答")]
        位置信息查询应答 = 0x0201,
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
        /// 文本信息下发
        /// 0x8300
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x8300))]
        [JT808MsgIdDescription("0x8300", "文本信息下发")]
        文本信息下发 = 0x8300,
        /// <summary>
        /// 事件设置
        /// 0x8301
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x8301))]
        [JT808MsgIdDescription("0x8301", "事件设置")]
        事件设置 = 0x8301,
        /// <summary>
        /// 事件报告
        /// 0x0301
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x8301))]
        [JT808MsgIdDescription("0x0301", "事件报告")]
        事件报告 = 0x0301,
        /// <summary>
        /// 提问下发
        /// 0x8302
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x8302))]
        [JT808MsgIdDescription("0x8302", "提问下发")]
        提问下发 = 0x8302,
        /// <summary>
        /// 提问应答
        /// 0x0302
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x0302))]
        [JT808MsgIdDescription("0x8302", "提问应答")]
        提问应答 = 0x0302,
        /// <summary>
        /// 信息点播菜单设置
        /// 0x8303
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x8303))]
        [JT808MsgIdDescription("0x8303", "信息点播菜单设置")]
        信息点播菜单设置 = 0x8303,
        /// <summary>
        /// 信息点播/取消
        /// 0x0303
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x0303))]
        [JT808MsgIdDescription("0x0303", "信息点播/取消")]
        信息点播_取消 = 0x0303,
        /// <summary>
        /// 信息服务
        /// 0x8304
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x8304))]
        [JT808MsgIdDescription("0x8304", "信息服务")]
        信息服务 = 0x8304,
        /// <summary>
        /// 电话回拨
        /// 0x8400
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x8400))]
        [JT808MsgIdDescription("0x8400", "电话回拨")]
        电话回拨 = 0x8400,
        /// <summary>
        /// 设置电话本
        /// 0x8401
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x8401))]
        [JT808MsgIdDescription("0x8401", "设置电话本")]
        设置电话本 = 0x8401,
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
        设置圆形区域 = 0x8600,
        /// <summary>
        /// 删除圆形区域
        /// 0x8601
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x8601))]
        [JT808MsgIdDescription("0x8601", "删除圆形区域")]
        删除圆形区域 = 0x8601,
        /// <summary>
        /// 设置矩形区域
        /// 0x8602
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x8602))]
        [JT808MsgIdDescription("0x8602", "设置矩形区域")]
        设置矩形区域 = 0x8602,
        /// <summary>
        /// 删除矩形区域
        /// 0x8603
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x8603))]
        [JT808MsgIdDescription("0x8603", "删除矩形区域")]
        删除矩形区域 = 0x8603,
        /// <summary>
        /// 设置多边形区域
        /// 0x8604
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x8604))]
        [JT808MsgIdDescription("0x8604", "设置多边形区域")]
        设置多边形区域 = 0x8604,
        /// <summary>
        /// 删除多边形区域
        /// 0x8605
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x8605))]
        [JT808MsgIdDescription("0x8605", "删除多边形区域")]
        删除多边形区域 = 0x8605,
        /// <summary>
        /// 设置路线
        /// 0x8606
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x8606))]
        [JT808MsgIdDescription("0x8606", "设置路线")]
        设置路线 = 0x8606,
        /// <summary>
        /// 删除路线
        /// 0x8607
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x8607))]
        [JT808MsgIdDescription("0x8607", "删除路线")]
        删除路线 = 0x8607,

        ///// <summary>
        ///// 行驶记录仪数据采集命令
        ///// 0x8700
        ///// </summary>
        //[JT808BodiesType(typeof(JT808_0x8700))]
        //[JT808MsgIdDescription("0x8700", "行驶记录仪数据采集命令")]
        //行驶记录仪数据采集命令 = 0x8700,
        ///// <summary>
        ///// 行驶记录仪数据上传
        ///// 0x0700
        ///// </summary>
        //[JT808BodiesType(typeof(JT808_0x0700))]
        //[JT808MsgIdDescription("0x0700", "行驶记录仪数据上传")]
        //行驶记录仪数据上传 = 0x0700,
        ///// <summary>
        ///// 行驶记录仪参数下传命令
        ///// 0x8701
        ///// </summary>
        //[JT808BodiesType(typeof(JT808_0x8701))]
        //[JT808MsgIdDescription("0x8701", "行驶记录仪参数下传命令")]
        //行驶记录仪参数下传命令 = 0x8701,
        /// <summary>
        /// 电子运单上报
        /// 0x0701
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x0701))]
        [JT808MsgIdDescription("0x0701", "电子运单上报")]
        电子运单上报 = 0x0701,
        /// <summary>
        /// 驾驶员身份信息采集上报
        /// 0x0702
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x0702))]
        [JT808MsgIdDescription("0x0702", "驾驶员身份信息采集上报")]
        驾驶员身份信息采集上报 = 0x0702,
        /// <summary>
        /// 上报驾驶员身份信息请求
        /// 0x8702
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x8702))]
        [JT808MsgIdDescription("0x8702", "上报驾驶员身份信息请求")]
        上报驾驶员身份信息请求 = 0x8702,
        /// <summary>
        /// 定位数据批量上传
        /// 0x0704
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x0704))]
        [JT808MsgIdDescription("0x0704", "定位数据批量上传")]
        定位数据批量上传 = 0x0704,
        /// <summary>
        /// CAN总线数据上传
        /// 0x0705
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x0705))]
        [JT808MsgIdDescription("0x0705", "CAN总线数据上传")]
        CAN总线数据上传 = 0x0705,
        /// <summary>
        /// 多媒体事件信息上传
        /// 0x0800
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x0800))]
        [JT808MsgIdDescription("0x0800", "多媒体事件信息上传")]
        多媒体事件信息上传 = 0x0800,
        /// <summary>
        /// 多媒体数据上传
        /// 0x0801
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x0801))]
        [JT808MsgIdDescription("0x0801", "多媒体数据上传")]
        多媒体数据上传 = 0x0801,
        /// <summary>
        /// 多媒体数据上传应答
        /// 0x8800
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x8800))]
        [JT808MsgIdDescription("0x8800", "多媒体数据上传应答")]
        多媒体数据上传应答 = 0x8800,
        /// <summary>
        /// 摄像头立即拍摄命令
        /// 0x8801
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x8801))]
        [JT808MsgIdDescription("0x8801", "摄像头立即拍摄命令")]
        摄像头立即拍摄命令 = 0x8801,
        /// <summary>
        /// 摄像头立即拍摄命令应答
        /// 0x0805
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x0805))]
        [JT808MsgIdDescription("0x0805", "摄像头立即拍摄命令应答")]
        摄像头立即拍摄命令应答 = 0x0805,
        /// <summary>
        /// 存储多媒体数据检索
        /// 0x8802
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x8802))]
        [JT808MsgIdDescription("0x8802", "存储多媒体数据检索")]
        存储多媒体数据检索 = 0x8802,
        /// <summary>
        /// 存储多媒体数据上传
        /// 0x8803
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x8803))]
        [JT808MsgIdDescription("0x8803", "存储多媒体数据上传")]
        存储多媒体数据上传 = 0x8803,
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
        [JT808MsgIdDescription("0x8804", "单条存储多媒体数据检索上传命令")]
        单条存储多媒体数据检索上传命令 = 0x8805,
        /// <summary>
        /// 数据下行透传
        /// 0x8900
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x8900))]
        [JT808MsgIdDescription("0x8900", "数据下行透传")]
        数据下行透传 = 0x8900,
        /// <summary>
        /// 数据上行透传
        /// 0x0900
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x0900))]
        [JT808MsgIdDescription("0x0900", "数据上行透传")]
        数据上行透传 = 0x0900,
        /// <summary>
        /// 数据压缩上报
        /// 0x0901
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x0901))]
        [JT808MsgIdDescription("0x0901", "数据压缩上报")]
        数据压缩上报 = 0x0901,
        /// <summary>
        ///  平台RSA公钥 
        ///  0x8A00
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x8A00))]
        [JT808MsgIdDescription("0x8A00", "平台RSA公钥")]
        平台RSA公钥 = 0x8A00,
        /// <summary>
        ///  终端RSA公钥 
        ///  0x0A00
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x0A00))]
        [JT808MsgIdDescription("0x0A00", "终端RSA公钥")]
        终端RSA公钥 = 0x0A00,
    }
}
