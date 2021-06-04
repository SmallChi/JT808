using JT808.Protocol.Extensions.SuBiao.MessageBody;
using JT808.Protocol.Extensions.SuBiao.Metadata;
using JT808.Protocol.MessageBody;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace JT808.Protocol.Extensions.SuBiao.Test
{
    public class JT808_0x0900_Test
    {
        JT808Serializer JT808Serializer;
        public JT808_0x0900_Test()
        {
            ServiceCollection serviceDescriptors = new ServiceCollection();
            serviceDescriptors.AddJT808Configure()
                                        .AddSuBiaoConfigure();
            IJT808Config jT808Config = serviceDescriptors.BuildServiceProvider().GetRequiredService<IJT808Config>();
            JT808Serializer = new JT808Serializer(jT808Config);
        }

        [Fact]
        public void Test_0xF7_1()
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
        public void Test_0xF7_2()
        {
            var jT808_0x0900_0xF7 = JT808Serializer.Deserialize<JT808_0x0900_0xF7>("020105020000000102050200000001".ToHexBytes());

            Assert.Equal(JT808_SuBiao_Constants.JT808_0X0900_0xF7, jT808_0x0900_0xF7.PassthroughType);
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
        public void Test_0xF7_3()
        {
            var json = JT808Serializer.Analyze<JT808_0x0900_0xF7>("020105020000000102050200000001".ToHexBytes());
        }

        [Fact]
        public void Test_0xF8_1()
        {
            JT808_0x0900_0xF8 jT808_0x0900_0xF8 = new JT808_0x0900_0xF8
            {
                USBMessageCount = 1,
                USBMessages = new List<JT808_0x0900_0xF8_USB> {
                    new JT808_0x0900_0xF8_USB {
                        USBID = 1,
                        CompantName = "CompantName",
                        CustomerCode = "CustomerCode",
                        DevicesID = "DevicesID",
                        HardwareVersionNumber = "HardwareVersionNumber",
                        ProductModel = "ProductModel",
                        SoftwareVersionNumber = "SoftwareVersionNumber"
                    }
                }
            };
            var hex = JT808Serializer.Serialize(jT808_0x0900_0xF8).ToHexString();
            Assert.Equal("01015C0B436F6D70616E744E616D650C50726F647563744D6F64656C15486172647761726556657273696F6E4E756D62657215536F66747761726556657273696F6E4E756D626572094465766963657349440C437573746F6D6572436F6465", hex);
        }

        [Fact]
        public void Test_0xF8_2()
        {
            var jT808_0x0900_0xF8 = JT808Serializer.Deserialize<JT808_0x0900_0xF8>("01015C0B436F6D70616E744E616D650C50726F647563744D6F64656C15486172647761726556657273696F6E4E756D62657215536F66747761726556657273696F6E4E756D626572094465766963657349440C437573746F6D6572436F6465".ToHexBytes());
            Assert.Equal(JT808_SuBiao_Constants.JT808_0X0900_0xF8, jT808_0x0900_0xF8.PassthroughType);
            Assert.Equal(1, jT808_0x0900_0xF8.USBMessageCount);
            Assert.Equal(1, jT808_0x0900_0xF8.USBMessages[0].USBID);
            Assert.Equal("CompantName", jT808_0x0900_0xF8.USBMessages[0].CompantName);
            Assert.Equal("CompantName".Length, jT808_0x0900_0xF8.USBMessages[0].CompantNameLength);
            Assert.Equal("CustomerCode", jT808_0x0900_0xF8.USBMessages[0].CustomerCode);
            Assert.Equal("CustomerCode".Length, jT808_0x0900_0xF8.USBMessages[0].CustomerCodeLength);
            Assert.Equal("DevicesID", jT808_0x0900_0xF8.USBMessages[0].DevicesID);
            Assert.Equal("DevicesID".Length, jT808_0x0900_0xF8.USBMessages[0].DevicesIDLength);
            Assert.Equal("HardwareVersionNumber", jT808_0x0900_0xF8.USBMessages[0].HardwareVersionNumber);
            Assert.Equal("HardwareVersionNumber".Length, jT808_0x0900_0xF8.USBMessages[0].HardwareVersionNumberLength);
            Assert.Equal("ProductModel", jT808_0x0900_0xF8.USBMessages[0].ProductModel);
            Assert.Equal("ProductModel".Length, jT808_0x0900_0xF8.USBMessages[0].ProductModelLength);
            Assert.Equal("SoftwareVersionNumber", jT808_0x0900_0xF8.USBMessages[0].SoftwareVersionNumber);
            Assert.Equal("SoftwareVersionNumber".Length, jT808_0x0900_0xF8.USBMessages[0].SoftwareVersionNumberLength);
        }

        [Fact]
        public void Test_0xF8_3()
        {
            var json = JT808Serializer.Analyze<JT808_0x0900_0xF8>("01015C0B436F6D70616E744E616D650C50726F647563744D6F64656C15486172647761726556657273696F6E4E756D62657215536F66747761726556657273696F6E4E756D626572094465766963657349440C437573746F6D6572436F6465".ToHexBytes());
        }
    }
}
