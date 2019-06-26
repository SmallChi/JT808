using JT808.Protocol.Attributes;
using JT808.Protocol.Enums;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using System;

namespace JT808.Protocol
{
    [JT808Formatter(typeof(JT808HeaderMessageBodyPropertyFormatter))]
    public class JT808HeaderMessageBodyProperty
    {
        private const string encrypt_none = "000";
        private const string encrypt_rsa = "001";
        private const char char_zero = '0';
        private const char char_one = '1';
        public JT808HeaderMessageBodyProperty()
        {
            IsPackge = false;
            Encrypt = JT808EncryptMethod.None;
            PackgeCount = 0;
            PackageIndex = 0;
        }
        /// <summary>
        /// 是否分包
        ///  true-1  表示消息体为长消息，进行分包发送处理
        ///  false-0 消息头中无消息包封装项字段。
        /// </summary>
        public bool IsPackge { get; set; }
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
        /// <summary>
        /// 消息总包数
        /// </summary>
        public ushort PackgeCount { get; set; }
        /// <summary>
        /// 报序号 从1开始
        /// </summary>
        public ushort PackageIndex { get; set; }
        public ushort Wrap(IJT808Config jT808Config)
        {
            // 2.消息体属性
            Span<char> msgMethod = new char[16];
            //  2.1.保留
            msgMethod[0] = char_zero;
            msgMethod[1] = char_zero;
            //  2.2.是否分包
            msgMethod[2] = IsPackge ? char_one : char_zero;
            //  2.3.数据加密方式
            switch (Encrypt)
            {
                case JT808EncryptMethod.None:
                    msgMethod[3] = char_zero;
                    msgMethod[4] = char_zero;
                    msgMethod[5] = char_zero;
                    break;
                case JT808EncryptMethod.RSA:
                    msgMethod[3] = char_zero;
                    msgMethod[4] = char_zero;
                    msgMethod[5] = char_one;
                    break;
                default:
                    msgMethod[3] = char_zero;
                    msgMethod[4] = char_zero;
                    msgMethod[5] = char_zero;
                    break;
            }
            //  2.4.数据长度
            if (DataLength <= 0)
            {
                // 判断有无数据体长度
                DataLength = 0;
            }
            ReadOnlySpan<char> dataLen = Convert.ToString(DataLength, 2).PadLeft(10, char_zero).AsSpan();
            for (int i = 1; i <= 10; i++)
            {
                msgMethod[5 + i] = dataLen[i - 1];
            }
            return Convert.ToUInt16(msgMethod.ToString(), 2);
        }
        public void Unwrap(ushort value, IJT808Config jT808Config)
        {
            ReadOnlySpan<char> msgMethod = Convert.ToString(value, 2).PadLeft(16, '0').AsSpan();
            DataLength = Convert.ToInt32(msgMethod.Slice(6, 10).ToString(), 2);
            //  2.2. 数据加密方式
            switch (msgMethod.Slice(3, 3).ToString())
            {
                case encrypt_none:
                    Encrypt = JT808EncryptMethod.None;
                    break;
                case encrypt_rsa:
                    Encrypt = JT808EncryptMethod.RSA;
                    break;
                default:
                    Encrypt = JT808EncryptMethod.None;
                    break;
            }
            IsPackge = msgMethod[2] != char_zero;
            PackgeCount = 0;
            PackageIndex = 0;
        }
    }
}
