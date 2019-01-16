using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.MessageBody
{
    public abstract class JT808_0x8103_BodyBase
    {
        public const uint JT808_0x8103_0x0001_Type = 0x0001;
        public const uint JT808_0x8103_0x0002_Type = 0x0002;
        public const uint JT808_0x8103_0x0003_Type = 0x0003;
        public const uint JT808_0x8103_0x0004_Type = 0x0004;
        public const uint JT808_0x8103_0x0005_Type = 0x0005;
        public const uint JT808_0x8103_0x0006_Type = 0x0006;
        public const uint JT808_0x8103_0x0007_Type = 0x0007;
        public const uint JT808_0x8103_0x0013_Type = 0x0013;

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
                {JT808_0x8103_0x0002_Type, typeof(JT808_0x8103_0x0002)},
                {JT808_0x8103_0x0003_Type, typeof(JT808_0x8103_0x0003)},
                {JT808_0x8103_0x0004_Type, typeof(JT808_0x8103_0x0004)},
                {JT808_0x8103_0x0005_Type, typeof(JT808_0x8103_0x0005)},
                {JT808_0x8103_0x0006_Type, typeof(JT808_0x8103_0x0006)},
                {JT808_0x8103_0x0007_Type, typeof(JT808_0x8103_0x0007)},
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
