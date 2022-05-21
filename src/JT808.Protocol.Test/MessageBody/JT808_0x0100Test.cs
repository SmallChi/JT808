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
                    TerminalModel = "smallchi123"
                }
            };
            var hex = JT808Serializer.Serialize(jT808_0X0100).ToHexString();
            Assert.Equal("7E0100002D000123456789000A002800323132333400736D616C6C6368693132330000000000000000004348493132330001D4C14131323334358A7E", hex);
        }

        [Fact]
        public void Test1_1()
        {
            byte[] bytes = "7E0100002D000123456789000A002800323132333400736D616C6C6368693132330000000000000000004348493132330001D4C14131323334358A7E".ToHexBytes();
            JT808Package jT808_0X0100 = JT808Serializer.Deserialize<JT808Package>(bytes);
            Assert.Equal(Enums.JT808MsgId._0x0100.ToUInt16Value(), jT808_0X0100.Header.MsgId);
            Assert.Equal(10, jT808_0X0100.Header.MsgNum);
            Assert.Equal("123456789", jT808_0X0100.Header.TerminalPhoneNo);

            JT808_0x0100 JT808Bodies = (JT808_0x0100)jT808_0X0100.Bodies;
            Assert.Equal(40, JT808Bodies.AreaID);
            Assert.Equal(50, JT808Bodies.CityOrCountyId);
            Assert.Equal("1234", JT808Bodies.MakerId);
            Assert.Equal(1, JT808Bodies.PlateColor);
            Assert.Equal("粤A12345", JT808Bodies.PlateNo);
            Assert.Equal("CHI123", JT808Bodies.TerminalId);
            Assert.Equal("smallchi123", JT808Bodies.TerminalModel);
        }

        [Fact]
        public void Test2019_1()
        {
            JT808Package jT808_0X0100 = new JT808Package
            {
                Header = new JT808Header
                {
                    MsgId = Enums.JT808MsgId._0x0100.ToUInt16Value(),
                    ManualMsgNum = 10,
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
            Assert.Equal("7E010040540100000000000123456789000A002800323132333400000000000000736D616C6C6368693132330000000000000000000000000000000000000043484931323300000000000000000000000000000000000000000000000001D4C1413132333435B27E", hex);
        }

        [Fact]
        public void Test2019_2()
        {
            byte[] bytes = "7E010040540100000000000123456789000A002800323132333400000000000000736D616C6C6368693132330000000000000000000000000000000000000043484931323300000000000000000000000000000000000000000000000001D4C1413132333435B27E".ToHexBytes();
            JT808Package jT808_0X0100 = JT808Serializer.Deserialize<JT808Package>(bytes);
            Assert.Equal(JT808MsgId._0x0100.ToUInt16Value(), jT808_0X0100.Header.MsgId);
            Assert.Equal(1, jT808_0X0100.Header.ProtocolVersion);
            Assert.Equal(JT808Version.JTT2019, jT808_0X0100.Version);
            Assert.True(jT808_0X0100.Header.MessageBodyProperty.VersionFlag);
            Assert.Equal(10, jT808_0X0100.Header.MsgNum);
            Assert.Equal("123456789", jT808_0X0100.Header.TerminalPhoneNo);

            JT808_0x0100 JT808Bodies = (JT808_0x0100)jT808_0X0100.Bodies;
            Assert.Equal(40, JT808Bodies.AreaID);
            Assert.Equal(50, JT808Bodies.CityOrCountyId);
            Assert.Equal("1234", JT808Bodies.MakerId);
            Assert.Equal(1, JT808Bodies.PlateColor);
            Assert.Equal("粤A12345", JT808Bodies.PlateNo);
            Assert.Equal("CHI123", JT808Bodies.TerminalId);
            Assert.Equal("smallchi123", JT808Bodies.TerminalModel);
        }

        [Fact]
        public void Test2019_3()
        {
            byte[] bytes = "7E010040540100000000000123456789000A00280032303030303030303132333430303030303030303030303030303030303030736D616C6C63686931323330303030303030303030303030303030303030303030303043484931323301D4C1413132333435B27E".ToHexBytes();
            string json = JT808Serializer.Analyze<JT808Package>(bytes);
        }    
        [Fact]
        public void Test2019_4_1()
        {
            var package = JT808MsgId._0x0100.Create2019("22222222222", new JT808_0x0100()
            {
                PlateNo = "粤A12346",
                PlateColor = 2,
                AreaID = 0,
                CityOrCountyId = 0,
                MakerId = "Koike002",
                TerminalId = "Koike002",
                TerminalModel = "Koike002"
            });
            var data = JT808Serializer.Serialize(package);
            var hex = data.ToHexString();
            Assert.Equal("7E0100405401000000000222222222220001000000004B6F696B653030320000004B6F696B65303032000000000000000000000000000000000000000000004B6F696B653030320000000000000000000000000000000000000000000002D4C1413132333436207E".ToUpper(), hex);
        }

        [Fact]
        public void Test2019_4_2()
        {
            byte[] bytes = "7e0100405401000000000222222222220001000000003030304b6f696b65303032303030303030303030303030303030303030303030304b6f696b65303032303030303030303030303030303030303030303030304b6f696b6530303202d4c1413132333436107e".ToHexBytes();
            JT808Package jT808_0X0100 = JT808Serializer.Deserialize<JT808Package>(bytes);
            Assert.Equal("22222222222", jT808_0X0100.Header.TerminalPhoneNo);
            Assert.Equal(1, jT808_0X0100.Header.ProtocolVersion);
            Assert.NotNull(jT808_0X0100.Bodies);
            JT808_0x0100 body = jT808_0X0100.Bodies as JT808_0x0100;
            Assert.Equal(0, body.AreaID);
            Assert.Equal(2, body.PlateColor);
            Assert.Equal(0, body.CityOrCountyId);
            Assert.Equal("粤A12346", body.PlateNo.TrimStart());
            Assert.Equal("Koike002", body.MakerId.TrimStart('0'));
            Assert.Equal("Koike002", body.TerminalId.TrimStart('0'));
            Assert.Equal("Koike002", body.TerminalModel.TrimStart('0'));
        }

        [Fact]
        public void Test2019_4_3()
        {
            byte[] bytes = "7e0100405401000000000222222222220001000000003030304b6f696b65303032303030303030303030303030303030303030303030304b6f696b65303032303030303030303030303030303030303030303030304b6f696b6530303202d4c1413132333436107e".ToHexBytes();
            string json = JT808Serializer.Analyze<JT808Package>(bytes);
        }

        [Fact]
        public void Test2011_1()
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
        public void Test2011_2()
        {
            byte[] bytes = "7E01000021000123456789000A002800323132333400746B3132333435004348493132330001D4C1413132333435857E".ToHexBytes();
            JT808Package jT808_0X0100 = JT808Serializer.Deserialize<JT808Package>(bytes);
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

        [Fact]
        public void Test2011_3()
        {
            byte[] bytes = "7E01000021000123456789000A002800323132333400746B3132333435004348493132330001D4C1413132333435857E".ToHexBytes();
            string json = JT808Serializer.Analyze<JT808Package>(bytes);
        }
    }
}
