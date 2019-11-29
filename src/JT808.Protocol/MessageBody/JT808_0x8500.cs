using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System.Collections.Generic;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 车辆控制
    /// </summary>
    public class JT808_0x8500 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x8500>, IJT808_2019_Version
    {
        public override ushort MsgId { get; } = 0x8500;
        /// <summary>
        /// 控制标志 
        /// 控制指令标志位数据格式
        /// 0：车门解锁；1：车门加锁
        /// 1-7 保留
        /// </summary>
        public byte ControlFlag { get; set; }
        /// <summary>
        /// 控制类型数量
        /// </summary>
        public ushort ControlTypeCount { get; set; }
        /// <summary>
        /// 用于反序列化的时候,由于厂家自定义类型比较多，所以直接用byte数组存储
        /// </summary>
        public byte[] ControlTypeBuffer { get; set; }
        /// <summary>
        /// 用于序列化的时候,由于厂家自定义类型比较多，所以直接用JT808_0x8500_ControlType
        /// </summary>
        public List<JT808_0x8500_ControlType> ControlTypes { get; set; }

        public JT808_0x8500 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8500 value = new JT808_0x8500();
            if(reader.Version== JT808Version.JTT2019)
            {
                value.ControlTypeCount = reader.ReadUInt16();
                value.ControlTypeBuffer = reader.ReadArray(reader.ReadCurrentRemainContentLength()).ToArray();
            }
            else
            {
                value.ControlFlag = reader.ReadByte();
            }
            return value;
        }
        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8500 value, IJT808Config config)
        {
            if (writer.Version == JT808Version.JTT2019)
            {
                if(value.ControlTypes!=null && value.ControlTypes.Count > 0)
                {
                    writer.WriteUInt16((ushort)value.ControlTypes.Count);
                    foreach (var item in value.ControlTypes)
                    {
                        JT808MessagePackFormatterResolverExtensions.JT808DynamicSerialize(item, ref writer, item, config);
                    }
                }
                else
                {
                    writer.WriteUInt16(value.ControlTypeCount);
                    if(value.ControlTypeBuffer!=null && value.ControlTypeBuffer.Length > 0)
                    {
                        writer.WriteArray(value.ControlTypeBuffer);
                    }
                }
            }
            else
            {
                writer.WriteByte(value.ControlFlag);
            }
        }
    }
}
