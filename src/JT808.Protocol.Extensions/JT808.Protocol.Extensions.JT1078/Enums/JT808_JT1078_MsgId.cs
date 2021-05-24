namespace JT808.Protocol.Extensions.JT1078.Enums
{
    /// <summary>
    /// JT808扩展消息体
    /// </summary>
    public enum JT808_JT1078_MsgId:ushort

    {
        /// <summary>
        /// 终端上传音视频属性
        /// </summary>
        终端上传音视频属性 = 0x1003,
        /// <summary>
        /// 终端上传乘客流量
        /// </summary>
        终端上传乘客流量 = 0x1005,
        /// <summary>
        /// 终端上传音视频资源列表
        /// </summary>
        终端上传音视频资源列表 = 0x1205,
        /// <summary>
        /// 文件上传完成通知
        /// </summary>
        文件上传完成通知 = 0x1206,
        /// <summary>
        /// 查询终端音视频属性
        /// </summary>
        查询终端音视频属性 = 0x9003,
        /// <summary>
        /// 实时音视频传输请求
        /// </summary>
        实时音视频传输请求 = 0x9101,
        /// <summary>
        /// 音视频实时传输控制
        /// </summary>
        音视频实时传输控制 = 0x9102,
        /// <summary>
        /// 实时音视频传输状态通知
        /// </summary>
        实时音视频传输状态通知 = 0x9105,
        /// <summary>
        /// 平台下发远程录像回放请求
        /// </summary>
        平台下发远程录像回放请求 = 0x9201,
        /// <summary>
        /// 平台下发远程录像回放控制
        /// </summary>
        平台下发远程录像回放控制 = 0x9202,
        /// <summary>
        /// 查询资源列表
        /// </summary>
        查询资源列表 = 0x9205,
        /// <summary>
        /// 文件上传指令
        /// </summary>
        文件上传指令 = 0x9206,
        /// <summary>
        /// 文件上传控制
        /// </summary>
        文件上传控制 = 0x9207,
        /// <summary>
        /// 云台旋转
        /// </summary>
        云台旋转 = 0x9301,
        /// <summary>
        /// 云台调整焦距控制
        /// </summary>
        云台调整焦距控制 = 0x9302,
        /// <summary>
        /// 云台调整光圈控制
        /// </summary>
        云台调整光圈控制 = 0x9303,
        /// <summary>
        /// 云台雨刷控制
        /// </summary>
        云台雨刷控制 = 0x9304,
        /// <summary>
        /// 红外补光控制
        /// </summary>
        红外补光控制 = 0x9305,
        /// <summary>
        /// 云台变倍控制
        /// </summary>
        云台变倍控制 = 0x9306
    }
}
