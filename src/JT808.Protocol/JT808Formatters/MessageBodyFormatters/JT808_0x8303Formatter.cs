using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x8303Formatter : IJT808Formatter<JT808_0x8303>
    {
        public JT808_0x8303 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0x8303 jT808_0X8303 = new JT808_0x8303
            {
                SettingType = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset),
                InformationItemCount = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset),
                InformationItems = new List<JT808Properties.JT808InformationItemProperty>()
            };
            for (var i = 0; i < jT808_0X8303.InformationItemCount; i++)
            {
                JT808Properties.JT808InformationItemProperty jT808InformationItemProperty = new JT808Properties.JT808InformationItemProperty
                {
                    InformationType = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset),
                    InformationLength = JT808BinaryExtensions.ReadUInt16Little(bytes, ref offset)
                };
                jT808InformationItemProperty.InformationName = JT808BinaryExtensions.ReadStringLittle(bytes, ref offset, jT808InformationItemProperty.InformationLength);
                jT808_0X8303.InformationItems.Add(jT808InformationItemProperty);
            }
            readSize = offset;
            return jT808_0X8303;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x8303 value)
        {
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, value.SettingType);
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, (byte)value.InformationItems.Count);
            foreach (var item in value.InformationItems)
            {
                offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, item.InformationType);
                // 先计算内容长度（汉字为两个字节）
                offset += 2;
                int byteLength = JT808BinaryExtensions.WriteStringLittle(bytes, offset, item.InformationName);
                JT808BinaryExtensions.WriteUInt16Little(bytes, offset - 2, (ushort)byteLength);
                offset += byteLength;
            }
            return offset;
        }
    }
}
