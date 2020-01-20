using System;
using System.Buffers.Binary;
using System.Text.Json;
using JT808.Protocol.Attributes;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// CAN 总线 ID 单独采集设置：
    /// bit63-bit32 表示此 ID 采集时间间隔(ms)，0 表示不采集；
    /// bit31 表示 CAN 通道号，0：CAN1，1：CAN2；
    /// bit30 表示帧类型，0：标准帧，1：扩展帧；
    /// bit29 表示数据采集方式，0：原始数据，1：采集区间的计算值；
    /// bit28-bit0 表示 CAN 总线 ID。
    /// </summary>
    public class JT808_0x8103_0x0110 : JT808_0x8103_BodyBase, IJT808MessagePackFormatter<JT808_0x8103_0x0110>, IJT808Analyze
    {
        public override uint ParamId { get; set; } = 0x0110;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; }
        /// <summary>
        /// CAN 总线 ID 单独采集设置：
        /// bit63-bit32 表示此 ID 采集时间间隔(ms)，0 表示不采集；
        /// bit31 表示 CAN 通道号，0：CAN1，1：CAN2；
        /// bit30 表示帧类型，0：标准帧，1：扩展帧；
        /// bit29 表示数据采集方式，0：原始数据，1：采集区间的计算值；
        /// bit28-bit0 表示 CAN 总线 ID。
        /// </summary>
        public byte[] ParamValue { get; set; }

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8103_0x0110 jT808_0x8103_0x0110 = new JT808_0x8103_0x0110();
            jT808_0x8103_0x0110.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x0110.ParamLength = reader.ReadByte();
            jT808_0x8103_0x0110.ParamValue = reader.ReadArray(jT808_0x8103_0x0110.ParamLength).ToArray();
            writer.WriteNumber($"[ { jT808_0x8103_0x0110.ParamId.ReadNumber()}]参数ID", jT808_0x8103_0x0110.ParamId);
            writer.WriteNumber($"[{jT808_0x8103_0x0110.ParamLength.ReadNumber()}]参数长度", jT808_0x8103_0x0110.ParamLength);
            writer.WriteStartArray($"[{ jT808_0x8103_0x0110.ParamValue.ToHexString()}]参数值[CAN总线ID单独采集设置]");
            writer.WriteNumber("此 ID 采集时间间隔(ms)", BinaryPrimitives.ReadUInt32BigEndian( jT808_0x8103_0x0110.ParamValue.AsSpan().Slice(0,4)));
            writer.WriteString("CAN通道号", (jT808_0x8103_0x0110.ParamValue.AsSpan().Slice(4, 1).ToArray()[0]&0x01)==0? "CAN1" : "CAN2");
            writer.WriteString("帧类型", (jT808_0x8103_0x0110.ParamValue.AsSpan().Slice(4, 1).ToArray()[0] & 0x02) == 0 ? "标准帧" : "扩展帧");
            writer.WriteString("数据采集方式", (jT808_0x8103_0x0110.ParamValue.AsSpan().Slice(4, 1).ToArray()[0] & 0x04) == 0 ? "原始数据" : "采集区间的计算值");
            writer.WriteNumber("CAN 总线 ID", BinaryPrimitives.ReadUInt32BigEndian(jT808_0x8103_0x0110.ParamValue.AsSpan().Slice(4, 4)) & 0x01FFFFFF);
            writer.WriteEndArray();
        }

        public JT808_0x8103_0x0110 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x0110 jT808_0x8103_0x0110 = new JT808_0x8103_0x0110();
            jT808_0x8103_0x0110.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x0110.ParamLength = reader.ReadByte();
            jT808_0x8103_0x0110.ParamValue = reader.ReadArray(jT808_0x8103_0x0110.ParamLength).ToArray();
            return jT808_0x8103_0x0110;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x0110 value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte((byte)value.ParamValue.Length);
            writer.WriteArray(value.ParamValue);
        }
    }
}
