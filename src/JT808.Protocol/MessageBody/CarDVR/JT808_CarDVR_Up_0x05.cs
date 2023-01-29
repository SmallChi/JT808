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
    public class JT808_CarDVR_Up_0x05 : JT808MessagePackFormatter<JT808_CarDVR_Up_0x05>, JT808CarDVRUpBodies,  IJT808Analyze
    {
        /// <summary>
        /// 0x05
        /// </summary>
        public byte CommandId =>  JT808CarDVRCommandID.collect_vehicle_information.ToByteValue();
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
        /// <summary>
        /// 车辆识别代号、机动车号牌号码和机动车号牌分类
        /// </summary>
        public string Description => "车辆识别代号、机动车号牌号码和机动车号牌分类";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_CarDVR_Up_0x05 value = new JT808_CarDVR_Up_0x05();
            var hex = reader.ReadVirtualArray(17);
            value.Vin = reader.ReadASCII(17);
            writer.WriteString($"[{hex.ToArray().ToHexString()}]车辆识别代号", value.Vin);
            hex = reader.ReadVirtualArray(12);
            value.VehicleNo = reader.ReadString(12);
            writer.WriteString($"[{hex.ToArray().ToHexString()}]机动车号牌号码", value.VehicleNo);
            hex = reader.ReadVirtualArray(12);
            value.VehicleType = reader.ReadString(12);
            writer.WriteString($"[{hex.ToArray().ToHexString()}]机动车号牌分类", value.VehicleType);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_CarDVR_Up_0x05 value, IJT808Config config)
        {
            var currentPosition = writer.GetCurrentPosition();
            writer.WriteASCII(value.Vin);
            writer.Skip(17 - (writer.GetCurrentPosition()- currentPosition), out var _);
            currentPosition = writer.GetCurrentPosition();
            writer.WriteString(value.VehicleNo);
            writer.Skip(12 - (writer.GetCurrentPosition() - currentPosition), out var _);
            currentPosition = writer.GetCurrentPosition();
            writer.WriteString(value.VehicleType);
            writer.Skip(12 - (writer.GetCurrentPosition() - currentPosition), out var _);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_CarDVR_Up_0x05 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_CarDVR_Up_0x05 value = new JT808_CarDVR_Up_0x05();
            value.Vin = reader.ReadASCII(17);
            value.VehicleNo = reader.ReadString(12);
            value.VehicleType = reader.ReadString(12);
            return value;
        }
    }
}
