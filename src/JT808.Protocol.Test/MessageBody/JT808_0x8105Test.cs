using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;
using Xunit;
using static JT808.Protocol.MessageBody.JT808_0x8105;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x8105Test
    {
        JT808Serializer JT808Serializer = new JT808Serializer();

        [Fact]
        public void Test1()
        {
            JT808Package jT808Package = new JT808Package
            {
                Header = new JT808Header
                {
                    MsgId = Enums.JT808MsgId._0x8105.ToUInt16Value(),
                    ManualMsgNum = 1,
                    TerminalPhoneNo = "12345678900",
                },
                Bodies = new JT808_0x8105
                {
                    CommandWord = 1,
                    CommandParameters=new System.Collections.Generic.List<ICommandParameter>
                    {
                        new JT808_0x8105.ConnectionControlCommandParameter{ Value=1},
                        new JT808_0x8105.DialPointNameCommandParameter{ Value="TKName"},
                        new JT808_0x8105.DialUserNameCommandParameter{ Value="TK"},
                        new JT808_0x8105.DialPwdCommandParameter{ Value="TK123"},
                        new JT808_0x8105.FirmwareVersionCommandParameter{  Value="1.0"},
                        new JT808_0x8105.HardwareVersionCommandParameter{ Value="2.0"},
                        new JT808_0x8105.ConnectTimeLimitCommandParameter{ Value=60},
                        new JT808_0x8105.MakerIdCommandParameter{ Value="12345"},
                        new JT808_0x8105.MonitorPlatformAuthCodeCommandParameter{ Value="code"},
                        new JT808_0x8105.ServerUrlCommandParameter{ Value="www.baidu.com"},
                        new JT808_0x8105.TcpPortCommandParameter{ Value=8806},
                        new JT808_0x8105.UdpPortCommandParameter{ Value=3306},
                        new JT808_0x8105.UrlCommandParameter{  Value= "www.TK.com"}
                    }
                }
            };
            var hex = JT808Serializer.Serialize(jT808Package).ToHexString();
            Assert.Equal("7E81050048012345678900000101013B544B4E616D653B544B3B544B3132333B7777772E62616964752E636F6D3B22663B0CEA3B31323334353B636F64653B322E303B312E303B7777772E544B2E636F6D3B003C3BB27E", hex);
        }

        [Fact]
        public void Test1_1()
        {
            var bytes = "7E81050048012345678900000101013B544B4E616D653B544B3B544B3132333B7777772E62616964752E636F6D3B22663B0CEA3B31323334353B636F64653B322E303B312E303B7777772E544B2E636F6D3B003C3BB27E".ToHexBytes();
            JT808Package jT808Package = JT808Serializer.Deserialize<JT808Package>(bytes);
            Assert.Equal(Enums.JT808MsgId._0x8105.ToUInt16Value(), jT808Package.Header.MsgId);
            Assert.Equal(1, jT808Package.Header.MsgNum);
            Assert.Equal("12345678900", jT808Package.Header.TerminalPhoneNo);
            var JT808_0x8105 = (JT808_0x8105)jT808Package.Bodies;
            Assert.Equal(1, JT808_0x8105.CommandWord);
            Assert.Equal("www.TK.com", JT808_0x8105.CommandParameters.GetCommandParameter<UrlCommandParameter>().Value);
            Assert.Equal(3306u, JT808_0x8105.CommandParameters.GetCommandParameter<UdpPortCommandParameter>().Value.Value);
            Assert.Equal(8806, JT808_0x8105.CommandParameters.GetCommandParameter<TcpPortCommandParameter>().Value.Value);
            Assert.Equal("www.baidu.com", JT808_0x8105.CommandParameters.GetCommandParameter<ServerUrlCommandParameter>().Value);
            Assert.Equal("code", JT808_0x8105.CommandParameters.GetCommandParameter<MonitorPlatformAuthCodeCommandParameter>().Value);
            Assert.Equal("12345", JT808_0x8105.CommandParameters.GetCommandParameter<MakerIdCommandParameter>().Value);
            Assert.Equal("2.0", JT808_0x8105.CommandParameters.GetCommandParameter<HardwareVersionCommandParameter>().Value);
            Assert.Equal("1.0", JT808_0x8105.CommandParameters.GetCommandParameter<FirmwareVersionCommandParameter>().Value);
            Assert.Equal("TK", JT808_0x8105.CommandParameters.GetCommandParameter<DialUserNameCommandParameter>().Value);
            Assert.Equal("TK123",JT808_0x8105.CommandParameters.GetCommandParameter<DialPwdCommandParameter>().Value);
            Assert.Equal("TKName", JT808_0x8105.CommandParameters.GetCommandParameter<DialPointNameCommandParameter>().Value);
            Assert.Equal(1, JT808_0x8105.CommandParameters.GetCommandParameter<ConnectionControlCommandParameter>().Value.Value);
            Assert.Equal(60, JT808_0x8105.CommandParameters.GetCommandParameter<ConnectTimeLimitCommandParameter>().Value.Value);
        }

        [Fact]
        public void Test1_2()
        {
            var bytes = "7E81050048012345678900000101013B544B4E616D653B544B3B544B3132333B7777772E62616964752E636F6D3B22663B0CEA3B31323334353B636F64653B322E303B312E303B7777772E544B2E636F6D3B003C3BB27E".ToHexBytes();
            string json = JT808Serializer.Analyze<JT808Package>(bytes);
        }

        [Fact]
        public void Test1_3()
        {
            var bytes = "7E81050048012345678900000101013B544B4E616D653B544B3B544B3132333B7777772E62616964752E636F6D3B22663B0CEA3B31323334353B636F64653B322E303B312E303B7777772E544B2E636F6D3B003C3BB27E".ToHexBytes();
            JT808Package jT808Package = JT808Serializer.Deserialize<JT808Package>(bytes);
            Assert.Equal(Enums.JT808MsgId._0x8105.ToUInt16Value(), jT808Package.Header.MsgId);
            Assert.Equal(1, jT808Package.Header.MsgNum);
            Assert.Equal("12345678900", jT808Package.Header.TerminalPhoneNo);
            var JT808_0x8105 = (JT808_0x8105)jT808Package.Bodies;
            Assert.Equal(1, JT808_0x8105.CommandWord);
            var valueString = JT808_0x8105.CommandParameters.GetCommandParameter<UrlCommandParameter>().ToValueString();
        }

        [Fact]
        public void Test2()
        {
            JT808Package jT808Package = new JT808Package();
            jT808Package.Header = new JT808Header
            {
                MsgId = Enums.JT808MsgId._0x8105.ToUInt16Value(),
                ManualMsgNum = 1,
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
            Assert.Equal(Enums.JT808MsgId._0x8105.ToUInt16Value(), jT808Package.Header.MsgId);
            Assert.Equal(1, jT808Package.Header.MsgNum);
            Assert.Equal("12345678900", jT808Package.Header.TerminalPhoneNo);

            var JT808_0x8105 = (JT808_0x8105)jT808Package.Bodies;
            Assert.Equal(5, JT808_0x8105.CommandWord);
        }
    }
}
