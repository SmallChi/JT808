using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Internal;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using Xunit;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x8500Test
    {
        JT808Serializer JT808Serializer;

        public JT808_0x8500Test()
        {
            IJT808Config jT808Config = new DefaultGlobalConfig();
            jT808Config.FormatterFactory.SetMap<JT808_0x8500_0xF001>();
            JT808Serializer = new JT808Serializer(jT808Config);
        }
        [Fact]
        public void Test1()
        {
            JT808_0x8500 jT808_0X8500 = new JT808_0x8500
            {
                ControlFlag = 12
            };
            var hex = JT808Serializer.Serialize(jT808_0X8500).ToHexString();
            Assert.Equal("0C", hex);
        }

        [Fact]
        public void Test2()
        {
            var bytes = "0C".ToHexBytes();
            JT808_0x8500 jT808_0X8500 = JT808Serializer.Deserialize<JT808_0x8500>(bytes);
            Assert.Equal(12, jT808_0X8500.ControlFlag);
        }

        [Fact]
        public void Test_2019_1()
        {
            JT808_0x8500 jT808_0X8500 = new JT808_0x8500
            {
                 ControlTypes=new List<JT808_0x8500_ControlType> 
                 { 
                    new JT808_0x8500_0x0001()
                    {
                        ControlTypeParameter=0
                    },
                    new JT808_0x8500_0xF001()
                    {
                        ControlTypeParameter=1
                    }
                 }
            };
            var hex = JT808Serializer.Serialize(jT808_0X8500, JT808Version.JTT2019).ToHexString();
            Assert.Equal("0002000100F00101", hex);
        }

        [Fact]
        public void Test_2019_2()
        {
            var bytes = "0002000100F00101".ToHexBytes();
            JT808_0x8500 jT808_0X8500 = JT808Serializer.Deserialize<JT808_0x8500>(bytes, JT808Version.JTT2019);
            Assert.Equal(2, jT808_0X8500.ControlTypeCount);
            //0001 00
            //F001 01
            Dictionary<ushort, int> keys = new Dictionary<ushort, int>();
            keys.Add(0x0001, 1);
            keys.Add(0xF001, 1);
            var span = jT808_0X8500.ControlTypeBuffer.AsSpan();
            JT808_0x8500_0x0001 jT808_0x8500_0x0001 = JT808Serializer.Deserialize<JT808_0x8500_0x0001>(span.Slice(0,3), JT808Version.JTT2019);
            Assert.Equal(0x0001, jT808_0x8500_0x0001.ControlTypeId);
            Assert.Equal(0, jT808_0x8500_0x0001.ControlTypeParameter);

            JT808_0x8500_0xF001 jT808_0x8500_0xF001 = JT808Serializer.Deserialize<JT808_0x8500_0xF001>(span.Slice(3,3), JT808Version.JTT2019);
            Assert.Equal(0xF001, jT808_0x8500_0xF001.ControlTypeId);
            Assert.Equal(1, jT808_0x8500_0xF001.ControlTypeParameter);
        }
    }

    /// <summary>
    /// 自定义控制类型
    /// </summary>
    public class JT808_0x8500_0xF001 : JT808_0x8500_ControlType, IJT808MessagePackFormatter<JT808_0x8500_0xF001>
    {
        public override ushort ControlTypeId { get; set; } = 0xF001;

        public byte ControlTypeParameter { get; set; }

        public JT808_0x8500_0xF001 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8500_0xF001 value = new JT808_0x8500_0xF001();
            value.ControlTypeId = reader.ReadUInt16();
            value.ControlTypeParameter = reader.ReadByte();
            return value;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8500_0xF001 value, IJT808Config config)
        {
            writer.WriteUInt16(value.ControlTypeId);
            writer.WriteByte(value.ControlTypeParameter);
        }
    }
}
