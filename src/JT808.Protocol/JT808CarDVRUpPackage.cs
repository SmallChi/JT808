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
    /// 行车记录仪上行数据包
    /// </summary>
    public class JT808CarDVRUpPackage : JT808MessagePackFormatter<JT808CarDVRUpPackage>, IJT808_CarDVR_Up_Package, IJT808Analyze
    {
        /// <summary>
        /// 起始字头
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
        /// 错误标志
        /// CommandId == 0xFA || CommandId == 0xFB
        /// </summary>
        public bool ErrorFlag { get; set; }
        /// <summary>
        /// 数据块长度
        /// </summary>
        public ushort DataLength { get; set; }
        /// <summary>
        /// 保留字段
        /// </summary>
        public byte KeepFields { get; set; } = 0x00;
        /// <summary>
        /// 记录仪体上行数据体
        /// </summary>
        public JT808CarDVRUpBodies Bodies { get; set; }
        /// <summary>
        /// 校验字
        /// </summary>
        public byte CheckCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808CarDVRUpPackage value = new JT808CarDVRUpPackage();
            writer.WriteStartObject("行车记录仪上行数据包");
            int currentPosition = reader.ReaderCount;
            value.Begin = reader.ReadUInt16();
            writer.WriteNumber($"[{value.Begin.ReadNumber()}]起始字头", value.Begin);
            value.CommandId = reader.ReadByte();
            //出错标志位
            value.ErrorFlag = value.CommandId == 0xFA || value.CommandId == 0xFB;
            if (!value.ErrorFlag)
            {
                writer.WriteString($"[{value.CommandId.ReadNumber()}]命令字", ((JT808CarDVRCommandID)value.CommandId).ToString());
                value.DataLength = reader.ReadUInt16();
                writer.WriteNumber($"[{value.DataLength.ReadNumber()}]数据块长度", value.DataLength);
            }
            else
            {
                writer.WriteString($"[{value.CommandId.ReadNumber()}]出错标志字", value.CommandId.ToString());
            }
            value.KeepFields = reader.ReadByte();
            writer.WriteNumber($"[{value.KeepFields.ReadNumber()}]保留字", value.KeepFields);
            if (value.DataLength > 0)
            {
                if (config.JT808_CarDVR_Up_Factory.Map.TryGetValue(value.CommandId, out var instance))
                {
                    //4.2.处理消息体
                    writer.WriteStartObject(((JT808CarDVRCommandID)value.CommandId).ToString());
                    instance.Analyze(ref reader,writer, config);
                    writer.WriteEndObject();
                }
            }
            var (CalculateXorCheckCode, RealXorCheckCode) = reader.ReadCarDVRCheckCode(currentPosition);
            value.CheckCode = reader.ReadByte();
            if (RealXorCheckCode != CalculateXorCheckCode)
            {
                writer.WriteString($"[{value.CheckCode.ReadNumber()}]校验位错误", $"{reader.RealCheckXorCode}!={reader.CalculateCheckXorCode}");
            }
            else
            {
                writer.WriteNumber($"[{value.CheckCode.ReadNumber()}]校验位", value.CheckCode);
            }
            writer.WriteEndObject();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808CarDVRUpPackage Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808CarDVRUpPackage value = new JT808CarDVRUpPackage();
            int currentPosition = reader.ReaderCount;
            value.Begin = reader.ReadUInt16();
            value.CommandId = reader.ReadByte();
            //出错标志位
            value.ErrorFlag = value.CommandId == 0xFA || value.CommandId == 0xFB;
            if (!value.ErrorFlag)
            {
                value.DataLength = reader.ReadUInt16();
            }
            value.KeepFields = reader.ReadByte();
            if (value.DataLength > 0)
            {
                if (config.JT808_CarDVR_Up_Factory.Map.TryGetValue(value.CommandId, out var instance))
                {
                    //4.2.处理消息体
                    value.Bodies = instance.DeserializeExt<JT808CarDVRUpBodies>(ref reader, config);
                }
            }
            var (CalculateXorCheckCode, RealXorCheckCode) = reader.ReadCarDVRCheckCode(currentPosition);
            if (!config.SkipCarDVRCRCCode)
            {
                if (RealXorCheckCode != CalculateXorCheckCode)
                    throw new JT808Exception(JT808ErrorCode.CarDVRCheckCodeNotEqual, $"{reader.RealCheckXorCode}!={reader.CalculateCheckXorCode}");
            }
            value.CheckCode = reader.ReadByte();
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808CarDVRUpPackage value, IJT808Config config)
        {
            var currentPosition = writer.GetCurrentPosition();
            writer.WriteUInt16(value.Begin);
            writer.WriteByte(value.CommandId);
            var isError = value.CommandId == 0xFA || value.CommandId == 0xFB;
            int datalengthPosition=0;
            if (!isError)
            {
                writer.Skip(2, out datalengthPosition);
            }
            writer.WriteByte(value.KeepFields);
            if (datalengthPosition > 0)
            {
                if (config.JT808_CarDVR_Up_Factory.Map.TryGetValue(value.CommandId, out var instance))
                {
                    if (!value.Bodies.SkipSerialization)
                    {
                        //4.2.处理消息体
                        instance.SerializeExt(ref writer, value.Bodies, config);
                    }
                }
                writer.WriteUInt16Return((ushort)(writer.GetCurrentPosition() - 2 - 1 - datalengthPosition), datalengthPosition);//此处-2：减去数据长度字段2位，-1：减去保留字长度
            }
            writer.WriteCarDVRCheckCode(currentPosition);
        }
    }
}
