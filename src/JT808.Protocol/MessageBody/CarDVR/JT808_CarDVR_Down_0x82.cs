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
    public class JT808_CarDVR_Down_0x82 : JT808CarDVRDownBodies, IJT808MessagePackFormatter<JT808_CarDVR_Down_0x82>, IJT808Analyze
    {
        public override byte CommandId => JT808CarDVRCommandID.设置车辆信息.ToByteValue();
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
        public override string Description => "车辆信息";

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {

        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_CarDVR_Down_0x82 value, IJT808Config config)
        {
            writer.WriteASCII(value.Vin.PadRight(17, '0'));
            writer.WriteASCII(value.VehicleNo.PadRight(9, '0'));
            writer.Skip(3, out var reversed1);
            writer.WriteString(value.VehicleType.PadRight(6, '0'));
            writer.Skip(4, out var reversed2);
        }

        public JT808_CarDVR_Down_0x82 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_CarDVR_Down_0x82 value = new JT808_CarDVR_Down_0x82();
            value.Vin = reader.ReadASCII(17);
            value.VehicleNo = reader.ReadASCII(9);
            reader.Skip(3);
            value.VehicleType = reader.ReadString(6);
            reader.Skip(4);
            return value;
        }
    }
}
