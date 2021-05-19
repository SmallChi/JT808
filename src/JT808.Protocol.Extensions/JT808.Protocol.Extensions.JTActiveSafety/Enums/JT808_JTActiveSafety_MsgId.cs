using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.JTActiveSafety.Enums
{
    /// <summary>
    /// 主动安全消息Id
    /// </summary>
    public enum JT808_JTActiveSafety_MsgId : ushort
    {
        /// <summary>
        /// 报警附件信息消息
        /// </summary>
        报警附件信息消息 = 0x1210,
        /// <summary>
        /// 文件信息上传
        /// </summary>
        文件信息上传 = 0x1211,
        /// <summary>
        /// 文件上传完成消息
        /// </summary>
        文件上传完成消息 = 0x1212,
        /// <summary>
        /// 报警附件上传指令
        /// </summary>
        报警附件上传指令 = 0x9208,
        /// <summary>
        /// 文件上传完成消息应答
        /// </summary>
        文件上传完成消息应答 = 0x9212,
    }
}
