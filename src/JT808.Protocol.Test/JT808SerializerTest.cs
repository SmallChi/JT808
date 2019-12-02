using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.Interfaces;
using JT808.Protocol.Internal;
using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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
        public void ReadOnlySpanTest1()
        {
            IJT808Config jT808Config = new DefaultGlobalConfig();
            JT808Serializer jT808Serializer = new JT808Serializer(jT808Config);
            JT808Package jT808Package = new JT808Package
            {
                Header = new JT808Header
                {
                    MsgId = Enums.JT808MsgId.终端通用应答.ToUInt16Value(),
                    MsgNum = 1203,
                    TerminalPhoneNo = "012345678900",
                    MessageBodyProperty=new JT808HeaderMessageBodyProperty()
                },
                Bodies = new JT808_0x0001
                {
                    ReplyMsgId = Enums.JT808MsgId.终端心跳.ToUInt16Value(),
                    ReplyMsgNum = 1000,
                    JT808TerminalResult = Enums.JT808TerminalResult.Success
                }
            };
            var hexSpan = jT808Serializer.SerializeReadOnlySpan(jT808Package);
            Assert.Equal(0x7e, hexSpan[0]);
        }

        [Fact]
        public unsafe void DefaultGlobalConfigTest1()
        {
            List<DefaultGlobalConfig> defaultGlobalConfigs = new List<DefaultGlobalConfig>();
            for(var i = 0; i < 100; i++)
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
    }
}
