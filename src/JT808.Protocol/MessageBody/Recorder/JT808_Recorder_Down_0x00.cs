using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace JT808.Protocol.MessageBody.Recorder
{
    /// <summary>
    /// 采集记录仪执行标准版本
    /// 返回：记录仪执行标准的年号及修改单号
    /// </summary>
    public class JT808_Recorder_Down_0x00 : JT808_RecorderBody
    {
        public override byte CommandId =>0x00;
         
        public override string Description => "采集记录仪执行标准版本";

        public override bool SkipSerialization { get; set; } = true;

        public override JT808_RecorderBody Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(ref JT808MessagePackWriter writer, JT808_RecorderBody value, IJT808Config config)
        {
            throw new NotImplementedException();
        }
    }
}
