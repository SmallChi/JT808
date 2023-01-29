using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 自定义位置附加信息
    /// </summary>
    public interface JT808_0x0200_CustomBodyBase2
    {
        /// <summary>
        /// 自定义附加信息Id扩展
        /// 两个字节
        /// </summary>
        ushort AttachInfoId { get; set; }
        /// <summary>
        /// 自定义附加信息长度
        /// </summary>
        byte AttachInfoLength { get; set; }
    }
}
