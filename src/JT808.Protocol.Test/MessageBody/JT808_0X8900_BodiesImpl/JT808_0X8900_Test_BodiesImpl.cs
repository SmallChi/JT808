using JT808.Protocol.Formatters;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Test.MessageBody.JT808_0X8900_BodiesImpl
{
    public class JT808_0X8900_Test_BodiesImpl: JT808_0x8900_BodyBase, IJT808MessagePackFormatter<JT808_0X8900_Test_BodiesImpl>
    {
         public uint Id { get; set; }

         public byte Sex { get; set; }
        public JT808_0X8900_Test_BodiesImpl Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0X8900_Test_BodiesImpl jT808_0X8900_Test_BodiesImpl = new JT808_0X8900_Test_BodiesImpl();
            jT808_0X8900_Test_BodiesImpl.Id = reader.ReadUInt32();
            jT808_0X8900_Test_BodiesImpl.Sex = reader.ReadByte();
            return jT808_0X8900_Test_BodiesImpl;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0X8900_Test_BodiesImpl value, IJT808Config config)
        {
            writer.WriteUInt32(value.Id);
            writer.WriteByte(value.Sex);
        }
    }
}
