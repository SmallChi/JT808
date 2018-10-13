using System;
using JT808.Protocol.Attributes;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters.JT808LocationAttach;


namespace JT808.Protocol.MessageBody.JT808LocationAttach
{
    [JT808Formatter(typeof(JT808_0x0200_0x2AFormatter))]
    public class JT808LocationAttachImpl0x2A : JT808LocationAttachBase
    {
        /// <summary>
        /// IO状态位
        /// </summary>
        public ushort IOStatus { get; set; }
        public override byte AttachInfoId { get;  set; } = 0x2A;
        public override byte AttachInfoLength { get;  set; } = 2;
    }
}
