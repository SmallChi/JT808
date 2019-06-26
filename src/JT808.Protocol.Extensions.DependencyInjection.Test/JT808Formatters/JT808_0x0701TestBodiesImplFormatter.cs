using JT808.Protocol.Extensions.DependencyInjection.Test.JT808_0x0701BodiesImpl;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;

namespace JT808.Protocol.Extensions.DependencyInjection.Test.JT808Formatters
{
    public class JT808_0x0701TestBodiesImplFormatter : IJT808MessagePackFormatter<JT808_0x0701TestBodiesImpl>
    {
        public JT808_0x0701TestBodiesImpl Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0701TestBodiesImpl jT808_0X0701TestBodiesImpl = new JT808_0x0701TestBodiesImpl();
            jT808_0X0701TestBodiesImpl.Id = reader.ReadUInt32();
            jT808_0X0701TestBodiesImpl.UserNameLength = reader.ReadUInt16();
            return jT808_0X0701TestBodiesImpl;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0701TestBodiesImpl value, IJT808Config config)
        {
            writer.WriteUInt32(value.Id);
            // 先计算内容长度（汉字为两个字节）
            writer.Skip(2, out int position);
            writer.WriteString(value.UserName);
            writer.WriteUInt16Return((ushort)(writer.GetCurrentPosition()- position-2), position);
        }
    }
}
