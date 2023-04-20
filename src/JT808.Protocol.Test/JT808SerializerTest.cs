using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.Interfaces;
using JT808.Protocol.Internal;
using JT808.Protocol.MessageBody;
using Xunit;

namespace JT808.Protocol.Test
{
    public class JT808SerializerTest
    {
        [Fact]
        public void ParallelTest1()
        {
            var result = Parallel.For(0, 100, new ParallelOptions { MaxDegreeOfParallelism = 2 }, (i) =>
            {
                IJT808Config jT808Config = new DefaultGlobalConfig();
                JT808Serializer jT808Serializer = new JT808Serializer(jT808Config);
            });
            if (result.IsCompleted)
            {

            }
        }

        [Fact]
        public unsafe void DefaultGlobalConfigTest1()
        {
            List<DefaultGlobalConfig> defaultGlobalConfigs = new List<DefaultGlobalConfig>();
            for (var i = 0; i < 100; i++)
            {
                if (i % 2 == 0)
                {
                    defaultGlobalConfigs.Add(new DefaultGlobalConfig(i.ToString()));
                }
                else
                {
                    defaultGlobalConfigs.Add(new DefaultGlobalConfig(i.ToString()));
                }
            }
        }

        [Fact]
        public void MergerTest()
        {
            var config = new DefaultGlobalConfig();
            config.EnableAutoMerge = true;
            config.AutoMergeTimeoutSecond = 5;
            var array = new[]
            {
                //分包数据第一包
                "7E010423F001127540038104D10002000100006B00000001040000025800000002040000000A00000003040000000500000004040000000A0000000504000000050000000604000000000000000704000000000000001005434D4E455400000011000000001200000000130D3232322E3138342E32362E323200000014000000001500000000160000000017000000001804000007E50000001904000007E50000002304000000000000002404000000000000002504000000000000002604000000000000003204000005000000001A000000001B04000000000000001C04000000000000001D0000000020040000000000000021040000000000000022040000012C000000270400001C2000000028040000001E00000029040000001E0000002C04000001F40000002D04000001F40000002E04000001F40000002F04000000C800000030040000001E000000310201F4000000400000000041000000004200000000430000000044000000004504000000010000004604FFFFFFFF0000004704FFFFFFFF0000004800000000490000000050040363160800000051040000000000000052040000000100000053040000000000000054040000000000000055040000000000000056040000001E0000005704000038400000005804000070800000005904000004B00000005A0400000E100000005B0200000000005C0207080000005D0224000000005E02001E00000064040000010100000065040000000000000070040000000500000071040000007F00000072040000004000000073040000004000000074040000007F00000080040004B9930000008102000000000082020000000000830CB2E2CAD4CAD3C6B5B3B5C1BE00000084010000000090010300000091010100000092010000000093040000000100000094010000000095040000000000000100040000000000000101020000000001020400000000000001030200000000C11204000000050000C11304000000050000C11404000000030000C11504000000000000C1160200000000C1170200000000C118033200030000C119030000000000CB00040000000200000075150103001E0F00000100000300191900000400002B00000000761B05000101010000020200000303000004040000050500000606020000000077A908010103001E0F00000100000300191900000400002B020103001E0F00000100000300191900000400002B030103001E0F00000100000300191900000400002B040103001E0F00000100000300191900000400002B050103001E0F00000100000300191900000400002B060103001E0F00000100000300191900000400002B070103001E0F00000100000300191900000400002B080103001E0F00000100000300191900000400002B0000007903140501C17E",
                //分包数据第二包
                "7E010420D301127540038104E2000200020000007A04000000000000007B0200000000007C1400000000000000000000000000000000000000000000F364381E06000E1007D003020101FFFFFFFFFFFFFFFFFF0000000302000000060302320503021B320503021E320503020A320503020302FFFFFFFF0000F365311E06000E1007D003020101FFFFFFFF00000007012C0078FFFFFF320503023205030232050302320503023205030200FFFF0000FF010537303231380000FF02144B352D50000000000000000000000000000000000000F373040BB80BB80000FF0006FEEA4C0510BD1F7E",
            };

            //正序
            for (int i = 1; i <= array.Length; i++)
            {
                var package = config.GetSerializer().Deserialize(array[i - 1].ToHexBytes());
                if (i == array.Length)
                {
                    Assert.NotNull(package.Bodies);
                    //分包合并成功并获取消息体进行处理
                    if (package.Bodies is JT808_0x0104 _0x0104 && _0x0104.AnswerParamsCount > 0)
                    {
                        Assert.NotEmpty(_0x0104.ParamList);
                    }
                }
                else
                {
                    Assert.Null(package.Bodies);
                }
            }

            //倒序
            for (int i = array.Length - 1; i >= 0; i--)
            {
                var package = config.GetSerializer().Deserialize(array[i].ToHexBytes());
                if (i == 0)
                {
                    Assert.NotNull(package.Bodies);
                    //分包合并成功并获取消息体进行处理
                    if (package.Bodies is JT808_0x0104 _0x0104 && _0x0104.AnswerParamsCount > 0)
                    {
                        Assert.NotEmpty(_0x0104.ParamList);
                    }
                }
                else
                {
                    Assert.Null(package.Bodies);
                }
            }

            //超时
            for (int i = 1; i <= array.Length; i++)
            {
                var package = config.GetSerializer().Deserialize(array[i - 1].ToHexBytes());
                if (i != array.Length)
                {
                    Thread.Sleep(TimeSpan.FromSeconds(config.AutoMergeTimeoutSecond + 1));
                }
                //由于超时导致合并分包失败，且package.Bodies为null
                Assert.Null(package.Bodies);
            }
        }
    }
}
