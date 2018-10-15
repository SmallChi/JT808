using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.MessageBody.JT808_0x8900_0x0900_Body
{
    /// <summary>
    /// 数据下行透传
    /// </summary>
    public abstract class JT808_0x8900_BodyBase
    {
        public static IDictionary<byte, Type> JT808_0x8900Method { get; private set; }

        static JT808_0x8900_BodyBase()
        {
            JT808_0x8900Method=new Dictionary<byte, Type>();
        }

        internal static void AddJT808_0x8900Method<TJT808_0x8900_Body>(byte  passthroughType)
            where TJT808_0x8900_Body : JT808_0x8900_BodyBase
        {
            JT808_0x8900Method.Add(passthroughType, typeof(TJT808_0x8900_Body));
        }
    }
}
