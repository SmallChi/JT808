using JT808.Protocol.Extensions.SuBiao.MessageBody;
using JT808.Protocol.MessageBody;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace JT808.Protocol.Extensions.SuBiao.Test
{
    public class JT808_0x9212_Test
    {
        JT808Serializer JT808Serializer;
        public JT808_0x9212_Test()
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
            JT808_0x9212 jT808UploadLocationRequest = new JT808_0x9212
            {
                DataPackageCount=2,
                DataPackages=new List<Metadata.DataPackageProperty> {
                new Metadata.DataPackageProperty{ 
                        Length=10,
                        Offset=20
                      },
                new Metadata.DataPackageProperty{
                        Length=30,
                        Offset=40
                      }
                },
                FileName= "FileName",
                FileType=1,
                UploadResult=2
            };
            var hex = JT808Serializer.Serialize(jT808UploadLocationRequest).ToHexString();
            Assert.Equal("0846696C654E616D65010202000000140000000A000000280000001E", hex);
        }
        [Fact]
        public void Deserialize()
        {
            var jT808UploadLocationRequest = JT808Serializer.Deserialize<JT808_0x9212>("0846696C654E616D65010202000000140000000A000000280000001E".ToHexBytes());
            Assert.Equal(2, jT808UploadLocationRequest.DataPackageCount);
            Assert.Equal("FileName", jT808UploadLocationRequest.FileName);
            Assert.Equal(1, jT808UploadLocationRequest.FileType);
            Assert.Equal(2, jT808UploadLocationRequest.UploadResult);

            Assert.Equal(10u, jT808UploadLocationRequest.DataPackages[0].Length);
            Assert.Equal(20u, jT808UploadLocationRequest.DataPackages[0].Offset);

            Assert.Equal(30u, jT808UploadLocationRequest.DataPackages[1].Length);
            Assert.Equal(40u, jT808UploadLocationRequest.DataPackages[1].Offset);
        }
        [Fact]
        public void Json()
        {
            var json = JT808Serializer.Analyze<JT808_0x9212>("0846696C654E616D65010202000000140000000A000000280000001E".ToHexBytes());
        }
    }
}
