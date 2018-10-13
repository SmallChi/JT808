using JT808.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using JT808.Protocol.Extensions;

namespace JT808.Protocol.Test
{
    public class JT808HeaderTest: JT808PackageBase
    {
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
            JT808Header jT808HeaderProperty = new JT808Header();
            jT808HeaderProperty.TerminalPhoneNo = "13812345678";
            jT808HeaderProperty.MessageBodyProperty.DataLength = 5;
            jT808HeaderProperty.MsgNum = 135;
            jT808HeaderProperty.MsgId = JT808MsgId.终端鉴权;
            //"01 02 00 05 01 38 12 34 56 78 00 87"
            var hex = JT808Serializer.Serialize(jT808HeaderProperty).ToHexString();
        }

        [Fact]
        public void Test1_2()
        {
            //01 02 00 05 01 38 12 34 56 78 00 87
            byte[] headerBytes = "01 02 00 05 01 38 12 34 56 78 00 87".ToHexBytes();
            JT808Header jT808Header = JT808Serializer.Deserialize<JT808Header>(headerBytes);
            Assert.Equal(135, jT808Header.MsgNum);
            Assert.Equal("13812345678", jT808Header.TerminalPhoneNo);
            Assert.False(jT808Header.MessageBodyProperty.IsPackge);
            Assert.Equal(JT808MsgId.终端鉴权, jT808Header.MsgId);
            Assert.Equal(5, jT808Header.MessageBodyProperty.DataLength);
        }

        [Fact]
        public void Test5()
        {
            JT808Header jT808HeaderProperty = new JT808Header();
            jT808HeaderProperty.TerminalPhoneNo = "13812345678";
            jT808HeaderProperty.MessageBodyProperty.DataLength = 5;
            jT808HeaderProperty.MsgNum = 135;
            jT808HeaderProperty.MsgId = JT808MsgId.终端鉴权;
            //"01 02 00 05 01 38 12 34 56 78 00 87"
            var hex = JT808Serializer.Serialize(jT808HeaderProperty).ToHexString();
            Assert.Equal("01 02 00 05 01 38 12 34 56 78 00 87", hex);
        }

        [Fact]
        public void Test5_2()
        {
            //01 02 00 05 01 38 12 34 56 78 00 87
            byte[] headerBytes = "01 02 00 05 01 38 12 34 56 78 00 87".ToHexBytes();
            JT808Header jT808Header = JT808Serializer.Deserialize<JT808Header>(headerBytes);
            Assert.Equal(135, jT808Header.MsgNum);
            Assert.Equal("13812345678", jT808Header.TerminalPhoneNo);
            Assert.False(jT808Header.MessageBodyProperty.IsPackge);
            Assert.Equal(JT808MsgId.终端鉴权, jT808Header.MsgId);
            Assert.Equal(5, jT808Header.MessageBodyProperty.DataLength);
        }
    }
}
