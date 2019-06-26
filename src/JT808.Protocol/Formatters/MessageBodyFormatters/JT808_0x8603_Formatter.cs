using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Interfaces;
using System;
using System.Collections.Generic;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x8603_Formatter : IJT808MessagePackFormatter<JT808_0x8603>
    {
        public JT808_0x8603 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8603 jT808_0X8603 = new JT808_0x8603();
            jT808_0X8603.AreaCount = reader.ReadByte();
            jT808_0X8603.AreaIds = new List<uint>();
            for (var i = 0; i < jT808_0X8603.AreaCount; i++)
            {
                jT808_0X8603.AreaIds.Add(reader.ReadUInt32());
            }
            return jT808_0X8603;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8603 value, IJT808Config config)
        {
            if (value.AreaIds != null)
            {
                writer.WriteByte((byte)value.AreaIds.Count);
                foreach (var item in value.AreaIds)
                {
                    writer.WriteUInt32(item);
                }
            }
        }
    }
}
