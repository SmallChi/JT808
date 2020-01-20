using System.Text.Json;
using JT808.Protocol.Attributes;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 位置汇报方案，0：根据 ACC 状态； 1：根据登录状态和 ACC 状态，先判断登录状态，若登录再根据 ACC 状态
    /// </summary>
    public class JT808_0x8103_0x0021 : JT808_0x8103_BodyBase, IJT808MessagePackFormatter<JT808_0x8103_0x0021>, IJT808Analyze
    {
        public override uint ParamId { get; set; } = 0x0021;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; } = 4;
        /// <summary>
        /// 位置汇报方案，0：根据 ACC 状态； 1：根据登录状态和 ACC 状态，先判断登录状态，若登录再根据 ACC 状态
        /// </summary>
        public uint ParamValue { get; set; }

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8103_0x0021 jT808_0x8103_0x0021 = new JT808_0x8103_0x0021();
            jT808_0x8103_0x0021.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x0021.ParamLength = reader.ReadByte();
            jT808_0x8103_0x0021.ParamValue = reader.ReadUInt32();
            writer.WriteNumber($"[{ jT808_0x8103_0x0021.ParamId.ReadNumber()}]参数ID", jT808_0x8103_0x0021.ParamId);
            writer.WriteNumber($"[{jT808_0x8103_0x0021.ParamLength.ReadNumber()}]参数长度", jT808_0x8103_0x0021.ParamLength);
            writer.WriteNumber($"[{ jT808_0x8103_0x0021.ParamValue.ReadNumber()}]参数值[位置汇报方案，0：根据 ACC 状态； 1：根据登录状态和 ACC 状态]", jT808_0x8103_0x0021.ParamValue);
        }

        public JT808_0x8103_0x0021 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x0021 jT808_0x8103_0x0021 = new JT808_0x8103_0x0021();
            jT808_0x8103_0x0021.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x0021.ParamLength = reader.ReadByte();
            jT808_0x8103_0x0021.ParamValue = reader.ReadUInt32();
            return jT808_0x8103_0x0021;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x0021 value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteUInt32(value.ParamValue);
        }
    }
}
