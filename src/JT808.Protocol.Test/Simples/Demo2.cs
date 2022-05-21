using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.Interfaces;
using JT808.Protocol.Internal;
using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xunit;

namespace JT808.Protocol.Test.Simples
{
    public class Demo2
    {
        public JT808Serializer JT808Serializer;
        public Demo2()
        {
            IJT808Config jT808Config = new DefaultGlobalConfig();
            JT808Serializer = new JT808Serializer(jT808Config);
        }

        [Fact]
        public void Test1()
        {
            //1.转成byte数组
            byte[] bytes = "7E 02 00 00 26 12 34 56 78 90 12 00 7D 02 00 00 00 01 00 00 00 02 00 BA 7F 0E 07 E4 F1 1C 00 28 00 3C 00 00 18 10 15 10 10 10 01 04 00 00 00 64 02 02 00 7D 01 13 7E".ToHexBytes();

            //2.将数组反序列化
            var jT808Package = JT808Serializer.Deserialize<JT808Package>(bytes);

            //3.数据包头
            Assert.Equal(Enums.JT808MsgId._0x0200.ToValue(), jT808Package.Header.MsgId);
            Assert.Equal(38, jT808Package.Header.MessageBodyProperty.DataLength);
            Assert.Equal(126, jT808Package.Header.MsgNum);
            Assert.Equal("123456789012", jT808Package.Header.TerminalPhoneNo);
            Assert.False(jT808Package.Header.MessageBodyProperty.IsPackage);
            Assert.Equal(0, jT808Package.Header.PackageIndex);
            Assert.Equal(0, jT808Package.Header.PackgeCount);
            Assert.Equal(JT808EncryptMethod.None, jT808Package.Header.MessageBodyProperty.Encrypt);

            //4.数据包体
            JT808_0x0200 jT808_0x0200 = (JT808_0x0200)jT808Package.Bodies;
            Assert.Equal((uint)1, jT808_0x0200.AlarmFlag);
            Assert.Equal((uint)40, jT808_0x0200.Altitude);
            Assert.Equal(DateTime.Parse("2018-10-15 10:10:10"), jT808_0x0200.GPSTime);
            Assert.Equal(12222222, jT808_0x0200.Lat);
            Assert.Equal(132444444, jT808_0x0200.Lng);
            Assert.Equal(60, jT808_0x0200.Speed);
            Assert.Equal(0, jT808_0x0200.Direction);
            Assert.Equal((uint)2, jT808_0x0200.StatusFlag);
            //4.1.附加信息1
            Assert.Equal(100, ((JT808_0x0200_0x01)jT808_0x0200.BasicLocationAttachData[JT808Constants.JT808_0x0200_0x01]).Mileage);
            //4.2.附加信息2
            Assert.Equal(125, ((JT808_0x0200_0x02)jT808_0x0200.BasicLocationAttachData[JT808Constants.JT808_0x0200_0x02]).Oil);
        }
    }
}
