using JT808.Protocol.Exceptions;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Metadata;
using JT808.Protocol.Interfaces;
using System;
using System.Collections.Generic;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x0705_Formatter : IJT808MessagePackFormatter<JT808_0x0705>
    {
        public JT808_0x0705 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0705 jT808_0X0705 = new JT808_0x0705();
            jT808_0X0705.CanItemCount = reader.ReadUInt16();
            jT808_0X0705.FirstCanReceiveTime = reader.ReadDateTime5();
            jT808_0X0705.CanItems = new List<JT808CanProperty>();
            for (var i = 0; i < jT808_0X0705.CanItemCount; i++)
            {
                JT808CanProperty jT808CanProperty = new JT808CanProperty();
                jT808CanProperty.CanId = reader.ReadArray(4).ToArray();
                if (jT808CanProperty.CanId.Length != 4)
                {
                    throw new JT808Exception(Enums.JT808ErrorCode.NotEnoughLength, $"{nameof(jT808CanProperty.CanId)}->4");
                }
                jT808CanProperty.CanData = reader.ReadArray(8).ToArray();
                if (jT808CanProperty.CanData.Length != 8)
                {
                    throw new JT808Exception(Enums.JT808ErrorCode.NotEnoughLength, $"{nameof(jT808CanProperty.CanData)}->8");
                }
                jT808_0X0705.CanItems.Add(jT808CanProperty);
            }
            return jT808_0X0705;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0705 value, IJT808Config config)
        {
            if (value.CanItems != null && value.CanItems.Count > 0)
            {
                writer.WriteUInt16((ushort)value.CanItems.Count);
                writer.WriteDateTime5(value.FirstCanReceiveTime);
                foreach (var item in value.CanItems)
                {
                    if (item.CanId.Length != 4)
                    {
                        throw new JT808Exception(Enums.JT808ErrorCode.NotEnoughLength, $"{nameof(item.CanId)}->4");
                    }
                    writer.WriteArray(item.CanId);
                    if (item.CanData.Length != 8)
                    {
                        throw new JT808Exception(Enums.JT808ErrorCode.NotEnoughLength, $"{nameof(item.CanData)}->8");
                    }
                    writer.WriteArray(item.CanData);
                }
            }
        }
    }
}
