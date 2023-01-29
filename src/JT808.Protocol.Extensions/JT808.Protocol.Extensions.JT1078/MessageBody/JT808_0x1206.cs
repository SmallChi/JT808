using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 文件上传完成通知
    /// </summary>
    public class JT808_0x1206 : JT808MessagePackFormatter<JT808_0x1206>, JT808Bodies,  IJT808Analyze
    {
        /// <summary>
        /// 
        /// </summary>
        public string Description => "文件上传完成通知";
        /// <summary>
        /// 
        /// </summary>
        public ushort MsgId => 0x1206;
        /// <summary>
        /// 流水号
        /// </summary>
        public ushort MsgNum { get; set; }
        /// <summary>
        /// 结果
        /// </summary>
        public byte Result{ get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x1206 value = new JT808_0x1206();
            value.MsgNum = reader.ReadUInt16();
            writer.WriteNumber($"[{value.MsgNum.ReadNumber()}]流水号", value.MsgNum);
            value.Result = reader.ReadByte();
            writer.WriteString($"[{value.Result.ReadNumber()}]结果", ResultDisplay(value.Result));
            string ResultDisplay(byte Result) {
                switch (Result)
                {
                    case 0:
                        return "成功";
                    case 1:
                        return "失败";
                    default:
                        return "未知";
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x1206 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x1206 jT808_0x1206 = new JT808_0x1206();
            jT808_0x1206.MsgNum = reader.ReadUInt16();
            jT808_0x1206.Result = reader.ReadByte();
            return jT808_0x1206;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x1206 value, IJT808Config config)
        {
            writer.WriteUInt16(value.MsgNum);
            writer.WriteByte(value.Result);
        }
    }
}
