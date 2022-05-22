using JT808.Protocol.MessageBody;

namespace JT808.Protocol.Enums
{
    /// <summary>
    /// JT808消息
    /// JT808 MsgId
    /// </summary>
    public enum JT808MsgId : ushort
    {
        /// <summary>
        /// 终端通用应答
        /// 0x0001
        /// Terminal universal reply
        /// </summary>
        _0x0001 = 0x0001,
        /// <summary>
        /// 平台通用应答
        /// 0x8001
        /// Platform Universal response
        /// </summary>
        _0x8001 = 0x8001,
        /// <summary>
        /// 终端心跳
        /// 0x0002
        /// Terminal heart
        /// </summary>
        _0x0002 = 0x0002,
        /// <summary>
        /// 补传分包请求
        /// 0x8003
        /// Forwarding subcontract request
        /// </summary>
        _0x8003 = 0x8003,
        /// <summary>
        /// 终端注册
        /// 0x0100
        /// Terminal registration
        /// </summary>
        _0x0100 = 0x0100,
        /// <summary>
        /// 终端注册应答
        /// 0x8100
        /// Terminal registration reply
        /// </summary>
        _0x8100 = 0x8100,
        /// <summary>
        /// 终端注销
        /// 0x0003
        /// Terminal logout
        /// </summary>
        _0x0003 = 0x0003,
        /// <summary>
        /// 终端鉴权
        /// 0x0102
        /// Terminal authentication
        /// </summary>
        _0x0102 = 0x0102,
        /// <summary>
        /// 设置终端参数
        /// 0x8103
        /// Setting Terminal Parameters
        /// </summary>
        _0x8103 = 0x8103,
        /// <summary>
        /// 查询终端参数
        /// 0x8104
        /// Querying Terminal Parameters
        /// </summary>
        _0x8104 = 0x8104,
        /// <summary>
        /// 查询终端参数应答
        /// 0x0104
        /// Query terminal parameter response
        /// </summary>
        _0x0104 = 0x0104,
        /// <summary>
        /// 终端控制
        /// 0x8105
        /// Terminal control
        /// </summary>
        _0x8105 = 0x8105,
        /// <summary>
        /// 查询指定终端参数
        /// 0x8106
        /// Example Query specified terminal parameters
        /// </summary>
        _0x8106 = 0x8106,
        /// <summary>
        /// 查询终端属性
        /// 0x8107
        /// Querying Terminal Properties
        /// </summary>
        _0x8107 = 0x8107,
        /// <summary>
        /// 查询终端属性应答
        /// 0x0107
        /// Query the response of the terminal properties
        /// </summary>
        _0x0107 = 0x0107,
        /// <summary>
        /// 下发终端升级包
        /// 0x8108
        /// Query terminal properties reply Deliver the terminal upgrade package
        /// </summary>
        _0x8108 = 0x8108,
        /// <summary>
        /// 终端升级结果通知
        /// 0x0108
        /// Terminal upgrade result notification
        /// </summary>
        _0x0108 = 0x0108,
        /// <summary>
        /// 位置信息汇报
        /// 0x0200
        /// Location information reporting
        /// </summary>
        _0x0200 = 0x0200,
        /// <summary>
        /// 位置信息查询
        /// 0x8201
        /// Location information query
        /// </summary>
        _0x8201 = 0x8201,
        /// <summary>
        /// 位置信息查询应答
        /// 0x0201
        /// Location information query response
        /// </summary>
        _0x0201 = 0x0201,
        /// <summary>
        /// 临时位置跟踪控制
        /// 0x8202
        /// Temporary position tracking control
        /// </summary>
        _0x8202 = 0x8202,
        /// <summary>
        /// 人工确认报警消息
        /// 0x8203
        /// Manually confirm the alarm message
        /// </summary>
        _0x8203 = 0x8203,
        /// <summary>
        /// 文本信息下发
        /// 0x8300
        /// Text message delivery
        /// </summary>
        _0x8300 = 0x8300,
        /// <summary>
        /// 事件设置
        /// 0x8301
        /// Event set
        /// </summary>
        _0x8301 = 0x8301,
        /// <summary>
        /// 事件报告
        /// 0x0301
        /// event report
        /// </summary>
        _0x0301 = 0x0301,
        /// <summary>
        /// 提问下发
        /// 0x8302
        /// Questions issued
        /// </summary>
        _0x8302 = 0x8302,
        /// <summary>
        /// 提问应答
        /// 0x0302
        /// Question answering
        /// </summary>
        _0x0302 = 0x0302,
        /// <summary>
        /// 信息点播菜单设置
        /// 0x8303
        /// Information on demand menu Settings
        /// </summary>
        _0x8303 = 0x8303,
        /// <summary>
        /// 信息点播/取消
        /// 0x0303
        /// Information on demand
        /// Information cancel
        /// </summary>
        _0x0303 = 0x0303,
        /// <summary>
        /// 信息服务
        /// 0x8304
        /// Information service
        /// </summary>
        _0x8304 = 0x8304,
        /// <summary>
        /// 电话回拨
        /// 0x8400
        /// Back to the dial
        /// </summary>
        _0x8400 = 0x8400,
        /// <summary>
        /// 设置电话本
        /// 0x8401
        /// Set up a phone book
        /// </summary>
        _0x8401 = 0x8401,
        /// <summary>
        /// 车辆控制
        /// 0x8500
        /// Vehicle control
        /// </summary>
        _0x8500 = 0x8500,
        /// <summary>
        /// 车辆控制应答
        /// 0x0500
        /// Vehicle control response
        /// </summary>
        _0x0500 = 0x0500,
        /// <summary>
        /// 设置圆形区域
        /// 0x8600
        /// Set the circular area
        /// </summary>
        _0x8600 = 0x8600,
        /// <summary>
        /// 删除圆形区域
        /// 0x8601
        /// Delete circular area
        /// </summary>
        _0x8601 = 0x8601,
        /// <summary>
        /// 设置矩形区域
        /// 0x8602
        /// Set rectangle area
        /// </summary>
        _0x8602 = 0x8602,
        /// <summary>
        /// 删除矩形区域
        /// 0x8603
        /// Delete rectangular area
        /// </summary>
        _0x8603 = 0x8603,
        /// <summary>
        /// 设置多边形区域
        /// 0x8604
        /// Set polygon region
        /// </summary>
        _0x8604 = 0x8604,
        /// <summary>
        /// 删除多边形区域
        /// 0x8605
        /// Delete polygon area
        /// </summary>
        _0x8605 = 0x8605,
        /// <summary>
        /// 设置路线
        /// 0x8606
        /// Set the route
        /// </summary>
        _0x8606 = 0x8606,
        /// <summary>
        /// 删除路线
        /// 0x8607
        /// Delete the route
        /// </summary>
        _0x8607 = 0x8607,
        /// <summary>
        /// 行驶记录仪数据采集命令
        /// 0x8700
        /// Drive recorder data acquisition command
        /// </summary>
        _0x8700 = 0x8700,
        /// <summary>
        /// 行驶记录仪数据上传
        /// 0x0700
        /// Data upload from driving recorder
        /// </summary>
        _0x0700 = 0x0700,
        /// <summary>
        /// 行驶记录仪参数下传命令
        /// 0x8701
        /// Driving recorder parameters down command
        /// </summary>
        _0x8701 = 0x8701,
        /// <summary>
        /// 电子运单上报
        /// 0x0701
        /// Electronic waybill reporting
        /// </summary>
        _0x0701 = 0x0701,
        /// <summary>
        /// 驾驶员身份信息采集上报
        /// 0x0702
        /// Collect and report driver identity information
        /// </summary>
        _0x0702 = 0x0702,
        /// <summary>
        /// 上报驾驶员身份信息请求
        /// 0x8702
        /// Report driver identification request
        /// </summary>
        _0x8702 = 0x8702,
        /// <summary>
        /// 定位数据批量上传
        /// 0x0704
        /// Upload location data in batches
        /// </summary>
        _0x0704 = 0x0704,
        /// <summary>
        /// CAN总线数据上传
        /// 0x0705
        /// CAN bus data upload
        /// </summary>
        _0x0705 = 0x0705,
        /// <summary>
        /// 多媒体事件信息上传
        /// 0x0800
        /// Upload multimedia event information
        /// </summary>
        _0x0800 = 0x0800,
        /// <summary>
        /// 多媒体数据上传
        /// 0x0801
        /// Multimedia Data upload
        /// </summary>
        _0x0801 = 0x0801,
        /// <summary>
        /// 多媒体数据上传应答
        /// 0x8800
        /// Reply to multimedia data upload
        /// </summary>
        _0x8800 = 0x8800,
        /// <summary>
        /// 摄像头立即拍摄命令
        /// 0x8801
        /// The camera immediately shoots the command
        /// </summary>
        _0x8801 = 0x8801,
        /// <summary>
        /// 摄像头立即拍摄命令应答
        /// 0x0805
        /// The camera immediately shoots the command reply
        /// </summary>
        _0x0805 = 0x0805,
        /// <summary>
        /// 存储多媒体数据检索
        /// 0x8802
        /// Store multimedia data retrieval
        /// </summary>
        _0x8802 = 0x8802,
        /// <summary>
        ///  存储多媒体数据检索应答 
        ///  0x0802
        ///  Store multimedia data retrieval replies
        /// </summary>
        _0x0802 = 0x0802,
        /// <summary>
        /// 存储多媒体数据上传
        /// 0x8803
        /// Store multimedia data upload
        /// </summary>
        _0x8803 = 0x8803,
        /// <summary>
        /// 录音开始命令
        /// 0x8804
        /// Recording Start Command
        /// </summary>
        _0x8804 = 0x8804,
        /// <summary>
        /// 单条存储多媒体数据检索上传命令
        /// 0x8805
        /// Single storage multimedia data retrieval upload command
        /// </summary>
        _0x8805 = 0x8805,
        /// <summary>
        /// 数据下行透传
        /// 0x8900
        /// Data is transmitted through downlink
        /// </summary>
        _0x8900 = 0x8900,
        /// <summary>
        /// 数据上行透传
        /// 0x0900
        /// Data is transparently transmitted upstream
        /// </summary>
        _0x0900 = 0x0900,
        /// <summary>
        /// 数据压缩上报
        /// 0x0901
        /// Data compression reporting
        /// </summary>
        _0x0901 = 0x0901,
        /// <summary>
        ///  平台RSA公钥 
        ///  0x8A00
        ///  Platform RSA Public Key  
        /// </summary>
        _0x8A00 = 0x8A00,
        /// <summary>
        ///  终端RSA公钥 
        ///  0x0A00
        ///  Terminal RSA Public Key  
        /// </summary>
        _0x0A00 = 0x0A00,
        /// <summary>
        ///  查询服务器时间请求 
        ///  0x0004
        ///  Queries server time requests
        /// </summary>
        _0x0004 = 0x0004,
        /// <summary>
        ///  查询服务器时间应答 
        ///  0x8004
        ///  Query the server time response
        /// </summary>
        _0x8004 = 0x8004,
        /// <summary>
        ///  终端补传分包请求 
        ///  0x0005
        ///  The terminal sends the subcontract request
        /// </summary>
        _0x0005 = 0x0005,
        /// <summary>
        ///  链路检测 
        ///  0x8204
        ///  Link detection
        /// </summary>
        _0x8204 = 0x8204,
        /// <summary>
        ///  查询区域或线路数据 
        ///  0x8608
        ///  Example Query area or line data
        /// </summary>
        _0x8608 = 0x8608,
        /// <summary>
        ///  查询区域或线路数据应答 
        ///  0x0608
        ///  Query area or line data reply
        /// </summary>
        _0x0608 = 0x0608,
    }
}
