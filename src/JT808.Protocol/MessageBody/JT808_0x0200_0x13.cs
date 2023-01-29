using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 路段行驶时间不足/过长报警附加信息
    /// </summary>
    public class JT808_0x0200_0x13 : JT808MessagePackFormatter<JT808_0x0200_0x13>, JT808_0x0200_BodyBase, IJT808Analyze
    {
        /// <summary>
        /// JT808_0x0200_0x13
        /// </summary>
        public byte AttachInfoId { get; set; } = JT808Constants.JT808_0x0200_0x13;
        /// <summary>
        /// 7 byte
        /// </summary>
        public byte AttachInfoLength { get; set; } = 7;

        /// <summary>
        /// 路段 ID
        /// </summary>
        public int DrivenRouteId { get; set; }

        /// <summary>
        /// 路段行驶时间
        /// 单位为秒（s)
        /// </summary>
        public ushort Time { get; set; }

        /// <summary>
        ///  结果 0：不足；1：过长
        /// </summary>
        public JT808DrivenRouteType DrivenRoute { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0200_0x13 value = new JT808_0x0200_0x13();
            value.AttachInfoId = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoId.ReadNumber()}]附加信息Id", value.AttachInfoId);
            value.AttachInfoLength = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoLength.ReadNumber()}]附加信息长度", value.AttachInfoLength);
            value.DrivenRouteId = reader.ReadInt32();
            writer.WriteNumber($"[{((byte)value.DrivenRouteId).ReadNumber()}]路段ID", value.DrivenRouteId);
            value.Time = reader.ReadUInt16();
            writer.WriteNumber($"[{value.Time.ReadNumber()}]路段行驶时间", value.Time);
            value.DrivenRoute = (JT808DrivenRouteType)reader.ReadByte();
            writer.WriteNumber($"[{((byte)value.DrivenRoute).ReadNumber()}]结果-{value.DrivenRoute.ToString()}", (byte)value.DrivenRoute);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x0200_0x13 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x13 value = new JT808_0x0200_0x13();
            value.AttachInfoId = reader.ReadByte();
            value.AttachInfoLength = reader.ReadByte();
            value.DrivenRouteId = reader.ReadInt32();
            value.Time = reader.ReadUInt16();
            value.DrivenRoute = (JT808DrivenRouteType)reader.ReadByte();
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x13 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteInt32(value.DrivenRouteId);
            writer.WriteUInt16(value.Time);
            writer.WriteByte((byte)value.DrivenRoute);
        }
    }
}
