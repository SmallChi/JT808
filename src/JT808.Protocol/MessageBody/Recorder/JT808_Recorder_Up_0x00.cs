using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace JT808.Protocol.MessageBody.Recorder
{
    /// <summary>
    /// 采集记录仪执行标准版本
    /// 返回：记录仪执行标准的年号及修改单号
    /// </summary>
    public class JT808_Recorder_Up_0x00 : JT808_RecorderBody, IJT808Analyze
    {
        public override byte CommandId => 0x00;
        /// <summary>
        /// 记录仪执行标准年号后 2 位  BCD 码
        /// 无应答则默认为 03
        /// </summary>
        public string StandardYear { get; set; }
        /// <summary>
        /// 修改单号
        /// 无修改单或无应答则默认为 00H
        /// </summary>
        public byte ModifyNumber { get; set; }
        public override string Description => "采集记录仪执行标准版本应答";

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {

        }

        public override JT808_RecorderBody Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_Recorder_Up_0x00 value = new JT808_Recorder_Up_0x00();
            value.StandardYear = reader.ReadBCD(2);
            value.ModifyNumber = reader.ReadByte();
            return value;
        }

        public override void Serialize(ref JT808MessagePackWriter writer, JT808_RecorderBody jT808_RecorderBody, IJT808Config config)
        {
            JT808_Recorder_Up_0x00 value = jT808_RecorderBody as JT808_Recorder_Up_0x00;
            writer.WriteBCD(value.StandardYear, 2);
            writer.WriteByte(value.ModifyNumber);
        }

    }
}
