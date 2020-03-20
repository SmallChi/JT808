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
    public class JT808_CarDVR_Up_0x07 : JT808CarDVRUpBodies, IJT808Analyze
    {
        public override byte CommandId => JT808CarDVRCommandID.采集记录仪唯一性编号.ToByteValue();
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
        public override string Description => "唯一性编号及初次安装日期";

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {

        }

        public override JT808CarDVRUpBodies Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_CarDVR_Up_0x07 value = new JT808_CarDVR_Up_0x07();
            value.ProductionPlantCCCCertificationCode = reader.ReadASCII(7);
            value.CertifiedProductModels = reader.ReadASCII(16);
            value.ProductionDate = reader.ReadDateTime3();
            value.ProductProductionFlowNumber = reader.ReadASCII(4);
            value.Reversed = reader.ReadASCII(5);
            return value;
        }

        public override void Serialize(ref JT808MessagePackWriter writer, JT808CarDVRUpBodies jT808CarDVRUpBodies, IJT808Config config)
        {
            JT808_CarDVR_Up_0x07 value = jT808CarDVRUpBodies as JT808_CarDVR_Up_0x07;
            writer.WriteASCII(value.ProductionPlantCCCCertificationCode.PadRight(7,'0'));
            writer.WriteASCII(value.CertifiedProductModels.PadRight(16, '0'));
            writer.WriteDateTime3(value.ProductionDate);
            writer.WriteASCII(value.ProductProductionFlowNumber.PadRight(4,'0'));
            writer.WriteASCII(value.Reversed.PadRight(5, '0'));
        }
    }
}
