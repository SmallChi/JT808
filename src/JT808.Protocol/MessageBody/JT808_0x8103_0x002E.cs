using System.Text.Json;

using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 休眠时汇报距离间隔，单位为米（m），>0
    /// </summary>
    public class JT808_0x8103_0x002E : JT808MessagePackFormatter<JT808_0x8103_0x002E>, JT808_0x8103_BodyBase, IJT808Analyze
    {
        /// <summary>
        /// 0x002E
        /// </summary>
        public  uint ParamId { get; set; } = 0x002E;
        /// <summary>
        /// 数据长度
        /// 4 byte
        /// </summary>
        public  byte ParamLength { get; set; } = 4;
        /// <summary>
        /// 休眠时汇报距离间隔，单位为米（m），>0
        /// </summary>
        public uint ParamValue { get; set; }
        /// <summary>
        /// 休眠时汇报距离间隔
        /// </summary>
        public  string Description => "休眠时汇报距离间隔";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8103_0x002E jT808_0x8103_0x002E = new JT808_0x8103_0x002E();
            jT808_0x8103_0x002E.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x002E.ParamLength = reader.ReadByte();
            jT808_0x8103_0x002E.ParamValue = reader.ReadUInt32();
            writer.WriteNumber($"[{ jT808_0x8103_0x002E.ParamId.ReadNumber()}]参数ID", jT808_0x8103_0x002E.ParamId);
            writer.WriteNumber($"[{jT808_0x8103_0x002E.ParamLength.ReadNumber()}]参数长度", jT808_0x8103_0x002E.ParamLength);
            writer.WriteNumber($"[{ jT808_0x8103_0x002E.ParamValue.ReadNumber()}]参数值[休眠时汇报距离间隔m]", jT808_0x8103_0x002E.ParamValue);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8103_0x002E Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x002E jT808_0x8103_0x002E = new JT808_0x8103_0x002E();
            jT808_0x8103_0x002E.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x002E.ParamLength = reader.ReadByte();
            jT808_0x8103_0x002E.ParamValue = reader.ReadUInt32();
            return jT808_0x8103_0x002E;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x002E value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteUInt32(value.ParamValue);
        }
    }
}
