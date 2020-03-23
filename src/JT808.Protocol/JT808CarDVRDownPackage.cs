using JT808.Protocol.Enums;
using JT808.Protocol.Exceptions;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol
{
    public  class JT808CarDVRDownPackage : IJT808MessagePackFormatter<JT808CarDVRDownPackage>
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

        public void Serialize(ref JT808MessagePackWriter writer, JT808CarDVRDownPackage value, IJT808Config config)
        {
            var currentPosition = writer.GetCurrentPosition();
            writer.WriteUInt16(value.Begin);
            writer.WriteByte(value.CommandId);
            writer.Skip(2, out var datalengthPosition);
            writer.WriteByte(value.KeepFields);
            if (config.JT808_CarDVR_Down_Factory.Map.TryGetValue(value.CommandId, out var instance))
            {                
                if (!value.Bodies.SkipSerialization)
                {
                    //4.2.处理消息体
                    JT808MessagePackFormatterResolverExtensions.JT808DynamicSerialize(instance, ref writer, value.Bodies, config);
                }
            }
            writer.WriteUInt16Return((ushort)(writer.GetCurrentPosition() -2-1- datalengthPosition), datalengthPosition);//此处-2：减去数据长度字段2位，-1：减去保留字长度
            writer.WriteCarDVRCheckCode(currentPosition);
        }

        public JT808CarDVRDownPackage Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
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
                    dynamic attachImpl = JT808MessagePackFormatterResolverExtensions.JT808DynamicDeserialize(instance, ref reader, config);
                    value.Bodies = attachImpl;
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
    }
}
