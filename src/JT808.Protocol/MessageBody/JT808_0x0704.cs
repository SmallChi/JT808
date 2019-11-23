using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 定位数据批量上传
    /// </summary>
    public class JT808_0x0704 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x0704>
    {
        public override ushort MsgId { get; } = 0x0704;
        /// <summary>
        /// 数据项个数
        /// </summary>
        public ushort Count { get; set; }

        /// <summary>
        /// 位置数据类型
        /// </summary>
        public BatchLocationType LocationType { get; set; }

        /// <summary>
        /// 位置汇报数据集合
        /// </summary>
        public IList<JT808_0x0200> Positions { get; set; }

        /// <summary>
        /// 位置数据类型
        /// </summary>
        public enum BatchLocationType : byte
        {
            正常位置批量汇报 = 0x00,
            盲区补报 = 0x01
        }

        public JT808_0x0704 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0704 jT808_0X0704 = new JT808_0x0704();
            jT808_0X0704.Count = reader.ReadUInt16();
            jT808_0X0704.LocationType = (JT808_0x0704.BatchLocationType)reader.ReadByte();
            List<JT808_0x0200> jT808_0X0200s = new List<JT808_0x0200>();
            for (int i = 0; i < jT808_0X0704.Count; i++)
            {
                int buflen = reader.ReadUInt16();
                try
                {
                    JT808MessagePackReader tmpReader = new JT808MessagePackReader(reader.ReadArray(buflen));
                    JT808_0x0200 jT808_0X0200 = config.GetMessagePackFormatter<JT808_0x0200>().Deserialize(ref tmpReader, config);
                    jT808_0X0200s.Add(jT808_0X0200);
                }
                catch (Exception)
                {

                }
            }
            jT808_0X0704.Positions = jT808_0X0200s;
            return jT808_0X0704;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0704 value, IJT808Config config)
        {
            writer.WriteUInt16(value.Count);
            writer.WriteByte((byte)value.LocationType);
            foreach (var item in value?.Positions)
            {
                try
                {
                    writer.Skip(2, out int position);
                    config.GetMessagePackFormatter<JT808_0x0200>().Serialize(ref writer, item, config);
                    ushort length = (ushort)(writer.GetCurrentPosition() - position - 2);
                    writer.WriteUInt16Return(length, position);
                }
                catch (Exception)
                {

                }
            }
        }
    }
}
