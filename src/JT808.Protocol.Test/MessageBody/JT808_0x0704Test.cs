using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using Xunit;

namespace JT808.Protocol.Test.MessageBodyRequest
{
    public class JT808_0x0704Test : JT808PackageBase
    {
        [Fact]
        public void Test1()
        {
            JT808Package jT808Package = new JT808Package
            {
                Header = new JT808Header
                {
                    MsgId = Enums.JT808MsgId.定位数据批量上传.ToUInt16Value(),
                    MsgNum = 8888,
                    TerminalPhoneNo = "112233445566",
                }
            };

            JT808_0x0704 jT808_0X0704 = new JT808_0x0704
            {
                Count = 2,
                LocationType = JT808_0x0704.BatchLocationType.正常位置批量汇报
            };

            JT808_0x0200 JT808_0x0200_1 = new JT808_0x0200
            {
                AlarmFlag = 1,
                Altitude = 40,
                GPSTime = DateTime.Parse("2018-07-15 10:10:10"),
                Lat = 12222222,
                Lng = 132444444,
                Speed = 60,
                Direction = 0,
                StatusFlag = 2,
                JT808LocationAttachData = new Dictionary<byte, JT808_0x0200_BodyBase>()
            };
            JT808_0x0200_1.JT808LocationAttachData.Add(JT808_0x0200_BodyBase.AttachId0x01, new JT808_0x0200_0x01
            {
                Mileage = 100
            });
            JT808_0x0200_1.JT808LocationAttachData.Add(JT808_0x0200_BodyBase.AttachId0x02, new JT808_0x0200_0x02
            {
                Oil = 55
            });

            List<JT808_0x0200> jT808_0X0200s = new List<JT808_0x0200>();
            jT808_0X0200s.Add(JT808_0x0200_1);

            JT808_0x0200 JT808_0x0200_2 = new JT808_0x0200
            {
                AlarmFlag = 2,
                Altitude = 41,
                GPSTime = DateTime.Parse("2018-07-15 10:10:30"),
                Lat = 13333333,
                Lng = 132555555,
                Speed = 54,
                Direction = 120,
                StatusFlag = 1,
                JT808LocationAttachData = new Dictionary<byte, JT808_0x0200_BodyBase>()
            };
            JT808_0x0200_2.JT808LocationAttachData.Add(JT808_0x0200_BodyBase.AttachId0x01, new JT808_0x0200_0x01
            {
                Mileage = 96
            });
            JT808_0x0200_2.JT808LocationAttachData.Add(JT808_0x0200_BodyBase.AttachId0x02, new JT808_0x0200_0x02
            {
                Oil = 66
            });
            jT808_0X0200s.Add(JT808_0x0200_2);
            jT808_0X0704.Positions = jT808_0X0200s;
            jT808Package.Bodies = jT808_0X0704;
            //7E 
            //07 04 
            //00 53 
            //11 22 33 44 55 66 
            //22 B8 
            //00 02 
            //00 
            //00 26 
            //00 00 00 01 
            //00 00 00 02 
            //00 BA 7F 0E 
            //07 E4 F1 1C 
            //00 28 
            //00 3C 
            //00 00 
            //18 07 15 10 10 10 
            //01 
            //  04 
            //      00 00 00 64 
            //02 
            //  02 
            //      00 37 
            //00 26 
            //00 00 00 02 
            //00 00 00 01 
            //00 CB 73 55 
            //07 E6 A3 23 
            //00 29 
            //00 36 
            //00 78 
            //18 07 15 10 10 30 
            //01 
            //  04 
            //      00 00 00 60 
            //02 
            //  02 
            //      00 42 
            //D4 
            //7E
            var hex = JT808Serializer.Serialize(jT808Package).ToHexString();
            Assert.Equal("7E0704005311223344556622B80002000026000000010000000200BA7F0E07E4F11C0028003C0000180715101010010400000064020200370026000000020000000100CB735507E6A32300290036007818071510103001040000006002020042D47E", hex);
        }

        [Fact]
        public void Test2()
        {
            byte[] bytes = "7E 07 04 00 53 11 22 33 44 55 66 22 B8 00 02 00 00 26 00 00 00 01 00 00 00 02 00 BA 7F 0E 07 E4 F1 1C 00 28 00 3C 00 00 18 07 15 10 10 10 01 04 00 00 00 64 02 02 00 37 00 26 00 00 00 02 00 00 00 01 00 CB 73 55 07 E6 A3 23 00 29 00 36 00 78 18 07 15 10 10 30 01 04 00 00 00 60 02 02 00 42 D4 7E".ToHexBytes();
            JT808Package jT808Package = JT808Serializer.Deserialize<JT808Package>(bytes);
            Assert.Equal(Enums.JT808MsgId.定位数据批量上传.ToUInt16Value(), jT808Package.Header.MsgId);
            Assert.Equal(8888, jT808Package.Header.MsgNum);
            Assert.Equal("112233445566", jT808Package.Header.TerminalPhoneNo);

            JT808_0x0704 JT808Bodies = (JT808_0x0704)jT808Package.Bodies;
            Assert.Equal(2, JT808Bodies.Count);
            Assert.Equal(JT808_0x0704.BatchLocationType.正常位置批量汇报, JT808Bodies.LocationType);

            Assert.Equal((uint)1, JT808Bodies.Positions[0].AlarmFlag);
            Assert.Equal(DateTime.Parse("2018-07-15 10:10:10"), JT808Bodies.Positions[0].GPSTime);
            Assert.Equal(12222222, JT808Bodies.Positions[0].Lat);
            Assert.Equal(132444444, JT808Bodies.Positions[0].Lng);
            Assert.Equal(0, JT808Bodies.Positions[0].Direction);
            Assert.Equal(60, JT808Bodies.Positions[0].Speed);
            Assert.Equal((uint)2, JT808Bodies.Positions[0].StatusFlag);
            Assert.Equal(100, ((JT808_0x0200_0x01)JT808Bodies.Positions[0].JT808LocationAttachData[JT808_0x0200_BodyBase.AttachId0x01]).Mileage);
            Assert.Equal(55, ((JT808_0x0200_0x02)JT808Bodies.Positions[0].JT808LocationAttachData[JT808_0x0200_BodyBase.AttachId0x02]).Oil);

            Assert.Equal((uint)2, JT808Bodies.Positions[1].AlarmFlag);
            Assert.Equal(DateTime.Parse("2018-07-15 10:10:30"), JT808Bodies.Positions[1].GPSTime);
            Assert.Equal(13333333, JT808Bodies.Positions[1].Lat);
            Assert.Equal(132555555, JT808Bodies.Positions[1].Lng);
            Assert.Equal(54, JT808Bodies.Positions[1].Speed);
            Assert.Equal(120, JT808Bodies.Positions[1].Direction);
            Assert.Equal((uint)1, JT808Bodies.Positions[1].StatusFlag);
            Assert.Equal(96, ((JT808_0x0200_0x01)JT808Bodies.Positions[1].JT808LocationAttachData[JT808_0x0200_BodyBase.AttachId0x01]).Mileage);
            Assert.Equal(66, ((JT808_0x0200_0x02)JT808Bodies.Positions[1].JT808LocationAttachData[JT808_0x0200_BodyBase.AttachId0x02]).Oil);
        }
    }
}
