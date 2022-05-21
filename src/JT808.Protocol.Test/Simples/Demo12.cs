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
    public class Demo12
    {
        JT808Serializer JT808Serializer;

        public Demo12()
        {
            IJT808Config jT808Config = new DefaultGlobalConfig();
            jT808Config.JT808_0x8105_Cusotm_Factory.SetMap<Koike1CommandParameter>();
            jT808Config.JT808_0x8105_Cusotm_Factory.SetMap<Koike2CommandParameter>();
            jT808Config.JT808_0x8105_Cusotm_Factory.SetMap<Koike3CommandParameter>();
            JT808Serializer = new JT808Serializer(jT808Config);
        }

        [Fact]
        public void Test1()
        {
            JT808Package jT808Package = new JT808Package
            {
                Header = new JT808Header
                {
                    MsgId = Enums.JT808MsgId._0x8105.ToUInt16Value(),
                    ManualMsgNum = 1,
                    TerminalPhoneNo = "12345678900",
                },
                Bodies = new JT808_0x8105
                {
                    CommandWord = 1,
                    CustomCommandParameters=new List<ICommandParameter>
                    {
                        new Koike1CommandParameter
                        {
                             Value=23
                        },
                        new Koike2CommandParameter
                        {
                             Value="SmallChi"
                        },
                        new Koike3CommandParameter
                        {
                             Value=new Koike3Object
                             {
                                  Value1=0xff,
                                  Value2="Happy"
                             }
                        }
                    }
                }
            };
            var hex = JT808Serializer.Serialize(jT808Package).ToHexString();
            Assert.Equal("7E8105002A0123456789000001013B3B3B3B3B3B3B3B3B3B3B3B3B000000173B536D616C6C43686900003BFF486170707900000000003B827E", hex);
        }

        [Fact]
        public void Test2()
        {
            byte[] bytes = "7E8105002A0123456789000001013B3B3B3B3B3B3B3B3B3B3B3B3B000000173B536D616C6C43686900003BFF486170707900000000003B827E".ToHexBytes();
            var jT808Package = JT808Serializer.Deserialize<JT808Package>(bytes);
            Assert.Equal(Enums.JT808MsgId._0x8105.ToUInt16Value(), jT808Package.Header.MsgId);
            Assert.Equal(1, jT808Package.Header.MsgNum);
            Assert.Equal("12345678900", jT808Package.Header.TerminalPhoneNo);
            var JT808_0x8105 = (JT808_0x8105)jT808Package.Bodies;
            Assert.Equal(1, JT808_0x8105.CommandWord);
            Assert.Equal(23u, JT808_0x8105.CustomCommandParameters.GetCommandParameter<Koike1CommandParameter>().Value.Value);
            Assert.Equal("SmallChi", JT808_0x8105.CustomCommandParameters.GetCommandParameter<Koike2CommandParameter>().Value);
            Assert.Equal(new Koike3Object() 
            {
                Value1 = 0xff,
                Value2 = "Happy"
            }, JT808_0x8105.CustomCommandParameters.GetCommandParameter<Koike3CommandParameter>().Value);
        }

        [Fact]
        public void Test3()
        {
            byte[] bytes = "7E8105002A0123456789000001013B3B3B3B3B3B3B3B3B3B3B3B3B000000173B536D616C6C43686900003BFF486170707900000000003B827E".ToHexBytes();
            var json = JT808Serializer.Analyze(bytes); 
        }

        [Fact]
        public void Test4()
        {
            var ex= Assert.Throws<System.ArgumentException>(() => 
            {
                IJT808Config jT808Config = new DefaultGlobalConfig();
                jT808Config.JT808_0x8105_Cusotm_Factory.SetMap<ErrorCommandParameter>();
            });
            Assert.Equal(ex.Message, $"{typeof(ErrorCommandParameter).FullName} Order is {3}. We're starting at 13 and we're incremying by 1.");
        }

        /// <summary>
        /// ICusotmCommandParameter  自定义命令参数接口
        /// ICommandParameterValue<> 对应的数据类型值
        /// 注意：Order必须从13开始逐一递增
        /// </summary>
        public class Koike1CommandParameter : ICusotmCommandParameter, ICommandParameterValue<uint?>
        {
            public int Order => 13;

            public string CommandName => "Koike1";

            public uint? Value { get; set; }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public byte[] ToBytes()
            {
                if (!Value.HasValue) return default;
                var value = new byte[4];
                BinaryPrimitives.WriteUInt32BigEndian(value, Value.Value);
                return value;
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="bytes"></param>
            public void ToValue(byte[] bytes)
            {
                if (bytes != null && bytes.Length > 0)
                {
                    Value = BinaryPrimitives.ReadUInt32BigEndian(bytes);
                }
            }
        }
        /// <summary>
        /// ICusotmCommandParameter  自定义命令参数接口
        /// ICommandParameterValue<> 对应的数据类型值
        /// 注意：Order必须从13开始逐一递增
        /// </summary>
        public class Koike2CommandParameter : ICusotmCommandParameter, ICommandParameterValue<string>
        {
            public int Order => 14;
            public string CommandName => "Koike2";
            public string Value { get; set; }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public byte[] ToBytes()
            {
                if (string.IsNullOrEmpty(Value)) return default;
                return JT808Constants.Encoding.GetBytes(Value.PadRight(10, '\0'));
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="bytes"></param>
            public void ToValue(byte[] bytes)
            {
                if (bytes != null && bytes.Length > 0)
                {
                    Value = JT808Constants.Encoding.GetString(bytes).Trim('\0');
                }
            }
        }
        /// <summary>
        /// ICusotmCommandParameter  自定义命令参数接口
        /// ICommandParameterValue<> 对应的数据类型值
        /// 注意：Order必须从13开始逐一递增
        /// </summary>
        public class Koike3CommandParameter : ICusotmCommandParameter, ICommandParameterValue<Koike3Object>
        {
            public int Order => 15;
            public string CommandName => "Koike3";
            public Koike3Object Value { get; set; }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public byte[] ToBytes()
            {
                if (Value==null) return default;
                return Value.ToBytes();
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="bytes"></param>
            public void ToValue(byte[] bytes)
            {
                if (bytes != null && bytes.Length > 0)
                {
                    Value = new Koike3Object();
                    Value.ToValue(bytes);
                }
            }
        }
        /// <summary>
        /// Koike3为对象
        /// ICommandParameterConvert:命令参数转换
        /// ToString:为对象类型用于分析器自定义显示
        /// </summary>
        public record Koike3Object : ICommandParameterConvert
        {
            public byte Value1 { get; set; }
            public string Value2 { get; set; }
            public byte[] ToBytes()
            {
                byte[] value = new byte[11];
                value[0] = Value1;
                var val2 = JT808Constants.Encoding.GetBytes(Value2.PadRight(10, '\0'));
                Array.Copy(val2, 0, value, 1, val2.Length);
                return value;
            }
            public void ToValue(byte[] bytes)
            {
                if (bytes != null && bytes.Length > 0)
                {
                    var val = bytes.AsSpan();
                    Value1 = val[0];
                    Value2 = JT808Constants.Encoding.GetString(val.Slice(1)).Trim('\0');
                }
            }
            /// <summary>
            /// 用于分析器描述
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return JsonConvert.SerializeObject(this);
            }
        }

        /// <summary>
        /// ICusotmCommandParameter  自定义命令参数接口
        /// ICommandParameterValue<> 对应的数据类型值
        /// 注意：Order必须从13开始逐一递增
        /// </summary>
        public class ErrorCommandParameter : ICusotmCommandParameter, ICommandParameterValue<bool>
        {
            public int Order => 3;
            public string CommandName => "Error";
            public bool Value { get; set; }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public byte[] ToBytes()
            {
                return default;
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="bytes"></param>
            public void ToValue(byte[] bytes)
            {

            }
        }
    }
}
