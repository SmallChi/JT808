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
using JT808.Protocol.MessageBody.CarDVR;
using System.Linq;
using JT808.Protocol.Test.JT808LocationAttach;
using static JT808.Protocol.MessageBody.JT808_0x8105;
using System.Buffers.Binary;
using Newtonsoft.Json;

namespace JT808.Protocol.Test.Simples
{
    public class Demo15
    {
        JT808Serializer JT808Serializer;

        public Demo15()
        {
            IServiceCollection serviceDescriptors = new ServiceCollection();
            serviceDescriptors.AddJT808Configure();
            //通常在startup中使用app的Use进行扩展
            IServiceProvider serviceProvider = serviceDescriptors.BuildServiceProvider();
            JT808Serializer = serviceProvider.GetRequiredService<IJT808Config>().GetSerializer();
        }

        [Fact]
        public void Test1()
        {
            JT808Package jT808_0X0100 = new JT808Package
            {
                Header = new JT808Header
                {
                    MsgId = Enums.JT808MsgId._0x0100.ToUInt16Value(),
                    ManualMsgNum = 10,
                    TerminalPhoneNo = "123456789",
                },
                Bodies = new JT808_0x0100
                {
                    AreaID = 40,
                    CityOrCountyId = 50,
                    MakerId = "1234",
                    PlateColor = 1,
                    PlateNo = "粤A12345",
                    TerminalId = "CHI123",
                    TerminalModel = "tk12345"
                }
            };
            var hex = JT808Serializer.Serialize(jT808_0X0100, JT808Version.JTT2011).ToHexString();
            Assert.Equal("7E01000021000123456789000A002800323132333400746B3132333435004348493132330001D4C1413132333435857E", hex);
        }

        [Fact]
        public void Test2()
        {
            var bytes = "7E01000021000123456789000A002800323132333400746B3132333435004348493132330001D4C1413132333435857E".ToHexBytes();
            JT808Package jT808_0X0100 = JT808Serializer.Deserialize<JT808Package>(bytes);
            //  采用2011协议 的终端注册消息解析
            Assert.Equal(JT808MsgId._0x0100.ToUInt16Value(), jT808_0X0100.Header.MsgId);
            Assert.Equal(1, jT808_0X0100.Header.ProtocolVersion);
            Assert.Equal(10, jT808_0X0100.Header.MsgNum);
            Assert.Equal("123456789", jT808_0X0100.Header.TerminalPhoneNo);

            JT808_0x0100 JT808Bodies = (JT808_0x0100)jT808_0X0100.Bodies;
            Assert.Equal(40, JT808Bodies.AreaID);
            Assert.Equal(50, JT808Bodies.CityOrCountyId);
            Assert.Equal("1234", JT808Bodies.MakerId);
            Assert.Equal(1, JT808Bodies.PlateColor);
            Assert.Equal("粤A12345", JT808Bodies.PlateNo);
            Assert.Equal("CHI123", JT808Bodies.TerminalId);
            Assert.Equal("tk12345", JT808Bodies.TerminalModel);
        }
    }
}
