using JT808.Protocol.Attributes;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters.JT808_0x8103Formatters;

namespace JT808.Protocol.MessageBody.JT808_0x8103_Body
{
    [JT808Formatter(typeof(JT808_0x8103_0x0001Formatter))]
    public class JT808_0x8103_0x0001 : JT808_0x8103_BodyBase
    {
        /// <summary>
        /// 数据 长度
        /// </summary>
       public byte ParamLength { get; set; } = 4;
        /// <summary>
        /// 终端心跳发送间隔，单位为秒（s）
        /// </summary>
        public uint ParamValue { get; set; }
    }
}
