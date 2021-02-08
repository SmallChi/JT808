
using JT808.Protocol.Enums;
using JT808.Protocol.Formatters;
using System;

namespace JT808.Protocol
{
    /// <summary>
    /// 头部消息体属性
    /// </summary>
    public class JT808HeaderMessageBodyProperty
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataLength"></param>
        /// <param name="isPackage"></param>
        /// <param name="versionFlag"></param>
        /// <param name="jT808EncryptMethod"></param>
        public JT808HeaderMessageBodyProperty(int dataLength,bool isPackage, bool versionFlag= false, JT808EncryptMethod jT808EncryptMethod= JT808EncryptMethod.None)
        {
            IsPackage = isPackage;
            Encrypt = jT808EncryptMethod;
            DataLength = dataLength;
            VersionFlag = versionFlag;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isPackage"></param>
        /// <param name="versionFlag"></param>
        /// <param name="jT808EncryptMethod"></param>
        public JT808HeaderMessageBodyProperty(bool isPackage, bool versionFlag, JT808EncryptMethod jT808EncryptMethod = JT808EncryptMethod.None)
        {
            IsPackage = isPackage;
            Encrypt = jT808EncryptMethod;
            DataLength = 0;
            VersionFlag = versionFlag;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="versionFlag"></param>
        /// <param name="jT808EncryptMethod"></param>
        public JT808HeaderMessageBodyProperty(bool versionFlag, JT808EncryptMethod jT808EncryptMethod = JT808EncryptMethod.None)
        {
            IsPackage = false;
            Encrypt = jT808EncryptMethod;
            DataLength = 0;
            VersionFlag = versionFlag;
        }
        /// <summary>
        /// 
        /// </summary>
        public JT808HeaderMessageBodyProperty(){}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public JT808HeaderMessageBodyProperty(ushort value)
        {
            VersionFlag = (value >> 14 & 0x01) == 1;
            IsPackage = ((value >> 13) & 0x001) == 1;
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
        /// 版本标识（默认为1=true）
        /// </summary>
        public bool VersionFlag { get; set; } = false;
        /// <summary>
        /// 是否分包
        ///  true-1  表示消息体为长消息，进行分包发送处理
        ///  false-0 消息头中无消息包封装项字段。
        /// </summary>
        public bool IsPackage { get; set; } = false;
        /// <summary>
        /// 加密标识，0为不加密
        /// 当此三位都为 0，表示消息体不加密；
        /// 当第 10 位为 1，表示消息体经过 RSA 算法加密；
        /// todo:没有涉及到加密先不考虑
        /// </summary>
        public JT808EncryptMethod Encrypt { get; set; } = JT808EncryptMethod.None;
        /// <summary>
        /// 消息体长度
        /// </summary>
        public int DataLength { get; set; } = 0;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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
            //  3.版本标识
            int versionFlag = 0;
            if (VersionFlag)
            {
                versionFlag = 1 << 14;
            }
            return (ushort)(versionFlag|tmpIsPacke | tmpEncrypt | DataLength);
        }
    }
}
