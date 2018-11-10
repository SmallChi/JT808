using JT808.Protocol.Attributes;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters.JT808_0x8103Formatters;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.MessageBody.JT808_0x8103_Body
{
    /// <summary>
    /// 主服务器地址,IP 或域名
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8103_0x0013Formatter))]
    public class JT808_0x8103_0x0013 : JT808_0x8103_BodyBase
    {
        public override uint ParamId { get; set; } = 0x0013;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public byte ParamLength { get; set; }
        /// <summary>
        /// 主服务器地址,IP 或域名
        /// </summary>
        public string ParamValue { get; set; }
    }
}
