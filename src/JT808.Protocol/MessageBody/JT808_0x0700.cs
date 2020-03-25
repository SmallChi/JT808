using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 行驶记录数据上传
    /// 0x0700
    /// </summary>
    public class JT808_0x0700 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x0700>, IJT808Analyze
    {
        public override ushort MsgId { get; } = 0x0700;
        public override string Description => "行驶记录数据上传";
        /// <summary>
        /// 应答流水号
        /// </summary>
        public ushort ReplyMsgNum { get; set; }
        /// <summary>
        /// 命令字
        /// </summary>
        public byte CommandId { get; set; }

        public JT808CarDVRUpPackage JT808CarDVRUpPackage { get; set; }

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0700 value = new JT808_0x0700();
            writer.WriteStartObject("行驶记录参数下传命令");
            value.ReplyMsgNum = reader.ReadUInt16();
            writer.WriteNumber($"[{value.ReplyMsgNum.ReadNumber()}]应答流水号", value.ReplyMsgNum);
            value.CommandId = reader.ReadByte();
            writer.WriteString($"[{value.CommandId.ReadNumber()}]命令字", ((JT808CarDVRCommandID)value.CommandId).ToString());
            writer.WriteStartObject(((JT808CarDVRCommandID)value.CommandId).ToString());
            JT808CarDVRSerializer.JT808CarDVRUpPackage.Analyze(ref reader, writer, config);
            writer.WriteEndObject();
            writer.WriteEndObject();
        }

        public JT808_0x0700 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0700 value = new JT808_0x0700();
            value.ReplyMsgNum = reader.ReadUInt16();
            value.CommandId = reader.ReadByte();
            value.JT808CarDVRUpPackage = JT808CarDVRSerializer.JT808CarDVRUpPackage.Deserialize(ref reader, config);
            return value;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0700 value, IJT808Config config)
        {
            writer.WriteUInt16(value.ReplyMsgNum);
            writer.WriteByte(value.CommandId);
            JT808CarDVRSerializer.JT808CarDVRUpPackage.Serialize(ref writer, value.JT808CarDVRUpPackage, config);
        }
    }
}
