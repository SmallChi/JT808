using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessagePack;
using System.Text.Json;

namespace JT808.Protocol.Extensions.YueBiao.MessageBody
{
    /// <summary>
    /// 胎压监测系统参数
    /// </summary>
    public class JT808_0x8103_0xF366 : JT808MessagePackFormatter<JT808_0x8103_0xF366>, JT808_0x8103_BodyBase,  IJT808Analyze, IJT808_2019_Version
    {
        /// <summary>
        /// 胎压监测系统参数Id
        /// </summary>
        public uint ParamId { get; set; } = JT808_YueBiao_Constants.JT808_0X8103_0xF366;
        /// <summary>
        /// 胎压监测系统参数长度
        /// </summary>
        public byte ParamLength { get; set; } = 46;
        /// <summary>
        /// 轮胎规格型号 12位
        /// </summary>
        public string TyreSpecificationType { get; set; }
        /// <summary>
        /// 胎压单位
        /// </summary>
        public ushort TyrePressureUnit { get; set; }
        /// <summary>
        /// 正常胎压值
        /// </summary>
        public ushort NormalFetalPressure { get; set; }
        /// <summary>
        /// 胎压不平衡门限
        /// </summary>
        public ushort ThresholdUnbalancedTirePressure { get; set; }
        /// <summary>
        /// 慢漏气门限
        /// </summary>
        public ushort SlowLeakageThreshold { get; set; }
        /// <summary>
        /// 低压阈值
        /// </summary>
        public ushort LowVoltageThreshold { get; set; }
        /// <summary>
        /// 高压阈值
        /// </summary>
        public ushort HighVoltageThreshold { get; set; }
        /// <summary>
        /// 高温阈值
        /// </summary>
        public ushort HighTemperatureThreshold { get; set; }
        /// <summary>
        /// 电压阈值
        /// </summary>
        public ushort VoltageThreshold { get; set; }
        /// <summary>
        /// 定时上报时间间隔
        /// </summary>
        public ushort TimedReportingInterval { get; set; }
        /// <summary>
        /// 保留项
        /// </summary>
        public byte[] Retain { get; set; } = new byte[6];
        /// <summary>
        /// 胎压监测系统参数
        /// </summary>
        public string Description => "胎压监测系统参数";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8103_0xF366 value = new JT808_0x8103_0xF366();
            value.ParamId = reader.ReadUInt32();
            value.ParamLength = reader.ReadByte();
            writer.WriteNumber($"[{value.ParamId.ReadNumber()}]参数ID", value.ParamId);
            writer.WriteNumber($"[{value.ParamLength.ReadNumber()}]参数长度", value.ParamLength);
            string tyreSpecificationTypeHex = reader.ReadVirtualArray(12).ToArray().ToHexString();
            value.TyreSpecificationType = reader.ReadString(12);
            writer.WriteString($"[{tyreSpecificationTypeHex}]轮胎规格型号", value.TyreSpecificationType);
            value.TyrePressureUnit = reader.ReadUInt16();
            writer.WriteNumber($"[{value.TyrePressureUnit.ReadNumber()}]胎压单位", value.TyrePressureUnit);
            value.NormalFetalPressure = reader.ReadUInt16();
            writer.WriteNumber($"[{value.NormalFetalPressure.ReadNumber()}]正常胎压值", value.NormalFetalPressure);
            value.ThresholdUnbalancedTirePressure = reader.ReadUInt16();
            writer.WriteNumber($"[{value.ThresholdUnbalancedTirePressure.ReadNumber()}]胎压不平衡门限", value.ThresholdUnbalancedTirePressure);
            value.SlowLeakageThreshold = reader.ReadUInt16();
            writer.WriteNumber($"[{value.SlowLeakageThreshold.ReadNumber()}]慢漏气门限", value.SlowLeakageThreshold);
            value.LowVoltageThreshold = reader.ReadUInt16();
            writer.WriteNumber($"[{value.LowVoltageThreshold.ReadNumber()}]低压阈值", value.LowVoltageThreshold);
            value.HighVoltageThreshold = reader.ReadUInt16();
            writer.WriteNumber($"[{value.HighVoltageThreshold.ReadNumber()}]高压阈值", value.HighVoltageThreshold);
            value.HighTemperatureThreshold = reader.ReadUInt16();
            writer.WriteNumber($"[{value.HighTemperatureThreshold.ReadNumber()}]高温阈值", value.HighTemperatureThreshold);
            value.VoltageThreshold = reader.ReadUInt16();
            writer.WriteNumber($"[{value.VoltageThreshold.ReadNumber()}]电压阈值", value.VoltageThreshold);
            value.TimedReportingInterval = reader.ReadUInt16();
            writer.WriteNumber($"[{value.TimedReportingInterval.ReadNumber()}]定时上报时间间隔", value.TimedReportingInterval);
            value.Retain = reader.ReadArray(reader.ReadCurrentRemainContentLength()).ToArray();
            writer.WriteString("保留项", value.Retain.ToHexString());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8103_0xF366 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0xF366 value = new JT808_0x8103_0xF366();
            value.ParamId = reader.ReadUInt32();
            value.ParamLength = reader.ReadByte();
            value.TyreSpecificationType = reader.ReadASCII(12);
            value.TyrePressureUnit = reader.ReadUInt16();
            value.NormalFetalPressure = reader.ReadUInt16();
            value.ThresholdUnbalancedTirePressure = reader.ReadUInt16();
            value.SlowLeakageThreshold = reader.ReadUInt16();
            value.LowVoltageThreshold = reader.ReadUInt16();
            value.HighVoltageThreshold = reader.ReadUInt16();
            value.HighTemperatureThreshold = reader.ReadUInt16();
            value.VoltageThreshold = reader.ReadUInt16();
            value.TimedReportingInterval = reader.ReadUInt16();
            value.Retain = reader.ReadArray(reader.ReadCurrentRemainContentLength()).ToArray();
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0xF366 value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.Skip(1, out int ParamLengthPosition);
            writer.WriteASCII(value.TyreSpecificationType);
            writer.WriteUInt16(value.TyrePressureUnit);
            writer.WriteUInt16(value.NormalFetalPressure);
            writer.WriteUInt16(value.ThresholdUnbalancedTirePressure);
            writer.WriteUInt16(value.SlowLeakageThreshold);
            writer.WriteUInt16(value.LowVoltageThreshold);
            writer.WriteUInt16(value.HighVoltageThreshold);
            writer.WriteUInt16(value.HighTemperatureThreshold);
            writer.WriteUInt16(value.VoltageThreshold);
            writer.WriteUInt16(value.TimedReportingInterval);
            writer.WriteArray(value.Retain);
            writer.WriteByteReturn((byte)(writer.GetCurrentPosition() - ParamLengthPosition - 1), ParamLengthPosition);
        }
    }
}
