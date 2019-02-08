using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x8001Formatter : IJT808Formatter<JT808_0x8001>
    {
        public JT808_0x8001 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0x8001 jT808_0X8001 = new JT808_0x8001
            {
                MsgNum = JT808BinaryExtensions.ReadUInt16Little(bytes, ref offset),
                MsgId = JT808BinaryExtensions.ReadUInt16Little(bytes, ref offset),
                JT808PlatformResult = (JT808PlatformResult)JT808BinaryExtensions.ReadByteLittle(bytes, ref offset)
            };
            readSize = offset;
            return jT808_0X8001;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x8001 value)
        {
            offset += JT808BinaryExtensions.WriteUInt16Little(bytes, offset, value.MsgNum);
            offset += JT808BinaryExtensions.WriteUInt16Little(bytes, offset, (ushort)value.MsgId);
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, (byte)value.JT808PlatformResult);
            return offset;
        }
    }
}
