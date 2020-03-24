using JT808.Protocol.MessageBody;
using JT808.Protocol.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using JT808.Protocol.Internal;
using JT808.Protocol.MessageBody.CarDVR;

namespace JT808.Protocol.Test.MessageBody
{
    public  class JT808_0x8700Test
    {
        JT808Serializer JT808Serializer;

        public JT808_0x8700Test()
        {
            IJT808Config jT808Config = new DefaultGlobalConfig();
            JT808Serializer = new JT808Serializer(jT808Config);
        }
        [Fact]
        public void Test_Analyze()
        {
            //0x00 -- 0x07 指令和这个单元测试一样
            byte[] bytes = "00557A000000002F".ToHexBytes();
            var value = JT808Serializer.Analyze<JT808_0x8700>(bytes);
            // 0x08 -- 0x15 测试用例一样
            bytes = "08557A08000E00200322101010200323101010000129".ToHexBytes();
            value = JT808Serializer.Analyze<JT808_0x8700>(bytes);
        }
        /// <summary>
        /// 0x00 -- 0x07 指令和这个单元测试一样
        /// </summary>
        [Fact]
        public void Test_Serilize_0x00()
        {
            JT808_0x8700 value = new JT808_0x8700();
            value.CommandId = 0x00;
            value.JT808CarDVRDownPackage = new JT808CarDVRDownPackage { 
             CommandId=0x00,
              Bodies=new JT808_CarDVR_Down_0x00()
            };
            var hex = JT808Serializer.Serialize(value).ToHexString();
            Assert.Equal("00557A000000002F", hex);
        }
        /// <summary>
        /// 0x00 -- 0x07 指令和这个单元测试一样
        /// </summary>
        [Fact]
        public void Test_Deserilize_0x00()
        {
            byte[] bytes = "00557A000000002F".ToHexBytes();
            JT808_0x8700 value = JT808Serializer.Deserialize<JT808_0x8700>(bytes);
            Assert.Equal(0, value.CommandId);
            var body = value.JT808CarDVRDownPackage as JT808CarDVRDownPackage;
            Assert.Equal(0, body.CommandId);
        }
        /// <summary>
        /// 0x08 -- 0x15 测试用例一样
        /// </summary>
        [Fact]
        public void Test_Serilize_0x08()
        {
            JT808_0x8700 value = new JT808_0x8700();
            value.CommandId = 0x08;
            value.JT808CarDVRDownPackage = new JT808CarDVRDownPackage
            {
                CommandId = 0x08,
                Bodies = new JT808_CarDVR_Down_0x08()
                {
                     StartTime = Convert.ToDateTime("2020-03-22 10:10:10"),
                     EndTime = Convert.ToDateTime("2020-03-23 10:10:10"),
                     Count =1
                }
            };
            var hex = JT808Serializer.Serialize(value).ToHexString();
            Assert.Equal("08557A08000E00200322101010200323101010000129", hex);
        }
        /// <summary>
        /// 0x08 -- 0x15 测试用例一样
        /// </summary>
        [Fact]
        public void Test_Deserilize_0x08()
        {
            byte[] bytes = "08557A08000E00200322101010200323101010000129".ToHexBytes();
            JT808_0x8700 value = JT808Serializer.Deserialize<JT808_0x8700>(bytes);
            Assert.Equal(0x08, value.CommandId);
            var body = value.JT808CarDVRDownPackage as JT808CarDVRDownPackage;
            Assert.Equal(0x08, body.CommandId);
            var subBody = body.Bodies as JT808_CarDVR_Down_0x08;
            Assert.Equal("2020-03-22 10:10:10", subBody.StartTime.ToString("yyyy-MM-dd HH:mm:ss"));
            Assert.Equal("2020-03-23 10:10:10", subBody.EndTime.ToString("yyyy-MM-dd HH:mm:ss"));
            Assert.Equal(1, subBody.Count);
        }
    }
}
