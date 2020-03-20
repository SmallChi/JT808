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
    /// 设置车辆信息
    /// 返回：车辆信息
    /// </summary>
    public class JT808_CarDVR_Up_0x82 : JT808CarDVRUpBodies, IJT808Analyze
    {
        public override byte CommandId => JT808CarDVRCommandID.设置车辆信息.ToByteValue();

        public override string Description => "车辆信息";

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {

        }

        public override JT808CarDVRUpBodies Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_CarDVR_Up_0x82 value = new JT808_CarDVR_Up_0x82();

            return value;
        }

        public override void Serialize(ref JT808MessagePackWriter writer, JT808CarDVRUpBodies jT808CarDVRUpBodies, IJT808Config config)
        {
            JT808_CarDVR_Up_0x82 value = jT808CarDVRUpBodies as JT808_CarDVR_Up_0x82;

        }

    }
}
