using JT808.Protocol.Enums;
using JT808.Protocol.Interfaces;
using JT808.Protocol.Internal;
using JT808.Protocol.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using JT808.Protocol.MessageBody;

namespace JT808.Protocol.Test.Simples
{
    public class Demo7
    {
        public JT808Serializer JT808Serializer;
        public Demo7()
        {
            IJT808Config jT808Config = new DefaultGlobalConfig();
            JT808Serializer = new JT808Serializer(jT808Config);
        }

        [Fact]
        public void Test1()
        {
            JT808Package jT808Package = JT808MsgId._0x8004.Create_查询服务器时间应答_2019("123456789012",
            new JT808_0x8004
            {
                Time = DateTime.Parse("2019-12-02 10:10:10"),
            });
            jT808Package.Header.ManualMsgNum = 1;
            byte[] data = JT808Serializer.Serialize(jT808Package);
            var hex = data.ToHexString();
            Assert.Equal("7E8004400601000000001234567890120001191202101010517E", hex);
        }

        [Fact]
        public void Test2()
        {
            var data = "7E8004400601000000001234567890120001191202101010517E".ToHexBytes();
            JT808Package jT808Package = JT808Serializer.Deserialize(data);
            Assert.Equal(JT808MsgId._0x8004.ToUInt16Value(), jT808Package.Header.MsgId);
            Assert.Equal(JT808Version.JTT2019, jT808Package.Version);
            Assert.True(jT808Package.Header.MessageBodyProperty.VersionFlag);
            Assert.Equal(DateTime.Parse("2019-12-02 10:10:10"), ((JT808_0x8004)jT808Package.Bodies).Time);
        }
    }
}
