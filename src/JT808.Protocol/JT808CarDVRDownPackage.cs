using JT808.Protocol.Enums;
using JT808.Protocol.Exceptions;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace JT808.Protocol
{
    /// <summary>
    /// 行车记录仪下行数据包
    /// </summary>
    public class JT808CarDVRDownPackage : JT808MessagePackFormatter<JT808CarDVRDownPackage>, IJT808_CarDVR_Down_Package, IJT808Analyze
    {
        /// <summary>
        /// 头标识
        /// </summary>
        public const ushort BeginFlag = 0x557A;
        /// <summary>
        /// 起始字头
        /// </summary>
        public ushort Begin { get; set; } = BeginFlag;
        /// <summary>
        /// 命令字
        /// </summary>
        public byte CommandId { get; set; }
        /// <summary>
        /// 保留字段
        /// </summary>
        public byte KeepFields { get; set; } = 0x00;
        /// <summary>
        /// 数据块长度
        /// </summary>
        public ushort DataLength { get; set; }
        /// <summary>
        /// 记录仪体下行数据体
        /// </summary>
        public JT808CarDVRDownBodies Bodies { get; set; }
        /// <summary>
        /// 校验字
        /// </summary>
        public byte CheckCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808CarDVRDownPackage value, IJT808Config config)
        {
            var currentPosition = writer.GetCurrentPosition();
            writer.WriteUInt16(value.Begin);
            writer.WriteByte(value.CommandId);
            writer.Skip(2, out var datalengthPosition);
            writer.WriteByte(value.KeepFields);
            if (config.JT808_CarDVR_Down_Factory.Map.TryGetValue(value.CommandId, out var instance))
            {
                if (!value.Bodies?.SkipSerialization == true)
                {
                    //4.2.处理消息体
                    instance.SerializeExt(ref writer, value.Bodies, config);
                }
            }
            writer.WriteUInt16Return((ushort)(writer.GetCurrentPosition() - 2 - 1 - datalengthPosition), datalengthPosition);//此处-2：减去数据长度字段2位，-1：减去保留字长度
            writer.WriteCarDVRCheckCode(currentPosition);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808CarDVRDownPackage Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808CarDVRDownPackage value = new JT808CarDVRDownPackage();
            int currentPosition = reader.ReaderCount;
            value.Begin = reader.ReadUInt16();
            value.CommandId = reader.ReadByte();
            value.DataLength = reader.ReadUInt16();
            value.KeepFields = reader.ReadByte();
            if (value.DataLength > 0)
            {
                if (config.JT808_CarDVR_Down_Factory.Map.TryGetValue(value.CommandId, out var instance))
                {
                    //4.2.处理消息体
                    value.Bodies = instance.DeserializeExt<JT808CarDVRDownBodies>(ref reader, config);
                }
            }
            var carDVRCheckCode = reader.ReadCarDVRCheckCode(currentPosition);
            if (!config.SkipCarDVRCRCCode)
            {
                if (carDVRCheckCode.RealXorCheckCode != carDVRCheckCode.CalculateXorCheckCode)
                    throw new JT808Exception(JT808ErrorCode.CarDVRCheckCodeNotEqual, $"{reader.RealCheckXorCode}!={reader.CalculateCheckXorCode}");
            }
            value.CheckCode = reader.ReadByte();
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808CarDVRDownPackage value = new JT808CarDVRDownPackage();
            writer.WriteStartObject("行车记录仪下行数据包");
            int currentPosition = reader.ReaderCount;
            value.Begin = reader.ReadUInt16();
            writer.WriteNumber($"[{value.Begin.ReadNumber()}]起始字头", value.Begin);
            value.CommandId = reader.ReadByte();
            writer.WriteString($"[{value.Begin.ReadNumber()}]命令字", ((JT808CarDVRCommandID)value.CommandId).ToString());
            value.DataLength = reader.ReadUInt16();
            writer.WriteNumber($"[{value.DataLength.ReadNumber()}]数据块长度", value.DataLength);
            value.KeepFields = reader.ReadByte();
            writer.WriteNumber($"[{value.KeepFields.ReadNumber()}]保留字", value.KeepFields);
            if (value.DataLength > 0)
            {
                if (config.JT808_CarDVR_Down_Factory.Map.TryGetValue(value.CommandId, out var instance))
                {
                    //4.2.处理消息体
                    writer.WriteStartObject(((JT808CarDVRCommandID)value.CommandId).ToString());
                    instance.Analyze(ref reader, writer, config);
                    writer.WriteEndObject();
                }
            }
            var carDVRCheckCode = reader.ReadCarDVRCheckCode(currentPosition);
            value.CheckCode = reader.ReadByte();
            if (carDVRCheckCode.RealXorCheckCode != carDVRCheckCode.CalculateXorCheckCode)
            {
                writer.WriteString($"[{value.CheckCode.ReadNumber()}]校验位错误", $"{reader.RealCheckXorCode}!={reader.CalculateCheckXorCode}");
            }
            else
            {
                writer.WriteNumber($"[{value.CheckCode.ReadNumber()}]校验位", value.CheckCode);
            }
            writer.WriteEndObject();
        }
    }
}
