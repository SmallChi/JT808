using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using Xunit;

namespace JT808.Protocol.Test.MessageBodyReply
{
    public class JT808_0x0107Test
    {
        [Fact]
        public void Test1()
        {
            JT808Package jT808Package = new JT808Package
            {
                Header = new JT808Header
                {
                    MsgId = Enums.JT808MsgId.查询终端属性应答.ToUInt16Value(),
                    MsgNum = 8888,
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
                Terminal_SIM_ICCID = "1232355987",
                Terminal_Hardware_Version_Num = "abcdefg",
                Terminal_Firmware_Version_Num = "poiuytrewq",
                // 00000111
                GNSSModule = 7,
                // 00001001
                CommunicationModule = 9
            };
            jT808Package.Bodies = jT808_0X0107;
            string hex = JT808Serializer.Serialize(jT808Package).ToHexString();
            //"7E 01 07 00 3C 11 22 33 44 55 66 22 B8 00 05 31 30 36 30 31 31 30 34 35 35 35 34 35 39 35 35 31 30 33 30 30 30 30 30 30 34 64 36 61 31 33 30 12 32 35 59 87 07 61 62 63 64 65 66 67 0A 70 6F 69 75 79 74 72 65 77 71 07 09 6C 7E"
            Assert.Equal("7E 01 07 00 3C 11 22 33 44 55 66 22 B8 00 05 31 30 36 30 31 31 30 34 35 35 35 34 35 39 35 35 31 30 33 30 30 30 30 30 30 34 64 36 61 31 33 30 12 32 35 59 87 07 61 62 63 64 65 66 67 0A 70 6F 69 75 79 74 72 65 77 71 07 09 6C 7E".Replace(" ", ""), hex);
        }

        [Fact]
        public void Test2()
        {
            byte[] bytes = "7E 01 07 00 3C 11 22 33 44 55 66 22 B8 00 05 31 30 36 30 31 31 30 34 35 35 35 34 35 39 35 35 31 30 33 30 30 30 30 30 30 34 64 36 61 31 33 30 12 32 35 59 87 07 61 62 63 64 65 66 67 0A 70 6F 69 75 79 74 72 65 77 71 07 09 6C 7E".ToHexBytes();
            JT808Package jT808Package = JT808Serializer.Deserialize(bytes);
            JT808_0x0107 jT808_0X0107 = (JT808_0x0107)jT808Package.Bodies;
            Assert.Equal(8888, jT808Package.Header.MsgNum);
            Assert.Equal("112233445566", jT808Package.Header.TerminalPhoneNo);
            Assert.Equal(Enums.JT808MsgId.查询终端属性应答.ToUInt16Value(), jT808Package.Header.MsgId);
            Assert.Equal(5, jT808_0X0107.TerminalType);
            Assert.Equal("10601", jT808_0X0107.MakerId);
            Assert.Equal("10455545955103000000", jT808_0X0107.TerminalModel);
            Assert.Equal("4d6a130", jT808_0X0107.TerminalId);
            Assert.Equal("1232355987", jT808_0X0107.Terminal_SIM_ICCID);
            Assert.Equal("abcdefg", jT808_0X0107.Terminal_Hardware_Version_Num);
            Assert.Equal("poiuytrewq", jT808_0X0107.Terminal_Firmware_Version_Num);
            Assert.Equal(7, jT808_0X0107.GNSSModule);
            Assert.Equal(9, jT808_0X0107.CommunicationModule);
        }
    }
}
