using JT808.Protocol.Extensions.YueBiao.MessageBody;
using JT808.Protocol.Extensions.YueBiao.Metadata;
using JT808.Protocol.MessageBody;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Xunit;

namespace JT808.Protocol.Extensions.YueBiao.Test
{
    public class JT808_0x1FC4_Test
    {
        JT808Serializer JT808Serializer;
        public JT808_0x1FC4_Test()
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
            JT808_0x1FC4 jT808UploadLocationRequest = new JT808_0x1FC4
            {
                ErrorCode=1,
                 MsgNum=2,
                  UpgradeStatus= Enums.JT808UpgradeStatus.firmware_download,
                   UpgradeType= Protocol.Enums.JT808UpgradeType.beidou_module,
                    UploadProgress=3
            };
            var hex = JT808Serializer.Serialize(jT808UploadLocationRequest).ToHexString();
            Assert.Equal("000234010301", hex);
        }
        [Fact]
        public void Deserialize()
        {
            var jT808UploadLocationRequest = JT808Serializer.Deserialize<JT808_0x1FC4>("000234010301".ToHexBytes());
            Assert.Equal(1, jT808UploadLocationRequest.ErrorCode);
            Assert.Equal(2, jT808UploadLocationRequest.MsgNum);
            Assert.Equal(Enums.JT808UpgradeStatus.firmware_download, jT808UploadLocationRequest.UpgradeStatus);
            Assert.Equal(Protocol.Enums.JT808UpgradeType.beidou_module, jT808UploadLocationRequest.UpgradeType);
            Assert.Equal(3, jT808UploadLocationRequest.UploadProgress);
        }
        [Fact]
        public void Json()
        {
            var json = JT808Serializer.Analyze<JT808_0x1FC4>("000234010301".ToHexBytes());
        }
    }
}
