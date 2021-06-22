using JT808.Protocol.Extensions.YueBiao.MessageBody;
using JT808.Protocol.Extensions.YueBiao.Metadata;
using JT808.Protocol.MessageBody;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Xunit;

namespace JT808.Protocol.Extensions.YueBiao.Test
{
    public class JT808_0x1212_Test
    {
        JT808Serializer JT808Serializer;
        public JT808_0x1212_Test()
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
            JT808_0x1212 jT808UploadLocationRequest = new JT808_0x1212
            {
                FileName = "FileName", 
                FileSize = 1,
                FileType = 2
            };
            var hex = JT808Serializer.Serialize(jT808UploadLocationRequest).ToHexString();
            Assert.Equal("0846696C654E616D650200000001", hex);
        }
        [Fact]
        public void Deserialize()
        {
            var jT808UploadLocationRequest = JT808Serializer.Deserialize<JT808_0x1212>("0846696C654E616D650200000001".ToHexBytes());
            Assert.Equal("FileName", jT808UploadLocationRequest.FileName);
            Assert.Equal(1u, jT808UploadLocationRequest.FileSize);
            Assert.Equal(2, jT808UploadLocationRequest.FileType);
            Assert.Equal("FileName".Length, jT808UploadLocationRequest.FileNameLength);
        }
        [Fact]
        public void Json()
        {
            var json = JT808Serializer.Analyze<JT808_0x1211>("0846696C654E616D650200000001".ToHexBytes());
        }
    }
}
