using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 查询终端音视频属性
    /// </summary>
    public class JT808_0x9003:JT808Bodies
    {
        /// <summary>
        /// 查询终端音视频属性
        /// </summary>
        public string Description => "查询终端音视频属性";
        /// <summary>
        /// 0x9003
        /// </summary>
        public ushort MsgId => 0x9003;
        /// <summary>
        /// SkipSerialization
        /// </summary>
        public bool SkipSerialization => true;
    }
}
