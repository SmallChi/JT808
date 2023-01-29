using JT808.Protocol.Interfaces;
using System;
using System.Collections.Generic;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 自定义终端参数设置
    /// </summary>
    public interface JT808_0x8103_CustomBodyBase : IJT808Description
    {
        /// <summary>
        /// 参数 ID
        /// </summary>
        uint ParamId { get; set; }

        /// <summary>
        /// 参数长度
        /// </summary>
        byte ParamLength { get; set; }
    }
}
