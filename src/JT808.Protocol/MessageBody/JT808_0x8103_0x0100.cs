using System.Text.Json;

using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// CAN 总线通道 1 采集时间间隔(ms)，0 表示不采集
    /// </summary>
    public class JT808_0x8103_0x0100 : JT808MessagePackFormatter<JT808_0x8103_0x0100>, JT808_0x8103_BodyBase, IJT808Analyze
    {
        /// <summary>
        /// 0x0100
        /// </summary>
        public uint ParamId { get; set; } = 0x0100;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public  byte ParamLength { get; set; } = 4;
        /// <summary>
        /// CAN 总线通道 1 采集时间间隔(ms)，0 表示不采集
        /// </summary>
        public uint ParamValue { get; set; }
        /// <summary>
        /// CAN 总线通道 1 采集时间间隔
        /// </summary>
        public  string Description => "CAN总线通道1采集时间间隔";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8103_0x0100 jT808_0x8103_0x0100 = new JT808_0x8103_0x0100();
            jT808_0x8103_0x0100.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x0100.ParamLength = reader.ReadByte();
            jT808_0x8103_0x0100.ParamValue = reader.ReadUInt32();
            writer.WriteNumber($"[{ jT808_0x8103_0x0100.ParamId.ReadNumber()}]参数ID", jT808_0x8103_0x0100.ParamId);
            writer.WriteNumber($"[{jT808_0x8103_0x0100.ParamLength.ReadNumber()}]参数长度", jT808_0x8103_0x0100.ParamLength);
            writer.WriteNumber($"[{ jT808_0x8103_0x0100.ParamValue.ReadNumber()}]参数值[CAN总线通道1 ,采集时间间隔(ms)，0 表示不采集]", jT808_0x8103_0x0100.ParamValue);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8103_0x0100 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x0100 jT808_0x8103_0x0100 = new JT808_0x8103_0x0100();
            jT808_0x8103_0x0100.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x0100.ParamLength = reader.ReadByte();
            jT808_0x8103_0x0100.ParamValue = reader.ReadUInt32();
            return jT808_0x8103_0x0100;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x0100 value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteUInt32(value.ParamValue);
        }
    }
}
