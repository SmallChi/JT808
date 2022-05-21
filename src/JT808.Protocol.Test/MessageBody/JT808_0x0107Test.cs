using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using Xunit;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x0107Test
    {
        JT808Serializer JT808Serializer = new JT808Serializer();
        [Fact]
        public void Test1()
        {
            JT808Package jT808Package = new JT808Package
            {
                Header = new JT808Header
                {
                    MsgId = Enums.JT808MsgId._0x0107.ToUInt16Value(),
                    ManualMsgNum = 8888,
                    TerminalPhoneNo = "112233445566",
                }
            };
            JT808_0x0107 jT808_0X0107 = new JT808_0x0107
            {
                // 00000000 00000101
                TerminalType = 5,
                MakerId = "10601",
                TerminalModel = "10455545955103",
                TerminalId = "4d6a13",
                Terminal_SIM_ICCID = "12345678901234567890",
                Terminal_Hardware_Version_Num = "abcdefg",
                Terminal_Firmware_Version_Num = "poiuytrewq",
                // 00000111
                GNSSModule = 7,
                // 00001001
                CommunicationModule = 9
            };
            jT808Package.Bodies = jT808_0X0107;
            string hex = JT808Serializer.Serialize(jT808Package).ToHexString();
            Assert.Equal("7E0107004111223344556622B8000531303630313130343535353435393535313033000000000000346436613133001234567890123456789007616263646566670A706F69757974726577710709EA7E".Replace(" ", ""), hex);
        }

        [Fact]
        public void Test2()
        {
            byte[] bytes = "7E0107004111223344556622B8000531303630313130343535353435393535313033000000000000346436613133001234567890123456789007616263646566670A706F69757974726577710709EA7E".ToHexBytes();
            JT808Package jT808Package = JT808Serializer.Deserialize(bytes);
            JT808_0x0107 jT808_0X0107 = (JT808_0x0107)jT808Package.Bodies;
            Assert.Equal(8888, jT808Package.Header.MsgNum);
            Assert.Equal("112233445566", jT808Package.Header.TerminalPhoneNo);
            Assert.Equal(Enums.JT808MsgId._0x0107.ToUInt16Value(), jT808Package.Header.MsgId);
            Assert.Equal(5, jT808_0X0107.TerminalType);
            Assert.Equal("10601", jT808_0X0107.MakerId);
            Assert.Equal("10455545955103", jT808_0X0107.TerminalModel);
            Assert.Equal("4d6a13", jT808_0X0107.TerminalId);
            Assert.Equal("12345678901234567890", jT808_0X0107.Terminal_SIM_ICCID);
            Assert.Equal("abcdefg", jT808_0X0107.Terminal_Hardware_Version_Num);
            Assert.Equal("poiuytrewq", jT808_0X0107.Terminal_Firmware_Version_Num);
            Assert.Equal(7, jT808_0X0107.GNSSModule);
            Assert.Equal(9, jT808_0X0107.CommunicationModule);
        }

        [Fact]
        public void Test3()
        {
            byte[] bytes = "7E0107004111223344556622B8000531303630313130343535353435393535313033303030303030346436613133301234567890123456789007616263646566670A706F69757974726577710709DA7E".ToHexBytes();
            string json = JT808Serializer.Analyze(bytes); 
        }
    }
}
