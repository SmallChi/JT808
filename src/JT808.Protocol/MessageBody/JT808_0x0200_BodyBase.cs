using System;
using System.Collections.Generic;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 位置附加信息
    /// </summary>
    public interface JT808_0x0200_BodyBase
    {
        /// <summary>
        /// 附加信息Id
        /// </summary>
        byte AttachInfoId { get; set; }

        /// <summary>
        /// 附加信息长度
        /// </summary>
        byte AttachInfoLength { get; set; }
    }
}
