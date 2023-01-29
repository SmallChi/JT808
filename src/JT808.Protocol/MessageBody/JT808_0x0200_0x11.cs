using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 超速报警附加信息
    /// </summary>
    public class JT808_0x0200_0x11 : JT808MessagePackFormatter<JT808_0x0200_0x11>, JT808_0x0200_BodyBase,  IJT808Analyze
    {
        /// <summary>
        /// 超速报警附加信息
        /// 0：无特定位置；
        /// 1：圆形区域；
        /// 2：矩形区域；
        /// 3：多边形区域；
        /// 4：路段
        /// </summary>
        public JT808PositionType JT808PositionType { get; set; }

        /// <summary>
        /// 区域或路段 ID
        /// 若位置类型为 0，无该字段
        /// </summary>
        public uint AreaId { get; set; }
        /// <summary>
        /// JT808_0x0200_0x11
        /// </summary>
        public byte AttachInfoId { get; set; } = JT808Constants.JT808_0x0200_0x11;
        /// <summary>
        /// 1或5 byte
        /// </summary>
        public byte AttachInfoLength
        {
            get
            {
                if (JT808PositionType != JT808PositionType.no_specific_position)
                {
                    return 5;
                }
                else
                {
                    return 1;
                }
            }
            set { }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0200_0x11 value = new JT808_0x0200_0x11();
            value.AttachInfoId = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoId.ReadNumber()}]附加信息Id", value.AttachInfoId);
            value.AttachInfoLength = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoLength.ReadNumber()}]附加信息长度", value.AttachInfoLength);
            value.JT808PositionType = (JT808PositionType)reader.ReadByte();
            writer.WriteNumber($"[{((byte)value.JT808PositionType).ReadNumber()}]超速报警附加信息-{value.JT808PositionType.ToString()}", (byte)value.JT808PositionType);
            if (value.JT808PositionType != JT808PositionType.no_specific_position)
            {
                value.AreaId = reader.ReadUInt32();
                writer.WriteNumber($"[{value.AreaId.ReadNumber()}]区域或路段ID", value.AreaId);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x0200_0x11 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x11 value = new JT808_0x0200_0x11();
            value.AttachInfoId = reader.ReadByte();
            value.AttachInfoLength = reader.ReadByte();
            value.JT808PositionType = (JT808PositionType)reader.ReadByte();
            if (value.JT808PositionType != JT808PositionType.no_specific_position)
            {
                value.AreaId = reader.ReadUInt32();
            }
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x11 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteByte((byte)value.JT808PositionType);
            if (value.JT808PositionType != JT808PositionType.no_specific_position)
            {
                writer.WriteUInt32(value.AreaId);
            }
        }
    }
}
