using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Interfaces;
using System;
using System.Collections.Generic;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x0805_Formatter : IJT808MessagePackFormatter<JT808_0x0805>
    {
        public JT808_0x0805 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0805 jT808_0X0805 = new JT808_0x0805();
            jT808_0X0805.MsgNum = reader.ReadUInt16();
            jT808_0X0805.Result = reader.ReadByte();
            jT808_0X0805.MultimediaIdCount = reader.ReadUInt16();
            jT808_0X0805.MultimediaIds = new List<uint>();
            for (var i = 0; i < jT808_0X0805.MultimediaIdCount; i++)
            {
                uint id = reader.ReadUInt32();
                jT808_0X0805.MultimediaIds.Add(id);
            }
            return jT808_0X0805;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0805 value, IJT808Config config)
        {
            writer.WriteUInt16(value.MsgNum);
            writer.WriteByte(value.Result);
            writer.WriteUInt16((ushort)value.MultimediaIds.Count);
            foreach (var item in value.MultimediaIds)
            {
                writer.WriteUInt32(item);
            }
        }
    }
}
