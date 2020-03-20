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
    /// 采集车辆信息
    /// 返回：车辆识别代号、机动车号牌号码和机动车号牌分类
    /// </summary>
    public class JT808_CarDVR_Up_0x05 : JT808CarDVRUpBodies, IJT808Analyze
    {
        public override byte CommandId =>  JT808CarDVRCommandID.采集车辆信息.ToByteValue();
        /// <summary>
        /// 车辆识别代号
        /// </summary>
        public string Vin { get; set; }
        /// <summary>
        /// 机动车号牌号码
        /// 后 3 个字节为备用字
        /// </summary>
        public string VehicleNo { get; set; }
        /// <summary>
        /// 机动车号牌分类
        /// 后 4 个字节为备用字
        /// </summary>
        public string VehicleType { get; set; }
        public override string Description => "车辆识别代号、机动车号牌号码和机动车号牌分类";

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {

        }

        public override JT808CarDVRUpBodies Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_CarDVR_Up_0x05 value = new JT808_CarDVR_Up_0x05();
            value.Vin= reader.ReadASCII(17);
            value.VehicleNo = reader.ReadASCII(9);
            reader.Skip(3);
            value.VehicleType = reader.ReadString(6);
            reader.Skip(4);
            return value;
        }

        public override void Serialize(ref JT808MessagePackWriter writer, JT808CarDVRUpBodies jT808CarDVRUpBodies, IJT808Config config)
        {
            JT808_CarDVR_Up_0x05 value = jT808CarDVRUpBodies as JT808_CarDVR_Up_0x05;
            writer.WriteASCII(value.Vin.PadRight(17,'0'));
            writer.WriteASCII(value.VehicleNo);
            writer.Skip(12 - value.VehicleNo.Length, out var vehicleNo);
            writer.WriteString(value.VehicleType);
            writer.Skip(10 - value.VehicleType.Length, out var vehicleType);
        }
    }
}
