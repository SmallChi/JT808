using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x8801Formatter : IJT808Formatter<JT808_0x8801>
    {
        public JT808_0x8801 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0x8801 jT808_0X8801 = new JT808_0x8801
            {
                ChannelId = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset),
                ShootingCommand = JT808BinaryExtensions.ReadUInt16Little(bytes, ref offset),
                VideoTime = JT808BinaryExtensions.ReadUInt16Little(bytes, ref offset),
                SaveFlag = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset),
                Resolution = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset),
                VideoQuality = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset),
                Lighting = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset),
                Contrast = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset),
                Saturability = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset),
                Chroma = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset)
            };
            readSize = offset;
            return jT808_0X8801;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x8801 value)
        {
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, value.ChannelId);
            offset += JT808BinaryExtensions.WriteUInt16Little(bytes, offset, value.ShootingCommand);
            offset += JT808BinaryExtensions.WriteUInt16Little(bytes, offset, value.VideoTime);
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, value.SaveFlag);
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, value.Resolution);
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, value.VideoQuality);
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, value.Lighting);
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, value.Contrast);
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, value.Saturability);
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, value.Chroma);
            return offset;
        }
    }
}
