using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 控制类型
    /// </summary>
    public  class JT808_0x8500_0x0001 : JT808_0x8500_ControlType, IJT808MessagePackFormatter<JT808_0x8500_0x0001>
    {
        public override ushort ControlTypeId { get; set; } = 0x0001;

        public byte ControlTypeParameter { get; set; }

        public JT808_0x8500_0x0001 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8500_0x0001 value = new JT808_0x8500_0x0001();
            value.ControlTypeId = reader.ReadUInt16();
            value.ControlTypeParameter = reader.ReadByte();
            return value;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8500_0x0001 value, IJT808Config config)
        {
            writer.WriteUInt16(value.ControlTypeId);
            writer.WriteByte(value.ControlTypeParameter);
        }
    }
}
