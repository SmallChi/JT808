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
    public abstract class JT808_0x8103_BodyBase: IJT808Description
    {
        /// <summary>
        /// 参数 ID
        /// </summary>
        public abstract uint ParamId { get; set; }
        /// <summary>
        /// 参数长度
        /// </summary>
        public abstract byte ParamLength { get; set; }
        /// <summary>
        /// 消息描述
        /// </summary>
        public abstract string Description { get; }
    }
}
