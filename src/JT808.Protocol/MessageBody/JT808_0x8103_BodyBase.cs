using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 终端参数设置
    /// </summary>
    public interface JT808_0x8103_BodyBase: IJT808Description
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
