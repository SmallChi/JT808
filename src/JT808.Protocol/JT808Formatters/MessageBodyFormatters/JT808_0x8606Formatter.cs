using JT808.Protocol.Extensions;
using JT808.Protocol.JT808Properties;
using JT808.Protocol.MessageBody;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x8606Formatter : IJT808Formatter<JT808_0x8606>
    {
        public JT808_0x8606 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0x8606 jT808_0X8606 = new JT808_0x8606();
            jT808_0X8606.RouteId = JT808BinaryExtensions.ReadUInt32Little(bytes, ref offset);
            jT808_0X8606.RouteProperty = JT808BinaryExtensions.ReadUInt16Little(bytes, ref offset);
            ReadOnlySpan<char> routeProperty16Bit = Convert.ToString(jT808_0X8606.RouteProperty, 2).PadLeft(16, '0').AsSpan();
            bool bit0Flag = routeProperty16Bit.Slice(routeProperty16Bit.Length - 1).ToString().Equals("0");
            if (!bit0Flag)
            {
                jT808_0X8606.StartTime = JT808BinaryExtensions.ReadDateTimeLittle(bytes, ref offset);
                jT808_0X8606.EndTime = JT808BinaryExtensions.ReadDateTimeLittle(bytes, ref offset);
            }
            jT808_0X8606.InflectionPointCount = JT808BinaryExtensions.ReadUInt16Little(bytes, ref offset);
            jT808_0X8606.InflectionPointItems = new List<JT808InflectionPointProperty>();
            for(var i=0;i< jT808_0X8606.InflectionPointCount; i++)
            {
                JT808InflectionPointProperty jT808InflectionPointProperty = new JT808InflectionPointProperty();
                jT808InflectionPointProperty.InflectionPointId= JT808BinaryExtensions.ReadUInt32Little(bytes, ref offset);
                jT808InflectionPointProperty.SectionId = JT808BinaryExtensions.ReadUInt32Little(bytes, ref offset);
                jT808InflectionPointProperty.InflectionPointLat = JT808BinaryExtensions.ReadUInt32Little(bytes, ref offset);
                jT808InflectionPointProperty.InflectionPointLng = JT808BinaryExtensions.ReadUInt32Little(bytes, ref offset);
                jT808InflectionPointProperty.SectionWidth = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
                jT808InflectionPointProperty.SectionProperty = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
                ReadOnlySpan<char> sectionProperty16Bit = Convert.ToString(jT808_0X8606.RouteProperty, 2).PadLeft(16, '0').AsSpan();
                bool sectionBit0Flag = sectionProperty16Bit.Slice(sectionProperty16Bit.Length - 1).ToString().Equals("0");
                if (!sectionBit0Flag)
                {
                    jT808InflectionPointProperty.SectionLongDrivingThreshold = JT808BinaryExtensions.ReadUInt16Little(bytes, ref offset);
                    jT808InflectionPointProperty.SectionDrivingUnderThreshold = JT808BinaryExtensions.ReadUInt16Little(bytes, ref offset);
                }
                bool sectionBit1Flag = sectionProperty16Bit.Slice(sectionProperty16Bit.Length - 2, 1).ToString().Equals("0");
                if (!sectionBit1Flag)
                {
                    jT808InflectionPointProperty.SectionHighestSpeed = JT808BinaryExtensions.ReadUInt16Little(bytes, ref offset);
                    jT808InflectionPointProperty.SectionOverspeedDuration = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
                }
                jT808_0X8606.InflectionPointItems.Add(jT808InflectionPointProperty);
            }
            readSize = offset;
            return jT808_0X8606;
        }

        public int Serialize(IMemoryOwner<byte> memoryOwner, int offset, JT808_0x8606 value)
        {
            offset += JT808BinaryExtensions.WriteUInt32Little(memoryOwner, offset, value.RouteId);
            offset += JT808BinaryExtensions.WriteUInt16Little(memoryOwner, offset, value.RouteProperty);
            ReadOnlySpan<char> routeProperty16Bit = Convert.ToString(value.RouteProperty, 2).PadLeft(16, '0').AsSpan();
            bool bit0Flag = routeProperty16Bit.Slice(routeProperty16Bit.Length - 1).ToString().Equals("0");
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
            bool bit1Flag = routeProperty16Bit.Slice(routeProperty16Bit.Length - 2, 1).ToString().Equals("0");
            if(value.InflectionPointItems!=null && value.InflectionPointItems.Count > 0)
            {
                offset += JT808BinaryExtensions.WriteUInt16Little(memoryOwner, offset,(ushort)value.InflectionPointItems.Count);
                foreach(var item in value.InflectionPointItems)
                {
                    offset += JT808BinaryExtensions.WriteUInt32Little(memoryOwner, offset, item.InflectionPointId);
                    offset += JT808BinaryExtensions.WriteUInt32Little(memoryOwner, offset, item.SectionId);
                    offset += JT808BinaryExtensions.WriteUInt32Little(memoryOwner, offset, item.InflectionPointLat);
                    offset += JT808BinaryExtensions.WriteUInt32Little(memoryOwner, offset, item.InflectionPointLng);
                    offset += JT808BinaryExtensions.WriteByteLittle(memoryOwner, offset, item.SectionWidth);
                    offset += JT808BinaryExtensions.WriteByteLittle(memoryOwner, offset, item.SectionProperty);

                    ReadOnlySpan<char> sectionProperty16Bit = Convert.ToString(item.SectionProperty, 2).PadLeft(16, '0').AsSpan();
                    bool sectionBit0Flag = sectionProperty16Bit.Slice(sectionProperty16Bit.Length - 1).ToString().Equals("0");
                    if (!sectionBit0Flag)
                    {
                        if (item.SectionLongDrivingThreshold.HasValue)
                            offset += JT808BinaryExtensions.WriteUInt16Little(memoryOwner, offset, item.SectionLongDrivingThreshold.Value);
                        if (item.SectionDrivingUnderThreshold.HasValue)
                            offset += JT808BinaryExtensions.WriteUInt16Little(memoryOwner, offset, item.SectionDrivingUnderThreshold.Value);
                    }
                    bool sectionBit1Flag = sectionProperty16Bit.Slice(sectionProperty16Bit.Length - 2, 1).ToString().Equals("0");
                    if (!sectionBit1Flag)
                    {
                        if(item.SectionHighestSpeed.HasValue)
                            offset += JT808BinaryExtensions.WriteUInt16Little(memoryOwner, offset, item.SectionHighestSpeed.Value);
                        if(item.SectionOverspeedDuration.HasValue)
                            offset += JT808BinaryExtensions.WriteByteLittle(memoryOwner, offset, item.SectionOverspeedDuration.Value);
                    }
                }
            }
            return offset;
        }
    }
}
