using JT808.Protocol.Enums;
using JT808.Protocol.Extensions.JT1078.MessageBody;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessageBody;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xunit;

namespace JT808.Protocol.Extensions.JT1078.Test
{
    public class JT808_0x8103CustomId
    {
        JT808Serializer JT808Serializer;

        public JT808_0x8103CustomId()
        {
            IServiceCollection serviceDescriptors1 = new ServiceCollection();
            serviceDescriptors1.AddJT808Configure(new DefaultGlobalConfig())
                .AddJT1078Configure();
            var ServiceProvider1 = serviceDescriptors1.BuildServiceProvider();
            var defaultConfig = ServiceProvider1.GetRequiredService<DefaultGlobalConfig>();
            JT808Serializer = new JT808Serializer(defaultConfig);
        }
        [Fact]
        public void Test1()
        {
            JT808Package jT808Package = new JT808Package
            {
                Header = new JT808Header
                {
                    MsgId = JT808MsgId._0x8103.ToUInt16Value(),
                    ManualMsgNum = 10,
                    TerminalPhoneNo = "123456789",
                },
                Bodies = new JT808_0x8103
                {
                    ParamList = new List<JT808_0x8103_BodyBase> {
                        new JT808_0x8103_0x0075 {
                            AudioOutputEnabled=1,
                            OSD=2,
                            RTS_EncodeMode=3,
                            RTS_KF_Interval=4,
                            RTS_Resolution=5,
                            RTS_Target_CodeRate=6,
                            RTS_Target_FPS=7,
                            StreamStore_EncodeMode=8,
                            StreamStore_KF_Interval=9,
                            StreamStore_Resolution=10,
                            StreamStore_Target_CodeRate=11,
                            StreamStore_Target_FPS=12
                        },
                        new JT808_0x8103_0x0076 {
                            AudioChannelTotal=1,
                            AVChannelTotal=2,
                            VudioChannelTotal=3,
                            ParamLength=27,
                            AVChannelRefTables=new List<JT808_0x8103_0x0076_AVChannelRefTable>{
                                new JT808_0x8103_0x0076_AVChannelRefTable{
                                    ChannelType =0,
                                    IsConnectCloudPlat =1,
                                    LogicChannelNo =2,
                                    PhysicalChannelNo =3 },
                                new JT808_0x8103_0x0076_AVChannelRefTable{
                                    ChannelType =4,
                                    IsConnectCloudPlat =5,
                                    LogicChannelNo =6,
                                    PhysicalChannelNo =7  },
                                new JT808_0x8103_0x0076_AVChannelRefTable{
                                    ChannelType =8,
                                    IsConnectCloudPlat =9,
                                    LogicChannelNo =10,
                                    PhysicalChannelNo =11  },
                                new JT808_0x8103_0x0076_AVChannelRefTable{
                                    ChannelType =12,
                                    IsConnectCloudPlat =13,
                                    LogicChannelNo =14,
                                    PhysicalChannelNo =15  },
                                new JT808_0x8103_0x0076_AVChannelRefTable{
                                    ChannelType =16,
                                    IsConnectCloudPlat =17,
                                    LogicChannelNo =18,
                                    PhysicalChannelNo =19 },
                                new JT808_0x8103_0x0076_AVChannelRefTable{
                                    ChannelType =20,
                                    IsConnectCloudPlat =21,
                                    LogicChannelNo =22,
                                    PhysicalChannelNo =23 }
                            }
                        },
                        new JT808_0x8103_0x0077{
                                NeedSetChannelTotal=2,
                                ParamLength=43,
                                SignalChannels=new List<JT808_0x8103_0x0077_SignalChannel>{
                                        new JT808_0x8103_0x0077_SignalChannel{
                                            LogicChannelNo =1,
                                            OSD =2,
                                            RTS_EncodeMode =3,
                                            RTS_KF_Interval =4,
                                            RTS_Resolution =5,
                                            RTS_Target_CodeRate =6,
                                            RTS_Target_FPS =7,
                                            StreamStore_EncodeMode =8,
                                            StreamStore_KF_Interval =9,
                                            StreamStore_Resolution =10,
                                            StreamStore_Target_CodeRate=11,
                                            StreamStore_Target_FPS =12
                                        },
                                        new JT808_0x8103_0x0077_SignalChannel{
                                            LogicChannelNo=1,
                                            OSD =2,
                                            RTS_EncodeMode =3,
                                            RTS_KF_Interval =4,
                                            RTS_Resolution =5,
                                            RTS_Target_CodeRate =6,
                                            RTS_Target_FPS =7,
                                            StreamStore_EncodeMode =8,
                                            StreamStore_KF_Interval =9,
                                            StreamStore_Resolution =10,
                                            StreamStore_Target_CodeRate=11,
                                            StreamStore_Target_FPS =12
                                        }
                                    }
                                },
                        new JT808_0x8103_0x0079{
                            BeginMinute=1,
                            Duration =2,
                            StorageThresholds =3
                        },
                        new JT808_0x8103_0x007A{
                                AlarmShielding=1
                        },
                        new JT808_0x8103_0x007B{
                            NuclearLoadNumber=1,
                            FatigueThreshold =2
                        },
                        new JT808_0x8103_0x007C{
                                SleepWakeMode=1,
                                TimerWakeDaySet =2,
                                WakeConditionType =3,
                                TimerWakeDayParamter=new JT808_0x8103_0x007C_TimerWakeDayParamter{
                                    TimePeriod1CloseTime="12",
                                    TimePeriod1WakeTime="23",
                                    TimePeriod2CloseTime="34",
                                    TimePeriod2WakeTime="45",
                                    TimePeriod3CloseTime="56",
                                    TimePeriod3WakeTime="67",
                                    TimePeriod4CloseTime="78",
                                    TimePeriod4WakeTime="89",
                                    TimerWakeEnableFlag=10
                                }
                        }
                    }
                }
            };
            var hex = JT808Serializer.Serialize(jT808Package).ToHexString();
            Assert.Equal("7E8103009C000123456789000A070000007515030500040700000006080A00090C0000000B000201000000761B02010303020001070604050B0A08090F0E0C0D1312101117161415000000772B0201030500040700000006080A00090C0000000B000201030500040700000006080A00090C0000000B000200000079030302010000007A04000000010000007B0201020000007C140103020A00230012004500340067005600890078587E", hex);
        }

        [Fact]
        public void Test2()
        {
            byte[] bytes = "7E8103009C000123456789000A070000007515030500040700000006080A00090C0000000B000201000000761B02010303020001070604050B0A08090F0E0C0D1312101117161415000000772B0201030500040700000006080A00090C0000000B000201030500040700000006080A00090C0000000B000200000079030302010000007A04000000010000007B0201020000007C140103020A00230012004500340067005600890078587E".ToHexBytes();
            JT808Package jT808_0X8103 = JT808Serializer.Deserialize(bytes);
            Assert.Equal(JT808MsgId._0x8103.ToUInt16Value(), jT808_0X8103.Header.MsgId);
            Assert.Equal(10, jT808_0X8103.Header.MsgNum);
            Assert.Equal("123456789", jT808_0X8103.Header.TerminalPhoneNo);

            JT808_0x8103 jT808_0x8103 = (JT808_0x8103)jT808_0X8103.Bodies;
            foreach (var item in jT808_0x8103.ParamList)
            {
                switch (item)
                {
                    case JT808_0x8103_0x0075 jT808_0x8103_0x0075:
                        Assert.Equal(1,jT808_0x8103_0x0075.AudioOutputEnabled);
                        Assert.Equal(2,jT808_0x8103_0x0075.OSD);
                        Assert.Equal(3,jT808_0x8103_0x0075.RTS_EncodeMode);
                        Assert.Equal(4,jT808_0x8103_0x0075.RTS_KF_Interval);
                        Assert.Equal(5,jT808_0x8103_0x0075.RTS_Resolution);
                        Assert.Equal(6u,jT808_0x8103_0x0075.RTS_Target_CodeRate);
                        Assert.Equal(7,jT808_0x8103_0x0075.RTS_Target_FPS);
                        Assert.Equal(8,jT808_0x8103_0x0075.StreamStore_EncodeMode);
                        Assert.Equal(9,jT808_0x8103_0x0075.StreamStore_KF_Interval);
                        Assert.Equal(10,jT808_0x8103_0x0075.StreamStore_Resolution);
                        Assert.Equal(11u,jT808_0x8103_0x0075.StreamStore_Target_CodeRate);
                        Assert.Equal(12u,jT808_0x8103_0x0075.StreamStore_Target_FPS);
                        break;
                    case JT808_0x8103_0x0076 jT808_0x8103_0x0076:
                        Assert.Equal(1,jT808_0x8103_0x0076.AudioChannelTotal);
                        Assert.Equal(2, jT808_0x8103_0x0076.AVChannelTotal);
                        Assert.Equal(3,jT808_0x8103_0x0076.VudioChannelTotal);
                        Assert.Equal(27,jT808_0x8103_0x0076.ParamLength);
                       
                        Assert.Equal(0,jT808_0x8103_0x0076.AVChannelRefTables[0].ChannelType);
                        Assert.Equal(1,jT808_0x8103_0x0076.AVChannelRefTables[0].IsConnectCloudPlat);
                        Assert.Equal(2,jT808_0x8103_0x0076.AVChannelRefTables[0].LogicChannelNo);
                        Assert.Equal(3,jT808_0x8103_0x0076.AVChannelRefTables[0].PhysicalChannelNo);

                        Assert.Equal(4,jT808_0x8103_0x0076.AVChannelRefTables[1].ChannelType);
                        Assert.Equal(5,jT808_0x8103_0x0076.AVChannelRefTables[1].IsConnectCloudPlat);
                        Assert.Equal(6,jT808_0x8103_0x0076.AVChannelRefTables[1].LogicChannelNo);
                        Assert.Equal(7,jT808_0x8103_0x0076.AVChannelRefTables[1].PhysicalChannelNo);

                        Assert.Equal(8,jT808_0x8103_0x0076.AVChannelRefTables[2].ChannelType);
                        Assert.Equal(9,jT808_0x8103_0x0076.AVChannelRefTables[2].IsConnectCloudPlat);
                        Assert.Equal(10,jT808_0x8103_0x0076.AVChannelRefTables[2].LogicChannelNo);
                        Assert.Equal(11,jT808_0x8103_0x0076.AVChannelRefTables[2].PhysicalChannelNo);

                        Assert.Equal(12,jT808_0x8103_0x0076.AVChannelRefTables[3].ChannelType);
                        Assert.Equal(13,jT808_0x8103_0x0076.AVChannelRefTables[3].IsConnectCloudPlat);
                        Assert.Equal(14,jT808_0x8103_0x0076.AVChannelRefTables[3].LogicChannelNo);
                        Assert.Equal(15,jT808_0x8103_0x0076.AVChannelRefTables[3].PhysicalChannelNo);

                        Assert.Equal(16,jT808_0x8103_0x0076.AVChannelRefTables[4].ChannelType);
                        Assert.Equal(17,jT808_0x8103_0x0076.AVChannelRefTables[4].IsConnectCloudPlat);
                        Assert.Equal(18,jT808_0x8103_0x0076.AVChannelRefTables[4].LogicChannelNo);
                        Assert.Equal(19,jT808_0x8103_0x0076.AVChannelRefTables[4].PhysicalChannelNo);

                        Assert.Equal(20,jT808_0x8103_0x0076.AVChannelRefTables[5].ChannelType);
                        Assert.Equal(21,jT808_0x8103_0x0076.AVChannelRefTables[5].IsConnectCloudPlat);
                        Assert.Equal(22,jT808_0x8103_0x0076.AVChannelRefTables[5].LogicChannelNo);
                        Assert.Equal(23,jT808_0x8103_0x0076.AVChannelRefTables[5].PhysicalChannelNo);
                        break;
                    case JT808_0x8103_0x0077 jT808_0x8103_0x0077:
                        Assert.Equal(2,jT808_0x8103_0x0077.NeedSetChannelTotal);
                        Assert.Equal(43,jT808_0x8103_0x0077.ParamLength);

                        Assert.Equal(1,jT808_0x8103_0x0077.SignalChannels[0].LogicChannelNo);
                        Assert.Equal(2,jT808_0x8103_0x0077.SignalChannels[0].OSD);
                        Assert.Equal(3,jT808_0x8103_0x0077.SignalChannels[0].RTS_EncodeMode);
                        Assert.Equal(4,jT808_0x8103_0x0077.SignalChannels[0].RTS_KF_Interval);
                        Assert.Equal(5,jT808_0x8103_0x0077.SignalChannels[0].RTS_Resolution);
                        Assert.Equal(6u,jT808_0x8103_0x0077.SignalChannels[0].RTS_Target_CodeRate);
                        Assert.Equal(7,jT808_0x8103_0x0077.SignalChannels[0].RTS_Target_FPS);
                        Assert.Equal(8,jT808_0x8103_0x0077.SignalChannels[0].StreamStore_EncodeMode);
                        Assert.Equal(9,jT808_0x8103_0x0077.SignalChannels[0].StreamStore_KF_Interval);
                        Assert.Equal(10,jT808_0x8103_0x0077.SignalChannels[0].StreamStore_Resolution);
                        Assert.Equal(11u,jT808_0x8103_0x0077.SignalChannels[0].StreamStore_Target_CodeRate);
                        Assert.Equal(12,jT808_0x8103_0x0077.SignalChannels[0].StreamStore_Target_FPS);

                        Assert.Equal(1,jT808_0x8103_0x0077.SignalChannels[1].LogicChannelNo);
                        Assert.Equal(2,jT808_0x8103_0x0077.SignalChannels[1].OSD);
                        Assert.Equal(3,jT808_0x8103_0x0077.SignalChannels[1].RTS_EncodeMode);
                        Assert.Equal(4,jT808_0x8103_0x0077.SignalChannels[1].RTS_KF_Interval);
                        Assert.Equal(5,jT808_0x8103_0x0077.SignalChannels[1].RTS_Resolution);
                        Assert.Equal(6u,jT808_0x8103_0x0077.SignalChannels[1].RTS_Target_CodeRate);
                        Assert.Equal(7,jT808_0x8103_0x0077.SignalChannels[1].RTS_Target_FPS);
                        Assert.Equal(8,jT808_0x8103_0x0077.SignalChannels[1].StreamStore_EncodeMode);
                        Assert.Equal(9,jT808_0x8103_0x0077.SignalChannels[1].StreamStore_KF_Interval);
                        Assert.Equal(10,jT808_0x8103_0x0077.SignalChannels[1].StreamStore_Resolution);
                        Assert.Equal(11u,jT808_0x8103_0x0077.SignalChannels[1].StreamStore_Target_CodeRate);
                        Assert.Equal(12,jT808_0x8103_0x0077.SignalChannels[1].StreamStore_Target_FPS);

                        break;
                    case JT808_0x8103_0x0079 jT808_0x8103_0x0079:
                        Assert.Equal(1,jT808_0x8103_0x0079.BeginMinute);
                        Assert.Equal(2,jT808_0x8103_0x0079.Duration);
                        Assert.Equal(3,jT808_0x8103_0x0079.StorageThresholds);
                        break;
                    case JT808_0x8103_0x007A jT808_0x8103_0x007A:
                        Assert.Equal(1u,jT808_0x8103_0x007A.AlarmShielding);
                        break;
                    case JT808_0x8103_0x007B jT808_0x8103_0x007B:
                        Assert.Equal(1,jT808_0x8103_0x007B.NuclearLoadNumber);
                        Assert.Equal(2,jT808_0x8103_0x007B.FatigueThreshold);
                        break;
                    case JT808_0x8103_0x007C jT808_0x8103_0x007C:
                        Assert.Equal(1,jT808_0x8103_0x007C.SleepWakeMode);
                        Assert.Equal(2,jT808_0x8103_0x007C.TimerWakeDaySet);
                        Assert.Equal(3,jT808_0x8103_0x007C.WakeConditionType);
                        Assert.Equal("12", jT808_0x8103_0x007C.TimerWakeDayParamter.TimePeriod1CloseTime);
                        Assert.Equal("23",jT808_0x8103_0x007C.TimerWakeDayParamter.TimePeriod1WakeTime);
                        Assert.Equal("34",jT808_0x8103_0x007C.TimerWakeDayParamter.TimePeriod2CloseTime);
                        Assert.Equal("45",jT808_0x8103_0x007C.TimerWakeDayParamter.TimePeriod2WakeTime);
                        Assert.Equal("56",jT808_0x8103_0x007C.TimerWakeDayParamter.TimePeriod3CloseTime);
                        Assert.Equal("67",jT808_0x8103_0x007C.TimerWakeDayParamter.TimePeriod3WakeTime);
                        Assert.Equal("78",jT808_0x8103_0x007C.TimerWakeDayParamter.TimePeriod4CloseTime);
                        Assert.Equal("89", jT808_0x8103_0x007C.TimerWakeDayParamter.TimePeriod4WakeTime);
                        Assert.Equal(10,jT808_0x8103_0x007C.TimerWakeDayParamter.TimerWakeEnableFlag);
                        break;
                    default:
                        break;
                }
            }
        }

        [Fact]
        public void Test3()
        {
            byte[] bytes = "7E8103009C000123456789000A070000007515030500040700000006080A00090C0000000B000201000000761B02010303020001070604050B0A08090F0E0C0D1312101117161415000000772B0201030500040700000006080A00090C0000000B000201030500040700000006080A00090C0000000B000200000079030302010000007A04000000010000007B0201020000007C140103020A00230012004500340067005600890078587E".ToHexBytes();
            var jT808_0X8103 = JT808Serializer.Analyze(bytes);
        }
    }
    class DefaultGlobalConfig : GlobalConfigBase
    {
        public override string ConfigId { protected set; get; } = "Default";
    }
}
