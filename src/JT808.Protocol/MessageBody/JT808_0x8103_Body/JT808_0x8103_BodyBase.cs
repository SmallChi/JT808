using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.MessageBody.JT808_0x8103_Body
{
    public class JT808_0x8103_BodyBase
    {
        private const uint JT808_0x8103_0x0001_Type = 0x0001;

        public static IDictionary<uint, Type> JT808_0x8103Method { get; private set; }

        public uint ParamId { get; set; }

        static JT808_0x8103_BodyBase()
        {
            JT808_0x8103Method = new Dictionary<uint, Type>
            {
                {JT808_0x8103_0x0001_Type, typeof(JT808_0x8103_0x0001)},
            };
        }

        internal static void AddJT808_0x8103Method<JT808_0x8103_Body>(uint paramType)
            where JT808_0x8103_Body : JT808_0x8103_BodyBase
        {
            JT808_0x8103Method.Add(paramType, typeof(JT808_0x8103_Body));
        }
    }
}
