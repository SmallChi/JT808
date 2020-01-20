using System.Text.Json;
using JT808.Protocol.Attributes;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 定时拍照控制，见 表 14
    /// </summary>
    public class JT808_0x8103_0x0064 : JT808_0x8103_BodyBase, IJT808MessagePackFormatter<JT808_0x8103_0x0064>, IJT808Analyze
    {
        public override uint ParamId { get; set; } = 0x0064;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; } = 4;
        /// <summary>
        /// 定时拍照控制，见 表 14
        /// </summary>
        public uint ParamValue { get; set; }

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8103_0x0064 jT808_0x8103_0x0064 = new JT808_0x8103_0x0064();
            jT808_0x8103_0x0064.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x0064.ParamLength = reader.ReadByte();
            jT808_0x8103_0x0064.ParamValue = reader.ReadUInt32();
            writer.WriteNumber($"[{ jT808_0x8103_0x0064.ParamId.ReadNumber()}]参数ID", jT808_0x8103_0x0064.ParamId);
            writer.WriteNumber($"[{jT808_0x8103_0x0064.ParamLength.ReadNumber()}]参数长度", jT808_0x8103_0x0064.ParamLength);
            writer.WriteNumber($"[{ jT808_0x8103_0x0064.ParamValue.ReadNumber()}]参数值[定时拍照控制,见表14]", jT808_0x8103_0x0064.ParamValue);
        }

        public JT808_0x8103_0x0064 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x0064 jT808_0x8103_0x0064 = new JT808_0x8103_0x0064();
            jT808_0x8103_0x0064.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x0064.ParamLength = reader.ReadByte();
            jT808_0x8103_0x0064.ParamValue = reader.ReadUInt32();
            return jT808_0x8103_0x0064;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x0064 value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteUInt32(value.ParamValue);
        }
    }
}
