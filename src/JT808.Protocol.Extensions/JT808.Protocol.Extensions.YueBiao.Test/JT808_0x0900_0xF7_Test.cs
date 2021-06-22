using JT808.Protocol.Extensions.YueBiao.MessageBody;
using JT808.Protocol.Extensions.YueBiao.Metadata;
using JT808.Protocol.MessageBody;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Xunit;

namespace JT808.Protocol.Extensions.YueBiao.Test
{
    public class JT808_0x0900_0xF7_Test
    {
        JT808Serializer JT808Serializer;
        public JT808_0x0900_0xF7_Test()
        {
            ServiceCollection serviceDescriptors = new ServiceCollection();
            serviceDescriptors.AddJT808Configure()
                                        .AddYueBiaoConfigure();
            IJT808Config jT808Config = serviceDescriptors.BuildServiceProvider().GetRequiredService<IJT808Config>();
            JT808Serializer = new JT808Serializer(jT808Config);
        }
        [Fact]
        public void Serializer()
        {
            JT808_0x0900_0xF7 jT808_0x0900_0xF7 = new JT808_0x0900_0xF7
            {                 
                USBMessageCount = 2,
                USBMessages = new List<JT808_0x0900_0xF7_USB> {
                    new JT808_0x0900_0xF7_USB {
                        USBID = 1,
                        AlarmStatus = 1,
                        WorkingCondition = 2
                    },
                    new JT808_0x0900_0xF7_USB {
                        USBID = 2,
                        AlarmStatus = 1,
                        WorkingCondition = 2
                    }
                }
            };
            var hex = JT808Serializer.Serialize(jT808_0x0900_0xF7).ToHexString();
            Assert.Equal("020105020000000102050200000001", hex);
        }

        [Fact]
        public void Deserialize()
        {
            var jT808_0x0900_0xF7 = JT808Serializer.Deserialize<JT808_0x0900_0xF7>("020105020000000102050200000001".ToHexBytes());

            Assert.Equal(JT808_YueBiao_Constants.JT808_0X0900_0xF7, jT808_0x0900_0xF7.PassthroughType);
            Assert.Equal(2, jT808_0x0900_0xF7.USBMessageCount);
            Assert.Equal(1, jT808_0x0900_0xF7.USBMessages[0].USBID);
            Assert.Equal(5, jT808_0x0900_0xF7.USBMessages[0].MessageLength);
            Assert.Equal(2, jT808_0x0900_0xF7.USBMessages[0].WorkingCondition);
            Assert.Equal(1u, jT808_0x0900_0xF7.USBMessages[0].AlarmStatus);

            Assert.Equal(2, jT808_0x0900_0xF7.USBMessages[1].USBID);
            Assert.Equal(5, jT808_0x0900_0xF7.USBMessages[1].MessageLength);
            Assert.Equal(2, jT808_0x0900_0xF7.USBMessages[1].WorkingCondition);
            Assert.Equal(1u, jT808_0x0900_0xF7.USBMessages[1].AlarmStatus);
        }
        [Fact]
        public void Json()
        {
            var json = JT808Serializer.Analyze<JT808_0x0900_0xF7>("020105020000000102050200000001".ToHexBytes());
        }
    }
}
