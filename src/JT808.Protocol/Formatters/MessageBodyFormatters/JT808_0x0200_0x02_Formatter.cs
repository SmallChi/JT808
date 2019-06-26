using JT808.Protocol.MessageBody;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x0200_0x02_Formatter : IJT808MessagePackFormatter<JT808_0x0200_0x02>
    {
        public JT808_0x0200_0x02 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x02 jT808LocationAttachImpl0X02 = new JT808_0x0200_0x02();
            jT808LocationAttachImpl0X02.AttachInfoId = reader.ReadByte();
            jT808LocationAttachImpl0X02.AttachInfoLength = reader.ReadByte();
            jT808LocationAttachImpl0X02.Oil = reader.ReadUInt16();
            return jT808LocationAttachImpl0X02;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x02 value, IJT808Config config)
        {
            writer.WriteByte( value.AttachInfoId);
            writer.WriteByte( value.AttachInfoLength);
            writer.WriteUInt16( value.Oil);
        }
    }
}
