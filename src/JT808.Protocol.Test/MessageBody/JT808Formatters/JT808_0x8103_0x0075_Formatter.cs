using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using JT808.Protocol.Test.MessageBody.JT808_0x8103CustomIdExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Test.MessageBody.JT808Formatters
{
    public class JT808_0x8103_0x0075_Formatter : IJT808MessagePackFormatter<JT808_0x8103_0x0075>
    {
        public JT808_0x8103_0x0075 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x0075 jT808_0X8103_0X0075 = new JT808_0x8103_0x0075();
            jT808_0X8103_0X0075.ParamId = reader.ReadUInt32();
            jT808_0X8103_0X0075.ParamLength = reader.ReadByte();
            jT808_0X8103_0X0075.RTS_EncodeMode = reader.ReadByte();
            jT808_0X8103_0X0075.RTS_Resolution = reader.ReadByte();
            jT808_0X8103_0X0075.RTS_KF_Interval = reader.ReadUInt16();
            jT808_0X8103_0X0075.RTS_Target_FPS = reader.ReadByte();
            jT808_0X8103_0X0075.RTS_Target_CodeRate = reader.ReadUInt32();
            jT808_0X8103_0X0075.StreamStore_EncodeMode = reader.ReadByte();
            jT808_0X8103_0X0075.StreamStore_Resolution = reader.ReadByte();
            jT808_0X8103_0X0075.StreamStore_KF_Interval = reader.ReadUInt16();
            jT808_0X8103_0X0075.StreamStore_Target_FPS = reader.ReadByte();
            jT808_0X8103_0X0075.StreamStore_Target_CodeRate = reader.ReadUInt32();
            jT808_0X8103_0X0075.OSD = reader.ReadUInt16();
            jT808_0X8103_0X0075.AudioOutputEnabled = reader.ReadByte();
            return jT808_0X8103_0X0075;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x0075 value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteByte(value.RTS_EncodeMode);
            writer.WriteByte(value.RTS_Resolution);
            writer.WriteUInt16(value.RTS_KF_Interval);
            writer.WriteByte(value.RTS_Target_FPS);
            writer.WriteUInt32(value.RTS_Target_CodeRate);
            writer.WriteByte(value.StreamStore_EncodeMode);
            writer.WriteByte(value.StreamStore_Resolution);
            writer.WriteUInt16(value.StreamStore_KF_Interval);
            writer.WriteByte(value.StreamStore_Target_FPS);
            writer.WriteUInt32(value.StreamStore_Target_CodeRate);
            writer.WriteUInt16(value.OSD);
            writer.WriteByte(value.AudioOutputEnabled);
        }
    }
}
