using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;
using Xunit;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x8105Test
    {
        [Fact]
        public void Test1()
        {
            JT808Package jT808Package = new JT808Package
            {
                Header = new JT808Header
                {
                    MsgId = Enums.JT808MsgId.终端控制.ToUInt16Value(),
                    MsgNum = 1,
                    TerminalPhoneNo = "12345678900",
                },
                Bodies = new JT808_0x8105
                {
                    CommandWord = 1,
                    CommandValue = new CommandParams
                    {
                        ConnectionControl = 1,
                        DialPointName = "TKName",
                        DialUserName = "TK",
                        DialPwd = "TK123",
                        FirmwareVersion = "1.0",
                        HardwareVersion = "2.0",
                        ConnectTimeLimit = 60,
                        ManufacturerCode = 12345,
                        MonitoringPlatformAuthenticationCode = "code",
                        ServerUrl = "www.baidu.com",
                        TCPPort = 8806,
                        UDPPort = 3306,
                        URL = "www.TK.com"
                    }
                }
            };
            var hex = JT808Serializer.Serialize(jT808Package).ToHexString();
            Assert.Equal("7E8105004B012345678900000101313B544B4E616D653B544B3B544B3132333B7777772E62616964752E636F6D3B383830363B333330363B31323334353B636F64653B322E303B312E303B7777772E544B2E636F6D3B3630227E", hex);
        }

        [Fact]
        public void Test1_1()
        {
            var bytes = "7E8105004B012345678900000101313B544B4E616D653B544B3B544B3132333B7777772E62616964752E636F6D3B383830363B333330363B31323334353B636F64653B322E303B312E303B7777772E544B2E636F6D3B3630227E".ToHexBytes();
            JT808Package jT808Package = JT808Serializer.Deserialize<JT808Package>(bytes);
            Assert.Equal(Enums.JT808MsgId.终端控制.ToUInt16Value(), jT808Package.Header.MsgId);
            Assert.Equal(1, jT808Package.Header.MsgNum);
            Assert.Equal("12345678900", jT808Package.Header.TerminalPhoneNo);

            var JT808_0x8105 = (JT808_0x8105)jT808Package.Bodies;
            Assert.Equal(1, JT808_0x8105.CommandWord);
            Assert.Equal("www.TK.com", JT808_0x8105.CommandValue.URL);
            Assert.Equal(3306, (int)JT808_0x8105.CommandValue.UDPPort);
            Assert.Equal(8806, (int)JT808_0x8105.CommandValue.TCPPort);
            Assert.Equal("www.baidu.com", JT808_0x8105.CommandValue.ServerUrl);
            Assert.Equal("code", JT808_0x8105.CommandValue.MonitoringPlatformAuthenticationCode);
            Assert.Equal(12345, JT808_0x8105.CommandValue.ManufacturerCode);
            Assert.Equal("2.0", JT808_0x8105.CommandValue.HardwareVersion);
            Assert.Equal("1.0", JT808_0x8105.CommandValue.FirmwareVersion);
            Assert.Equal("TK", JT808_0x8105.CommandValue.DialUserName);
            Assert.Equal("TK123", JT808_0x8105.CommandValue.DialPwd);
            Assert.Equal("TKName", JT808_0x8105.CommandValue.DialPointName);
            Assert.Equal(1, (byte)JT808_0x8105.CommandValue.ConnectionControl);
            Assert.Equal(60, (UInt16)JT808_0x8105.CommandValue.ConnectTimeLimit);
        }


        [Fact]
        public void Test2()
        {
            JT808Package jT808Package = new JT808Package();
            jT808Package.Header = new JT808Header
            {
                MsgId = Enums.JT808MsgId.终端控制.ToUInt16Value(),
                MsgNum = 1,
                TerminalPhoneNo = "12345678900",
            };
            jT808Package.Bodies = new JT808_0x8105
            {
                CommandWord = 5
            };
            var hex = JT808Serializer.Serialize(jT808Package).ToHexString();
            Assert.Equal("7E81050001012345678900000105087E", hex);
        }

        [Fact]
        public void Test2_1()
        {
            var bytes = "7E81050001012345678900000105087E".ToHexBytes();
            JT808Package jT808Package = JT808Serializer.Deserialize<JT808Package>(bytes);
            Assert.Equal(Enums.JT808MsgId.终端控制.ToUInt16Value(), jT808Package.Header.MsgId);
            Assert.Equal(1, jT808Package.Header.MsgNum);
            Assert.Equal("12345678900", jT808Package.Header.TerminalPhoneNo);

            var JT808_0x8105 = (JT808_0x8105)jT808Package.Bodies;
            Assert.Equal(5, JT808_0x8105.CommandWord);
        }
    }
}
