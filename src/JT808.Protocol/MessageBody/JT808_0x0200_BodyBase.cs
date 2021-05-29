using System;
using System.Collections.Generic;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 位置附加信息
    /// </summary>
    public abstract class JT808_0x0200_BodyBase
    {
        /// <summary>
        /// 附加信息Id
        /// </summary>
        public abstract byte AttachInfoId { get; set; }

        /// <summary>
        /// 附加信息长度
        /// </summary>
        public abstract byte AttachInfoLength { get; set; }
        /// <summary>
        /// 附加信息长度扩展
        /// 4个字节
        /// 注意：只适用于已知的协议才行
        /// </summary>
        public virtual uint AttachInfoLengthExtend { get; set; }
    }
}
