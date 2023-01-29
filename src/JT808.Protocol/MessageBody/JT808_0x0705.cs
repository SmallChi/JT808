using JT808.Protocol.Exceptions;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using JT808.Protocol.Metadata;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// CAN 总线数据上传
    /// 0x0705
    /// </summary>
    public class JT808_0x0705 : JT808MessagePackFormatter<JT808_0x0705>, JT808Bodies, IJT808Analyze, IJT808_2019_Version
    {
        /// <summary>
        /// 0x0705
        /// </summary>
        public ushort MsgId  => 0x0705;
        /// <summary>
        /// CAN总线数据上传
        /// </summary>
        public string Description => "CAN总线数据上传";
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0705 value = new JT808_0x0705();
            value.CanItemCount = reader.ReadUInt16();
            writer.WriteNumber($"[{value.CanItemCount.ReadNumber()}]数据项个数", value.CanItemCount);
            var dateTimeBuffer = reader.ReadVirtualArray(5).ToArray();
            value.FirstCanReceiveTime = reader.ReadDateTime_HHmmssfff();
            writer.WriteString($"[{dateTimeBuffer.ToHexString()}]CAN总线数据接收时间", value.FirstCanReceiveTime.ToString("HH-mm-ss:fff"));
            writer.WriteStartArray("CAN总线数据项");
            for (var i = 0; i < value.CanItemCount; i++)
            {
                writer.WriteStartObject();
                JT808CanProperty jT808CanProperty = new JT808CanProperty();
                jT808CanProperty.CanId = reader.ReadUInt32();
                writer.WriteNumber($"[{ jT808CanProperty.CanId.ReadNumber()}]CAN_ID", jT808CanProperty.CanId);
                jT808CanProperty.CanData = reader.ReadArray(8).ToArray();
                writer.WriteString($"CAN_数据", jT808CanProperty.CanData.ToHexString());
                if (jT808CanProperty.CanData.Length != 8)
                {
                    throw new JT808Exception(Enums.JT808ErrorCode.NotEnoughLength, $"{nameof(jT808CanProperty.CanData)}->8"); 
                }
                writer.WriteEndObject();
            }
            writer.WriteEndArray();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x0705 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0705 value = new JT808_0x0705();
            value.CanItemCount = reader.ReadUInt16();
            value.FirstCanReceiveTime = reader.ReadDateTime_HHmmssfff();
            value.CanItems = new List<JT808CanProperty>();
            for (var i = 0; i < value.CanItemCount; i++)
            {
                JT808CanProperty jT808CanProperty = new JT808CanProperty();
                jT808CanProperty.CanId = reader.ReadUInt32();
                jT808CanProperty.CanData = reader.ReadArray(8).ToArray();
                if (jT808CanProperty.CanData.Length != 8)
                {
                    throw new JT808Exception(Enums.JT808ErrorCode.NotEnoughLength, $"{nameof(jT808CanProperty.CanData)}->8");
                }
                value.CanItems.Add(jT808CanProperty);
            }
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0705 value, IJT808Config config)
        {
            if (value.CanItems != null && value.CanItems.Count > 0)
            {
                writer.WriteUInt16((ushort)value.CanItems.Count);
                writer.WriteDateTime_HHmmssfff(value.FirstCanReceiveTime);
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
