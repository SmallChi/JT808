using System;
using JT808.Protocol.Attributes;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters.JT808LocationAttach;


namespace JT808.Protocol.MessageBody.JT808LocationAttach
{
    [JT808Formatter(typeof(JT808_0x0200_0x25Formatter))]
    public class JT808LocationAttachImpl0x25 : JT808LocationAttachBase
    {
        /// <summary>
        /// 扩展车辆信号状态位
        /// </summary>
        public int CarSignalStatus { get; set; }
        public override byte AttachInfoId { get;  set; } = 0x25;
        public override byte AttachInfoLength { get;  set; } = 4;
    }
}
