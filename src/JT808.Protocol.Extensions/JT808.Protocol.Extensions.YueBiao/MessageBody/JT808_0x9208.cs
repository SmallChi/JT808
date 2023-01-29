using JT808.Protocol.Extensions.YueBiao.Metadata;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Text.Json;

namespace JT808.Protocol.Extensions.YueBiao.MessageBody
{
    /// <summary>
    /// 报警附件上传指令
    /// </summary>
    public class JT808_0x9208: JT808MessagePackFormatter<JT808_0x9208>, JT808Bodies, IJT808Analyze, IJT808_2019_Version
    {
        /// <summary>
        /// Description
        /// </summary>
        public string Description => "报警附件上传指令";
        /// <summary>
        /// 服务IP地址长度
        /// </summary>
        public byte AttachmentServerIPLength { get; set; }
        /// <summary>
        /// 服务IP地址
        /// </summary>
        public string AttachmentServerIP { get; set; }
        /// <summary>
        /// TCP端口
        /// </summary>
        public ushort AttachmentServerIPTcpPort { get; set; }
        /// <summary>
        /// UDP端口
        /// </summary>
        public ushort AttachmentServerIPUdpPort { get; set; }
        /// <summary>
        /// 报警标识号
        /// </summary>
        public AlarmIdentificationProperty AlarmIdentification { get; set; }
        /// <summary>
        /// 平台给报警分配的唯一编号
        /// 32
        /// </summary>
        public string AlarmId { get; set; }
        /// <summary>
        /// 预留
        /// </summary>
        public byte[] Retain { get; set; } = new byte[16];
        /// <summary>
        /// 报警附件上传指令Id
        /// </summary>
        public ushort MsgId => 0x9208;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x9208 value = new JT808_0x9208();
            value.AttachmentServerIPLength = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachmentServerIPLength.ReadNumber()}]服务IP地址长度", value.AttachmentServerIPLength);
            string attachmentServerIPHex = reader.ReadVirtualArray(value.AttachmentServerIPLength).ToArray().ToHexString();
            value.AttachmentServerIP = reader.ReadString(value.AttachmentServerIPLength);
            writer.WriteString($"[{AttachmentServerIP}]服务IP地址", value.AttachmentServerIP);
            value.AttachmentServerIPTcpPort = reader.ReadUInt16();
            writer.WriteNumber($"[{value.AttachmentServerIPTcpPort.ReadNumber()}]TCP端口", value.AttachmentServerIPTcpPort);
            value.AttachmentServerIPUdpPort = reader.ReadUInt16();
            writer.WriteNumber($"[{value.AttachmentServerIPUdpPort.ReadNumber()}]UDP端口", value.AttachmentServerIPUdpPort);
            value.AlarmIdentification = new AlarmIdentificationProperty();
            string terminalIdHex = reader.ReadVirtualArray(30).ToArray().ToHexString();
            value.AlarmIdentification.TerminalId = reader.ReadString(30);
            value.AlarmIdentification.Time = reader.ReadDateTime_yyMMddHHmmss();
            value.AlarmIdentification.SN = reader.ReadByte();
            value.AlarmIdentification.AttachCount = reader.ReadByte();
            value.AlarmIdentification.Retain1 = reader.ReadByte();
            value.AlarmIdentification.Retain2 = reader.ReadByte();
            writer.WriteString($"[{terminalIdHex}]终端ID", value.AlarmIdentification.TerminalId);
            writer.WriteString($"[{value.AlarmIdentification.Time.ToString("yyMMddHHmmss")}]日期时间", value.AlarmIdentification.Time.ToString("yyyy-MM-dd HH:mm:ss"));
            writer.WriteNumber($"[{value.AlarmIdentification.SN.ReadNumber()}]序号", value.AlarmIdentification.SN);
            writer.WriteNumber($"[{value.AlarmIdentification.AttachCount.ReadNumber()}]附件数量", value.AlarmIdentification.AttachCount);
            writer.WriteNumber($"[{value.AlarmIdentification.Retain1.ReadNumber()}]预留1", value.AlarmIdentification.Retain1);
            writer.WriteNumber($"[{value.AlarmIdentification.Retain2.ReadNumber()}]预留2", value.AlarmIdentification.Retain2);
            string alarmIdHex = reader.ReadVirtualArray(32).ToArray().ToHexString();
            value.AlarmId = reader.ReadString(32);
            writer.WriteString($"[{alarmIdHex}]平台给报警分配的唯一编号", value.AlarmId);
            string retainHex = reader.ReadVirtualArray(16).ToArray().ToHexString();
            writer.WriteString($"预留", retainHex);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x9208 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x9208 value = new JT808_0x9208();
            value.AttachmentServerIPLength = reader.ReadByte();
            value.AttachmentServerIP = reader.ReadString(value.AttachmentServerIPLength);
            value.AttachmentServerIPTcpPort = reader.ReadUInt16();
            value.AttachmentServerIPUdpPort = reader.ReadUInt16();
            value.AlarmIdentification = new AlarmIdentificationProperty
            {
                TerminalId = reader.ReadString(30),
                Time = reader.ReadDateTime_yyMMddHHmmss(),
                SN = reader.ReadByte(),
                AttachCount = reader.ReadByte(),
                Retain1 = reader.ReadByte(),
                Retain2 = reader.ReadByte()
            };
            value.AlarmId = reader.ReadString(32);
            value.Retain = reader.ReadArray(16).ToArray();
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x9208 value, IJT808Config config)
        {
            writer.Skip(1, out int AttachmentServerIPLengthPosition);
            writer.WriteString(value.AttachmentServerIP);
            writer.WriteByteReturn((byte)(writer.GetCurrentPosition() - AttachmentServerIPLengthPosition - 1), AttachmentServerIPLengthPosition);
            writer.WriteUInt16(value.AttachmentServerIPTcpPort);
            writer.WriteUInt16(value.AttachmentServerIPUdpPort);
            if (value.AlarmIdentification == null)
            {
                throw new NullReferenceException($"{nameof(AlarmIdentificationProperty)}不为空");
            }
            writer.WriteString(value.AlarmIdentification.TerminalId.PadRight(30, '\0'));
            writer.WriteDateTime_yyMMddHHmmss(value.AlarmIdentification.Time);
            writer.WriteByte(value.AlarmIdentification.SN);
            writer.WriteByte(value.AlarmIdentification.AttachCount);
            writer.WriteByte(value.AlarmIdentification.Retain1);
            writer.WriteByte(value.AlarmIdentification.Retain2);
            writer.WriteString(value.AlarmId);
            writer.WriteArray(value.Retain);
        }
    }
}
