using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 服务器 UDP 端口
    /// </summary>
    public class JT808_0x8103_0x0019 : JT808MessagePackFormatter<JT808_0x8103_0x0019>, JT808_0x8103_BodyBase, IJT808_2019_Version, IJT808Analyze
    {
        /// <summary>
        /// 0x0019
        /// </summary>
        public uint ParamId { get; set; } = 0x0019;
        /// <summary>
        /// 数据长度
        /// n byte
        /// </summary>
        public byte ParamLength { get; set; } = 4;
        /// <summary>
        ///服务器 UDP 端口
        /// </summary>
        public uint ParamValue { get; set; }
        /// <summary>
        /// 服务器UDP端口
        /// </summary>
        public  string Description => "服务器UDP端口";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8103_0x0019 value = new JT808_0x8103_0x0019();
            value.ParamId = reader.ReadUInt32();
            value.ParamLength = reader.ReadByte();
            value.ParamValue = reader.ReadUInt32();
            writer.WriteNumber($"[{ value.ParamId.ReadNumber()}]参数ID", value.ParamId);
            writer.WriteNumber($"[{value.ParamLength.ReadNumber()}]参数长度", value.ParamLength);
            writer.WriteNumber($"[{ value.ParamValue.ReadNumber()}]参数值[服务器UDP端口]", value.ParamValue);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8103_0x0019 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x0019 value = new JT808_0x8103_0x0019();
            value.ParamId = reader.ReadUInt32();
            value.ParamLength = reader.ReadByte();
            value.ParamValue = reader.ReadUInt32();
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x0019 value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteUInt32(value.ParamValue);
        }
    }
}
