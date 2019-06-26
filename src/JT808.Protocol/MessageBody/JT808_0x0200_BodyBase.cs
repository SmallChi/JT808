using JT808.Protocol.Formatters.MessageBodyFormatters;
using System;
using System.Collections.Generic;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 位置附加信息
    /// </summary>
    public abstract class JT808_0x0200_BodyBase
    {
        /// <summary>
        /// 位置附加信息方法
        /// </summary>
        internal static IDictionary<byte, Type> JT808LocationAttachMethod { get; private set; }

        static JT808_0x0200_BodyBase()
        {
            InitJT808LocationAttachMethod();
        }

        private static void InitJT808LocationAttachMethod()
        {
            JT808LocationAttachMethod = new Dictionary<byte, Type>
            {
                {AttachId0x01, typeof(JT808_0x0200_0x01)},
                {AttachId0x02, typeof(JT808_0x0200_0x02)},
                {AttachId0x03, typeof(JT808_0x0200_0x03)},
                {AttachId0x04, typeof(JT808_0x0200_0x04)},
                {AttachId0x11, typeof(JT808_0x0200_0x11)},
                {AttachId0x12, typeof(JT808_0x0200_0x12)},
                {AttachId0x13, typeof(JT808_0x0200_0x13)},
                {AttachId0x25, typeof(JT808_0x0200_0x25)},
                {AttachId0x2A, typeof(JT808_0x0200_0x2A)},
                {AttachId0x2B, typeof(JT808_0x0200_0x2B)},
                {AttachId0x30, typeof(JT808_0x0200_0x30)},
                {AttachId0x31, typeof(JT808_0x0200_0x31)},
            };
        }

        internal static void AddJT808LocationAttachMethod<TJT808LocationAttach>(byte attachInfoId)
            where TJT808LocationAttach : JT808_0x0200_BodyBase
        {
            JT808LocationAttachMethod.Add(attachInfoId, typeof(TJT808LocationAttach));
        }

        internal static void AddJT808LocationAttachMethod(byte attachInfoId, Type type)
        {
            JT808LocationAttachMethod.Add(attachInfoId, type);
        }

        /// <summary>
        /// 附加信息Id
        /// </summary>
        public abstract byte AttachInfoId { get; set; }

        /// <summary>
        /// 附加信息长度
        /// </summary>
        public abstract byte AttachInfoLength { get; set; }

        public const byte AttachId0x01 = 0x01;
        public const byte AttachId0x02 = 0x02;
        public const byte AttachId0x03 = 0x03;
        public const byte AttachId0x04 = 0x04;
        public const byte AttachId0x11 = 0x11;
        public const byte AttachId0x12 = 0x12;
        public const byte AttachId0x13 = 0x13;
        public const byte AttachId0x25 = 0x25;
        public const byte AttachId0x2A = 0x2A;
        public const byte AttachId0x2B = 0x2B;
        public const byte AttachId0x30 = 0x30;
        public const byte AttachId0x31 = 0x31;
    }
}
