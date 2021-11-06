using JT808.Protocol.Enums;
using JT808.Protocol.Exceptions;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol
{
    /// <summary>
    /// JT808头部数据包
    /// </summary>
    public record  JT808HeaderPackage
    {
        /// <summary>
        /// 起始符
        /// </summary>
        public byte Begin { get; set; }
        /// <summary>
        /// 头数据
        /// </summary>
        public JT808Header Header { get;  set; }
        /// <summary>
        /// 数据体
        /// </summary>
        public byte[] Bodies { get; set; }
        /// <summary>
        /// 校验码
        /// 从消息头开始，同后一字节异或，直到校验码前一个字节，占用一个字节。
        /// </summary>
        public byte CheckCode { get; set; }
        /// <summary>
        /// 终止符
        /// </summary>
        public byte End { get; set; }
        /// <summary>
        /// 808版本号
        /// </summary>
        public JT808Version Version { get; set; }
        /// <summary>
        /// 原数据
        /// </summary>
        public byte[] OriginalData { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        public  JT808HeaderPackage(ref JT808MessagePackReader reader, IJT808Config config)
        {
            // 1. 验证校验和
            if (!config.SkipCRCCode)
            {
                if (!reader.CheckXorCodeVali)
                {
                    throw new JT808Exception(JT808ErrorCode.CheckCodeNotEqual, $"{reader.RealCheckXorCode}!={reader.CalculateCheckXorCode}");
                }
            }
            // ---------------开始解包--------------
            // 2.读取起始位置        
            this.Begin = reader.ReadStart();
            // 3.读取头部信息
            this.Header = new JT808Header();
            //  3.1.读取消息Id
            this.Header.MsgId = reader.ReadUInt16();
            //  3.2.读取消息体属性
            ushort messageBodyPropertyValue = reader.ReadUInt16();
            //    3.2.1.解包消息体属性
            this.Header.MessageBodyProperty = new JT808HeaderMessageBodyProperty(messageBodyPropertyValue);
            if (reader.Version == JT808Version.JTT2013Force)
            {
                this.Header.TerminalPhoneNo = reader.ReadBCD(config.TerminalPhoneNoLength, config.Trim);
                reader.Version = JT808Version.JTT2013;
            }
            else
            {
                if (reader.Version == JT808Version.JTT2019 || this.Header.MessageBodyProperty.VersionFlag)
                {
                    //2019版本
                    //  3.3.读取协议版本号 
                    this.Header.ProtocolVersion = reader.ReadByte();
                    //  3.4.读取终端手机号 
                    this.Header.TerminalPhoneNo = reader.ReadBCD(20, config.Trim);
                    reader.Version = JT808Version.JTT2019;
                }
                else
                {
                    //2013版本
                    //  3.3.读取终端手机号 
                    this.Header.TerminalPhoneNo = reader.ReadBCD(config.TerminalPhoneNoLength, config.Trim);
                }
            }
            this.Version = reader.Version;
            // 3.4.读取消息流水号
            this.Header.MsgNum = reader.ReadUInt16();
            // 3.5.判断有无分包
            if (this.Header.MessageBodyProperty.IsPackage)
            {
                //3.5.1.读取消息包总数
                this.Header.PackgeCount = reader.ReadUInt16();
                //3.5.2.读取消息包序号
                this.Header.PackageIndex = reader.ReadUInt16();
            }
            // 4.处理数据体
            //  4.1.判断有无数据体
            if (this.Header.MessageBodyProperty.DataLength > 0)
            {
                this.Bodies = reader.ReadContent().ToArray();
            }
            else
            {
                this.Bodies = default;
            }
            // 5.读取校验码
            this.CheckCode = reader.ReadByte();
            // 6.读取终止位置
            this.End = reader.ReadEnd();
            // ---------------解包完成--------------
            this.OriginalData = reader.SrcBuffer.ToArray();
        }
    }
}
