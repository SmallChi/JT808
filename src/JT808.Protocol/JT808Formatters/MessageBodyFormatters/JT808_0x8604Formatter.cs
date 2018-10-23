using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.JT808Properties;
using JT808.Protocol.MessageBody;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x8604Formatter : IJT808Formatter<JT808_0x8604>
    {
        public JT808_0x8604 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0x8604 jT808_0X8604 = new JT808_0x8604();
            jT808_0X8604.AreaId= JT808BinaryExtensions.ReadUInt32Little(bytes, ref offset);
            jT808_0X8604.AreaProperty = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
            ReadOnlySpan<char> areaProperty16Bit = Convert.ToString(jT808_0X8604.AreaProperty, 2).PadLeft(16, '0').AsSpan();
            bool bit0Flag = areaProperty16Bit.Slice(areaProperty16Bit.Length - 1).ToString().Equals("0");
            if (!bit0Flag)
            {
                jT808_0X8604.StartTime = JT808BinaryExtensions.ReadDateTimeLittle(bytes, ref offset);
                jT808_0X8604.EndTime = JT808BinaryExtensions.ReadDateTimeLittle(bytes, ref offset);
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
                    var item = new JT808PeakProperty();
                    item.Lat = JT808BinaryExtensions.ReadUInt32Little(bytes, ref offset);
                    item.Lng = JT808BinaryExtensions.ReadUInt32Little(bytes, ref offset);
                    jT808_0X8604.PeakItems.Add(item);
                }
            }
            readSize = offset;
            return jT808_0X8604;
        }

        public int Serialize(IMemoryOwner<byte> memoryOwner, int offset, JT808_0x8604 value)
        {
            offset += JT808BinaryExtensions.WriteUInt32Little(memoryOwner, offset, value.AreaId);
            offset += JT808BinaryExtensions.WriteUInt16Little(memoryOwner, offset, value.AreaProperty);
            ReadOnlySpan<char> areaProperty16Bit = Convert.ToString(value.AreaProperty, 2).PadLeft(16, '0').AsSpan();
            bool bit0Flag = areaProperty16Bit.Slice(areaProperty16Bit.Length - 1).ToString().Equals("0");
            if (!bit0Flag)
            {
                if (value.StartTime.HasValue)
                {
                    offset += JT808BinaryExtensions.WriteDateTime6Little(memoryOwner, offset, value.StartTime.Value);
                }
                if (value.EndTime.HasValue)
                {
                    offset += JT808BinaryExtensions.WriteDateTime6Little(memoryOwner, offset, value.EndTime.Value);
                }
            }
            bool bit1Flag = areaProperty16Bit.Slice(areaProperty16Bit.Length - 2, 1).ToString().Equals("0");
            if (!bit1Flag)
            {
                if (value.HighestSpeed.HasValue)
                {
                    offset += JT808BinaryExtensions.WriteUInt16Little(memoryOwner, offset, value.HighestSpeed.Value);
                }
                if (value.OverspeedDuration.HasValue)
                {
                    offset += JT808BinaryExtensions.WriteByteLittle(memoryOwner, offset, value.OverspeedDuration.Value);
                }
            }
            if(value.PeakItems!=null && value.PeakItems.Count > 0)
            {
                offset += JT808BinaryExtensions.WriteUInt16Little(memoryOwner, offset,(ushort)value.PeakItems.Count);
                foreach(var item in value.PeakItems)
                {
                    offset += JT808BinaryExtensions.WriteUInt32Little(memoryOwner, offset, item.Lat);
                    offset += JT808BinaryExtensions.WriteUInt32Little(memoryOwner, offset, item.Lng);
                }
            }
            return offset;
        }
    }
}
