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
using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;
using System.Text.Json;
using JT808.Protocol.MessageBody.CarDVR;
using System.Linq;
using JT808.Protocol.Test.JT808LocationAttach;
using static JT808.Protocol.MessageBody.JT808_0x8105;
using System.Buffers.Binary;
using Newtonsoft.Json;

namespace JT808.Protocol.Test.Simples
{
    public class Demo14
    {
        JT808Serializer JT808Serializer;

        public Demo14()
        {
            IServiceCollection serviceDescriptors = new ServiceCollection();
            serviceDescriptors.AddJT808Configure();
            //通常在startup中使用app的Use进行扩展
            //The Use of the app is typically extended in startup  
            IServiceProvider serviceProvider = serviceDescriptors.BuildServiceProvider();
            JT808Serializer = serviceProvider.GetRequiredService<IJT808Config>().GetSerializer();
        }

        [Fact]
        public void Test1()
        {
            var bytes = "7e0102400c01003000068109024a3130303330303030363831857e".ToHexBytes();
            JT808Package jT808Package = JT808Serializer.Deserialize<JT808Package>(bytes, JT808Version.JTT2013Force);//Force the 2013 protocol conversion
            //因头部的版本标识号是2019，实际上是使用2013的协议
            //Since the version id in the header is 2019, it actually uses the 2013 protocol  
            jT808Package.Header.MessageBodyProperty.VersionFlag = false;
            //修改版本号之后，重新编码协议，以便后续服务正常使用
            //After the version number is changed, the protocol is re-encoded to facilitate the normal use of subsequent services  
            var newBytes =JT808Serializer.Serialize(jT808Package);
            JT808Package jT808PackageNew = JT808Serializer.Deserialize<JT808Package>(newBytes);
        }
    }
}
