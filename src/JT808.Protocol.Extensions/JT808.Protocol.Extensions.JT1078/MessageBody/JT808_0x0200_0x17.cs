using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessagePack;
using System;
using System.Text.Json;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 存储器故障报警状态
    /// 0x0200_0x17
    /// </summary>
    public class JT808_0x0200_0x17 : JT808MessagePackFormatter<JT808_0x0200_0x17>, JT808_0x0200_CustomBodyBase,  IJT808Analyze
    {
        /// <summary>
        /// 
        /// </summary>
        public byte AttachInfoId { get; set; } = 0x17;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public byte AttachInfoLength { get; set; } = 2;
        /// <summary>
        /// 存储器故障报警状态
        /// </summary>
        public ushort StorageFaultAlarmStatus{ get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0200_0x17 value = new JT808_0x0200_0x17();
            value.AttachInfoId = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoId.ReadNumber()}]附加信息Id", value.AttachInfoId);
            value.AttachInfoLength = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoLength.ReadNumber()}]附加信息长度", value.AttachInfoLength);
            value.StorageFaultAlarmStatus = reader.ReadUInt16();
            writer.WriteNumber($"[{value.StorageFaultAlarmStatus.ReadNumber()}]存储器故障报警状态", value.StorageFaultAlarmStatus);
            var storageFaultAlarmStatusSpan = Convert.ToString(value.StorageFaultAlarmStatus, 2).PadLeft(16, '0').AsSpan();
            writer.WriteStartArray("存储器故障报警状态集合");
            int index = 0;
            foreach (var item in storageFaultAlarmStatusSpan)
            {
                if (index < 4)
                {
                    if (item == '1')
                    {
                        writer.WriteStringValue($"{index}灾备存储装置故障");
                    }
                    else
                    {
                        writer.WriteStringValue($"{index}灾备存储装置正常");
                    }
                }
                else
                {
                    if (item == '1')
                    {
                        writer.WriteStringValue($"{index}主存储器故障");
                    }
                    else
                    {
                        writer.WriteStringValue($"{index}主存储器正常");
                    }
                }
                index++;
            }
            writer.WriteEndArray();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x0200_0x17 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x17 value = new JT808_0x0200_0x17();
            value.AttachInfoId = reader.ReadByte();
            value.AttachInfoLength = reader.ReadByte();
            value.StorageFaultAlarmStatus = reader.ReadUInt16();
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x17 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteUInt16(value.StorageFaultAlarmStatus);
        }
    }
}
