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
    /// 采集记录仪唯一性编号
    /// 返回：唯一性编号及初次安装日期
    /// </summary>
    public class JT808_CarDVR_Up_0x07 : JT808MessagePackFormatter<JT808_CarDVR_Up_0x07>, JT808CarDVRUpBodies,  IJT808Analyze
    {
        /// <summary>
        /// 0x07
        /// </summary>
        public byte CommandId => JT808CarDVRCommandID.collect_recorder_unique_number.ToByteValue();
        /// <summary>
        /// 生产厂 CCC 认证代码  7字节
        /// </summary>
        public string ProductionPlantCCCCertificationCode { get; set; }
        /// <summary>
        /// 认证产品型号  16字节
        /// </summary>
        public string CertifiedProductModels { get; set; }
        /// <summary>
        /// 生产日期  3字节
        /// </summary>
        public DateTime ProductionDate { get; set; }
        /// <summary>
        /// 产品生产流水号  4字节
        /// </summary>
        public string ProductProductionFlowNumber{get;set;}
        /// <summary>
        /// 备用  5字节
        /// </summary>
        public string Reversed { get; set; }
        /// <summary>
        /// 唯一性编号及初次安装日期
        /// </summary>
        public string Description => "唯一性编号及初次安装日期";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_CarDVR_Up_0x07 value = new JT808_CarDVR_Up_0x07();
            var hex = reader.ReadVirtualArray(7);
            value.ProductionPlantCCCCertificationCode = reader.ReadASCII(7);
            writer.WriteString($"[{hex.ToArray().ToHexString()}]生产厂 CCC 认证代码", value.ProductionPlantCCCCertificationCode);
            hex = reader.ReadVirtualArray(16);
            value.CertifiedProductModels = reader.ReadASCII(16);
            writer.WriteString($"[{hex.ToArray().ToHexString()}]认证产品型号", value.CertifiedProductModels);
            hex = reader.ReadVirtualArray(3);
            value.ProductionDate = reader.ReadDateTime_YYMMDD();
            writer.WriteString($"[{hex.ToArray().ToHexString()}]生产日期", value.ProductionDate);
            hex = reader.ReadVirtualArray(4);
            value.ProductProductionFlowNumber = reader.ReadString(4);
            writer.WriteString($"[{hex.ToArray().ToHexString()}]产品生产流水号", value.ProductProductionFlowNumber);
            hex = reader.ReadVirtualArray(5);
            value.Reversed = reader.ReadString(5);
            writer.WriteString($"[{hex.ToArray().ToHexString()}]备用", value.Reversed);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_CarDVR_Up_0x07 value, IJT808Config config)
        {
            var currentPosition = writer.GetCurrentPosition();
            writer.WriteASCII(value.ProductionPlantCCCCertificationCode);
            writer.Skip(7 - (writer.GetCurrentPosition()- currentPosition), out var _);
            currentPosition = writer.GetCurrentPosition();
            writer.WriteASCII(value.CertifiedProductModels);
            writer.Skip(16 - (writer.GetCurrentPosition()- currentPosition), out var _);
            writer.WriteDateTime_YYMMDD(value.ProductionDate);
            currentPosition = writer.GetCurrentPosition();
            writer.WriteString(value.ProductProductionFlowNumber);
            writer.Skip(4 - (writer.GetCurrentPosition() - currentPosition), out var _);
            currentPosition = writer.GetCurrentPosition();
            writer.WriteString(value.Reversed);
            writer.Skip(5 - (writer.GetCurrentPosition() - currentPosition), out var _);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_CarDVR_Up_0x07 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_CarDVR_Up_0x07 value = new JT808_CarDVR_Up_0x07();
            value.ProductionPlantCCCCertificationCode = reader.ReadASCII(7);
            value.CertifiedProductModels = reader.ReadASCII(16);
            value.ProductionDate = reader.ReadDateTime_YYMMDD();
            value.ProductProductionFlowNumber = reader.ReadString(4);
            value.Reversed = reader.ReadString(5);
            return value;
        }
    }
}
