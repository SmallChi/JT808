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

namespace JT808.Protocol.Test.Simples
{
    public class Demo10
    {
        JT808Serializer JT808Serializer;

        public Demo10()
        {
            IJT808Config jT808Config = new DefaultGlobalConfig();
            jT808Config.JT808_0X0200_Custom_Factory.SetMap<JT808LocationAttachImpl0x00>();
            JT808Serializer = new JT808Serializer(jT808Config);
        }

        [Fact]
        public void Test1()
        {
            JT808_0x0200 jT808UploadLocationRequest = new JT808_0x0200
            {
                AlarmFlag = 1,
                Altitude = 40,
                GPSTime = DateTime.Parse("2021-04-03 11:10:10"),
                Lat = 12222222,
                Lng = 132444444,
                Speed = 60,
                Direction = 0,
                StatusFlag = 2,
                JT808CustomLocationAttachData = new Dictionary<byte, JT808_0x0200_CustomBodyBase>(),
                ExceptionLocationAttachData = new List<JT808_0x0200_CustomBodyBase>()
            };
            jT808UploadLocationRequest.JT808CustomLocationAttachData.Add(0x00, new JT808LocationAttachImpl0x00
            {
                 TestValue=new byte[] {0,1,2,3}
            });
            jT808UploadLocationRequest.ExceptionLocationAttachData.Add(new JT808LocationAttachImpl0x00
            {
                 TestId= "012345678912"
            });
            var hex = JT808Serializer.Serialize(jT808UploadLocationRequest).ToHexString();
            Assert.Equal("000000010000000200BA7F0E07E4F11C0028003C0000210403111010000400010203000C303132333435363738393132", hex);
        }

        [Fact]
        public void Test1_1()
        {
            byte[] bodys = "000000010000000200BA7F0E07E4F11C0028003C0000210403111010000400010203000C303132333435363738393132".ToHexBytes();
            JT808_0x0200 jT808UploadLocationRequest = JT808Serializer.Deserialize<JT808_0x0200>(bodys);
            Assert.Equal((uint)1, jT808UploadLocationRequest.AlarmFlag);
            Assert.Equal(DateTime.Parse("2021-04-03 11:10:10"), jT808UploadLocationRequest.GPSTime);
            Assert.Equal(12222222, jT808UploadLocationRequest.Lat);
            Assert.Equal(132444444, jT808UploadLocationRequest.Lng);
            Assert.Equal(60, jT808UploadLocationRequest.Speed);
            Assert.Equal((uint)2, jT808UploadLocationRequest.StatusFlag);
            Assert.Equal(new byte[] { 0, 1, 2, 3 }, ((JT808LocationAttachImpl0x00)jT808UploadLocationRequest.JT808CustomLocationAttachData[0x00]).TestValue);
            Assert.Equal("012345678912", ((JT808LocationAttachImpl0x00)jT808UploadLocationRequest.ExceptionLocationAttachData[0]).TestId);
        }
    }

    public class JT808LocationAttachImpl0x00 : JT808_0x0200_CustomBodyBase, IJT808MessagePackFormatter<JT808LocationAttachImpl0x00>
    {
        /// <summary>
        /// 附加Id 0x00
        /// </summary>
        public override byte AttachInfoId { get; set; } = 0x00;
        /// <summary>
        /// 不固定
        /// </summary>
        public override byte AttachInfoLength { get; set; }
        /// <summary>
        /// 只能存在一个 AttachInfoLength == 12
        /// </summary>
        public string TestId { get; set; }

        /// <summary>
        /// 只能存在一个 AttachInfoLength == 4
        /// </summary>
        public byte[] TestValue { get; set; }

        public JT808LocationAttachImpl0x00 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808LocationAttachImpl0x00 jT808LocationAttachImpl0X00 = new JT808LocationAttachImpl0x00();
            jT808LocationAttachImpl0X00.AttachInfoId = reader.ReadByte();
            jT808LocationAttachImpl0X00.AttachInfoLength = reader.ReadByte();
            if (jT808LocationAttachImpl0X00.AttachInfoLength == 12)
            {
                jT808LocationAttachImpl0X00.TestId = reader.ReadString(12);
            }
            else if(jT808LocationAttachImpl0X00.AttachInfoLength == 4)
            {
                jT808LocationAttachImpl0X00.TestValue = reader.ReadArray(4).ToArray();
            }
            return jT808LocationAttachImpl0X00;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808LocationAttachImpl0x00 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            if (!string.IsNullOrEmpty(value.TestId))
            {
                writer.WriteByte(12);
                writer.WriteString(value.TestId.ValiString(nameof(TestId), 12));
            }
            else if (value.TestValue != null)
            {
                writer.WriteByte(4);
                writer.WriteArray(value.TestValue.ValiBytes(nameof(TestValue), 4));
            }
        }
    }
}
