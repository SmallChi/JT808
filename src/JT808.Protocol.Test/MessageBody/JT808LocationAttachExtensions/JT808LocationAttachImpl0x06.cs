using JT808.Protocol.Formatters;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Test.JT808LocationAttach
{
    /// <summary>
    /// 自定义附加信息
    /// Age-word-2
    /// UserName-BCD(10)
    /// Gerder-byte-1
    /// </summary>
    public class JT808LocationAttachImpl0x06: JT808_0x0200_CustomBodyBase, IJT808MessagePackFormatter<JT808LocationAttachImpl0x06>
    {
        public override byte AttachInfoId { get;  set; } = 0x06;
        public override byte AttachInfoLength { get;  set; } = 13;
        public int Age { get; set; }
        public byte Gender { get; set; }
        public string UserName { get; set; }
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
