using System;
using System.Text.Json;
using JT808.Protocol.Attributes;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// GNSS 波特率，定义如下：
    /// 0x00：4800；0x01：9600；
    /// 0x02：19200；0x03：38400；
    /// 0x04：57600；0x05：115200。
    /// </summary>
    public class JT808_0x8103_0x0091 : JT808_0x8103_BodyBase, IJT808MessagePackFormatter<JT808_0x8103_0x0091>, IJT808Analyze
    {
        public override uint ParamId { get; set; } = 0x0091;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; }
        /// <summary>
        /// GNSS 波特率，定义如下：
        /// 0x00：4800；0x01：9600；
        /// 0x02：19200；0x03：38400；
        /// 0x04：57600；0x05：115200。
        /// </summary>
        public byte ParamValue { get; set; }

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8103_0x0091 jT808_0x8103_0x0091 = new JT808_0x8103_0x0091();
            jT808_0x8103_0x0091.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x0091.ParamLength = reader.ReadByte();
            jT808_0x8103_0x0091.ParamValue = reader.ReadByte();
            writer.WriteNumber($"[{ jT808_0x8103_0x0091.ParamId.ReadNumber()}]参数ID", jT808_0x8103_0x0091.ParamId);
            writer.WriteNumber($"[{jT808_0x8103_0x0091.ParamLength.ReadNumber()}]参数长度", jT808_0x8103_0x0091.ParamLength);
            writer.WriteNumber($"[{ jT808_0x8103_0x0091.ParamValue.ReadNumber()}]参数值[GNSS波特率]",Math.Pow(4800, jT808_0x8103_0x0091.ParamValue));
        }

        public JT808_0x8103_0x0091 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x0091 jT808_0x8103_0x0091 = new JT808_0x8103_0x0091();
            jT808_0x8103_0x0091.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x0091.ParamLength = reader.ReadByte();
            jT808_0x8103_0x0091.ParamValue = reader.ReadByte();
            return jT808_0x8103_0x0091;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x0091 value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteByte(value.ParamValue);
        }
    }
}
