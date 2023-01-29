using System.Text.Json;

using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 车辆所在的市域 ID
    /// </summary>
    public class JT808_0x8103_0x0082 : JT808MessagePackFormatter<JT808_0x8103_0x0082>, JT808_0x8103_BodyBase, IJT808Analyze
    {
        /// <summary>
        /// 0x0082
        /// </summary>
        public  uint ParamId { get; set; } = 0x0082;
        /// <summary>
        /// 数据长度
        /// 2 byte
        /// </summary>
        public  byte ParamLength { get; set; } = 2;
        /// <summary>
        /// 车辆所在的市域 ID
        /// </summary>
        public ushort ParamValue { get; set; }
        /// <summary>
        /// 车辆所在的市域ID
        /// </summary>
        public  string Description => "车辆所在的市域ID";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8103_0x0082 jT808_0x8103_0x0082 = new JT808_0x8103_0x0082();
            jT808_0x8103_0x0082.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x0082.ParamLength = reader.ReadByte();
            jT808_0x8103_0x0082.ParamValue = reader.ReadUInt16();
            writer.WriteNumber($"[{ jT808_0x8103_0x0082.ParamId.ReadNumber()}]参数ID", jT808_0x8103_0x0082.ParamId);
            writer.WriteNumber($"[{jT808_0x8103_0x0082.ParamLength.ReadNumber()}]参数长度", jT808_0x8103_0x0082.ParamLength);
            writer.WriteNumber($"[{ jT808_0x8103_0x0082.ParamValue.ReadNumber()}]参数值[车辆所在的市域ID]", jT808_0x8103_0x0082.ParamValue);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8103_0x0082 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x0082 jT808_0x8103_0x0082 = new JT808_0x8103_0x0082();
            jT808_0x8103_0x0082.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x0082.ParamLength = reader.ReadByte();
            jT808_0x8103_0x0082.ParamValue = reader.ReadUInt16();
            return jT808_0x8103_0x0082;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x0082 value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteUInt16(value.ParamValue);
        }
    }
}
