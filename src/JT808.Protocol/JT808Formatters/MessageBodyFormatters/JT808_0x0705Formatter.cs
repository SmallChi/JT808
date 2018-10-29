using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x0705Formatter : IJT808Formatter<JT808_0x0705>
    {
        public JT808_0x0705 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0x0705 jT808_0X0705 = new JT808_0x0705();
            jT808_0X0705.CanItemCount = JT808BinaryExtensions.ReadUInt16Little(bytes, ref offset);
            jT808_0X0705.FirstCanReceiveTime = JT808BinaryExtensions.ReadDateTime5Little(bytes, ref offset);
            jT808_0X0705.CanItems = new List<JT808Properties.JT808CanProperty>();
            for (var i = 0; i < jT808_0X0705.CanItemCount; i++)
            {
                JT808Properties.JT808CanProperty jT808CanProperty = new JT808Properties.JT808CanProperty();
                jT808CanProperty.CanId = JT808BinaryExtensions.ReadBytesLittle(bytes, ref offset, 4);
                jT808CanProperty.CanData = JT808BinaryExtensions.ReadBytesLittle(bytes, ref offset, 8);
                jT808_0X0705.CanItems.Add(jT808CanProperty);
            }
            readSize = offset;
            return jT808_0X0705;
        }

        public int Serialize(IMemoryOwner<byte> memoryOwner, int offset, JT808_0x0705 value)
        {
            if (value.CanItems!=null && value.CanItems.Count > 0)
            {
                offset += JT808BinaryExtensions.WriteUInt16Little(memoryOwner, offset, (ushort)value.CanItems.Count);
                offset += JT808BinaryExtensions.WriteDateTime5Little(memoryOwner, offset, value.FirstCanReceiveTime);
                foreach(var item in value.CanItems)
                {
                    offset += JT808BinaryExtensions.WriteBytesLittle(memoryOwner, offset, item.CanId);
                    offset += JT808BinaryExtensions.WriteBytesLittle(memoryOwner, offset, item.CanData);
                }
            }
            return offset;
        }
    }
}
