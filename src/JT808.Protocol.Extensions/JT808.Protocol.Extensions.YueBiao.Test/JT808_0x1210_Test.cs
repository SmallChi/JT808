using JT808.Protocol.Extensions.YueBiao.MessageBody;
using JT808.Protocol.Extensions.YueBiao.Metadata;
using JT808.Protocol.MessageBody;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Xunit;

namespace JT808.Protocol.Extensions.YueBiao.Test
{
    public class JT808_0x1210_Test
    {
        JT808Serializer JT808Serializer;
        public JT808_0x1210_Test()
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
            JT808_0x1210 jT808UploadLocationRequest = new JT808_0x1210
            {
                AlarmId = "11111111111111111111111111111111",
                AlarmIdentification = new Metadata.AlarmIdentificationProperty
                {
                     Retain1 = 1,
                      Retain2=3,
                    AttachCount = 2,
                    SN = 3,
                     TerminalId = "4444444",
                    Time = Convert.ToDateTime("2019-12-10 18:31:00")
                },
                AttachInfos = new List<Metadata.AttachProperty> {
                    new Metadata.AttachProperty{
                        FileName="filename",
                        FileSize=9
                    },
                    new Metadata.AttachProperty{
                        FileName="filename1",
                        FileSize=10
                    }
                },
                TerminalId = "4444444",
                InfoType = 0
            };
            var hex = JT808Serializer.Serialize(jT808UploadLocationRequest).ToHexString();
            Assert.Equal("34343434343434000000000000000000000000000000000000000000000034343434343434000000000000000000000000000000000000000000000019121018310003020103313131313131313131313131313131313131313131313131313131313131313100020866696C656E616D65000000090966696C656E616D65310000000A", hex);

        }

        [Fact]
        public void Deserialize()
        {
            var jT808UploadLocationRequest = JT808Serializer.Deserialize<JT808_0x1210>("34343434343434000000000000000000000000000000000000000000000034343434343434000000000000000000000000000000000000000000000019121018310003020103313131313131313131313131313131313131313131313131313131313131313100020866696C656E616D65000000090966696C656E616D65310000000A".ToHexBytes());
            Assert.Equal("11111111111111111111111111111111", jT808UploadLocationRequest.AlarmId);
            Assert.Equal(2, jT808UploadLocationRequest.AlarmIdentification.AttachCount);
            Assert.Equal(3, jT808UploadLocationRequest.AlarmIdentification.SN);
            Assert.Equal(1, jT808UploadLocationRequest.AlarmIdentification.Retain1);
            Assert.Equal(3, jT808UploadLocationRequest.AlarmIdentification.Retain2);
            Assert.Equal("4444444", jT808UploadLocationRequest.AlarmIdentification.TerminalId);
            Assert.Equal(Convert.ToDateTime("2019-12-10 18:31:00"), jT808UploadLocationRequest.AlarmIdentification.Time);
            Assert.Equal(2, jT808UploadLocationRequest.AttachCount);

            Assert.Equal("filename", jT808UploadLocationRequest.AttachInfos[0].FileName);
            Assert.Equal(8, jT808UploadLocationRequest.AttachInfos[0].FileNameLength);
            Assert.Equal(9u, jT808UploadLocationRequest.AttachInfos[0].FileSize);

            Assert.Equal("filename1", jT808UploadLocationRequest.AttachInfos[1].FileName);
            Assert.Equal(9, jT808UploadLocationRequest.AttachInfos[1].FileNameLength);
            Assert.Equal(10u, jT808UploadLocationRequest.AttachInfos[1].FileSize);

            Assert.Equal("4444444", jT808UploadLocationRequest.TerminalId);
            Assert.Equal(0, jT808UploadLocationRequest.InfoType);

        }
        [Fact]
        public void Json()
        {
            var json = JT808Serializer.Analyze<JT808_0x1210>("34343434343434000000000000000000000000000000000000000000000034343434343434000000000000000000000000000000000000000000000019121018310003020103313131313131313131313131313131313131313131313131313131313131313100020866696C656E616D65000000090966696C656E616D65310000000A".ToHexBytes());
        }
    }
}
