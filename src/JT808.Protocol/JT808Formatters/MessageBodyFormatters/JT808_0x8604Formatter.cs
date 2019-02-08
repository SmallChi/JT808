using JT808.Protocol.Extensions;
using JT808.Protocol.JT808Properties;
using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x8604Formatter : IJT808Formatter<JT808_0x8604>
    {
        public JT808_0x8604 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0x8604 jT808_0X8604 = new JT808_0x8604
            {
                AreaId = JT808BinaryExtensions.ReadUInt32Little(bytes, ref offset),
                AreaProperty = JT808BinaryExtensions.ReadUInt16Little(bytes, ref offset)
            };
            ReadOnlySpan<char> areaProperty16Bit = Convert.ToString(jT808_0X8604.AreaProperty, 2).PadLeft(16, '0').AsSpan();
            bool bit0Flag = areaProperty16Bit.Slice(areaProperty16Bit.Length - 1).ToString().Equals("0");
            if (!bit0Flag)
            {
                jT808_0X8604.StartTime = JT808BinaryExtensions.ReadDateTime6Little(bytes, ref offset);
                jT808_0X8604.EndTime = JT808BinaryExtensions.ReadDateTime6Little(bytes, ref offset);
            }
            bool bit1Flag = areaProperty16Bit.Slice(areaProperty16Bit.Length - 2, 1).ToString().Equals("0");
            if (!bit1Flag)
            {
                jT808_0X8604.HighestSpeed = JT808BinaryExtensions.ReadUInt16Little(bytes, ref offset);
                jT808_0X8604.OverspeedDuration = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
            }
            jT808_0X8604.PeakCount = JT808BinaryExtensions.ReadUInt16Little(bytes, ref offset);
            jT808_0X8604.PeakItems = new List<JT808PeakProperty>();
            if (jT808_0X8604.PeakCount > 0)
            {
                for (var i = 0; i < jT808_0X8604.PeakCount; i++)
                {
                    var item = new JT808PeakProperty
                    {
                        Lat = JT808BinaryExtensions.ReadUInt32Little(bytes, ref offset),
                        Lng = JT808BinaryExtensions.ReadUInt32Little(bytes, ref offset)
                    };
                    jT808_0X8604.PeakItems.Add(item);
                }
            }
            readSize = offset;
            return jT808_0X8604;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x8604 value)
        {
            offset += JT808BinaryExtensions.WriteUInt32Little(bytes, offset, value.AreaId);
            offset += JT808BinaryExtensions.WriteUInt16Little(bytes, offset, value.AreaProperty);
            ReadOnlySpan<char> areaProperty16Bit = Convert.ToString(value.AreaProperty, 2).PadLeft(16, '0').AsSpan();
            bool bit0Flag = areaProperty16Bit.Slice(areaProperty16Bit.Length - 1).ToString().Equals("0");
            if (!bit0Flag)
            {
                if (value.StartTime.HasValue)
                {
                    offset += JT808BinaryExtensions.WriteDateTime6Little(bytes, offset, value.StartTime.Value);
                }
                if (value.EndTime.HasValue)
                {
                    offset += JT808BinaryExtensions.WriteDateTime6Little(bytes, offset, value.EndTime.Value);
                }
            }
            bool bit1Flag = areaProperty16Bit.Slice(areaProperty16Bit.Length - 2, 1).ToString().Equals("0");
            if (!bit1Flag)
            {
                if (value.HighestSpeed.HasValue)
                {
                    offset += JT808BinaryExtensions.WriteUInt16Little(bytes, offset, value.HighestSpeed.Value);
                }
                if (value.OverspeedDuration.HasValue)
                {
                    offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, value.OverspeedDuration.Value);
                }
            }
            if (value.PeakItems != null && value.PeakItems.Count > 0)
            {
                offset += JT808BinaryExtensions.WriteUInt16Little(bytes, offset, (ushort)value.PeakItems.Count);
                foreach (var item in value.PeakItems)
                {
                    offset += JT808BinaryExtensions.WriteUInt32Little(bytes, offset, item.Lat);
                    offset += JT808BinaryExtensions.WriteUInt32Little(bytes, offset, item.Lng);
                }
            }
            return offset;
        }
    }
}
