using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Enums
{
    public enum JT808ErrorCode
    {
        /// <summary>
        /// 校验和不相等
        /// </summary>
        CheckCodeNotEqual = 1001,
        /// <summary>
        /// 没有标记
        /// <see cref="JT808.Protocol.Attributes.JT808FormatterAttribute"/>
        /// </summary>
        GetFormatterAttributeError = 1002,
        /// <summary>
        /// 消息头解析错误
        /// </summary>
        HeaderParseError = 1003,
        /// <summary>
        /// 消息体解析错误
        /// </summary>
        BodiesParseError = 1004,
        GetAttributeError=1005
    }
}
