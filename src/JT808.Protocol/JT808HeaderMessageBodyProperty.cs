using JT808.Protocol.Attributes;
using JT808.Protocol.Enums;
using JT808.Protocol.Formatters;
using System;

namespace JT808.Protocol
{
    public struct JT808HeaderMessageBodyProperty
    {
        public JT808HeaderMessageBodyProperty(int dataLength,bool isPackage, JT808EncryptMethod jT808EncryptMethod= JT808EncryptMethod.None)
        {
            IsPackage = isPackage;
            Encrypt = jT808EncryptMethod;
            DataLength = dataLength;
        }
        public JT808HeaderMessageBodyProperty(ushort value)
        {
            IsPackage = (value >> 13) == 1;
            switch ((value & 0x400) >> 10)
            {
                case 0:
                    Encrypt = JT808EncryptMethod.None;
                    break;
                case 1:
                    Encrypt = JT808EncryptMethod.RSA;
                    break;
                default:
                    Encrypt = JT808EncryptMethod.None;
                    break;
            }
            DataLength = value & 0x3FF;
        }
        /// <summary>
        /// 是否分包
        ///  true-1  表示消息体为长消息，进行分包发送处理
        ///  false-0 消息头中无消息包封装项字段。
        /// </summary>
        public bool IsPackage { get; set; }
        /// <summary>
        /// 加密标识，0为不加密
        /// 当此三位都为 0，表示消息体不加密；
        /// 当第 10 位为 1，表示消息体经过 RSA 算法加密；
        /// todo:没有涉及到加密先不考虑
        /// </summary>
        public JT808EncryptMethod Encrypt { get; set; }
        /// <summary>
        /// 消息体长度
        /// </summary>
        public int DataLength { get; set; }
        public ushort Wrap()
        {
            //  1.是否分包
            int tmpIsPacke = 0;
            if (IsPackage)
            {
                tmpIsPacke = 1 << 13;
            }
            //  2.是否加密
            int tmpEncrypt;
            //  2.3.数据加密方式
            switch (Encrypt)
            {
                case JT808EncryptMethod.None:
                    tmpEncrypt = 0;
                    break;
                case JT808EncryptMethod.RSA:
                    tmpEncrypt = 1 << 10;
                    break;
                default:
                    tmpEncrypt = 0;
                    break;
            }
            //  2.4.数据长度
            if (DataLength <= 0)
            {
                // 判断有无数据体长度
                DataLength = 0;
            }       
            return (ushort)(tmpIsPacke | tmpEncrypt | DataLength);
        }
    }
}
