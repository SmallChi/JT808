using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using JT808.Protocol.Test.MessageBody.JT808_0x0701BodiesImpl;
using System;

namespace JT808.Protocol.Test.MessageBody.JT808Formatters
{
    public class JT808_0x0701TestBodiesImplFormatter : IJT808MessagePackFormatter<JT808_0x0701TestBodiesImpl>
    {
        public JT808_0x0701TestBodiesImpl Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0701TestBodiesImpl jT808_0X0701TestBodiesImpl = new JT808_0x0701TestBodiesImpl();
            jT808_0X0701TestBodiesImpl.Id = reader.ReadUInt32();
            jT808_0X0701TestBodiesImpl.UserNameLength = reader.ReadUInt16();
            jT808_0X0701TestBodiesImpl.UserName = reader.ReadString(jT808_0X0701TestBodiesImpl.UserNameLength);
            return jT808_0X0701TestBodiesImpl;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0701TestBodiesImpl value, IJT808Config config)
        {
            writer.WriteUInt32(value.Id);
            writer.Skip(2,out int position);
            writer.WriteString(value.UserName);
            int strLength = writer.GetCurrentPosition() - position - 2;
            writer.WriteUInt16Return((ushort)strLength, position);
        }
    }
}
