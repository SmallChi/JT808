using JT808.Protocol.Attributes;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Test.JT808Formatters.MessageBodyFormatters.JT808LocationAttach;

namespace JT808.Protocol.Test.JT808LocationAttach
{
    /// <summary>
    /// 自定义附加信息
    /// Age-word-2
    /// UserName-BCD(10)
    /// Gerder-byte-1
    /// </summary>
    [JT808Formatter(typeof(JT808_0x0200_0x06Formatter))]
    public class JT808LocationAttachImpl0x06: JT808_0x0200_CustomBodyBase
    {
        public override byte AttachInfoId { get;  set; } = 0x06;
        public override byte AttachInfoLength { get;  set; } = 13;
        public int Age { get; set; }
        public byte Gender { get; set; }
        public string UserName { get; set; }
    }
}
