using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace JT808.Protocol.MessageBody.CarDVR
{
    /// <summary>
    /// 设置初始里程
    /// 返回：记录仪初次安装时车辆已行驶的总里程
    /// </summary>
    public class JT808_CarDVR_Down_0xC4 : JT808CarDVRUpBodies, IJT808Analyze
    {
        public override byte CommandId =>  JT808CarDVRCommandID.设置初始里程.ToByteValue();
        public override string Description => "车辆识别代号、机动车号牌号码和机动车号牌分类";

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {

        }

        public override JT808CarDVRUpBodies Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_CarDVR_Up_0xC4 value = new JT808_CarDVR_Up_0xC4();
            return value;
        }

        public override void Serialize(ref JT808MessagePackWriter writer, JT808CarDVRUpBodies jT808CarDVRUpBodies, IJT808Config config)
        {
            JT808_CarDVR_Up_0xC4 value = jT808CarDVRUpBodies as JT808_CarDVR_Up_0xC4;
        }
    }
}
