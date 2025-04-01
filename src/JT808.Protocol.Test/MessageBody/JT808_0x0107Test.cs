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
        [Fact]
        public void Test4()
        {
            // 2013版本JT808_0x0107解析制造商有误 #43
            // 属于2019版本 非标准的JT808协议，不支持解析
            // 制造商ID
            byte[] bytes = "7E010740660100000000010941000493000700FF3838383838434B31303043000000000000000000000000000000000000000000000000313030303439330000000000000000000000000000000000000000000000898603249475600329000748572D56322E350E434B313030432D4A542D5630323402209B7E".ToHexBytes();
            string json = JT808Serializer.Analyze(bytes);
        }        
        
        [Fact]
        public void Test5()
        {
            //2019版本JT808_0x0107解析制造商ID #52
            byte[] bytes = "7E0107407F01000008686280748662930096000F00000000000000000000000000000000000000000000000000000000000000000000000000000000003000000000000000000000000000000000000000000000000000000000008986083319233076050122454332303041455548415230314132364D31365F363936303939323035323131303606362E322E383403FFE97E".ToHexBytes();
            string json = JT808Serializer.Analyze(bytes);
        }


    }
}
