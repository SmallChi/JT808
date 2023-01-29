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
    public class JT808_0x8800 : JT808MessagePackFormatter<JT808_0x8800>, JT808Bodies,  IJT808Analyze
    {
        /// <summary>
        /// 0x8800
        /// </summary>
        public ushort MsgId => 0x8800;
        /// <summary>
        /// 多媒体数据上传应答
        /// </summary>
        public string Description => "多媒体数据上传应答";
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8800 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8800 jT808_0X8800 = new JT808_0x8800();
            jT808_0X8800.MultimediaId = reader.ReadUInt32();
            if (reader.ReadCurrentRemainContentLength() > 0)
            {
                jT808_0X8800.RetransmitPackageCount = reader.ReadByte();//2011协议此处 为0
                if (jT808_0X8800.RetransmitPackageCount > 0) {
                    jT808_0X8800.RetransmitPackageIds = reader.ReadArray(jT808_0X8800.RetransmitPackageCount * 2).ToArray();
                } 
            }
            return jT808_0X8800;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8800 value, IJT808Config config)
        {
            writer.WriteUInt32(value.MultimediaId);
            if (writer.Version == Enums.JT808Version.JTT2011)
            {
                writer.WriteByte((byte)(value.RetransmitPackageIds.Length / 2));
                if (value.RetransmitPackageIds.Length > 0) {
                    writer.WriteArray(value.RetransmitPackageIds);
                }
            }
            else {
                if (value.RetransmitPackageIds.Length > 0) {
                    writer.WriteByte((byte)(value.RetransmitPackageIds.Length / 2));
                    writer.WriteArray(value.RetransmitPackageIds);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8800 value = new JT808_0x8800();
            value.MultimediaId = reader.ReadUInt32();
            writer.WriteNumber($"[{ value.MultimediaId.ReadNumber()}]多媒体ID", value.MultimediaId);
            if (reader.ReadCurrentRemainContentLength() > 0) {
                value.RetransmitPackageCount = reader.ReadByte();
                writer.WriteNumber($"[{ value.RetransmitPackageCount.ReadNumber()}]重传包总数", value.RetransmitPackageCount);
                if (value.RetransmitPackageCount > 0) {
                    writer.WriteString($"重传包", value.RetransmitPackageIds.ToHexString());
                    writer.WriteStartArray($"重传包ID列表");
                    value.RetransmitPackageIds = reader.ReadArray(value.RetransmitPackageCount * 2).ToArray();
                    ReadOnlySpan<byte> tmp = value.RetransmitPackageIds;
                    for (int i = 0; i < value.RetransmitPackageCount; i++)
                    {
                        writer.WriteStringValue($"{tmp.Slice(i * 2, 2).ToArray().ToHexString()}");
                    }
                    writer.WriteEndArray();
                }
            }
        }
    }
}
