using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace JT808.Protocol.MessageBody.Recorder
{
    /// <summary>
    /// 记录仪
    /// </summary>
    public class JT808_RecorderPackage :  IJT808MessagePackFormatter<JT808_RecorderPackage>, IJT808Analyze
    {
        public const ushort BeginFlag = 0x557A;
        /// <summary>
        /// 起始字头
        /// </summary>
        public ushort Begin { get; set; } = BeginFlag;
        /// <summary>
        /// 记录仪头部
        /// </summary>
        public JT808_RecorderHeader JT808_RecorderHeader { get; set; }
        /// <summary>
        /// 保留字段
        /// </summary>
        public byte KeepFields { get; set; } = 0x00;
        /// <summary>
        /// 记录仪体
        /// </summary>
        public JT808_RecorderBody JT808_RecorderBody { get; set; }
        /// <summary>
        /// 校验字
        /// </summary>
        public byte CheckCode { get; set; }
        /// <summary>
        /// 计算的异或校验码
        /// </summary>
        public byte CalculateCheckXorCode { get; set; } = 0;
        /// <summary>
        /// 跳过数据体序列化
        /// 默认不跳过
        /// 当数据体为空的时候，使用null作为空包感觉不适合，所以就算使用空包也需要new一下来表达意思。
        /// </summary>
        public virtual bool SkipSerialization { get; set; } = false;

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            throw new NotImplementedException();
        }

        public JT808_RecorderPackage Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_RecorderPackage value = new JT808_RecorderPackage();
            value.CalculateCheckXorCode = CalculateXorCheckCode(reader);
            value.Begin = reader.ReadUInt16();
            value.JT808_RecorderHeader = new JT808_RecorderHeader().Deserialize(ref reader, config);
            value.KeepFields = reader.ReadByte();
            if (value.JT808_RecorderHeader.DataLength > 0)
            {
                if (config.IJT808_Recorder_Factory.Map.TryGetValue(value.JT808_RecorderHeader.CommandId, out var instance))
                {
                    //4.2.处理消息体
                    value.JT808_RecorderBody = instance.Deserialize(ref reader, config);
                }
            }
            value.CheckCode = reader.ReadByte();
            return value; 
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_RecorderPackage value, IJT808Config config)
        {
            var currentPosition = writer.GetCurrentPosition();
            writer.WriteUInt16(value.Begin);
            value.JT808_RecorderHeader.Serialize(ref writer, value.JT808_RecorderHeader, config);
            writer.WriteByte(value.KeepFields);
            if (value.JT808_RecorderHeader.DataLength > 0) {
                if (config.IJT808_Recorder_Factory.Map.TryGetValue(value.JT808_RecorderHeader.CommandId, out var instance))
                {
                    if (!instance.SkipSerialization)
                    {
                        //4.2.处理消息体
                        instance.Serialize(ref writer, value.JT808_RecorderBody, config);
                    }
                }
            }
            writer.WriteByte(CalculateXorCheckCode(writer.FlushAndGetRealReadOnlySpan().Slice(currentPosition, writer.GetCurrentPosition() - currentPosition + 1)));
        }
        /// <summary>
        /// 计算校验码
        /// </summary>
        /// <param name="readOnlySpan"></param>
        /// <returns></returns>
        private byte CalculateXorCheckCode(JT808MessagePackReader reader) {
            var header = reader.GetVirtualReadOnlySpan(5);
            int xorByteLength = 5+1 + BinaryPrimitives.ReadInt16BigEndian(header.Slice(3));
            var xorReadOnlySpan = reader.GetVirtualReadOnlySpan(xorByteLength);            
            return CalculateXorCheckCode(xorReadOnlySpan);
        }
        /// <summary>
        /// 计算校验码
        /// </summary>
        /// <param name="xorReadOnlySpan"></param>
        /// <returns></returns>
        private byte CalculateXorCheckCode(ReadOnlySpan<byte> xorReadOnlySpan) {
            byte calculateXorCheckCode = 0;
            foreach (var item in xorReadOnlySpan)
            {
                calculateXorCheckCode = (byte)(calculateXorCheckCode ^ item);
            }
            return calculateXorCheckCode;
        }
    }
}