using JT808.Protocol.Extensions.YueBiao.Enums;
using JT808.Protocol.Extensions.YueBiao.Metadata;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace JT808.Protocol.Extensions.YueBiao.MessageBody
{
    /// <summary>
    /// 透传数据
    /// </summary>
    public class JT808_0x0900_0xF7 : JT808MessagePackFormatter<JT808_0x0900_0xF7>, JT808_0x0900_BodyBase,  IJT808Analyze, IJT808_2019_Version
    {
        /// <summary>
        /// 透传类型
        /// </summary>
        public byte PassthroughType { get; set; } = JT808_YueBiao_Constants.JT808_0X0900_0xF7;
        /// <summary>
        /// 消息列表总数
        /// </summary>
        public byte USBMessageCount { get; set; }
        /// <summary>
        /// 消息列表数据
        /// </summary>
        public List<JT808_0x0900_0xF7_USB> USBMessages { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0900_0xF7 value = new JT808_0x0900_0xF7();
            value.USBMessageCount = reader.ReadByte();
            writer.WriteNumber($"[{value.USBMessageCount.ReadNumber()}]消息列表总数", value.USBMessageCount);
            if (value.USBMessageCount > 0)
            {
                writer.WriteStartArray("消息列表");
                for (int i = 0; i < value.USBMessageCount; i++)
                {
                    writer.WriteStartObject();
                    JT808_0x0900_0xF7_USB item = new JT808_0x0900_0xF7_USB();
                    item.USBID = reader.ReadByte();
                    writer.WriteNumber($"[{item.USBID.ReadNumber()}]外设ID", item.USBID);
                    item.MessageLength = reader.ReadByte();
                    writer.WriteNumber($"[{item.MessageLength.ReadNumber()}]消息长度", item.MessageLength);
                    item.WorkingCondition = reader.ReadByte();
                    var workingCondition = (WorkingConditionType)item.WorkingCondition;
                    writer.WriteNumber($"[{item.WorkingCondition.ReadNumber()}]工作状态-{workingCondition.ToString()}", item.WorkingCondition);
                    item.AlarmStatus = reader.ReadUInt32();
                    writer.WriteNumber($"[{item.AlarmStatus.ReadNumber()}]报警状态", item.AlarmStatus);
                    var alarmStatusBits = Convert.ToString(item.AlarmStatus, 2).PadLeft(32, '0').Reverse().ToArray().AsSpan();
                    writer.WriteStartObject($"报警状态对象[{alarmStatusBits.ToString()}]");
                    writer.WriteString($"[bit12~bit31]预留", alarmStatusBits.Slice(12).ToString());
                    writer.WriteString($"]bit11]定位模块异常", alarmStatusBits[11].ToString());
                    writer.WriteString($"[bit10]通讯模块异常", alarmStatusBits[10].ToString());
                    writer.WriteString($"[bit6~bit9]预留", alarmStatusBits.Slice(6,4).ToString());
                    writer.WriteString($"[bit5]电池异常", alarmStatusBits[5].ToString());
                    writer.WriteString($"[bit4]扬声器异常", alarmStatusBits[4].ToString());
                    writer.WriteString($"[bit3]红外补光异常", alarmStatusBits[3].ToString());
                    writer.WriteString($"[bit2]辅存储器异常", alarmStatusBits[2].ToString());
                    writer.WriteString($"[bit1]主存储器异常", alarmStatusBits[1].ToString());
                    writer.WriteString($"[bit0]摄像头异常", alarmStatusBits[0].ToString());
                    writer.WriteEndObject();
                    writer.WriteEndObject();
                }
                writer.WriteEndArray();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x0900_0xF7 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0900_0xF7 value = new JT808_0x0900_0xF7();
            value.USBMessageCount = reader.ReadByte();
            if (value.USBMessageCount > 0)
            {
                value.USBMessages = new List<JT808_0x0900_0xF7_USB>();
                for (int i = 0; i < value.USBMessageCount; i++)
                {
                    JT808_0x0900_0xF7_USB item = new JT808_0x0900_0xF7_USB();
                    item.USBID = reader.ReadByte();
                    item.MessageLength = reader.ReadByte();
                    item.WorkingCondition = reader.ReadByte();
                    item.AlarmStatus = reader.ReadUInt32();
                    value.USBMessages.Add(item);
                }
            }
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0900_0xF7 value, IJT808Config config)
        {
            if (value.USBMessages != null && value.USBMessages.Count > 0)
            {
                writer.WriteByte((byte)value.USBMessages.Count);
                foreach (var item in value.USBMessages)
                {
                    writer.WriteByte(item.USBID);
                    writer.WriteByte(5);
                    writer.WriteByte(item.WorkingCondition);
                    writer.WriteUInt32(item.AlarmStatus);
                }
            }
        }
    }
}
