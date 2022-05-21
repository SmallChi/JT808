using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using Xunit;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x8100Test
    {
        JT808Serializer JT808Serializer = new JT808Serializer();
        [Fact]
        public void Test1()
        {
            JT808Package jT808Package = new JT808Package
            {
                Header = new JT808Header
                {
                    MsgId = Enums.JT808MsgId._0x8100.ToUInt16Value(),
                    ManualMsgNum = 10,
                    TerminalPhoneNo = "012345678900",
                },
                Bodies = new JT808_0x8100
                {
                    Code = "123456",
                    JT808TerminalRegisterResult = Enums.JT808TerminalRegisterResult.success,
                    AckMsgNum = 100
                }
            };
            //"7E 
            //81 00 
            //00 09 
            //01 23 45 67 89 00 
            //00 0A 
            //00 64 
            //00 
            //31 32 33 34 35 36 
            //68 
            //7E"
            var hex = JT808Serializer.Serialize(jT808Package).ToHexString();
            Assert.Equal("7E81000009012345678900000A006400313233343536687E", hex);
        }

        [Fact]
        public void Test2()
        {
            var bytes = "7E 81 00 00 09 01 23 45 67 89 00 00 0A 00 64 00 31 32 33 34 35 36 68 7E".ToHexBytes();
            JT808Package jT808Package = JT808Serializer.Deserialize<JT808Package>(bytes);
            Assert.Equal(Enums.JT808MsgId._0x8100.ToUInt16Value(), jT808Package.Header.MsgId);
            Assert.Equal(10, jT808Package.Header.MsgNum);
            Assert.Equal("12345678900", jT808Package.Header.TerminalPhoneNo);

            JT808_0x8100 JT808Bodies = (JT808_0x8100)jT808Package.Bodies;
            Assert.Equal("123456", JT808Bodies.Code);
            Assert.Equal(100, JT808Bodies.AckMsgNum);
            Assert.Equal(Enums.JT808TerminalRegisterResult.success, JT808Bodies.JT808TerminalRegisterResult);
        }

        [Fact]
        public void Test3()
        {
            JT808Package jT808Package = new JT808Package
            {
                Header = new JT808Header
                {
                    MsgId = Enums.JT808MsgId._0x8100.ToUInt16Value(),
                    ManualMsgNum = 10,
                    TerminalPhoneNo = "12345678900",
                },
                Bodies = new JT808_0x8100
                {
                    Code = "123456",
                    JT808TerminalRegisterResult = Enums.JT808TerminalRegisterResult.terminal_not_database,
                    AckMsgNum = 100
                }
            };
            //"7E 
            //81 00
            //00 03 
            //01 23 45 67 89 00 
            //00 0A 
            //00 64 
            //04 
            //61 
            //7E"
            var hex = JT808Serializer.Serialize(jT808Package).ToHexString();
            Assert.Equal("7E81000003012345678900000A006404617E", hex);
        }

        [Fact]
        public void Test4()
        {
            var bytes = "7E 81 00 00 03 01 23 45 67 89 00 00 0A 00 64 04 61 7E".ToHexBytes();
            JT808Package jT808Package = JT808Serializer.Deserialize<JT808Package>(bytes);
            Assert.Equal(Enums.JT808MsgId._0x8100.ToUInt16Value(), jT808Package.Header.MsgId);
            Assert.Equal(10, jT808Package.Header.MsgNum);
            Assert.Equal("12345678900", jT808Package.Header.TerminalPhoneNo);

            JT808_0x8100 JT808Bodies = (JT808_0x8100)jT808Package.Bodies;
            Assert.Null(JT808Bodies.Code);
            Assert.Equal(100, JT808Bodies.AckMsgNum);
            Assert.Equal(Enums.JT808TerminalRegisterResult.terminal_not_database, JT808Bodies.JT808TerminalRegisterResult);
        }


        [Fact]
        public void Test5()
        {
            JT808Package jT808Package = new JT808Package
            {
                Header = new JT808Header
                {
                    MsgId = Enums.JT808MsgId._0x8100.ToUInt16Value(),
                    ManualMsgNum = 10,
                    TerminalPhoneNo = "12345678900",
                },
                Bodies = new JT808_0x8100
                {
                    Code = "zssdaf23124sfdsc",
                    JT808TerminalRegisterResult = Enums.JT808TerminalRegisterResult.success,
                    AckMsgNum = 100
                }
            };
            //"7E 81 00 00 13 01 23 45 67 89 00 00 0A 00 64 00 7A 73 73 64 61 66 32 33 31 32 34 73 66 64 73 63 3B 7E"
            var hex = JT808Serializer.Serialize(jT808Package).ToHexString();
            Assert.Equal("7E81000013012345678900000A0064007A7373646166323331323473666473633B7E", hex);
        }

        [Fact]
        public void Test6()
        {
            var bytes = "7E 81 00 00 13 01 23 45 67 89 00 00 0A 00 64 00 7A 73 73 64 61 66 32 33 31 32 34 73 66 64 73 63 3B 7E".ToHexBytes();
            JT808Package jT808Package = JT808Serializer.Deserialize<JT808Package>(bytes);
            Assert.Equal(Enums.JT808MsgId._0x8100.ToUInt16Value(), jT808Package.Header.MsgId);
            Assert.Equal(10, jT808Package.Header.MsgNum);
            Assert.Equal("12345678900", jT808Package.Header.TerminalPhoneNo);

            JT808_0x8100 JT808Bodies = (JT808_0x8100)jT808Package.Bodies;
            Assert.Equal("zssdaf23124sfdsc", JT808Bodies.Code);
            Assert.Equal(100, JT808Bodies.AckMsgNum);
            Assert.Equal(Enums.JT808TerminalRegisterResult.success, JT808Bodies.JT808TerminalRegisterResult);
        }

        [Fact]
        public void Test7()
        {
            var bytes = "7E 81 00 00 13 01 23 45 67 89 00 00 0A 00 64 00 7A 73 73 64 61 66 32 33 31 32 34 73 66 64 73 63 3B 7E".ToHexBytes();
            string json = JT808Serializer.Analyze(bytes);
        }
    }
}
