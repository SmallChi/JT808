using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using JT808.Protocol.Metadata;
using System.Collections.Generic;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 设置电话本
    /// </summary>
    public class JT808_0x8401 : JT808MessagePackFormatter<JT808_0x8401>, JT808Bodies,  IJT808Analyze
    {
        /// <summary>
        /// 0x8401
        /// </summary>
        public ushort MsgId =>0x8401;
        /// <summary>
        /// 设置电话本
        /// </summary>
        public string Description => "设置电话本";
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8401 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8401 value, IJT808Config config)
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8401 value = new JT808_0x8401();
            value.SettingTelephoneBook = (JT808SettingTelephoneBook)reader.ReadByte();
            writer.WriteNumber($"[{((byte)value.SettingTelephoneBook).ReadNumber()}]设置类型",(byte) value.SettingTelephoneBook);
            value.ContactCount = reader.ReadByte();
            writer.WriteNumber($"[{value.ContactCount.ReadNumber()}]联系人总数", value.ContactCount);
            writer.WriteStartArray("联系人项");
            for (var i = 0; i < value.ContactCount; i++)
            {
                writer.WriteStartObject();
                JT808ContactProperty jT808ContactProperty = new JT808ContactProperty();
                jT808ContactProperty.TelephoneBookContactType = (JT808TelephoneBookContactType)reader.ReadByte();
                writer.WriteNumber($"[{((byte)jT808ContactProperty.TelephoneBookContactType).ReadNumber()}]{jT808ContactProperty.TelephoneBookContactType.ToString()}", (byte)jT808ContactProperty.TelephoneBookContactType);
                jT808ContactProperty.PhoneNumberLength = reader.ReadByte();
                writer.WriteNumber($"[{jT808ContactProperty.PhoneNumberLength.ReadNumber()}]号码长度", jT808ContactProperty.PhoneNumberLength);
                var pnoBuffer = reader.ReadVirtualArray(jT808ContactProperty.PhoneNumberLength);
                jT808ContactProperty.PhoneNumber = reader.ReadString(jT808ContactProperty.PhoneNumberLength);
                writer.WriteString($"[{pnoBuffer.ToArray().ToHexString()}]电话号码", jT808ContactProperty.PhoneNumber);
                jT808ContactProperty.ContactLength = reader.ReadByte();
                writer.WriteNumber($"[{jT808ContactProperty.ContactLength.ReadNumber()}]联系人长度", jT808ContactProperty.ContactLength);
                var contactBuffer = reader.ReadVirtualArray(jT808ContactProperty.ContactLength);
                jT808ContactProperty.Contact = reader.ReadString(jT808ContactProperty.ContactLength);
                writer.WriteString($"[{contactBuffer.ToArray().ToHexString()}]联系人", jT808ContactProperty.Contact);
                writer.WriteEndObject();
            }
            writer.WriteEndArray();
        }
    }
}
