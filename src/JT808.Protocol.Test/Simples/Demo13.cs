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
    public class Demo13
    {
        JT808Serializer JT808Serializer;

        public Demo13()
        {
            IServiceCollection serviceDescriptors = new ServiceCollection();
            serviceDescriptors.AddJT808Configure(new DefaultGlobalConfig("replace"));
            //通常在startup中使用app的Use进行扩展
            //The Use of the app is typically extended in startup  
            IServiceProvider serviceProvider = serviceDescriptors.BuildServiceProvider();
            Use(serviceProvider);
        }

        void Use(IServiceProvider serviceProvider)
        {
            IJT808Config jT808Config = serviceProvider.GetRequiredService<DefaultGlobalConfig>();
            //替换原有消息存在的BUG
            //Replace the bugs in the original message
            jT808Config.ReplaceMsgId<JT808_0x0001, JT808_0x0001_Replace>();
            JT808Serializer = jT808Config.GetSerializer();
        }

        [Fact]
        public void Test1()
        {
            JT808Package jT808Package = new JT808Package
            {
                Header = new JT808Header
                {
                    MsgId = Enums.JT808MsgId._0x0001.ToUInt16Value(),
                    ManualMsgNum = 1203,
                    TerminalPhoneNo = "012345678900",
                },
                Bodies = new JT808_0x0001_Replace
                {
                    ReplyMsgId = Enums.JT808MsgId._0x0002.ToUInt16Value(),
                    ReplyMsgNum = 1000,
                    TerminalResult = Enums.JT808TerminalResult.Success,
                    Test=168
                }
            };
            var hex = JT808Serializer.Serialize(jT808Package).ToHexString();
            Assert.Equal("7E0001000701234567890004B303E800020000A8797E", hex);
        }

        [Fact]
        public void Test2()
        {
            var bytes = "7E0001000701234567890004B303E800020000A8797E".ToHexBytes();
            JT808Package jT808Package = JT808Serializer.Deserialize<JT808Package>(bytes);
            Assert.Equal(Enums.JT808MsgId._0x0001.ToValue(), jT808Package.Header.MsgId);
            Assert.Equal(1203, jT808Package.Header.MsgNum);
            JT808_0x0001_Replace JT808Bodies = (JT808_0x0001_Replace)jT808Package.Bodies;
            Assert.Equal(Enums.JT808MsgId._0x0002.ToUInt16Value(), JT808Bodies.ReplyMsgId);
            Assert.Equal(1000, JT808Bodies.ReplyMsgNum);
            Assert.Equal(Enums.JT808TerminalResult.Success, JT808Bodies.TerminalResult);
            Assert.Equal(168u, JT808Bodies.Test);
        }
    }

    /// <summary>
    /// 终端通用应答_替换原有的消息，来解决库现有的bug
    /// The terminal general answer _ replaces the original message to solve the existing library bug  
    /// </summary>
    public class JT808_0x0001_Replace : JT808MessagePackFormatter<JT808_0x0001_Replace>, JT808Bodies, IJT808Analyze
    {
        /// <summary>
        /// 0x0001
        /// </summary>
        public ushort MsgId => 0x0001;
        /// <summary>
        /// 终端通用应答
        /// </summary>
        public string Description => "终端通用应答";
        /// <summary>
        /// 应答流水号
        /// 对应的平台消息的流水号
        /// </summary>
        public ushort ReplyMsgNum { get; set; }
        /// <summary>
        /// 应答 ID
        /// 对应的平台消息的 ID
        /// <see cref="JT808.Protocol.Enums.JT808MsgId"/>
        /// </summary>
        public ushort ReplyMsgId { get; set; }

        /// <summary>
        /// 结果
        /// 0：成功/确认；1：失败；2：消息有误；3：不支持
        /// </summary>
        public JT808TerminalResult TerminalResult { get; set; }
        /// <summary>
        /// 测试
        /// </summary>
        public ushort Test { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x0001_Replace Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0001_Replace jT808_0X0001 = new JT808_0x0001_Replace();
            jT808_0X0001.ReplyMsgNum = reader.ReadUInt16();
            jT808_0X0001.ReplyMsgId = reader.ReadUInt16();
            jT808_0X0001.TerminalResult = (JT808TerminalResult)reader.ReadByte();
            jT808_0X0001.Test = reader.ReadUInt16();
            return jT808_0X0001;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0001_Replace value, IJT808Config config)
        {
            writer.WriteUInt16(value.ReplyMsgNum);
            writer.WriteUInt16(value.ReplyMsgId);
            writer.WriteByte((byte)value.TerminalResult);
            writer.WriteUInt16(value.Test);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            var replyMsgNum = reader.ReadUInt16();
            var replyMsgId = reader.ReadUInt16();
            var terminalResult = reader.ReadByte();
            var test = reader.ReadUInt16();
            writer.WriteNumber($"[{replyMsgNum.ReadNumber()}]应答流水号", replyMsgNum);
            writer.WriteNumber($"[{replyMsgId.ReadNumber()}]应答消息Id", replyMsgId);
            writer.WriteString($"[{terminalResult.ReadNumber()}]结果", ((JT808TerminalResult)terminalResult).ToString());
            writer.WriteNumber($"[{test.ReadNumber()}]测试", test);
        }
    }
}
