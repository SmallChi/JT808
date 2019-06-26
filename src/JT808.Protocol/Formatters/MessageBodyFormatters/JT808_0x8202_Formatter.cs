using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Interfaces;
using System;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x8202_Formatter : IJT808MessagePackFormatter<JT808_0x8202>
    {
        public JT808_0x8202 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8202 jT808_0X8202 = new JT808_0x8202();
            jT808_0X8202.Interval = reader.ReadUInt16();
            jT808_0X8202.LocationTrackingValidity = reader.ReadInt32();
            return jT808_0X8202;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8202 value, IJT808Config config)
        {
           writer.WriteUInt16(value.Interval);
            writer.WriteInt32(value.LocationTrackingValidity);
        }
    }
}
