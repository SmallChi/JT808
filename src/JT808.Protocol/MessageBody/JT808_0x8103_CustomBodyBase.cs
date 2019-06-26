using System;
using System.Collections.Generic;

namespace JT808.Protocol.MessageBody
{
    public abstract class JT808_0x8103_CustomBodyBase
    {
        /// <summary>
        /// 参数 ID
        /// </summary>
        public abstract uint ParamId { get; set; }

        /// <summary>
        /// 参数长度
        /// </summary>
        public abstract byte ParamLength { get; set; }
    }
}
