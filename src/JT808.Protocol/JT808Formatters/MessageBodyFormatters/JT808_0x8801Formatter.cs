using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x8801Formatter : IJT808Formatter<JT808_0x8801>
    {
        public JT808_0x8801 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0x8801 jT808_0X8801 = new JT808_0x8801();
            jT808_0X8801.ChannelId = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
            jT808_0X8801.ShootingCommand = JT808BinaryExtensions.ReadUInt16Little(bytes, ref offset);
            jT808_0X8801.VideoTime = JT808BinaryExtensions.ReadUInt16Little(bytes, ref offset);
            jT808_0X8801.SaveFlag = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
            jT808_0X8801.Resolution = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
            jT808_0X8801.VideoQuality = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
            jT808_0X8801.Lighting = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
            jT808_0X8801.Contrast = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
            jT808_0X8801.Saturability = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
            jT808_0X8801.Chroma = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
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
