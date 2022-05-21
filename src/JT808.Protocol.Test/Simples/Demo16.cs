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
    public class Demo16
    {
        JT808Serializer JT808Serializer;

        IJT808Config JT808Config;

        public Demo16()
        {
            IServiceCollection serviceDescriptors = new ServiceCollection();
            serviceDescriptors.AddJT808Configure();
            //通常在startup中使用app的Use进行扩展
            IServiceProvider serviceProvider = serviceDescriptors.BuildServiceProvider();
            JT808Config = serviceProvider.GetRequiredService<IJT808Config>();
            JT808Serializer = JT808Config.GetSerializer();
        }

        [Fact]
        public void Test1()
        {
            var upgradePackage = new byte[1024 * 10];
            var splitPackage = JT808Config.SplitPackageStrategy.Processor(upgradePackage).ToList();
            ushort packgeCount = (ushort)splitPackage.Count();
            List<string> hexs = new List<string>();
            for (ushort i = 1; i <= packgeCount; i++)
            {
                if (i == 1)
                {
                    var bodies = new JT808_0x8108
                    {
                        UpgradeType = JT808UpgradeType.terminal,
                        VersionNum = "v1.2.1",
                        MakerId = "1234",
                        UpgradePackage = splitPackage[i - 1].Data
                    };
                    var firstBodies = JT808Serializer.Serialize(bodies);
                    JT808Package package = new JT808Package
                    {
                        Header = new JT808Header
                        {
                            MsgId = Enums.JT808MsgId._0x8108.ToUInt16Value(),
                            ManualMsgNum = 10,
                            TerminalPhoneNo = "123456789",
                            MessageBodyProperty = new JT808HeaderMessageBodyProperty()
                            {
                                IsPackage = true,
                            },
                            PackgeCount = packgeCount,
                            PackageIndex = i
                        },
                        SubDataBodies = firstBodies
                    };
                    var hex = JT808Serializer.Serialize(package).ToHexString();
                    hexs.Add(hex);
                }
                else
                {
                    JT808Package package = new JT808Package
                    {
                        Header = new JT808Header
                        {
                            MsgId = Enums.JT808MsgId._0x8108.ToUInt16Value(),
                            ManualMsgNum = 10,
                            TerminalPhoneNo = "123456789",
                            MessageBodyProperty = new JT808HeaderMessageBodyProperty()
                            {
                                IsPackage = true,
                            },
                            PackgeCount = packgeCount,
                            PackageIndex = i
                        },
                        SubDataBodies = splitPackage[i - 1].Data
                    };
                    var hex = JT808Serializer.Serialize(package).ToHexString();
                    hexs.Add(hex);
                }
            }
        }
    }
}
