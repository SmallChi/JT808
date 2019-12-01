using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System.Collections.Generic;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 查询区域或线路数据应答
    /// </summary>
    public class JT808_0x0608 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x0608>, IJT808_2019_Version
    {
        public override ushort MsgId { get; } = 0x0608;
        /// <summary>
        /// 查询类型
        /// </summary>
        public byte QueryType { get; set; }
        /// <summary>
        /// 查询的区域或线路的ID数量
        /// </summary>
        public uint Count { get; set; }

        /// <summary>
        /// 查询的区域或线路的ID
        /// </summary>
        public List<uint> Ids { get; set; }
        public JT808_0x0608 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0608 value = new JT808_0x0608();
            value.QueryType = reader.ReadByte();
            value.Count = reader.ReadUInt32();
            if (value.Count > 0)
            {
                value.Ids = new List<uint>();
                for (int i = 0; i < value.Count; i++)
                {
                    value.Ids.Add(reader.ReadUInt32());
                }
            }
            return value;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0608 value, IJT808Config config)
        {
            writer.WriteByte(value.QueryType);
            if (value.Ids != null && value.Ids.Count > 0)
            {
                writer.WriteUInt32((uint)value.Ids.Count);
                foreach (var item in value.Ids)
                {
                    writer.WriteUInt32(item);
                }
            }
            else
            {
                writer.WriteUInt32(0);
            }
        }
    }
}
