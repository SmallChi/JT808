using JT808.Protocol.Exceptions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using JT808.Protocol.Metadata;
using System;
using System.Collections.Generic;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// CAN 总线数据上传
    /// 0x0705
    /// </summary>
    public class JT808_0x0705 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x0705>,IJT808_2019_Version
    {
        public override ushort MsgId { get; } = 0x0705;
        /// <summary>
        /// 数据项个数
        /// 包含的 CAN 总线数据项个数，>0
        /// </summary>
        public ushort CanItemCount { get; set; }
        /// <summary>
        /// CAN 总线数据接收时间
        /// 第 1 条 CAN 总线数据的接收时间，hh-mm-ss-msms
        /// </summary>
        public DateTime FirstCanReceiveTime { get; set; }
        /// <summary>
        /// CAN 总线数据项
        /// </summary>
        public List<JT808CanProperty> CanItems { get; set; }

        public JT808_0x0705 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0705 jT808_0X0705 = new JT808_0x0705();
            jT808_0X0705.CanItemCount = reader.ReadUInt16();
            jT808_0X0705.FirstCanReceiveTime = reader.ReadDateTime5();
            jT808_0X0705.CanItems = new List<JT808CanProperty>();
            for (var i = 0; i < jT808_0X0705.CanItemCount; i++)
            {
                JT808CanProperty jT808CanProperty = new JT808CanProperty();
                jT808CanProperty.CanId = reader.ReadUInt32();
                jT808CanProperty.CanData = reader.ReadArray(8).ToArray();
                if (jT808CanProperty.CanData.Length != 8)
                {
                    throw new JT808Exception(Enums.JT808ErrorCode.NotEnoughLength, $"{nameof(jT808CanProperty.CanData)}->8");
                }
                jT808_0X0705.CanItems.Add(jT808CanProperty);
            }
            return jT808_0X0705;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0705 value, IJT808Config config)
        {
            if (value.CanItems != null && value.CanItems.Count > 0)
            {
                writer.WriteUInt16((ushort)value.CanItems.Count);
                writer.WriteDateTime5(value.FirstCanReceiveTime);
                foreach (var item in value.CanItems)
                {
                    writer.WriteUInt32(item.CanId);
                    if (item.CanData.Length != 8)
                    {
                        throw new JT808Exception(Enums.JT808ErrorCode.NotEnoughLength, $"{nameof(item.CanData)}->8");
                    }
                    writer.WriteArray(item.CanData);
                }
            }
        }
    }
}
