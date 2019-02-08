using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x0704Formatter : IJT808Formatter<JT808_0x0704>
    {
        public JT808_0x0704 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0x0704 jT808_0X0704 = new JT808_0x0704
            {
                Count = JT808BinaryExtensions.ReadUInt16Little(bytes, ref offset),
                LocationType = (JT808_0x0704.BatchLocationType)JT808BinaryExtensions.ReadByteLittle(bytes, ref offset)
            };
            List<JT808_0x0200> jT808_0X0200s = new List<JT808_0x0200>();
            int bufReadSize;
            for (int i = 0; i < jT808_0X0704.Count; i++)
            {
                int buflen = JT808BinaryExtensions.ReadUInt16Little(bytes, ref offset);
                //offset = offset + 2;
                try
                {
                    JT808_0x0200 jT808_0X0200 = JT808FormatterExtensions.GetFormatter<JT808_0x0200>().Deserialize(bytes.Slice(offset, buflen), out bufReadSize);
                    jT808_0X0200s.Add(jT808_0X0200);
                }
                catch (Exception)
                {

                }
                offset = offset + buflen;
            }
            jT808_0X0704.Positions = jT808_0X0200s;
            readSize = offset;
            return jT808_0X0704;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x0704 value)
        {
            offset += JT808BinaryExtensions.WriteUInt16Little(bytes, offset, value.Count);
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, (byte)value.LocationType);
            foreach (var item in value?.Positions)
            {
                try
                {
                    // 需要反着来，先序列化数据体（由于位置汇报数据体长度为2个字节，所以先偏移2个字节），再根据数据体的长度设置回去
                    int positionOffset = JT808FormatterExtensions.GetFormatter<JT808_0x0200>().Serialize(ref bytes, offset + 2, item);
                    JT808BinaryExtensions.WriteUInt16Little(bytes, offset, (ushort)(positionOffset - offset - 2));
                    offset = positionOffset;
                }
                catch (Exception)
                {

                }
            }
            return offset;
        }
    }
}
