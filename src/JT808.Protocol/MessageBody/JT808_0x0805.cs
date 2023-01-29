using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System.Collections.Generic;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 摄像头立即拍摄命令应答
    /// 0x0805
    /// </summary>
    public class JT808_0x0805 : JT808MessagePackFormatter<JT808_0x0805>, JT808Bodies, IJT808Analyze, IJT808_2019_Version
    {
        /// <summary>
        /// 0x0805
        /// </summary>
        public ushort MsgId  => 0x0805;
        /// <summary>
        /// 摄像头立即拍摄命令应答
        /// </summary>
        public string Description => "摄像头立即拍摄命令应答";
        /// <summary>
        /// 应答流水号
        /// 对应平台摄像头立即拍摄命令的消息流水号
        /// </summary>
        public ushort ReplyMsgNum { get; set; }
        /// <summary>
        /// 结果
        /// 0：成功；1：失败；2：通道不支持。以下字段在结果=0 时才有效。
        /// </summary>
        public byte Result { get; set; }
        /// <summary>
        /// 多媒体ID个数
        /// 拍摄成功的多媒体个数
        /// </summary>
        public ushort MultimediaIdCount { get; set; }
        /// <summary>
        /// 多媒体ID列表
        /// </summary>
        public List<uint> MultimediaIds { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0805 value = new JT808_0x0805();
            value.ReplyMsgNum = reader.ReadUInt16();
            writer.WriteNumber($"[{value.ReplyMsgNum.ReadNumber()}]应答流水号", value.ReplyMsgNum);
            value.Result = reader.ReadByte();
            string result = "成功";
            switch (value.Result)
            {
                case 1:
                    result = "失败";
                    break;
                case 2:
                    result = "通道不支持";
                    break;
            }
            writer.WriteNumber($"[{value.Result.ReadNumber()}]结果-{result}", value.Result);
            if (value.Result == 0)
            {
                value.MultimediaIdCount = reader.ReadUInt16();
                writer.WriteNumber($"[{value.MultimediaIdCount.ReadNumber()}]多媒体ID个数", value.MultimediaIdCount);
                writer.WriteStartArray("多媒体ID列表");
                for (var i = 0; i < value.MultimediaIdCount; i++)
                {
                    writer.WriteStartObject();
                    uint id = reader.ReadUInt32();
                    writer.WriteNumber($"[{id.ReadNumber()}]ID{i+1}", id);
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
        public override JT808_0x0805 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0805 value = new JT808_0x0805();
            value.ReplyMsgNum = reader.ReadUInt16();
            value.Result = reader.ReadByte();
            if (value.Result == 0)
            {
                value.MultimediaIdCount = reader.ReadUInt16();
                value.MultimediaIds = new List<uint>();
                for (var i = 0; i < value.MultimediaIdCount; i++)
                {
                    uint id = reader.ReadUInt32();
                    value.MultimediaIds.Add(id);
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
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0805 value, IJT808Config config)
        {
            writer.WriteUInt16(value.ReplyMsgNum);
            writer.WriteByte(value.Result);
            if (value.Result == 0)
            {
                writer.WriteUInt16((ushort)value.MultimediaIds.Count);
                foreach (var item in value.MultimediaIds)
                {
                    writer.WriteUInt32(item);
                }
            }
        }
    }
}
