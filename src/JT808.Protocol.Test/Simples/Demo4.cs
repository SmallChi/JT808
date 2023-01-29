using JT808.Protocol.Enums;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Extensions;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xunit;
using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using JT808.Protocol.Internal;

namespace JT808.Protocol.Test.Simples
{
    public class Demo4
    {
        public Demo4()
        {

        }
        private readonly Dictionary<string, DeviceType> cache = new Dictionary<string, DeviceType>
        {
            { "123456789012",DeviceType.DT1 },
            { "123456789013",DeviceType.DT2 }
        };
        /// <summary>
        /// 处理多设备多协议附加信息Id冲突
        /// </summary>
        [Fact]
        public void Test1()
        {
            IJT808Config jT808Config = new DefaultGlobalConfig();
            jT808Config.JT808_0X0200_Custom_Factory.SetMap<JT808_0x0200_DT1_0x81>();
            jT808Config.FormatterFactory.SetMap<JT808_0x0200_DT1_0x81>();
            JT808Serializer demo5JT808Serializer = new JT808Serializer(jT808Config);

            JT808Package jT808Package = JT808MsgId._0x0200.Create("123456789012",
                                        new JT808_0x0200
                                        {
                                            AlarmFlag = 1,
                                            Altitude = 40,
                                            GPSTime = DateTime.Parse("2018-12-20 20:10:10"),
                                            Lat = 12222222,
                                            Lng = 132444444,
                                            Speed = 60,
                                            Direction = 0,
                                            StatusFlag = 2,
                                            CustomLocationAttachData = new Dictionary<byte, JT808_0x0200_CustomBodyBase>
                                                {
                                                    {0x81,new JT808_0x0200_DT1_0x81 {
                                                        Age=15,
                                                        Gender=1,
                                                        UserName="smallchi"
                                                    } }
                                                }
                                        });

            byte[] data = demo5JT808Serializer.Serialize(jT808Package);
            var jT808PackageResult = demo5JT808Serializer.Deserialize<JT808Package>(data);
            JT808_0x0200 jT808_0X0200 = jT808PackageResult.Bodies as JT808_0x0200;

            var attach = DeviceTypeFactory.Create(cache[jT808PackageResult.Header.TerminalPhoneNo], jT808_0X0200.CustomLocationAttachData);
            var extJson = attach.ExtData.Data.ToString(Formatting.None);
            var attachinfo81 = (JT808_0x0200_DT1_0x81)attach.JT808CustomLocationAttachData[0x81];
            Assert.Equal((uint)15, attachinfo81.Age);
            Assert.Equal(1, attachinfo81.Gender);
            Assert.Equal("smallchi", attachinfo81.UserName);
        }
    }


    public interface IExtData
    {
        JObject Data { get; set; }
    }

    public interface IExtDataProcessor
    {
        void Processor(IExtData extData);
    }

    public class JT808_0x0200_DT1_0x81_ExtDataProcessor : IExtDataProcessor
    {
        private readonly JT808_0x0200_DT1_0x81 jT808_0X0200_DT1_0X81;
        public JT808_0x0200_DT1_0x81_ExtDataProcessor(JT808_0x0200_DT1_0x81 jT808_0X0200_DT1_0X81)
        {
            this.jT808_0X0200_DT1_0X81 = jT808_0X0200_DT1_0X81;
        }
        public void Processor(IExtData extData)
        {
            extData.Data.Add(nameof(JT808_0x0200_DT1_0x81.Age), jT808_0X0200_DT1_0X81.Age);
            extData.Data.Add(nameof(JT808_0x0200_DT1_0x81.UserName), jT808_0X0200_DT1_0X81.UserName);
            extData.Data.Add(nameof(JT808_0x0200_DT1_0x81.Gender), jT808_0X0200_DT1_0X81.Gender);
        }
    }

    public class JT808_0x0200_DT1_0x82_ExtDataProcessor : IExtDataProcessor
    {
        private readonly JT808_0x0200_DT1_0x82 jT808_0X0200_DT1_0X82;
        public JT808_0x0200_DT1_0x82_ExtDataProcessor(JT808_0x0200_DT1_0x82 jT808_0X0200_DT1_0X82)
        {
            this.jT808_0X0200_DT1_0X82 = jT808_0X0200_DT1_0X82;
        }
        public void Processor(IExtData extData)
        {
            extData.Data.Add(nameof(JT808_0x0200_DT1_0x82.Gender1), jT808_0X0200_DT1_0X82.Gender1);
        }
    }

    public class JT808_0x0200_DT2_0x81_ExtDataProcessor : IExtDataProcessor
    {
        private readonly JT808_0x0200_DT2_0x81 jT808_0X0200_DT2_0X81;
        public JT808_0x0200_DT2_0x81_ExtDataProcessor(JT808_0x0200_DT2_0x81 jT808_0X0200_DT2_0X81)
        {
            this.jT808_0X0200_DT2_0X81 = jT808_0X0200_DT2_0X81;
        }
        public void Processor(IExtData extData)
        {
            extData.Data.Add(nameof(JT808_0x0200_DT2_0x81.Age), jT808_0X0200_DT2_0X81.Age);
            extData.Data.Add(nameof(JT808_0x0200_DT2_0x81.Gender), jT808_0X0200_DT2_0X81.Gender);
        }
    }

    public class DeviceTypeFactory
    {
        public static DeviceTypeBase Create(DeviceType deviceType, Dictionary<byte, JT808_0x0200_CustomBodyBase> jT808CustomLocationAttachData)
        {
            return deviceType switch
            {
                DeviceType.DT1 => new DeviceType1(jT808CustomLocationAttachData),
                DeviceType.DT2 => new DeviceType2(jT808CustomLocationAttachData),
                _ => default,
            };
        }
    }

    public enum DeviceType
    {
        DT1 = 1,
        DT2 = 2
    }

    public abstract class DeviceTypeBase
    {
        protected JT808Serializer JT808Serializer;
        protected class DefaultExtDataImpl : IExtData
        {
            public JObject Data { get; set; } = new JObject();
        }
        public virtual IExtData ExtData { get; protected set; } = new DefaultExtDataImpl();
        public abstract Dictionary<byte, JT808_0x0200_CustomBodyBase> JT808CustomLocationAttachData { get; protected set; }
        protected DeviceTypeBase(Dictionary<byte, JT808_0x0200_CustomBodyBase> jT808CustomLocationAttachData)
        {
            Execute(jT808CustomLocationAttachData);
        }
        protected abstract void Execute(Dictionary<byte, JT808_0x0200_CustomBodyBase> jT808CustomLocationAttachData);
    }

    public class DeviceType1 : DeviceTypeBase
    {
        private const byte dt1_0x81 = 0x81;
        private const byte dt1_0x82 = 0x82;
        public DeviceType1(Dictionary<byte, JT808_0x0200_CustomBodyBase> jT808CustomLocationAttachData) : base(jT808CustomLocationAttachData)
        {
            IJT808Config jT808Config = new DefaultGlobalConfig();
            jT808Config.JT808_0X0200_Custom_Factory.SetMap<JT808_0x0200_DT1_0x81>();
            jT808Config.FormatterFactory.SetMap<JT808_0x0200_DT1_0x81>();
            jT808Config.JT808_0X0200_Custom_Factory.SetMap<JT808_0x0200_DT1_0x82>();
            jT808Config.FormatterFactory.SetMap<JT808_0x0200_DT1_0x82>();
            JT808Serializer = new JT808Serializer(jT808Config);
        }
        public override Dictionary<byte, JT808_0x0200_CustomBodyBase> JT808CustomLocationAttachData { get; protected set; }
        protected override void Execute(Dictionary<byte, JT808_0x0200_CustomBodyBase> jT808CustomLocationAttachData)
        {
            JT808CustomLocationAttachData = new Dictionary<byte, JT808_0x0200_CustomBodyBase>();
            foreach (var item in jT808CustomLocationAttachData)
            {
                try
                {
                    switch (item.Key)
                    {
                        case dt1_0x81:
                            var info81 = (JT808_0x0200_DT1_0x81)item.Value;
                            IExtDataProcessor extDataProcessor81 = new JT808_0x0200_DT1_0x81_ExtDataProcessor(info81);
                            extDataProcessor81.Processor(ExtData);
                            JT808CustomLocationAttachData.Add(dt1_0x81, info81);

                            break;
                        case dt1_0x82:
                            var info82 = (JT808_0x0200_DT1_0x82)item.Value;
                            IExtDataProcessor extDataProcessor82 = new JT808_0x0200_DT1_0x82_ExtDataProcessor(info82);
                            extDataProcessor82.Processor(ExtData);
                            JT808CustomLocationAttachData.Add(dt1_0x82, info82);
                            break;
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
    }

    public class DeviceType2 : DeviceTypeBase
    {
        public DeviceType2(Dictionary<byte, JT808_0x0200_CustomBodyBase> jT808CustomLocationAttachData) : base(jT808CustomLocationAttachData)
        {
            IJT808Config jT808Config = new DefaultGlobalConfig();
            jT808Config.JT808_0X0200_Custom_Factory.SetMap<JT808_0x0200_DT2_0x81>();
            jT808Config.FormatterFactory.SetMap<JT808_0x0200_DT2_0x81>();
            JT808Serializer = new JT808Serializer(jT808Config);
        }
        public override Dictionary<byte, JT808_0x0200_CustomBodyBase> JT808CustomLocationAttachData { get; protected set; }

        private const byte dt2_0x81 = 0x81;
        protected override void Execute(Dictionary<byte, JT808_0x0200_CustomBodyBase> jT808CustomLocationAttachOriginalData)
        {
            JT808CustomLocationAttachData = new Dictionary<byte, JT808_0x0200_CustomBodyBase>();
            foreach (var item in jT808CustomLocationAttachOriginalData)
            {
                try
                {
                    switch (item.Key)
                    {
                        case dt2_0x81:
                            var info81 = (JT808_0x0200_DT2_0x81)item.Value;
                            IExtDataProcessor extDataProcessor = new JT808_0x0200_DT2_0x81_ExtDataProcessor(info81);
                            extDataProcessor.Processor(ExtData);
                            JT808CustomLocationAttachData.Add(dt2_0x81, info81);
                            break;
                    }
                }
                catch (Exception)
                {

                }
            }
        }
    }

    /// <summary>
    /// 设备类型1-对应消息协议0x81
    /// </summary>
    public class JT808_0x0200_DT1_0x81 : JT808MessagePackFormatter<JT808_0x0200_DT1_0x81>, JT808_0x0200_CustomBodyBase
    {
        public byte AttachInfoId { get; set; } = 0x81;
        public byte AttachInfoLength { get; set; } = 13;
        public uint Age { get; set; }
        public byte Gender { get; set; }
        public string UserName { get; set; }

        public override JT808_0x0200_DT1_0x81 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_DT1_0x81 jT808_0X0200_DT1_0X81 = new JT808_0x0200_DT1_0x81();
            jT808_0X0200_DT1_0X81.AttachInfoId = reader.ReadByte();
            jT808_0X0200_DT1_0X81.AttachInfoLength = reader.ReadByte();
            jT808_0X0200_DT1_0X81.Age = reader.ReadUInt32();
            jT808_0X0200_DT1_0X81.Gender = reader.ReadByte();
            jT808_0X0200_DT1_0X81.UserName = reader.ReadRemainStringContent();
            return jT808_0X0200_DT1_0X81;
        }
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_DT1_0x81 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteUInt32(value.Age);
            writer.WriteByte(value.Gender);
            writer.WriteString(value.UserName);
        }
    }
    /// <summary>
    /// 设备类型1-对应消息协议0x82
    /// </summary>
    public class JT808_0x0200_DT1_0x82 : JT808MessagePackFormatter<JT808_0x0200_DT1_0x82>, JT808_0x0200_CustomBodyBase
    {
        public byte AttachInfoId { get; set; } = 0x82;
        public byte AttachInfoLength { get; set; } = 1;
        public byte Gender1 { get; set; }
        public override JT808_0x0200_DT1_0x82 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_DT1_0x82 jT808_0X0200_DT1_0X82 = new JT808_0x0200_DT1_0x82();
            jT808_0X0200_DT1_0X82.AttachInfoId = reader.ReadByte();
            jT808_0X0200_DT1_0X82.AttachInfoLength = reader.ReadByte();
            jT808_0X0200_DT1_0X82.Gender1 = reader.ReadByte();
            return jT808_0X0200_DT1_0X82;
        }

        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_DT1_0x82 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteByte(value.Gender1);
        }
    }
    /// <summary>
    /// 设备类型2-对应消息协议0x81
    /// </summary>
    public class JT808_0x0200_DT2_0x81 : JT808MessagePackFormatter<JT808_0x0200_DT2_0x81>, JT808_0x0200_CustomBodyBase
    {
        public byte AttachInfoId { get; set; } = 0x81;
        public byte AttachInfoLength { get; set; } = 7;
        public uint Age { get; set; }
        public byte Gender { get; set; }
        public ushort MsgNum { get; set; }
        public override JT808_0x0200_DT2_0x81 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_DT2_0x81 jT808_0X0200_DT2_0X81 = new JT808_0x0200_DT2_0x81();
            jT808_0X0200_DT2_0X81.AttachInfoId = reader.ReadByte();
            jT808_0X0200_DT2_0X81.AttachInfoLength = reader.ReadByte();
            jT808_0X0200_DT2_0X81.Age = reader.ReadUInt32();
            jT808_0X0200_DT2_0X81.Gender = reader.ReadByte();
            jT808_0X0200_DT2_0X81.MsgNum = reader.ReadUInt16();
            return jT808_0X0200_DT2_0X81;
        }

        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_DT2_0x81 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteUInt32(value.Age);
            writer.WriteByte(value.Gender);
            writer.WriteUInt16(value.MsgNum);
        }
    }
}
