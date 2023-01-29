using JT808.Protocol.Enums;
using JT808.Protocol.Interfaces;
using JT808.Protocol.Internal;
using JT808.Protocol.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;
using System.Text.Json;

namespace JT808.Protocol.Test.Simples
{
    public class Demo8
    {
        public JT808Serializer JT808Serializer;
        public Demo8()
        {
            IJT808Config jT808Config = new DefaultGlobalConfig();
            jT808Config.MsgIdFactory.SetMap<DT1Demo8>();
            JT808Serializer = new JT808Serializer(jT808Config);
        }

        [Fact]
        public void Test1()
        {
            JT808Package dt1Package = new JT808Package();
            dt1Package.Header = new JT808Header
            {
                MsgId = 0x93,
                ManualMsgNum = 126,
                TerminalPhoneNo = "1234567891"
            };
            DT1Demo8 dT1Demo8 = new DT1Demo8();
            dT1Demo8.Age1 = 18;
            dT1Demo8.Sex1 = 2;
            dt1Package.Bodies = dT1Demo8;
            byte[] dt1Data = JT808Serializer.Serialize(dt1Package);
            var dt1Hex = dt1Data.ToHexString();
            Assert.Equal("7E00930003001234567891007D02020012677E", dt1Hex);
        }

        [Fact]
        public void Test2()
        {
            var data = "7E00930003001234567891007D02020012677E".ToHexBytes();
            string json = JT808Serializer.Analyze(data);
            //{"[7E]\u5F00\u59CB":126,"[0093]\u6D88\u606FId":147,"\u6D88\u606F\u4F53\u5C5E\u6027\u5BF9\u8C61":{"[0000000000000011]\u6D88\u606F\u4F53\u5C5E\u6027":3,"[bit15]\u4FDD\u7559":0,"[bit14]\u4FDD\u7559":0,"[bit13]\u662F\u5426\u5206\u5305":false,"[bit10~bit12]\u6570\u636E\u52A0\u5BC6":"None","[bit0~bit9]\u6D88\u606F\u4F53\u957F\u5EA6":3},"[1234567891]\u7EC8\u7AEF\u624B\u673A\u53F7":"1234567891","[007E]\u6D88\u606F\u6D41\u6C34\u53F7":126,"\u6570\u636E\u4F53\u5BF9\u8C61":{"DT1Demo8":"020012","[02]\u6027\u522B":2,"[0012]\u5E74\u9F84":18},"[67]\u6821\u9A8C\u7801":103,"[7E]\u7ED3\u675F":126}
        }

        public class DT1Demo8 : JT808MessagePackFormatter<DT1Demo8>, JT808Bodies, IJT808Analyze
        {
            public byte Sex1 { get; set; }

            public ushort Age1 { get; set; }

            public ushort MsgId => 0x93;

            public string Description => "DT1Demo8";

            public  void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
            {
                DT1Demo8 dT1Demo6 = new DT1Demo8();
                dT1Demo6.Sex1 = reader.ReadByte();
                writer.WriteNumber($"[{dT1Demo6.Sex1.ReadNumber()}]性别", dT1Demo6.Sex1);
                dT1Demo6.Age1 = reader.ReadUInt16();
                writer.WriteNumber($"[{dT1Demo6.Age1.ReadNumber()}]年龄", dT1Demo6.Age1);
            }

            public override DT1Demo8 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
            {
                DT1Demo8 dT1Demo8 = new DT1Demo8();
                dT1Demo8.Sex1 = reader.ReadByte();
                dT1Demo8.Age1 = reader.ReadUInt16();
                return dT1Demo8;
            }

            public override void Serialize(ref JT808MessagePackWriter writer, DT1Demo8 value, IJT808Config config)
            {
                writer.WriteByte(value.Sex1);
                writer.WriteUInt16(value.Age1);
            }
        }
    }
}
