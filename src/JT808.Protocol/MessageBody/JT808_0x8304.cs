using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 信息服务
    /// 0x8304
    /// </summary>
    [Obsolete("2019版本已作删除")]
    public class JT808_0x8304 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x8304>, IJT808Analyze, IJT808_2019_Version
    {
        public override ushort MsgId { get; } = 0x8304;
        public override string Description => "信息服务";
        /// <summary>
        /// 信息类型
        /// </summary>
        public byte InformationType { get; set; }
        /// <summary>
        /// 信息长度
        /// </summary>
        public ushort InformationLength { get; set; }
        /// <summary>
        /// 信息内容
        /// 经 GBK 编码
        /// </summary>
        public string InformationContent { get; set; }
        public JT808_0x8304 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8304 jT808_0X8304 = new JT808_0x8304();
            jT808_0X8304.InformationType = reader.ReadByte();
            jT808_0X8304.InformationLength = reader.ReadUInt16();
            jT808_0X8304.InformationContent = reader.ReadString(jT808_0X8304.InformationLength);
            return jT808_0X8304;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8304 value, IJT808Config config)
        {
            writer.WriteByte(value.InformationType);
            // 先计算内容长度（汉字为两个字节）
            writer.Skip(2, out int position);
            writer.WriteString(value.InformationContent);
            ushort length = (ushort)(writer.GetCurrentPosition() - position - 2);
            writer.WriteUInt16Return(length, position);
        }
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8304 value = new JT808_0x8304();
            value.InformationType = reader.ReadByte();
            writer.WriteNumber($"[{value.InformationType.ReadNumber()}]信息类型", value.InformationType);
            value.InformationLength = reader.ReadUInt16();
            writer.WriteNumber($"[{value.InformationLength.ReadNumber()}]信息长度", value.InformationLength);
            var infoBuffer = reader.ReadVirtualArray(value.InformationLength).ToArray();
            value.InformationContent = reader.ReadString(value.InformationLength);
            writer.WriteString($"[{infoBuffer.ToHexString()}]信息内容", value.InformationContent);
        }
    }
}
