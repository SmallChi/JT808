using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 数据上行透传
    /// </summary>
    public abstract class JT808_0x0900_BodyBase
    {
        internal static HashSet<byte> JT808_0x0900Method { get; private set; }

        static JT808_0x0900_BodyBase()
        {
            JT808_0x0900Method = new HashSet<byte>();
        }
    }
}
