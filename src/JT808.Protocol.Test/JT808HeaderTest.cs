using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using System;
using Xunit;

namespace JT808.Protocol.Test
{
    public class JT808HeaderTest
    {
        JT808Serializer JT808Serializer = new JT808Serializer();

        [Fact]
        public void Test3()
        {
            ReadOnlySpan<char> dataLen = Convert.ToString(5, 2).PadLeft(10, '0').AsSpan();
        }

        [Fact]
        public void Test4()
        {
            // "0000000000000101"
            short a = Convert.ToInt16("0000000000000101",2);
            var msgMethodBytes = BitConverter.GetBytes(a);
        }

        [Fact]
        public void Test1()
        {
            JT808Header jT808HeaderProperty = new JT808Header
            {
                TerminalPhoneNo = "13812345678"
            };
            jT808HeaderProperty.MessageBodyProperty=new JT808HeaderMessageBodyProperty(5);
            jT808HeaderProperty.MsgNum = 135;
            jT808HeaderProperty.MsgId = JT808MsgId._0x0102.ToUInt16Value();

            var hex = JT808Serializer.Serialize(jT808HeaderProperty).ToHexString();
            Assert.Equal("01 02 00 05 01 38 12 34 56 78 00 87".Replace(" ", ""), hex);
        }

        [Fact]
        public void Test1_2()
        {
            //01 02 00 05 01 38 12 34 56 78 00 87
            byte[] headerBytes = "01 02 00 05 01 38 12 34 56 78 00 87".ToHexBytes();
            JT808Header jT808Header = JT808Serializer.Deserialize<JT808Header>(headerBytes);
            Assert.Equal(135, jT808Header.MsgNum);
            Assert.Equal("13812345678", jT808Header.TerminalPhoneNo);
            Assert.False(jT808Header.MessageBodyProperty.IsPackage);
            Assert.Equal(JT808MsgId._0x0102.ToValue(), jT808Header.MsgId);
            Assert.Equal(5, jT808Header.MessageBodyProperty.DataLength);
        }

        [Fact]
        public void Test5()
        {
            JT808Header jT808HeaderProperty = new JT808Header();
            jT808HeaderProperty.TerminalPhoneNo = "13812345678";
            jT808HeaderProperty.MessageBodyProperty=new JT808HeaderMessageBodyProperty(5);
            jT808HeaderProperty.MsgNum = 135;
            jT808HeaderProperty.MsgId = JT808MsgId._0x0102.ToUInt16Value();
            //"01 02 00 05 01 38 12 34 56 78 00 87"
            var hex = JT808Serializer.Serialize(jT808HeaderProperty).ToHexString();
            Assert.Equal("010200050138123456780087", hex);
        }

        [Fact]
        public void Test5_2()
        {
            //01 02 00 05 01 38 12 34 56 78 00 87
            byte[] headerBytes = "01 02 00 05 01 38 12 34 56 78 00 87".ToHexBytes();
            JT808Header jT808Header = JT808Serializer.Deserialize<JT808Header>(headerBytes);
            Assert.Equal(135, jT808Header.MsgNum);
            Assert.Equal("13812345678", jT808Header.TerminalPhoneNo);
            Assert.False(jT808Header.MessageBodyProperty.IsPackage);
            Assert.Equal(JT808MsgId._0x0102.ToValue(), jT808Header.MsgId);
            Assert.Equal(5, jT808Header.MessageBodyProperty.DataLength);
        }

        [Fact]
        public void JT808Header_2019Test1()
        {
            JT808Header jT808HeaderProperty = new JT808Header
            {
                TerminalPhoneNo = "13812345678"
            };
            JT808HeaderMessageBodyProperty jT808HeaderMessageBodyProperty = new JT808HeaderMessageBodyProperty();
            jT808HeaderMessageBodyProperty.DataLength = 255;
            jT808HeaderMessageBodyProperty.IsPackage = true;
            jT808HeaderMessageBodyProperty.Encrypt = JT808EncryptMethod.RSA;
            jT808HeaderMessageBodyProperty.VersionFlag = true;
            jT808HeaderProperty.MessageBodyProperty = jT808HeaderMessageBodyProperty;
            jT808HeaderProperty.MsgNum = 135;
            jT808HeaderProperty.MsgId = JT808MsgId._0x0102.ToUInt16Value();
            jT808HeaderProperty.ProtocolVersion = 2;
            var hex = JT808Serializer.Serialize(jT808HeaderProperty).ToHexString();
            Assert.Equal("010264FF0200000000013812345678008700000000", hex);
        }

        [Fact]
        public void JT808Header_2019Test2()
        {
            byte[] headerBytes = "010264FF0200000000013812345678008700000000".ToHexBytes();
            JT808Header jT808Header = JT808Serializer.Deserialize<JT808Header>(headerBytes);
            Assert.Equal(135, jT808Header.MsgNum);
            Assert.Equal(2, jT808Header.ProtocolVersion);
            Assert.Equal("13812345678", jT808Header.TerminalPhoneNo);
            Assert.True(jT808Header.MessageBodyProperty.IsPackage);
            Assert.True(jT808Header.MessageBodyProperty.VersionFlag);
            Assert.Equal(JT808MsgId._0x0102.ToValue(), jT808Header.MsgId);
            Assert.Equal(255, jT808Header.MessageBodyProperty.DataLength);
        }
    }
}
