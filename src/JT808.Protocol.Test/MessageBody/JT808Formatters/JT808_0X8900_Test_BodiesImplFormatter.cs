using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using JT808.Protocol.Test.MessageBody.JT808_0X8900_BodiesImpl;
using System;

namespace JT808.Protocol.Test.MessageBody.JT808Formatters
{
    public class JT808_0X8900_Test_BodiesImplFormatter : IJT808MessagePackFormatter<JT808_0X8900_Test_BodiesImpl>
    {
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
