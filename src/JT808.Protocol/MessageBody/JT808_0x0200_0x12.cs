using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    public class JT808_0x0200_0x12 : JT808_0x0200_BodyBase, IJT808MessagePackFormatter<JT808_0x0200_0x12>, IJT808Analyze
    {
        /// <summary>
        /// 位置类型
        /// 1：圆形区域；
        /// 2：矩形区域；
        /// 3：多边形区域；
        /// 4：路段
        /// </summary>
        public JT808PositionType JT808PositionType { get; set; }

        /// <summary>
        /// 区域或路段 ID
        /// </summary>
        public int AreaId { get; set; }

        /// <summary>
        /// 方向 
        /// 0：进
        /// 1：出
        /// </summary>
        public JT808DirectionType Direction { get; set; }
        public override byte AttachInfoId { get; set; } = JT808Constants.JT808_0x0200_0x12;
        public override byte AttachInfoLength { get; set; } = 6;

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0200_0x12 value = new JT808_0x0200_0x12();
            value.AttachInfoId = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoId.ReadNumber()}]附加信息Id", value.AttachInfoId);
            value.AttachInfoLength = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoLength.ReadNumber()}]附加信息长度", value.AttachInfoLength);
            value.JT808PositionType = (JT808PositionType)reader.ReadByte();
            writer.WriteNumber($"[{((byte)value.JT808PositionType).ReadNumber()}]位置类型-{value.JT808PositionType.ToString()}", (byte)value.JT808PositionType);
            value.AreaId = reader.ReadInt32();
            writer.WriteNumber($"[{value.AreaId.ReadNumber()}]区域或路段ID", value.AreaId);
            value.Direction = (JT808DirectionType)reader.ReadByte();
            writer.WriteNumber($"[{((byte)value.Direction).ReadNumber()}]方向-{value.Direction.ToString()}", (byte)value.Direction);
        }

        public JT808_0x0200_0x12 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x12 value = new JT808_0x0200_0x12();
            value.AttachInfoId = reader.ReadByte();
            value.AttachInfoLength = reader.ReadByte();
            value.JT808PositionType = (JT808PositionType)reader.ReadByte();
            value.AreaId = reader.ReadInt32();
            value.Direction = (JT808DirectionType)reader.ReadByte();
            return value;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x12 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteByte((byte)value.JT808PositionType);
            writer.WriteInt32(value.AreaId);
            writer.WriteByte((byte)value.Direction);
        }
    }
}
