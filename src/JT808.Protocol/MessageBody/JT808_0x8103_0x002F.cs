using System.Text.Json;

using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 紧急报警时汇报距离间隔，单位为米（m），>0
    /// </summary>
    public class JT808_0x8103_0x002F : JT808MessagePackFormatter<JT808_0x8103_0x002F>, JT808_0x8103_BodyBase,  IJT808Analyze
    {
        /// <summary>
        /// 0x002F
        /// </summary>
        public  uint ParamId { get; set; } = 0x002F;
        /// <summary>
        /// 数据长度
        /// 4 byte
        /// </summary>
        public  byte ParamLength { get; set; } = 4;
        /// <summary>
        /// 紧急报警时汇报距离间隔，单位为米（m），>0
        /// </summary>
        public uint ParamValue { get; set; }
        /// <summary>
        /// 紧急报警时汇报距离间隔
        /// </summary>
        public  string Description => "紧急报警时汇报距离间隔";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8103_0x002F jT808_0x8103_0x002F = new JT808_0x8103_0x002F();
            jT808_0x8103_0x002F.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x002F.ParamLength = reader.ReadByte();
            jT808_0x8103_0x002F.ParamValue = reader.ReadUInt32();
            writer.WriteNumber($"[{ jT808_0x8103_0x002F.ParamId.ReadNumber()}]参数ID", jT808_0x8103_0x002F.ParamId);
            writer.WriteNumber($"[{jT808_0x8103_0x002F.ParamLength.ReadNumber()}]参数长度", jT808_0x8103_0x002F.ParamLength);
            writer.WriteNumber($"[{ jT808_0x8103_0x002F.ParamValue.ReadNumber()}]参数值[紧急报警时汇报距离间隔m]", jT808_0x8103_0x002F.ParamValue);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8103_0x002F Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x002F jT808_0x8103_0x002F = new JT808_0x8103_0x002F();
            jT808_0x8103_0x002F.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x002F.ParamLength = reader.ReadByte();
            jT808_0x8103_0x002F.ParamValue = reader.ReadUInt32();
            return jT808_0x8103_0x002F;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x002F value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteUInt32(value.ParamValue);
        }
    }
}
