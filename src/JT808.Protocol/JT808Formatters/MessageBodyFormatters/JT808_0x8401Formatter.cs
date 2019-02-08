using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.JT808Properties;
using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x8401Formatter : IJT808Formatter<JT808_0x8401>
    {
        public JT808_0x8401 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0x8401 jT808_0X8401 = new JT808_0x8401
            {
                SettingTelephoneBook = (JT808SettingTelephoneBook)JT808BinaryExtensions.ReadByteLittle(bytes, ref offset),
                ContactCount = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset)
            };
            List<JT808ContactProperty> jT808_0X8401s = new List<JT808ContactProperty>();
            for (var i = 0; i < jT808_0X8401.ContactCount; i++)
            {
                JT808ContactProperty jT808ContactProperty = new JT808ContactProperty
                {
                    TelephoneBookContactType = (JT808TelephoneBookContactType)JT808BinaryExtensions.ReadByteLittle(bytes, ref offset),
                    PhoneNumberLength = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset)
                };
                jT808ContactProperty.PhoneNumber = JT808BinaryExtensions.ReadStringLittle(bytes, ref offset, jT808ContactProperty.PhoneNumberLength);
                jT808ContactProperty.ContactLength = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
                jT808ContactProperty.Contact = JT808BinaryExtensions.ReadStringLittle(bytes, ref offset, jT808ContactProperty.ContactLength);
                jT808_0X8401s.Add(jT808ContactProperty);
            }
            jT808_0X8401.JT808ContactProperties = jT808_0X8401s;
            readSize = offset;
            return jT808_0X8401;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x8401 value)
        {
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, (byte)value.SettingTelephoneBook);
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, (byte)value.JT808ContactProperties.Count);
            foreach (var item in value.JT808ContactProperties)
            {
                offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, (byte)item.TelephoneBookContactType);
                offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, (byte)item.PhoneNumber.Length);
                offset += JT808BinaryExtensions.WriteStringLittle(bytes, offset, item.PhoneNumber);
                offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, (byte)item.Contact.Length);
                offset += JT808BinaryExtensions.WriteStringLittle(bytes, offset, item.Contact);
            }
            return offset;
        }
    }
}
