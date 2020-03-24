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
    /// 机动车驾驶证号码
    /// 返回：当前驾驶人的机动车驾驶证号码
    /// </summary>
    public class JT808_CarDVR_Up_0x01 : JT808CarDVRUpBodies, IJT808MessagePackFormatter<JT808_CarDVR_Up_0x01>, IJT808Analyze
    {
        public override byte CommandId =>  JT808CarDVRCommandID.采集当前驾驶人信息.ToByteValue();
        /// <summary>
        /// 机动车驾驶证号码
        /// 机动车驾驶证号码为 15 位时，后 3 位以 00H 补齐。驾驶人身份未知时以 00H 表示
        /// </summary>
        public string DriverLicenseNo { get; set; }
        public override string Description => "当前驾驶人的机动车驾驶证号码";

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_CarDVR_Up_0x01 value = new JT808_CarDVR_Up_0x01();
            var hex = reader.ReadVirtualArray(18);
            value.DriverLicenseNo = reader.ReadASCII(18);
            writer.WriteString($"[{hex.ToArray().ToHexString()}]当前驾驶人的机动车驾驶证号码", value.DriverLicenseNo);
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_CarDVR_Up_0x01 value, IJT808Config config)
        {
            var currentPosition = writer.GetCurrentPosition();
            writer.WriteASCII(value.DriverLicenseNo);
            writer.Skip(18 - (writer.GetCurrentPosition() - currentPosition), out var _);
        }

        public JT808_CarDVR_Up_0x01 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_CarDVR_Up_0x01 value = new JT808_CarDVR_Up_0x01();
            value.DriverLicenseNo = reader.ReadASCII(18);
            return value;
        }
    }
}
