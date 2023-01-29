using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol
{
    /// <summary>
    /// 头部
    /// </summary>
    public class JT808Header : JT808MessagePackFormatter<JT808Header>
    {
        /// <summary>
        /// 消息ID 
        /// <see cref="JT808.Protocol.Enums.JT808MsgId"/>
        /// </summary>
        public ushort MsgId { get; set; }
        /// <summary>
        /// 消息体属性
        /// </summary>
        public JT808HeaderMessageBodyProperty MessageBodyProperty { get; set; } = new JT808HeaderMessageBodyProperty();
        /// <summary>
        /// 协议版本号(2019版本)
        /// </summary>
        public byte ProtocolVersion { get; set; } = 1;
        /// <summary>
        /// 终端手机号
        /// 根据安装后终端自身的手机号转换。手机号不足 12 位，则在前补充数字，大陆手机号补充数字 0，港澳台则根据其区号进行位数补充
        /// (2019版本)手机号不足 20 位，则在前补充数字 0
        /// </summary>
        public string TerminalPhoneNo { get;  set; }
        /// <summary>
        /// 消息流水号
        /// 发送计数器
        /// 占用两个字节，为发送信息的序列号，用于接收方检测是否有信息的丢失，上级平台和下级平台接自己发送数据包的个数计数，互不影响。
        /// 程序开始运行时等于零，发送第一帧数据时开始计数，到最大数后自动归零
        /// </summary>
        public ushort MsgNum { get;  set; }
        /// <summary>
        /// 手动消息流水号（only test）
        /// 发送计数器
        /// 占用两个字节，为发送信息的序列号，用于接收方检测是否有信息的丢失，上级平台和下级平台接自己发送数据包的个数计数，互不影响。
        /// 程序开始运行时等于零，发送第一帧数据时开始计数，到最大数后自动归零
        /// </summary>
        public ushort? ManualMsgNum { get; set; }
        /// <summary>
        /// 消息总包数
        /// </summary>
        public ushort PackgeCount { get; set; }
        /// <summary>
        /// 报序号 从1开始
        /// </summary>
        public ushort PackageIndex { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808Header Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808Header jT808Header = new JT808Header();
            // 1.消息ID
            jT808Header.MsgId = reader.ReadUInt16();
            // 2.消息体属性
            jT808Header.MessageBodyProperty = new JT808HeaderMessageBodyProperty(reader.ReadUInt16());
            if (jT808Header.MessageBodyProperty.VersionFlag)
            {            
                // 2019 版本
                // 3.协议版本号
                jT808Header.ProtocolVersion = reader.ReadByte();
                // 4.终端手机号
                jT808Header.TerminalPhoneNo = reader.ReadBCD(20, config.Trim);
            }
            else
            {
                // 2013 版本
                // 3.终端手机号
                jT808Header.TerminalPhoneNo = reader.ReadBCD(config.TerminalPhoneNoLength, config.Trim);
            }
            jT808Header.MsgNum = reader.ReadUInt16();
            // 4.判断有无分包
            if (jT808Header.MessageBodyProperty.IsPackage)
            {
                //5.读取消息包总数
                jT808Header.PackgeCount = reader.ReadUInt16();
                //6.读取消息包序号
                jT808Header.PackageIndex = reader.ReadUInt16();
            }
            return jT808Header;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808Header value, IJT808Config config)
        {
            // 1.消息ID
            writer.WriteUInt16(value.MsgId);
            // 2.消息体属性
            writer.WriteUInt16(value.MessageBodyProperty.Wrap());
            if (value.MessageBodyProperty.VersionFlag)
            {
                // 2019 版本
                // 3.协议版本号
                writer.WriteByte(value.ProtocolVersion);
                // 4.终端手机号
                writer.WriteBCD(value.TerminalPhoneNo, 20);
            }
            else
            {
                // 2013 版本
                // 3.终端手机号
                writer.WriteBCD(value.TerminalPhoneNo, config.TerminalPhoneNoLength);
            }
            // 4.消息流水号
            writer.WriteUInt16(value.MsgNum);
            // 5.判断是否分包
            if (value.MessageBodyProperty.IsPackage)
            {
                // 6.消息包总数
                writer.WriteUInt16(value.PackgeCount);
                // 7.消息包序号
                writer.WriteUInt16(value.PackageIndex);
            }
        }
    }
}
