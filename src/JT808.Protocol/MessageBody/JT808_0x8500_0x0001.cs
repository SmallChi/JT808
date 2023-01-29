using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 控制类型
    /// </summary>
    public  class JT808_0x8500_0x0001 : JT808MessagePackFormatter<JT808_0x8500_0x0001>, JT808_0x8500_ControlType,  IJT808Analyze
    {
        /// <summary>
        /// 0x0001
        /// </summary>
        public ushort ControlTypeId { get; set; } = 0x0001;
        /// <summary>
        /// 控制类型参数
        /// </summary>

        public byte ControlTypeParameter { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8500_0x0001 value = new JT808_0x8500_0x0001();
            value.ControlTypeId = reader.ReadUInt16();
            writer.WriteNumber($"[{ value.ControlTypeId.ReadNumber()}]控制类型Id", value.ControlTypeId);
            value.ControlTypeParameter = reader.ReadByte();
            writer.WriteNumber($"[{ value.ControlTypeParameter.ReadNumber()}]控制类型参数", value.ControlTypeParameter);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8500_0x0001 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8500_0x0001 value = new JT808_0x8500_0x0001();
            value.ControlTypeId = reader.ReadUInt16();
            value.ControlTypeParameter = reader.ReadByte();
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8500_0x0001 value, IJT808Config config)
        {
            writer.WriteUInt16(value.ControlTypeId);
            writer.WriteByte(value.ControlTypeParameter);
        }
    }
}
