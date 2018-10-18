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
    public class JT808_0x8602Formatter : IJT808Formatter<JT808_0x8602>
    {
        public JT808_0x8602 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0x8602 jT808_0X8602 = new JT808_0x8602();
            jT808_0X8602.SettingProperty = (JT808_0x8602_SettingProperty)JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
            jT808_0X8602.AreaCount = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
            jT808_0X8602.AreaItems = new List<JT808RectangleAreaProperty>();
            for (var i = 0; i < jT808_0X8602.AreaCount; i++)
            {
                JT808RectangleAreaProperty jT808CircleAreaProperty = new JT808RectangleAreaProperty();
                jT808CircleAreaProperty.AreaId = JT808BinaryExtensions.ReadUInt32Little(bytes, ref offset);
                jT808CircleAreaProperty.AreaProperty = JT808BinaryExtensions.ReadUInt16Little(bytes, ref offset);
                jT808CircleAreaProperty.UpLeftPointLat = JT808BinaryExtensions.ReadUInt32Little(bytes, ref offset);
                jT808CircleAreaProperty.UpLeftPointLng = JT808BinaryExtensions.ReadUInt32Little(bytes, ref offset);
                jT808CircleAreaProperty.LowRightPointLat = JT808BinaryExtensions.ReadUInt32Little(bytes, ref offset);
                jT808CircleAreaProperty.LowRightPointLng = JT808BinaryExtensions.ReadUInt32Little(bytes, ref offset);
                ReadOnlySpan<char> areaProperty16Bit = Convert.ToString(jT808CircleAreaProperty.AreaProperty, 2).PadLeft(16, '0').AsSpan();
                bool bit0Flag = areaProperty16Bit.Slice(areaProperty16Bit.Length - 1).ToString().Equals("0");
                if (!bit0Flag)
                {
                    jT808CircleAreaProperty.StartTime = JT808BinaryExtensions.ReadDateTimeLittle(bytes, ref offset);
                    jT808CircleAreaProperty.EndTime = JT808BinaryExtensions.ReadDateTimeLittle(bytes, ref offset);
                }
                bool bit1Flag = areaProperty16Bit.Slice(areaProperty16Bit.Length - 2, 1).ToString().Equals("0");
                if (!bit1Flag)
                {
                    jT808CircleAreaProperty.HighestSpeed = JT808BinaryExtensions.ReadUInt16Little(bytes, ref offset);
                    jT808CircleAreaProperty.OverspeedDuration = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
                }
                jT808_0X8602.AreaItems.Add(jT808CircleAreaProperty);
            }
            readSize = offset;
            return jT808_0X8602;
        }

        public int Serialize(IMemoryOwner<byte> memoryOwner, int offset, JT808_0x8602 value)
        {
            offset += JT808BinaryExtensions.WriteByteLittle(memoryOwner, offset, (byte)value.SettingProperty);
            if (value.AreaItems != null)
            {
                offset += JT808BinaryExtensions.WriteByteLittle(memoryOwner, offset, (byte)value.AreaItems.Count);
                foreach (var item in value.AreaItems)
                {
                    offset += JT808BinaryExtensions.WriteUInt32Little(memoryOwner, offset, item.AreaId);
                    offset += JT808BinaryExtensions.WriteUInt16Little(memoryOwner, offset, item.AreaProperty);
                    offset += JT808BinaryExtensions.WriteUInt32Little(memoryOwner, offset, item.UpLeftPointLat);
                    offset += JT808BinaryExtensions.WriteUInt32Little(memoryOwner, offset, item.UpLeftPointLng);
                    offset += JT808BinaryExtensions.WriteUInt32Little(memoryOwner, offset, item.LowRightPointLat);
                    offset += JT808BinaryExtensions.WriteUInt32Little(memoryOwner, offset, item.LowRightPointLng);
                    ReadOnlySpan<char> areaProperty16Bit = Convert.ToString(item.AreaProperty, 2).PadLeft(16, '0').AsSpan();
                    bool bit0Flag = areaProperty16Bit.Slice(areaProperty16Bit.Length - 1).ToString().Equals("0");
                    if (!bit0Flag)
                    {
                        if (item.StartTime.HasValue)
                        {
                            offset += JT808BinaryExtensions.WriteDateTime6Little(memoryOwner, offset, item.StartTime.Value);
                        }
                        if (item.EndTime.HasValue)
                        {
                            offset += JT808BinaryExtensions.WriteDateTime6Little(memoryOwner, offset, item.EndTime.Value);
                        }
                    }
                    bool bit1Flag = areaProperty16Bit.Slice(areaProperty16Bit.Length - 2, 1).ToString().Equals("0");
                    if (!bit1Flag)
                    {
                        if (item.HighestSpeed.HasValue)
                        {
                            offset += JT808BinaryExtensions.WriteUInt16Little(memoryOwner, offset, item.HighestSpeed.Value);
                        }
                        if (item.OverspeedDuration.HasValue)
                        {
                            offset += JT808BinaryExtensions.WriteByteLittle(memoryOwner, offset, item.OverspeedDuration.Value);
                        }
                    }
                }
            }
            return offset;
        }
    }
}
