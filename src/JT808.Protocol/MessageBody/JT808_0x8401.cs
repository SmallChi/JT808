using JT808.Protocol.Enums;
using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;
using JT808.Protocol.Metadata;
using System.Collections.Generic;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 设置电话本
    /// </summary>
    public class JT808_0x8401 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x8401>
    {
        public override ushort MsgId { get; } = 0x8401;
        /// <summary>
        /// 设置类型
        /// </summary>
        public JT808SettingTelephoneBook SettingTelephoneBook { get; set; }
        /// <summary>
        /// 联系人总数
        /// </summary>
        public byte ContactCount { get; set; }
        /// <summary>
        /// 联系人项
        /// </summary>
        public IList<JT808ContactProperty> JT808ContactProperties { get; set; }
        public JT808_0x8401 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8401 jT808_0X8401 = new JT808_0x8401();
            jT808_0X8401.SettingTelephoneBook = (JT808SettingTelephoneBook)reader.ReadByte();
            jT808_0X8401.ContactCount = reader.ReadByte();
            List<JT808ContactProperty> jT808_0X8401s = new List<JT808ContactProperty>();
            for (var i = 0; i < jT808_0X8401.ContactCount; i++)
            {
                JT808ContactProperty jT808ContactProperty = new JT808ContactProperty();
                jT808ContactProperty.TelephoneBookContactType = (JT808TelephoneBookContactType)reader.ReadByte();
                jT808ContactProperty.PhoneNumberLength = reader.ReadByte();
                jT808ContactProperty.PhoneNumber = reader.ReadString(jT808ContactProperty.PhoneNumberLength);
                jT808ContactProperty.ContactLength = reader.ReadByte();
                jT808ContactProperty.Contact = reader.ReadString(jT808ContactProperty.ContactLength);
                jT808_0X8401s.Add(jT808ContactProperty);
            }
            jT808_0X8401.JT808ContactProperties = jT808_0X8401s;
            return jT808_0X8401;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8401 value, IJT808Config config)
        {
            writer.WriteByte((byte)value.SettingTelephoneBook);
            writer.WriteByte((byte)value.JT808ContactProperties.Count);
            foreach (var item in value.JT808ContactProperties)
            {
                writer.WriteByte((byte)item.TelephoneBookContactType);
                writer.WriteByte((byte)item.PhoneNumber.Length);
                writer.WriteString(item.PhoneNumber);
                writer.WriteByte((byte)item.Contact.Length);
                writer.WriteString(item.Contact);
            }
        }
    }
}
