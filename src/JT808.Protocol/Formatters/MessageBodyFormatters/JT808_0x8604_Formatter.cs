using JT808.Protocol.Extensions;
using JT808.Protocol.Metadata;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Interfaces;
using System;
using System.Collections.Generic;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x8604_Formatter : IJT808MessagePackFormatter<JT808_0x8604>
    {
        public JT808_0x8604 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8604 jT808_0X8604 = new JT808_0x8604();
            jT808_0X8604.AreaId = reader.ReadUInt32();
            jT808_0X8604.AreaProperty = reader.ReadUInt16();
            ReadOnlySpan<char> areaProperty16Bit = Convert.ToString(jT808_0X8604.AreaProperty, 2).PadLeft(16, '0').AsSpan();
            bool bit0Flag = areaProperty16Bit.Slice(areaProperty16Bit.Length - 1).ToString().Equals("0");
            if (!bit0Flag)
            {
                jT808_0X8604.StartTime = reader.ReadDateTime6();
                jT808_0X8604.EndTime = reader.ReadDateTime6();
            }
            bool bit1Flag = areaProperty16Bit.Slice(areaProperty16Bit.Length - 2, 1).ToString().Equals("0");
            if (!bit1Flag)
            {
                jT808_0X8604.HighestSpeed = reader.ReadUInt16();
                jT808_0X8604.OverspeedDuration = reader.ReadByte();
            }
            jT808_0X8604.PeakCount = reader.ReadUInt16();
            jT808_0X8604.PeakItems = new List<JT808PeakProperty>();
            for (var i = 0; i < jT808_0X8604.PeakCount; i++)
            {
                var item = new JT808PeakProperty();
                item.Lat = reader.ReadUInt32();
                item.Lng = reader.ReadUInt32();
                jT808_0X8604.PeakItems.Add(item);
            }
            return jT808_0X8604;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8604 value, IJT808Config config)
        {
            writer.WriteUInt32(value.AreaId);
            writer.WriteUInt16(value.AreaProperty);
            ReadOnlySpan<char> areaProperty16Bit = Convert.ToString(value.AreaProperty, 2).PadLeft(16, '0').AsSpan();
            bool bit0Flag = areaProperty16Bit.Slice(areaProperty16Bit.Length - 1).ToString().Equals("0");
            if (!bit0Flag)
            {
                if (value.StartTime.HasValue)
                {
                    writer.WriteDateTime6(value.StartTime.Value);
                }
                if (value.EndTime.HasValue)
                {
                    writer.WriteDateTime6(value.EndTime.Value);
                }
            }
            bool bit1Flag = areaProperty16Bit.Slice(areaProperty16Bit.Length - 2, 1).ToString().Equals("0");
            if (!bit1Flag)
            {
                if (value.HighestSpeed.HasValue)
                {
                    writer.WriteUInt16(value.HighestSpeed.Value);
                }
                if (value.OverspeedDuration.HasValue)
                {
                    writer.WriteByte(value.OverspeedDuration.Value);
                }
            }
            if (value.PeakItems != null && value.PeakItems.Count > 0)
            {
                writer.WriteUInt16((ushort)value.PeakItems.Count);
                foreach (var item in value.PeakItems)
                {
                    writer.WriteUInt32(item.Lat);
                    writer.WriteUInt32(item.Lng);
                }
            }
        }
    }
}
