using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 终端上传音视频资源列表
    /// </summary>
    public class JT808_0x1205 : JT808MessagePackFormatter<JT808_0x1205>, JT808Bodies,  IJT808Analyze
    {
        /// <summary>
        /// 
        /// </summary>
        public string Description => "终端上传音视频资源列表";
        /// <summary>
        /// 
        /// </summary>
        public ushort MsgId => 0x1205;
        /// <summary>
        /// 流水号
        /// </summary>
        public ushort MsgNum { get; set; }
        /// <summary>
        /// 音视频资源总数
        /// </summary>
        public uint AVResouceTotal{ get; set; }
        /// <summary>
        /// 音视频资源列表
        /// </summary>
        public List<JT808_0x1205_AVResouce> AVResouces { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x1205 value = new JT808_0x1205();
            value.MsgNum = reader.ReadUInt16();
            writer.WriteNumber($"[{value.MsgNum.ReadNumber()}]流水号", value.MsgNum);
            value.AVResouceTotal = reader.ReadUInt32();
            writer.WriteNumber($"[{value.AVResouceTotal.ReadNumber()}]音视频资源总数", value.AVResouceTotal);
            writer.WriteStartArray("音视频资源列表");
            var formatter = config.GetMessagePackFormatter<JT808_0x1205_AVResouce>();
            for (int i = 0; i < value.AVResouceTotal; i++)
            {
                writer.WriteStartObject();
                formatter.Analyze(ref reader, writer, config);
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
        public override JT808_0x1205 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x1205 jT808_0x1205 = new JT808_0x1205();
            jT808_0x1205.MsgNum = reader.ReadUInt16();
            jT808_0x1205.AVResouceTotal = reader.ReadUInt32();
            var channelTotal = jT808_0x1205.AVResouceTotal;//音视频资源总数
            if (channelTotal > 0)
            {
                jT808_0x1205.AVResouces = new List<JT808_0x1205_AVResouce>();
                var formatter = config.GetMessagePackFormatter<JT808_0x1205_AVResouce>();
                for (int i = 0; i < channelTotal; i++)
                {
                    jT808_0x1205.AVResouces.Add(formatter.Deserialize(ref reader, config));
                }
            }
            return jT808_0x1205;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x1205 value, IJT808Config config)
        {
            writer.WriteUInt16(value.MsgNum);
            writer.WriteUInt32(value.AVResouceTotal);
            if (value.AVResouces.Any())
            {
                var formatter = config.GetMessagePackFormatter<JT808_0x1205_AVResouce>();
                foreach (var AVResouce in value.AVResouces)
                {
                    formatter.Serialize(ref writer, AVResouce, config);
                }
            }
        }
    }
}
