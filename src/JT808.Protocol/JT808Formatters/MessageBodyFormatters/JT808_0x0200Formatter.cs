using JT808.Protocol.Extensions;
using JT808.Protocol.JT808Properties;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessageBody.JT808LocationAttach;
using System;
using System.Buffers;
using System.Collections.Generic;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x0200Formatter : IJT808Formatter<JT808_0x0200>
    {
        public JT808_0x0200 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0x0200 jT808_0X0200 = new JT808_0x0200();
            jT808_0X0200.AlarmFlag = JT808BinaryExtensions.ReadUInt32Little(bytes,ref offset);
            jT808_0X0200.StatusFlag = JT808BinaryExtensions.ReadUInt32Little(bytes,ref offset);
            jT808_0X0200.Lat = JT808BinaryExtensions.ReadInt32Little(bytes,ref offset);
            jT808_0X0200.Lng = JT808BinaryExtensions.ReadInt32Little(bytes,ref offset);
            JT808StatusProperty jT808StatusProperty = new JT808StatusProperty(Convert.ToString(jT808_0X0200.StatusFlag, 2).PadLeft(32, '0'));
            if (jT808StatusProperty.Bit28 == '1')//西经
            {
                jT808_0X0200.Lng = -jT808_0X0200.Lng;
            }
            if (jT808StatusProperty.Bit29 == '1')//南纬
            {
                jT808_0X0200.Lat = -jT808_0X0200.Lat;
            }
            jT808_0X0200.Altitude = JT808BinaryExtensions.ReadUInt16Little(bytes,ref offset);
            jT808_0X0200.Speed = JT808BinaryExtensions.ReadUInt16Little(bytes,ref offset);
            jT808_0X0200.Direction = JT808BinaryExtensions.ReadUInt16Little(bytes,ref offset);
            jT808_0X0200.GPSTime=JT808BinaryExtensions.ReadDateTimeLittle(bytes,ref offset);
            // 位置附加信息
            jT808_0X0200.JT808LocationAttachData = new Dictionary<byte, JT808LocationAttachBase>();
            if (bytes.Length > 28)
            {
                int attachOffset = 0;
                ReadOnlySpan<byte> locationAttachMemory = bytes;
                ReadOnlySpan<byte> locationAttachSpan = locationAttachMemory.Slice(28);
                while (locationAttachSpan.Length > attachOffset)
                {
                    int attachId = 1;
                    int attachLen = 1;
                    try
                    {
                        Type jT808LocationAttachType;
                        if (JT808LocationAttachBase.JT808LocationAttachMethod.TryGetValue(locationAttachSpan[attachOffset], out jT808LocationAttachType))
                        {
                            int attachContentLen = locationAttachSpan[attachOffset + 1];
                            int locationAttachTotalLen = attachId + attachLen + attachContentLen;
                            ReadOnlySpan<byte> attachBuffer = locationAttachSpan.Slice(attachOffset, locationAttachTotalLen);
                            object attachImplObj = JT808FormatterExtensions.GetFormatter(jT808LocationAttachType);
                            dynamic attachImpl = JT808FormatterResolverExtensions.JT808DynamicDeserialize(attachImplObj, attachBuffer,out readSize);
                            attachOffset = attachOffset + locationAttachTotalLen;
                            jT808_0X0200.JT808LocationAttachData.Add(attachImpl.AttachInfoId, attachImpl);
                        }
                        else
                        {
                            int attachContentLen = locationAttachSpan[attachOffset + 1];
                            int locationAttachTotalLen = attachId + attachLen + attachContentLen;
                            attachOffset = attachOffset + locationAttachTotalLen;
                        }
                    }
                    catch (Exception ex)
                    {
                        int attachContentLen = locationAttachSpan[attachOffset + 1];
                        int locationAttachTotalLen = attachId + attachLen + attachContentLen;
                        attachOffset = attachOffset + locationAttachTotalLen;
                    }
                }
                offset= offset + attachOffset;
            }
            readSize = offset;
            return jT808_0X0200;
        }

        public int Serialize(IMemoryOwner<byte> memoryOwner, int offset, JT808_0x0200 value)
        {
            offset += JT808BinaryExtensions.WriteUInt32Little(memoryOwner, offset,value.AlarmFlag);
            offset += JT808BinaryExtensions.WriteUInt32Little(memoryOwner, offset, value.StatusFlag);
            offset += JT808BinaryExtensions.WriteInt32Little(memoryOwner, offset, value.Lat);
            offset += JT808BinaryExtensions.WriteInt32Little(memoryOwner, offset, value.Lng);
            offset += JT808BinaryExtensions.WriteUInt16Little(memoryOwner, offset, value.Altitude);
            offset += JT808BinaryExtensions.WriteUInt16Little(memoryOwner, offset, value.Speed);
            offset += JT808BinaryExtensions.WriteUInt16Little(memoryOwner, offset, value.Direction);
            offset += JT808BinaryExtensions.WriteDateTime6Little(memoryOwner, offset, value.GPSTime);
            if (value.JT808LocationAttachData != null && value.JT808LocationAttachData.Count > 0)
            {
                foreach (var item in value.JT808LocationAttachData)
                {
                    try
                    {
                        object attachImplObj = JT808FormatterExtensions.GetFormatter(item.Value.GetType());
                        offset = JT808FormatterResolverExtensions.JT808DynamicSerialize(attachImplObj, memoryOwner, offset, item.Value);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            return offset;
        }
    }
}
