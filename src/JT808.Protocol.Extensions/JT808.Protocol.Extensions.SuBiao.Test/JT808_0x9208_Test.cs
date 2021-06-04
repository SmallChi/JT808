using JT808.Protocol.Extensions.SuBiao.MessageBody;
using JT808.Protocol.MessageBody;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace JT808.Protocol.Extensions.SuBiao.Test
{
    public class JT808_0x9208_Test
    {
        JT808Serializer JT808Serializer;
        public JT808_0x9208_Test()
        {
            ServiceCollection serviceDescriptors = new ServiceCollection();
            serviceDescriptors.AddJT808Configure()
                                        .AddSuBiaoConfigure();
            IJT808Config jT808Config = serviceDescriptors.BuildServiceProvider().GetRequiredService<IJT808Config>();
            JT808Serializer = new JT808Serializer(jT808Config);
        }
        [Fact]
        public void Serializer()
        {
            JT808_0x9208 jT808UploadLocationRequest = new JT808_0x9208
            {
                AlarmId="11111111111111111111111111111111",
                AlarmIdentification=new Metadata.AlarmIdentificationProperty {
                    Retain=1,
                    AttachCount = 2,
                    SN = 3,
                    TerminalID = "4444444",
                    Time = Convert.ToDateTime("2019-12-10 18:31:00")
                },
                AttachmentServerIP="192.168.1.1",
                AttachmentServerIPTcpPort=5000,
                AttachmentServerIPUdpPort=5001
            };
            var hex = JT808Serializer.Serialize(jT808UploadLocationRequest).ToHexString();
            Assert.Equal("0B3139322E3136382E312E311388138934343434343434191210183100030201313131313131313131313131313131313131313131313131313131313131313100000000000000000000000000000000", hex);
        }
        [Fact]
        public void Deserialize()
        {
            var jT808UploadLocationRequest = JT808Serializer.Deserialize<JT808_0x9208>("0B3139322E3136382E312E311388138934343434343434191210183100030201313131313131313131313131313131313131313131313131313131313131313100000000000000000000000000000000".ToHexBytes());
            Assert.Equal("11111111111111111111111111111111", jT808UploadLocationRequest.AlarmId);
            Assert.Equal(1, jT808UploadLocationRequest.AlarmIdentification.Retain);
            Assert.Equal(2, jT808UploadLocationRequest.AlarmIdentification.AttachCount);
            Assert.Equal(3, jT808UploadLocationRequest.AlarmIdentification.SN);
            Assert.Equal("4444444", jT808UploadLocationRequest.AlarmIdentification.TerminalID);
            Assert.Equal(Convert.ToDateTime("2019-12-10 18:31:00"), jT808UploadLocationRequest.AlarmIdentification.Time);
            Assert.Equal("192.168.1.1", jT808UploadLocationRequest.AttachmentServerIP);
            Assert.Equal("192.168.1.1".Length, jT808UploadLocationRequest.AttachmentServerIPLength);
            Assert.Equal(5000, jT808UploadLocationRequest.AttachmentServerIPTcpPort);
            Assert.Equal(5001, jT808UploadLocationRequest.AttachmentServerIPUdpPort);
        }

        [Fact]
        public void Json()
        {
            var json = JT808Serializer.Analyze<JT808_0x9208>("0B3139322E3136382E312E311388138934343434343434191210183100030201313131313131313131313131313131313131313131313131313131313131313100000000000000000000000000000000".ToHexBytes());
        }
    }
}
