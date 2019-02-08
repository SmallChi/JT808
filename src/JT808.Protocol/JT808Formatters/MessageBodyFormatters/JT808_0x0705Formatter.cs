using JT808.Protocol.Exceptions;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x0705Formatter : IJT808Formatter<JT808_0x0705>
    {
        public JT808_0x0705 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0x0705 jT808_0X0705 = new JT808_0x0705
            {
                CanItemCount = JT808BinaryExtensions.ReadUInt16Little(bytes, ref offset),
                FirstCanReceiveTime = JT808BinaryExtensions.ReadDateTime5Little(bytes, ref offset),
                CanItems = new List<JT808Properties.JT808CanProperty>()
            };
            for (var i = 0; i < jT808_0X0705.CanItemCount; i++)
            {
                JT808Properties.JT808CanProperty jT808CanProperty = new JT808Properties.JT808CanProperty
                {
                    CanId = JT808BinaryExtensions.ReadBytesLittle(bytes, ref offset, 4)
                };
                if (jT808CanProperty.CanId.Length != 4)
                {
                    throw new JT808Exception(Enums.JT808ErrorCode.NotEnoughLength, $"{nameof(jT808CanProperty.CanId)}->4");
                }
                jT808CanProperty.CanData = JT808BinaryExtensions.ReadBytesLittle(bytes, ref offset, 8);
                if (jT808CanProperty.CanData.Length != 8)
                {
                    throw new JT808Exception(Enums.JT808ErrorCode.NotEnoughLength, $"{nameof(jT808CanProperty.CanData)}->8");
                }
                jT808_0X0705.CanItems.Add(jT808CanProperty);
            }
            readSize = offset;
            return jT808_0X0705;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x0705 value)
        {
            if (value.CanItems != null && value.CanItems.Count > 0)
            {
                offset += JT808BinaryExtensions.WriteUInt16Little(bytes, offset, (ushort)value.CanItems.Count);
                offset += JT808BinaryExtensions.WriteDateTime5Little(bytes, offset, value.FirstCanReceiveTime);
                foreach (var item in value.CanItems)
                {
                    if (item.CanId.Length != 4)
                    {
                        throw new JT808Exception(Enums.JT808ErrorCode.NotEnoughLength, $"{nameof(item.CanId)}->4");
                    }
                    offset += JT808BinaryExtensions.WriteBytesLittle(bytes, offset, item.CanId);
                    if (item.CanData.Length != 8)
                    {
                        throw new JT808Exception(Enums.JT808ErrorCode.NotEnoughLength, $"{nameof(item.CanData)}->8");
                    }
                    offset += JT808BinaryExtensions.WriteBytesLittle(bytes, offset, item.CanData);
                }
            }
            return offset;
        }
    }
}
