using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;
using System.Collections.Generic;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 摄像头立即拍摄命令应答
    /// 0x0805
    /// </summary>
    [JT808Formatter(typeof(JT808_0x0805_Formatter))]
    public class JT808_0x0805 : JT808Bodies
    {
        /// <summary>
        /// 应答流水号
        /// 对应平台摄像头立即拍摄命令的消息流水号
        /// </summary>
        public ushort MsgNum { get; set; }
        /// <summary>
        /// 结果
        /// 0：成功；1：失败；2：通道不支持。以下字段在结果=0 时才有效。
        /// </summary>
        public byte Result { get; set; }
        /// <summary>
        /// 多媒体ID个数
        /// 拍摄成功的多媒体个数
        /// </summary>
        public ushort MultimediaIdCount { get; set; }
        /// <summary>
        /// 多媒体ID列表
        /// </summary>
        public List<uint> MultimediaIds { get; set; }
    }
}
