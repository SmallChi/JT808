﻿using JT808.Protocol.Extensions.SuBiao.MessageBody;
using JT808.Protocol.MessageBody;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace JT808.Protocol.Extensions.SuBiao.Test
{
    public class JT808_0x8900_Test
    {
        JT808Serializer JT808Serializer;
        public JT808_0x8900_Test()
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
            JT808_0x8900_0xF7 jT808UploadLocationRequest = new JT808_0x8900_0xF7
            {
                  USBCount=2,
                  MultipleUSB=new List<byte> {1,2 }
            };
            var hex = JT808Serializer.Serialize(jT808UploadLocationRequest).ToHexString();
            Assert.Equal("020102", hex);
        }
        [Fact]
        public void Deserialize()
        {
            var jT808UploadLocationRequest = JT808Serializer.Deserialize<JT808_0x8900_0xF7>("020102".ToHexBytes());
            Assert.Equal(2, jT808UploadLocationRequest.USBCount);
            Assert.Equal(new List<byte> { 1, 2 }.ToArray().ToHexString(), jT808UploadLocationRequest.MultipleUSB.ToArray().ToHexString());
        }
        [Fact]
        public void Json()
        {
            var json = JT808Serializer.Analyze<JT808_0x8900_0xF7>("020102".ToHexBytes());
        }
    }
}
