using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.MessageBody.JT808_0x8103_Body
{
    public abstract class JT808_0x8103_BodyBase
    {
        private const uint JT808_0x8103_0x0001_Type = 0x0001;
        private const uint JT808_0x8103_0x0013_Type = 0x0013;

        public static IDictionary<uint, Type> JT808_0x8103Method { get; private set; }
        /// <summary>
        /// 参数 ID
        /// </summary>
        public abstract uint ParamId { get; set; }

        /// <summary>
        /// 参数长度
        /// </summary>
        public abstract byte ParamLength { get; set; }

        static JT808_0x8103_BodyBase()
        {
            JT808_0x8103Method = new Dictionary<uint, Type>
            {
                {JT808_0x8103_0x0001_Type, typeof(JT808_0x8103_0x0001)},
                {JT808_0x8103_0x0013_Type, typeof(JT808_0x8103_0x0013)},
            };
        }

        internal static void AddJT808_0x8103Method<JT808_0x8103_Body>(uint paramType)
            where JT808_0x8103_Body : JT808_0x8103_BodyBase
        {
            JT808_0x8103Method.Add(paramType, typeof(JT808_0x8103_Body));
        }
    }
}
