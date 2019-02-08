using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x0805Formatter : IJT808Formatter<JT808_0x0805>
    {
        public JT808_0x0805 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0x0805 jT808_0X0805 = new JT808_0x0805
            {
                MsgNum = JT808BinaryExtensions.ReadUInt16Little(bytes, ref offset),
                Result = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset),
                MultimediaIdCount = JT808BinaryExtensions.ReadUInt16Little(bytes, ref offset),
                MultimediaIds = new List<uint>()
            };
            for (var i = 0; i < jT808_0X0805.MultimediaIdCount; i++)
            {
                uint id = JT808BinaryExtensions.ReadUInt32Little(bytes, ref offset);
                jT808_0X0805.MultimediaIds.Add(id);
            }
            readSize = offset;
            return jT808_0X0805;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x0805 value)
        {
            offset += JT808BinaryExtensions.WriteUInt16Little(bytes, offset, value.MsgNum);
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, value.Result);
            offset += JT808BinaryExtensions.WriteUInt16Little(bytes, offset, (ushort)value.MultimediaIds.Count);
            foreach (var item in value.MultimediaIds)
            {
                offset += JT808BinaryExtensions.WriteUInt32Little(bytes, offset, item);
            }
            return offset;
        }
    }
}
