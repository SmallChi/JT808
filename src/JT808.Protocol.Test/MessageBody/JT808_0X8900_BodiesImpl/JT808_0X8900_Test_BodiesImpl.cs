using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessagePack;
using JT808.Protocol.Extensions;
using System.Text.Json;

namespace JT808.Protocol.Test.MessageBody.JT808_0X8900_BodiesImpl
{
    public class JT808_0X8900_Test_BodiesImpl: JT808MessagePackFormatter<JT808_0X8900_Test_BodiesImpl>, JT808_0x8900_BodyBase,  IJT808Analyze
    {
         public uint Id { get; set; }

         public byte Sex { get; set; }
        public byte PassthroughType { get; set; } = 0x0B;

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0X8900_Test_BodiesImpl value = new JT808_0X8900_Test_BodiesImpl();
            value.Id = reader.ReadUInt32();
            writer.WriteNumber($"[{value.Id.ReadNumber()}]编号Id", value.Id);
            value.Sex = reader.ReadByte();
            writer.WriteNumber($"[{value.Sex.ReadNumber()}]性别", value.Sex);
        }

        public override JT808_0X8900_Test_BodiesImpl Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0X8900_Test_BodiesImpl value = new JT808_0X8900_Test_BodiesImpl();
            value.Id = reader.ReadUInt32();
            value.Sex = reader.ReadByte();
            return value;
        }

        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0X8900_Test_BodiesImpl value, IJT808Config config)
        {
            writer.WriteUInt32(value.Id);
            writer.WriteByte(value.Sex);
        }
    }
}
