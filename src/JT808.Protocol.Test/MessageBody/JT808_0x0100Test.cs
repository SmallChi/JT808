using Xunit;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Extensions;
using JT808.Protocol.Enums;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x0100Test
    {
        JT808Serializer JT808Serializer = new JT808Serializer();

        [Fact]
        public void Test1()
        {
            JT808Package jT808_0X0100 = new JT808Package
            {
                Header = new JT808Header
                {
                    MsgId = Enums.JT808MsgId.终端注册.ToUInt16Value(),
                    MsgNum = 10,
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
                    TerminalModel = "smallchi123"
                }
            };
            var hex = JT808Serializer.Serialize(jT808_0X0100).ToHexString();
            Assert.Equal("7E0100002D000123456789000A002800323132333430736D616C6C6368693132333030303030303030304348493132333001D4C1413132333435BA7E", hex);
        }

        [Fact]
        public void Test1_1()
        {
            byte[] bytes = "7E 01 00 00 2D 00 01 23 45 67 89 00 0A 00 28 00 32 31 32 33 34 30 73 6D 61 6C 6C 63 68 69 31 32 33 30 30 30 30 30 30 30 30 30 43 48 49 31 32 33 30 01 D4 C1 41 31 32 33 34 35 BA 7E".ToHexBytes();
            JT808Package jT808_0X0100 = JT808Serializer.Deserialize<JT808Package>(bytes);
            Assert.Equal(Enums.JT808MsgId.终端注册.ToUInt16Value(), jT808_0X0100.Header.MsgId);
            Assert.Equal(10, jT808_0X0100.Header.MsgNum);
            Assert.Equal("123456789", jT808_0X0100.Header.TerminalPhoneNo);

            JT808_0x0100 JT808Bodies = (JT808_0x0100)jT808_0X0100.Bodies;
            Assert.Equal(40, JT808Bodies.AreaID);
            Assert.Equal(50, JT808Bodies.CityOrCountyId);
            Assert.Equal("12340", JT808Bodies.MakerId);
            Assert.Equal(1, JT808Bodies.PlateColor);
            Assert.Equal("粤A12345", JT808Bodies.PlateNo);
            Assert.Equal("CHI1230", JT808Bodies.TerminalId);
            Assert.Equal("smallchi123000000000", JT808Bodies.TerminalModel);
        }

        [Fact]
        public void Test2019_1()
        {
            JT808Package jT808_0X0100 = new JT808Package
            {
                Header = new JT808Header
                {
                    MsgId = Enums.JT808MsgId.终端注册.ToUInt16Value(),
                    MsgNum = 10,
                    TerminalPhoneNo = "123456789",
                    ProtocolVersion=1,
                },
                Bodies = new JT808_0x0100
                {
                    AreaID = 40,
                    CityOrCountyId = 50,
                    MakerId = "1234",
                    PlateColor = 1,
                    PlateNo = "粤A12345",
                    TerminalId = "CHI123",
                    TerminalModel = "smallchi123"
                }
            };
            JT808HeaderMessageBodyProperty jT808HeaderMessageBodyProperty = new JT808HeaderMessageBodyProperty(true);
            jT808_0X0100.Header.MessageBodyProperty = jT808HeaderMessageBodyProperty;
            var hex = JT808Serializer.Serialize(jT808_0X0100).ToHexString();
            Assert.Equal("7E010040540100000000000123456789000A00280032303030303030303132333430303030303030303030303030303030303030736D616C6C63686931323330303030303030303030303030303030303030303030303043484931323301D4C1413132333435B27E", hex);
        }

        [Fact]
        public void Test2019_2()
        {
            byte[] bytes = "7E010040540100000000000123456789000A00280032303030303030303132333430303030303030303030303030303030303030736D616C6C63686931323330303030303030303030303030303030303030303030303043484931323301D4C1413132333435B27E".ToHexBytes();
            JT808Package jT808_0X0100 = JT808Serializer.Deserialize<JT808Package>(bytes);
            Assert.Equal(JT808MsgId.终端注册.ToUInt16Value(), jT808_0X0100.Header.MsgId);
            Assert.Equal(1, jT808_0X0100.Header.ProtocolVersion);
            Assert.Equal(JT808Version.JTT2019, jT808_0X0100.Version);
            Assert.True(jT808_0X0100.Header.MessageBodyProperty.VersionFlag);
            Assert.Equal(10, jT808_0X0100.Header.MsgNum);
            Assert.Equal("123456789", jT808_0X0100.Header.TerminalPhoneNo);

            JT808_0x0100 JT808Bodies = (JT808_0x0100)jT808_0X0100.Bodies;
            Assert.Equal(40, JT808Bodies.AreaID);
            Assert.Equal(50, JT808Bodies.CityOrCountyId);
            Assert.Equal("1234".PadLeft(11,'0'), JT808Bodies.MakerId);
            Assert.Equal(1, JT808Bodies.PlateColor);
            Assert.Equal("粤A12345", JT808Bodies.PlateNo);
            Assert.Equal("CHI123".PadLeft(30, '0'), JT808Bodies.TerminalId);
            Assert.Equal("smallchi123".PadLeft(30, '0'), JT808Bodies.TerminalModel);
        }
    }
}
