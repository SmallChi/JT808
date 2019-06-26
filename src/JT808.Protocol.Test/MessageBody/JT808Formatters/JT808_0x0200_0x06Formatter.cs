using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using JT808.Protocol.Test.JT808LocationAttach;
using System;

namespace JT808.Protocol.Test.JT808Formatters.MessageBodyFormatters.JT808LocationAttach
{
    public class JT808_0x0200_0x06Formatter : IJT808MessagePackFormatter<JT808LocationAttachImpl0x06>
    {
        public JT808LocationAttachImpl0x06 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808LocationAttachImpl0x06 jT808LocationAttachImpl0x06 = new JT808LocationAttachImpl0x06();
            jT808LocationAttachImpl0x06.AttachInfoId = reader.ReadByte();
            jT808LocationAttachImpl0x06.AttachInfoLength = reader.ReadByte();
            jT808LocationAttachImpl0x06.Age = reader.ReadInt32();
            jT808LocationAttachImpl0x06.Gender = reader.ReadByte();
            jT808LocationAttachImpl0x06.UserName = reader.ReadRemainStringContent();
            return jT808LocationAttachImpl0x06;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808LocationAttachImpl0x06 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteInt32(value.Age);
            writer.WriteByte(value.Gender);
            writer.WriteString(value.UserName);
        }
    }
}
