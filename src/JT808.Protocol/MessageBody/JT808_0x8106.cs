using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 查询指定终端参数
    /// 0x8106
    /// </summary>
    public class JT808_0x8106 : JT808MessagePackFormatter<JT808_0x8106>, JT808Bodies,  IJT808Analyze
    {
        /// <summary>
        /// 0x8106
        /// </summary>
        public ushort MsgId  => 0x8106;
        /// <summary>
        /// 查询指定终端参数
        /// </summary>
        public string Description => "查询指定终端参数";
        /// <summary>
        /// 参数总数
        /// 参数总数为 n
        /// </summary>
        public byte ParameterCount { get; set; }
        /// <summary>
        /// 参数 ID 列表
        /// 参数顺序排列，如“参数 ID1 参数 ID2......参数IDn”。
        /// </summary>
        public uint[] Parameters { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8106 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8106 jT808_0X8106 = new JT808_0x8106();
            jT808_0X8106.ParameterCount = reader.ReadByte();
            jT808_0X8106.Parameters = new uint[jT808_0X8106.ParameterCount];
            for (int i = 0; i < jT808_0X8106.ParameterCount; i++)
            {
                jT808_0X8106.Parameters.SetValue(reader.ReadUInt32(), i);
            }
            return jT808_0X8106;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8106 value, IJT808Config config)
        {
            writer.WriteByte(value.ParameterCount);
            for (int i = 0; i < value.ParameterCount; i++)
            {
                writer.WriteUInt32(value.Parameters[i]);
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
            JT808_0x8106 value = new JT808_0x8106();
            value.ParameterCount = reader.ReadByte();
            writer.WriteNumber($"[{ value.ParameterCount.ReadNumber()}]参数总数", value.ParameterCount);
            writer.WriteStartArray("参数ID列表");
            for (int i = 0; i < value.ParameterCount; i++)
            {
                writer.WriteStartObject();
                uint parameterId = reader.ReadUInt32();
                writer.WriteNumber($"[{parameterId.ReadNumber()}]Id{i+1}",parameterId);
                writer.WriteEndObject();
            }
            writer.WriteEndArray();
        }
    }
}
