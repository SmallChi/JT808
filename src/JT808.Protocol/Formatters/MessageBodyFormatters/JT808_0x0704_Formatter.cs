using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Interfaces;
using System;
using System.Collections.Generic;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x0704_Formatter : IJT808MessagePackFormatter<JT808_0x0704>
    {
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
