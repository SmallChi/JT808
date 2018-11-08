using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x0901Formatter : IJT808Formatter<JT808_0x0901>
    {
        public JT808_0x0901 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0x0901 jT808_0X0901 = new JT808_0x0901();
            jT808_0X0901.CompressMessageLength = JT808BinaryExtensions.ReadUInt32Little(bytes, ref offset);
            var data = JT808BinaryExtensions.ReadBytesLittle(bytes, ref offset, (int)jT808_0X0901.CompressMessageLength);
            jT808_0X0901.CompressMessage = JT808GlobalConfig.Instance.Compress.Decompress(data);
            readSize = offset;
            return jT808_0X0901;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x0901 value)
        {
            var data= JT808GlobalConfig.Instance.Compress.Compress(value.CompressMessage);
            offset += JT808BinaryExtensions.WriteUInt32Little(bytes, offset,(uint) data.Length);
            offset += JT808BinaryExtensions.WriteBytesLittle(bytes, offset, data);
            return offset;
        }
    }
}
