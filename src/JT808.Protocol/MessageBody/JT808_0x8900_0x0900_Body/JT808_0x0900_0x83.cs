using JT808.Protocol.Attributes;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters.JT808_0x8900_0x0900Formatters;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.MessageBody.JT808_0x8900_0x0900_Body
{
    [JT808Formatter(typeof(JT808_0x0900_0x83Formatter))]
    public class JT808_0x0900_0x83 : JT808_0x0900_BodyBase
    {
        /// <summary>
        /// 透传内容
        /// </summary>
        public string PassthroughContent { get; set; }
    }
}
