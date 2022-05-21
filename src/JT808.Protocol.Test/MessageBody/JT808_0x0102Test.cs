using Xunit;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Extensions;
using JT808.Protocol.Enums;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x0102Test
    {
        JT808Serializer JT808Serializer = new JT808Serializer();
        [Fact]
        public void Test1()
        {
            JT808_0x0102 jT808LoginRequestProperty = new JT808_0x0102
            {
                Code = "45612"
            };
            string hex = JT808Serializer.Serialize(jT808LoginRequestProperty).ToHexString();
            Assert.Equal("3435363132", hex);
        }

        [Fact]
        public void Test2()
        {
            byte[] bodys = "34 35 36 31 32".ToHexBytes();
            JT808_0x0102 jT808LoginRequest = JT808Serializer.Deserialize<JT808_0x0102>(bodys);
            Assert.Equal("45612", jT808LoginRequest.Code);
        }

        [Fact]
        public void Test5()
        {
            byte[] bodys = "34 35 36 31 32".ToHexBytes();
            string json = JT808Serializer.Analyze<JT808_0x0102>(bodys);
        }

        [Fact]
        public void Test2019_1()
        {
            JT808_0x0102 jT808LoginRequestProperty = new JT808_0x0102
            {
                Code = "45612",
                IMEI="1234567890abcde",
                SoftwareVersion="v2.0.0"
            };
            string hex = JT808Serializer.Serialize(jT808LoginRequestProperty,JT808Version.JTT2019).ToHexString();
            Assert.Equal("05343536313231323334353637383930616263646576322E302E300000000000000000000000000000", hex);
        }

        [Fact]
        public void Test2019_2()
        {
            byte[] bodys = "05343536313231323334353637383930616263646576322E302E300000000000000000000000000000".ToHexBytes();
            JT808_0x0102 jT808LoginRequest = JT808Serializer.Deserialize<JT808_0x0102>(bodys, JT808Version.JTT2019);
            Assert.Equal("45612", jT808LoginRequest.Code);
            Assert.Equal(5, jT808LoginRequest.CodeLength);
            Assert.Equal("1234567890abcde", jT808LoginRequest.IMEI);
            Assert.Equal("v2.0.0", jT808LoginRequest.SoftwareVersion);
        }

        [Fact]
        public void Test2019_3()
        {
            byte[] bodys = "05343536313231323334353637383930616263646576322E302E303030303030303030303030303030".ToHexBytes();
            string json = JT808Serializer.Analyze<JT808_0x0102>(bodys, JT808Version.JTT2019);
        }

        [Fact]
        public void Test3()
        {
            JT808Package jT808LoginRequest = new JT808Package
            {
                Header = new JT808Header
                {
                    MsgId = Enums.JT808MsgId._0x0102.ToUInt16Value(),
                    ManualMsgNum = 12345,
                    TerminalPhoneNo = "12345678900",
                },
                Bodies = new JT808_0x0102
                {
                    Code = "456121111"
                }
            };
            string hex = JT808Serializer.Serialize(jT808LoginRequest).ToHexString();
            Assert.Equal("7E010200090123456789003039343536313231313131BE7E", hex);
        }

        [Fact]
        public void Test4()
        {
            byte[] bodys = "7E 01 02 00 09 01 23 45 67 89 00 30 39 34 35 36 31 32 31 31 31 31 BE 7E".ToHexBytes();
            JT808Package jT808LoginRequest = JT808Serializer.Deserialize<JT808Package>(bodys);
            Assert.Equal(Enums.JT808MsgId._0x0102.ToUInt16Value(), jT808LoginRequest.Header.MsgId);
            Assert.Equal(12345, jT808LoginRequest.Header.MsgNum);
            Assert.Equal("12345678900", jT808LoginRequest.Header.TerminalPhoneNo);

            JT808_0x0102 JT808Bodies = (JT808_0x0102)jT808LoginRequest.Bodies;
            Assert.Equal("456121111", JT808Bodies.Code);
        }
    }
}
