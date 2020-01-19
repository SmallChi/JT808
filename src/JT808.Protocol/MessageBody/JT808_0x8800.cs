using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 多媒体数据上传应答
    /// 0x8800
    /// </summary>
    public class JT808_0x8800 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x8800>, IJT808Analyze
    {
        public override ushort MsgId { get; } = 0x8800;
        public override string Description => "多媒体数据上传应答";
        /// <summary>
        /// 多媒体ID
        /// </summary>
        public uint MultimediaId { get; set; }
        /// <summary>
        /// 重传包总数
        /// </summary>
        public byte RetransmitPackageCount { get; set; }
        /// <summary>
        /// 重传包 ID 列表
        /// 重传包序号顺序排列，如“包 ID1 包 ID2......包 IDn”。
        /// </summary>
        public byte[] RetransmitPackageIds { get; set; }

        public JT808_0x8800 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8800 jT808_0X8800 = new JT808_0x8800();
            jT808_0X8800.MultimediaId = reader.ReadUInt32();
            jT808_0X8800.RetransmitPackageCount = reader.ReadByte();
            jT808_0X8800.RetransmitPackageIds = reader.ReadArray(jT808_0X8800.RetransmitPackageCount * 2).ToArray();
            return jT808_0X8800;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8800 value, IJT808Config config)
        {
            writer.WriteUInt32(value.MultimediaId);
            writer.WriteByte((byte)(value.RetransmitPackageIds.Length / 2));
            writer.WriteArray(value.RetransmitPackageIds);
        }

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8800 value = new JT808_0x8800();
            value.MultimediaId = reader.ReadUInt32();
            value.RetransmitPackageCount = reader.ReadByte();
            value.RetransmitPackageIds = reader.ReadArray(value.RetransmitPackageCount * 2).ToArray();

            writer.WriteNumber($"[{ value.MultimediaId.ReadNumber()}]多媒体ID", value.MultimediaId);
            writer.WriteNumber($"[{ value.RetransmitPackageCount.ReadNumber()}]重传包总数", value.RetransmitPackageCount);
            writer.WriteString($"重传包", value.RetransmitPackageIds.ToHexString());
            writer.WriteStartArray($"重传包ID列表");
            ReadOnlySpan<byte> tmp = value.RetransmitPackageIds;
            for(int i=0; i< value.RetransmitPackageCount; i++)
            {
                writer.WriteStringValue($"{tmp.Slice(i*2 , 2).ToArray().ToHexString()}");
            }
            writer.WriteEndArray();
        }
    }
}
