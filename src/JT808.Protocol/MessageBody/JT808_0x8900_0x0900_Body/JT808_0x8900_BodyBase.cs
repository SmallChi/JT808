using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.MessageBody.JT808_0x8900_0x0900_Body
{
    public abstract class JT808_0x8900_BodyBase
    {
        public static IDictionary<byte, Type> JT808_0x8900Method { get; private set; }

        static JT808_0x8900_BodyBase()
        {
            JT808_0x8900Method=new Dictionary<byte, Type>();
        }

        public static void AddJT808LocationAttachMethod<TJT808_0x8900_Body>(byte  passthroughType)
            where TJT808_0x8900_Body : JT808_0x8900_BodyBase
        {
            JT808_0x8900Method.Add(passthroughType, typeof(TJT808_0x8900_Body));
        }
    }
}
