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
            var carDVRCheckCode = reader.ReadCarDVRCheckCode(currentPosition, value.DataLength);
            //todo:定义一个行车记录仪的异常和跳过校验的配置属性
            //比如:config.SkipCRCCode
            //if (carDVRCheckCode.RealXorCheckCode != carDVRCheckCode.CalculateXorCheckCode)
            value.KeepFields = reader.ReadByte();
            //todo:数据体
            value.CheckCode = reader.ReadByte();
            return value;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808CarDVRUpPackage value, IJT808Config config)
        {
            throw new NotImplementedException();
        }
    }
}
