using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using JT808.Protocol.Extensions;
using JT808.Protocol.Test.JT808LocationAttach;
using System.IO;
using JT808.Protocol.JT808Formatters;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters;
using JT808.Protocol.Enums;

namespace JT808.Protocol.Test
{
    public class DemoTest
    {
        [Fact]
        public void Demo1()
        {
            JT808Package jT808Package = new JT808Package();

            jT808Package.Header = new JT808Header
            {
                MsgId = Enums.JT808MsgId.位置信息汇报.ToUInt16Value(),
                MsgNum = 126,
                TerminalPhoneNo = "123456789012"
            };

            JT808_0x0200 jT808_0x0200 = new JT808_0x0200();
            jT808_0x0200.AlarmFlag = 1;
            jT808_0x0200.Altitude = 40;
            jT808_0x0200.GPSTime = DateTime.Parse("2018-10-15 10:10:10");
            jT808_0x0200.Lat = 12222222;
            jT808_0x0200.Lng = 132444444;
            jT808_0x0200.Speed = 60;
            jT808_0x0200.Direction = 0;
            jT808_0x0200.StatusFlag = 2;
            jT808_0x0200.JT808LocationAttachData = new Dictionary<byte, JT808_0x0200_BodyBase>();

            jT808_0x0200.JT808LocationAttachData.Add(JT808_0x0200_BodyBase.AttachId0x01, new JT808_0x0200_0x01
            {
                Mileage = 100
            });

            jT808_0x0200.JT808LocationAttachData.Add(JT808_0x0200_BodyBase.AttachId0x02, new JT808_0x0200_0x02
            {
                Oil = 125
            });

            jT808Package.Bodies = jT808_0x0200;

            byte[] data = JT808Serializer.Serialize(jT808Package);

            var hex = data.ToHexString();
            Assert.Equal("7E02000026123456789012007D02000000010000000200BA7F0E07E4F11C0028003C00001810151010100104000000640202007D01137E", hex);
            // 输出结果Hex：
            // 7E 02 00 00 26 12 34 56 78 90 12 00 7D 02 00 00 00 01 00 00 00 02 00 BA 7F 0E 07 E4 F1 1C 00 28 00 3C 00 00 18 10 15 10 10 10 01 04 00 00 00 64 02 02 00 7D 01 13 7E
        }

        [Fact]
        public void Demo2()
        {
            //1.转成byte数组
            byte[] bytes = "7E 02 00 00 26 12 34 56 78 90 12 00 7D 02 00 00 00 01 00 00 00 02 00 BA 7F 0E 07 E4 F1 1C 00 28 00 3C 00 00 18 10 15 10 10 10 01 04 00 00 00 64 02 02 00 7D 01 13 7E".ToHexBytes();

            //2.将数组反序列化
            var jT808Package = JT808Serializer.Deserialize<JT808Package>(bytes);

            //3.数据包头
            Assert.Equal(Enums.JT808MsgId.位置信息汇报.ToValue(), jT808Package.Header.MsgId);
            Assert.Equal(38, jT808Package.Header.MessageBodyProperty.DataLength);
            Assert.Equal(126, jT808Package.Header.MsgNum);
            Assert.Equal("123456789012", jT808Package.Header.TerminalPhoneNo);
            Assert.False(jT808Package.Header.MessageBodyProperty.IsPackge);
            Assert.Equal(0, jT808Package.Header.MessageBodyProperty.PackageIndex);
            Assert.Equal(0, jT808Package.Header.MessageBodyProperty.PackgeCount);
            Assert.Equal(JT808EncryptMethod.None, jT808Package.Header.MessageBodyProperty.Encrypt);

            //4.数据包体
            JT808_0x0200 jT808_0x0200 = (JT808_0x0200)jT808Package.Bodies;
            Assert.Equal((uint)1, jT808_0x0200.AlarmFlag);
            Assert.Equal((uint)40, jT808_0x0200.Altitude);
            Assert.Equal(DateTime.Parse("2018-10-15 10:10:10"), jT808_0x0200.GPSTime);
            Assert.Equal(12222222, jT808_0x0200.Lat);
            Assert.Equal(132444444, jT808_0x0200.Lng);
            Assert.Equal(60, jT808_0x0200.Speed);
            Assert.Equal(0, jT808_0x0200.Direction);
            Assert.Equal((uint)2, jT808_0x0200.StatusFlag);
            //4.1.附加信息1
            Assert.Equal(100, ((JT808_0x0200_0x01)jT808_0x0200.JT808LocationAttachData[JT808_0x0200_BodyBase.AttachId0x01]).Mileage);
            //4.2.附加信息2
            Assert.Equal(125, ((JT808_0x0200_0x02)jT808_0x0200.JT808LocationAttachData[JT808_0x0200_BodyBase.AttachId0x02]).Oil);
        }

        [Fact]
        public void Demo3()
        {
            JT808Package jT808Package = Enums.JT808MsgId.位置信息汇报.Create("123456789012",
                new JT808_0x0200
                {
                    AlarmFlag = 1,
                    Altitude = 40,
                    GPSTime = DateTime.Parse("2018-10-15 10:10:10"),
                    Lat = 12222222,
                    Lng = 132444444,
                    Speed = 60,
                    Direction = 0,
                    StatusFlag = 2,
                    JT808LocationAttachData = new Dictionary<byte, JT808_0x0200_BodyBase>
                    {
                        { JT808_0x0200_BodyBase.AttachId0x01,new JT808_0x0200_0x01{Mileage = 100}},
                        { JT808_0x0200_BodyBase.AttachId0x02,new JT808_0x0200_0x02{Oil = 125}}
                    }
                });

            byte[] data = JT808Serializer.Serialize(jT808Package);

            var hex = data.ToHexString();
            //输出结果Hex：
            //7E 02 00 00 26 12 34 56 78 90 12 00 01 00 00 00 01 00 00 00 02 00 BA 7F 0E 07 E4 F1 1C 00 28 00 3C 00 00 18 10 15 10 10 10 01 04 00 00 00 64 02 02 00 7D 01 6C 7E
            Assert.Equal("7E020000261234567890120001000000010000000200BA7F0E07E4F11C0028003C00001810151010100104000000640202007D016C7E", hex);
        }

        public void Demo4()
        {
            JT808GlobalConfig.Instance
                // 注册自定义位置附加信息
                .Register_0x0200_Attach(0x06)
                //.SetMsgSNDistributed(//todo 实现IMsgSNDistributed消息流水号)
                // 注册自定义数据上行透传信息
                //.Register_0x0900_Ext<>(//todo 继承自JT808_0x0900_BodyBase类)
                // 注册自定义数据下行透传信息
                //.Register_0x8900_Ext<>(//todo 继承自JT808_0x8900_BodyBase类)
                // 跳过校验码验证
                .SetSkipCRCCode(true);
        }

        /// <summary>
        /// 处理多设备多协议附加信息Id冲突
        /// </summary>
        [Fact]
        public void Demo5()
        {

        }

        public class CustomAttachIdConstant
        {
            public const byte dt1_0x81 = 0x81;
            public const byte dt2_0x81 = 0x81;
        }

        public class DeviceType1
        {

        }

        public class DeviceType2
        {

        }

        /// <summary>
        /// 设备类型1-对应消息协议0x81
        /// </summary>
        public class JT808_0x0200_DT1_0x81 : JT808_0x0200_CustomBodyBase
        {
            public override byte AttachInfoId { get; set; } = 0x81;
            public override byte AttachInfoLength { get; set; } = 13;
            public uint Age { get; set; }
            public byte Gender { get; set; }
            public string UserName { get; set; }
        }
        /// <summary>
        /// 设备类型1-对应消息协议0x82
        /// </summary>
        public class JT808_0x0200_DT1_0x82 : JT808_0x0200_CustomBodyBase
        {
            public override byte AttachInfoId { get; set; } = 0x82;
            public override byte AttachInfoLength { get; set; } = 1;
            public byte Gender { get; set; }
        }
        /// <summary>
        /// 设备类型2-对应消息协议0x81
        /// </summary>
        public class JT808_0x0200_DT2_0x81 : JT808_0x0200_CustomBodyBase
        {
            public override byte AttachInfoId { get; set; } = 0x81;
            public override byte AttachInfoLength { get; set; } = 7;
            public uint Age { get; set; }
            public byte Gender { get; set; }
            public ushort MsgNum { get; set; }
        }
        /// <summary>
        /// 设备类型1-对应消息协议序列化器 0x81
        /// </summary>
        public class JT808_0x0200_DT1_0x81Formatter : IJT808Formatter<JT808_0x0200_DT1_0x81>
        {
            public JT808_0x0200_DT1_0x81 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
            {
                int offset = 0;
                JT808_0x0200_DT1_0x81 jT808_0X0200_DT1_0X81 = new JT808_0x0200_DT1_0x81();
                jT808_0X0200_DT1_0X81.AttachInfoId = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
                jT808_0X0200_DT1_0X81.AttachInfoLength = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
                jT808_0X0200_DT1_0X81.Age = JT808BinaryExtensions.ReadUInt32Little(bytes, ref offset);
                jT808_0X0200_DT1_0X81.Gender = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
                jT808_0X0200_DT1_0X81.UserName = JT808BinaryExtensions.ReadStringLittle(bytes, ref offset);
                readSize = offset;
                return jT808_0X0200_DT1_0X81;
            }

            public int Serialize(ref byte[] bytes, int offset, JT808_0x0200_DT1_0x81 value)
            {
                offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, value.AttachInfoId);
                offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, value.AttachInfoLength);
                offset += JT808BinaryExtensions.WriteUInt32Little(bytes, offset, value.Age);
                offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, value.Gender);
                offset += JT808BinaryExtensions.WriteStringLittle(bytes, offset, value.UserName);
                return offset;
            }
        }
        /// <summary>
        /// 设备类型1-对应消息协议序列化器 0x82
        /// </summary>
        public class JT808_0x0200_DT1_0x82Formatter : IJT808Formatter<JT808_0x0200_DT1_0x82>
        {
            public JT808_0x0200_DT1_0x82 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
            {
                int offset = 0;
                JT808_0x0200_DT1_0x82 jT808_0X0200_DT1_0X82 = new JT808_0x0200_DT1_0x82();
                jT808_0X0200_DT1_0X82.AttachInfoId = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
                jT808_0X0200_DT1_0X82.AttachInfoLength = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
                jT808_0X0200_DT1_0X82.Gender = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
                readSize = offset;
                return jT808_0X0200_DT1_0X82;
            }

            public int Serialize(ref byte[] bytes, int offset, JT808_0x0200_DT1_0x82 value)
            {
                offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, value.AttachInfoId);
                offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, value.AttachInfoLength);
                offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, value.Gender);
                return offset;
            }
        }
        /// <summary>
        /// 设备类型2-对应消息协议序列化器 0x81
        /// </summary>
        public class JT808_0x0200_DT2_0x81Formatter : IJT808Formatter<JT808_0x0200_DT2_0x81>
        {
            public JT808_0x0200_DT2_0x81 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
            {
                int offset = 0;
                JT808_0x0200_DT2_0x81 jT808_0X0200_DT2_0X81 = new JT808_0x0200_DT2_0x81();
                jT808_0X0200_DT2_0X81.AttachInfoId = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
                jT808_0X0200_DT2_0X81.AttachInfoLength = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
                jT808_0X0200_DT2_0X81.Age = JT808BinaryExtensions.ReadUInt32Little(bytes, ref offset);
                jT808_0X0200_DT2_0X81.Gender = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
                jT808_0X0200_DT2_0X81.MsgNum = JT808BinaryExtensions.ReadUInt16Little(bytes, ref offset);
                readSize = offset;
                return jT808_0X0200_DT2_0X81;
            }

            public int Serialize(ref byte[] bytes, int offset, JT808_0x0200_DT2_0x81 value)
            {
                offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, value.AttachInfoId);
                offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, value.AttachInfoLength);
                offset += JT808BinaryExtensions.WriteUInt32Little(bytes, offset, value.Age);
                offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, value.Gender);
                offset += JT808BinaryExtensions.WriteUInt16Little(bytes, offset, value.MsgNum);
                return offset;
            }
        }
    }
}
