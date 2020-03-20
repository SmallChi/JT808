using JT808.Protocol.Enums;
using JT808.Protocol.Exceptions;
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
    public class JT808CarDVRUpPackage : IJT808MessagePackFormatter<JT808CarDVRUpPackage>, IJT808Analyze
    {
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

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            throw new NotImplementedException();
        }

        public JT808CarDVRUpPackage Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808CarDVRUpPackage value = new JT808CarDVRUpPackage();
            int currentPosition = reader.ReaderCount;
            value.Begin = reader.ReadUInt16();
            value.CommandId = reader.ReadByte();
            value.DataLength = reader.ReadUInt16();
            value.KeepFields = reader.ReadByte();
            if (value.DataLength > 0)
            {
                if (config.JT808_CarDVR_Up_Factory.Map.TryGetValue(value.CommandId, out var instance))
                {
                    //4.2.处理消息体
                    value.Bodies = instance.Deserialize(ref reader, config);
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

        public void Serialize(ref JT808MessagePackWriter writer, JT808CarDVRUpPackage value, IJT808Config config)
        {
            var currentPosition = writer.GetCurrentPosition();
            writer.WriteUInt16(value.Begin);
            writer.WriteByte(value.CommandId);
            writer.Skip(2, out var datalengthPosition);
            writer.WriteByte(value.KeepFields);
            if (config.JT808_CarDVR_Up_Factory.Map.TryGetValue(value.CommandId, out var instance))
            {
                if (!instance.SkipSerialization)
                {
                    //4.2.处理消息体
                    instance.Serialize(ref writer, value.Bodies, config);
                }
            }
            writer.WriteUInt16Return((ushort)(writer.GetCurrentPosition() - datalengthPosition + 1), datalengthPosition);
            writer.WriteCarDVRCheckCode(currentPosition);
        }
    }
}
