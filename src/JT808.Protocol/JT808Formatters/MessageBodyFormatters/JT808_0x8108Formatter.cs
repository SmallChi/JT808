using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x8108Formatter : IJT808Formatter<JT808_0x8108>
    {
        public JT808_0x8108 Deserialize(ReadOnlySpan<byte> bytes,  out int readSize)
        {
            int offset = 0;
            JT808_0x8108 jT808_0X8108 = new JT808_0x8108();
            jT808_0X8108.UpgradeType =(JT808UpgradeType) JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
            jT808_0X8108.MakerId = JT808BinaryExtensions.ReadStringLittle(bytes, ref offset, 5);
            jT808_0X8108.VersionNumLength= JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
            jT808_0X8108.VersionNum = JT808BinaryExtensions.ReadStringLittle(bytes, ref offset, jT808_0X8108.VersionNumLength);
            jT808_0X8108.UpgradePackageLength = JT808BinaryExtensions.ReadInt32Little(bytes, ref offset);
            jT808_0X8108.UpgradePackage = JT808BinaryExtensions.ReadBytesLittle(bytes, ref offset, jT808_0X8108.UpgradePackageLength);
            readSize = offset;
            return jT808_0X8108;
        }

        public int Serialize(IMemoryOwner<byte> memoryOwner, int offset, JT808_0x8108 value)
        {
            offset += JT808BinaryExtensions.WriteByteLittle(memoryOwner, offset,(byte)value.UpgradeType);
            offset += JT808BinaryExtensions.WriteStringLittle(memoryOwner, offset, value.MakerId.PadRight(5, '0'));
            offset += JT808BinaryExtensions.WriteByteLittle(memoryOwner, offset,(byte)value.VersionNum.Length);
            offset += JT808BinaryExtensions.WriteStringLittle(memoryOwner, offset, value.VersionNum);
            offset += JT808BinaryExtensions.WriteInt32Little(memoryOwner, offset, value.UpgradePackage.Length);
            offset += JT808BinaryExtensions.WriteBytesLittle(memoryOwner, offset, value.UpgradePackage);
            return offset;
        }
    }
}
