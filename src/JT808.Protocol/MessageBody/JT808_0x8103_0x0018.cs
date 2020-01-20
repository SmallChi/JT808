using JT808.Protocol.Attributes;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 服务器 TCP 端口
    /// </summary>
    [Obsolete("2019版本已作为保留")]
    public class JT808_0x8103_0x0018 : JT808_0x8103_BodyBase, IJT808MessagePackFormatter<JT808_0x8103_0x0018>, IJT808_2019_Version, IJT808Analyze
    {
        public override uint ParamId { get; set; } = 0x0018;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; } = 4;
        /// <summary>
        ///服务器 TCP 端口
        /// </summary>
        public uint ParamValue { get; set; }

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8103_0x0018 value = new JT808_0x8103_0x0018();
            value.ParamId = reader.ReadUInt32();
            value.ParamLength = reader.ReadByte();
            value.ParamValue = reader.ReadUInt32();
            writer.WriteNumber($"[{ value.ParamId.ReadNumber()}]参数ID", value.ParamId);
            writer.WriteNumber($"[{value.ParamLength.ReadNumber()}]参数长度", value.ParamLength);
            writer.WriteNumber($"[{ value.ParamValue.ReadNumber()}]参数值[服务器TCP端口]", value.ParamValue);
        }

        public JT808_0x8103_0x0018 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x0018 value = new JT808_0x8103_0x0018();
            value.ParamId = reader.ReadUInt32();
            value.ParamLength = reader.ReadByte();
            value.ParamValue = reader.ReadUInt32();
            return value;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x0018 value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteUInt32(value.ParamValue);
        }
    }
}
