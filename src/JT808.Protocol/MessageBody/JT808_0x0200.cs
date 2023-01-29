using JT808.Protocol.Enums;
using JT808.Protocol.Exceptions;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 位置信息汇报
    /// </summary>
    public class JT808_0x0200 : JT808MessagePackFormatter<JT808_0x0200>, JT808Bodies, IJT808Analyze
    {
        /// <summary>
        /// 0x0200
        /// </summary>
        public ushort MsgId  => 0x0200;
        /// <summary>
        /// 位置信息汇报
        /// </summary>
        public string Description => "位置信息汇报";
        /// <summary>
        /// 报警标志 
        /// <see cref="JT808.Protocol.Enums.JT808Alarm"/>
        /// </summary>
        public uint AlarmFlag { get; set; }
        /// <summary>
        /// 状态位标志
        /// <see cref="JT808.Protocol.Enums.JT808Status"/>
        /// </summary>
        public uint StatusFlag { get; set; }
        /// <summary>
        /// 纬度
        /// 以度为单位的纬度值乘以 10 的 6 次方，精确到百万分之一度
        /// </summary>
        public int Lat { get; set; }
        /// <summary>
        /// 经度
        /// 以度为单位的经度值乘以 10 的 6 次方，精确到百万分之一度
        /// </summary>
        public int Lng { get; set; }
        /// <summary>
        /// 高程
        /// 海拔高度，单位为米（m）
        /// </summary>
        public ushort Altitude { get; set; }
        /// <summary>
        /// 速度 1/10km/h
        /// </summary>
        public ushort Speed { get; set; }
        /// <summary>
        /// 方向 0-359，正北为 0，顺时针
        /// </summary>
        public ushort Direction { get; set; }
        /// <summary>
        /// YY-MM-DD-hh-mm-ss（GMT+8 时间，本标准中之后涉及的时间均采用此时区）
        /// </summary>
        public DateTime GPSTime { get; set; }
        /// <summary>
        /// 基础位置附加信息
        /// </summary>
        public Dictionary<byte, JT808_0x0200_BodyBase> BasicLocationAttachData { get; set; }
        /// <summary>
        /// 自定义位置附加信息
        /// 场景：
        /// 一个设备厂商对应多个设备类型，不同设备类型可能存在相同的自定义位置附加信息Id，导致自定义附加信息Id冲突，无法解析。
        /// 解决方式：
        /// 1.凡是解析自定义附加信息Id协议的，先进行分割存储，然后在根据外部的设备类型进行统一处理。
        /// 2.可以根据设备类型做个工厂，解耦对公共序列化器的依赖。
        /// 缺点：
        /// 依赖平台录入的设备类型
        /// </summary>
        public Dictionary<byte, JT808_0x0200_CustomBodyBase> CustomLocationAttachData { get; set; }
        /// <summary>
        /// 自定义位置附加信息2
        /// </summary>
        public Dictionary<ushort, JT808_0x0200_CustomBodyBase2> CustomLocationAttachData2 { get; set; }
        /// <summary>
        /// 自定义位置附加信息3
        /// </summary>
        public Dictionary<ushort, JT808_0x0200_CustomBodyBase3> CustomLocationAttachData3 { get; set; }
        /// <summary>
        /// 自定义位置附加信息4
        /// </summary>
        public Dictionary<byte, JT808_0x0200_CustomBodyBase4> CustomLocationAttachData4 { get; set; }
        /// <summary>
        /// 未知自定义附加数据【一切都是为了尽可能兼容】
        /// 形如:自定义_附加Id字节数_附加数据长度_附加Id
        /// 注意：这边不是最好的解决方式，最好的方式就是通过已知的自定义协议附加，根据提供的文档进行组织后在注册。
        /// 这边采用优先1-1的，然后是绝大多数设备厂家有2-1，少部分是2-2，最后是1_4。
        /// </summary>
        public Dictionary<ushort, byte[]> UnknownLocationAttachData { get; set; }
        /// <summary>
        /// 设备未知自定义附加数据（未注册）、数据解析异常
        /// </summary>
        public List<byte[]> ExceptionLocationAttachOriginalData { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x0200 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200 jT808_0X0200 = new ();
            jT808_0X0200.AlarmFlag = reader.ReadUInt32();
            jT808_0X0200.StatusFlag = reader.ReadUInt32();
            if (((jT808_0X0200.StatusFlag >> 28) & 1) == 1)
            {   //南纬 268435456 0x10000000
                jT808_0X0200.Lat = (int)reader.ReadUInt32();
            }
            else
            {
                jT808_0X0200.Lat = reader.ReadInt32();
            }
            if (((jT808_0X0200.StatusFlag >> 27) & 1) == 1)
            {   //西经 134217728 0x8000000
                jT808_0X0200.Lng = (int)reader.ReadUInt32();
            }
            else
            {
                jT808_0X0200.Lng = reader.ReadInt32();
            }
            jT808_0X0200.Altitude = reader.ReadUInt16();
            jT808_0X0200.Speed = reader.ReadUInt16();
            jT808_0X0200.Direction = reader.ReadUInt16();
            jT808_0X0200.GPSTime = reader.ReadDateTime_yyMMddHHmmss();
            // 位置附加信息
            jT808_0X0200.BasicLocationAttachData = new Dictionary<byte, JT808_0x0200_BodyBase>();
            jT808_0X0200.CustomLocationAttachData = new Dictionary<byte, JT808_0x0200_CustomBodyBase>();
            jT808_0X0200.CustomLocationAttachData2 = new Dictionary<ushort, JT808_0x0200_CustomBodyBase2>();
            jT808_0X0200.CustomLocationAttachData3 = new Dictionary<ushort, JT808_0x0200_CustomBodyBase3>();
            jT808_0X0200.CustomLocationAttachData4 = new Dictionary<byte, JT808_0x0200_CustomBodyBase4>();
            jT808_0X0200.ExceptionLocationAttachOriginalData = new List<byte[]>();
            jT808_0X0200.UnknownLocationAttachData = new Dictionary<ushort, byte[]>();
            while (reader.ReadCurrentRemainContentLength() > 0)
            {
                try
                {
                    //正常自定义注册、正常数据解析，不支持国标乱序组包
                    //优先国标组包->自定义附加数据注册->未知/异常数据
                    //注意：最坏的是自定义的跟基础标准的附加信息Id冲突了,那么优先使用标准的进行解析
                    //基础标准附加Id、自定义标准附加Id、自定义标准附加Id 4
                    byte attachId = reader.ReadVirtualByte();
                    //自定义标准附加Id2、自定义标准附加Id3
                    ushort attachId2_3 = reader.ReadVirtualUInt16();
                    if (config.JT808_0X0200_Factory.TryGetValue(reader.Version, attachId, out object attachInstance))
                    {
                        if (jT808_0X0200.BasicLocationAttachData.ContainsKey(attachId))
                        {
                            //存在重复的就不解析，把数据统一放在异常定位数据里面
                            reader.Skip(1);
                            byte attachLen = reader.ReadByte();
                            jT808_0X0200.ExceptionLocationAttachOriginalData.Add(reader.ReadArray(reader.ReaderCount - 2, attachLen + 2).ToArray());
                            reader.Skip(attachLen);
                        }
                        else
                        {
                            var attachImpl = attachInstance.DeserializeExt<JT808_0x0200_BodyBase>(ref reader, config); ;
                            if (attachImpl != null)
                            {
                                jT808_0X0200.BasicLocationAttachData.Add(attachImpl.AttachInfoId, attachImpl);
                            }
                        }
                    }
                    else if (config.JT808_0X0200_Custom_Factory.Map.TryGetValue(attachId, out object customAttachInstance))
                    {
                        if (jT808_0X0200.CustomLocationAttachData.ContainsKey(attachId))
                        {
                            reader.Skip(1);
                            byte attachLen = reader.ReadByte();
                            jT808_0X0200.ExceptionLocationAttachOriginalData.Add(reader.ReadArray(reader.ReaderCount - 2, attachLen + 2).ToArray());
                            reader.Skip(attachLen);
                        }
                        else
                        {
                            var attachImpl = customAttachInstance.DeserializeExt<JT808_0x0200_CustomBodyBase>(ref reader, config); ;
                            if (attachImpl != null)
                            {
                                jT808_0X0200.CustomLocationAttachData.Add(attachImpl.AttachInfoId, attachImpl);
                            }
                        }
                    }
                    else if (config.JT808_0X0200_Custom_Factory.Map2.TryGetValue(attachId2_3, out object customAttachInstance2))
                    {
                        if (jT808_0X0200.CustomLocationAttachData2.ContainsKey(attachId2_3))
                        {
                            reader.Skip(2);
                            byte attachLen = reader.ReadByte();
                            jT808_0X0200.ExceptionLocationAttachOriginalData.Add(reader.ReadArray(reader.ReaderCount - 3, attachLen + 3).ToArray());
                            reader.Skip(attachLen);
                        }
                        else
                        {
                            var attachImpl = customAttachInstance2.DeserializeExt<JT808_0x0200_CustomBodyBase2>(ref reader, config); ;
                            if (attachImpl != null)
                            {
                                jT808_0X0200.CustomLocationAttachData2.Add(attachImpl.AttachInfoId, attachImpl);
                            }
                        }
                    }
                    else if (config.JT808_0X0200_Custom_Factory.Map4.TryGetValue(attachId, out object customAttachInstance4))
                    {
                        if (jT808_0X0200.CustomLocationAttachData4.ContainsKey(attachId))
                        {
                            reader.Skip(1);
                            int attachLen = reader.ReadInt32();
                            jT808_0X0200.ExceptionLocationAttachOriginalData.Add(reader.ReadArray(reader.ReaderCount - 5, attachLen + 5).ToArray());
                            reader.Skip(attachLen);
                        }
                        else
                        {
                            var attachImpl = customAttachInstance4.DeserializeExt<JT808_0x0200_CustomBodyBase4>(ref reader, config); ;
                            if (attachImpl != null)
                            {
                                jT808_0X0200.CustomLocationAttachData4.Add(attachImpl.AttachInfoId, attachImpl);
                            }
                        }
                    }
                    else if (config.JT808_0X0200_Custom_Factory.Map3.TryGetValue(attachId2_3, out object customAttachInstance3))
                    {
                        if (jT808_0X0200.CustomLocationAttachData3.ContainsKey(attachId2_3))
                        {
                            reader.Skip(2);
                            ushort attachLen = reader.ReadUInt16();
                            jT808_0X0200.ExceptionLocationAttachOriginalData.Add(reader.ReadArray(reader.ReaderCount - 4, attachLen + 4).ToArray());
                            reader.Skip(attachLen);
                        }
                        else
                        {
                            var attachImpl = customAttachInstance3.DeserializeExt<JT808_0x0200_CustomBodyBase3>(ref reader, config); ;
                            if (attachImpl != null)
                            {
                                jT808_0X0200.CustomLocationAttachData3.Add(attachImpl.AttachInfoId, attachImpl);
                            }
                        }
                    }
                    else
                    {
                        //未知的附加只通过标准的自定义附加信息来解析，其余的通过自己注册，自己实现的方式来解析
                        reader.Skip(1);
                        byte attachLen = reader.ReadByte();
                        int remainLength = reader.ReadCurrentRemainContentLength();
                        if(remainLength < attachLen)
                        {
                            jT808_0X0200.ExceptionLocationAttachOriginalData.Add(reader.ReadArray(reader.ReaderCount - 2, remainLength+2).ToArray());
                            reader.ReadArray(remainLength);
                        }
                        else
                        {
                            if (jT808_0X0200.UnknownLocationAttachData.ContainsKey(attachId))
                            {
                                //存在重复的就不解析，把数据统一放在异常定位数据里面
                                jT808_0X0200.ExceptionLocationAttachOriginalData.Add(reader.ReadArray(reader.ReaderCount - 2, attachLen + 2).ToArray());
                            }
                            else
                            {
                                jT808_0X0200.UnknownLocationAttachData.Add(attachId, reader.ReadArray(reader.ReaderCount - 2, attachLen + 2).ToArray());
                            }
                            reader.Skip(attachLen);
                        }
                    }
                }
                catch
                {
                    try
                    {
                        var remainLength = reader.ReadCurrentRemainContentLength();
                        if (remainLength > 0)
                        {
                            jT808_0X0200.ExceptionLocationAttachOriginalData.Add(reader.ReadArray(remainLength).ToArray());
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        break;
                    }
                }
            }
            return jT808_0X0200;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200 value, IJT808Config config)
        {
            writer.WriteUInt32(value.AlarmFlag);
            writer.WriteUInt32(value.StatusFlag);
            //0x10000000 南纬 134217728
            //0x8000000  西经 268435456
            //0x18000000 南纬-西经 134217728+268435456
            if (((value.StatusFlag >> 28) & 1) == 1)
            {
                uint lat = (uint)value.Lat;
                writer.WriteUInt32(lat);
            }
            else
            {
                if (value.Lat < 0)
                {
                    throw new JT808Exception(JT808ErrorCode.LatOrLngError, $"Lat {nameof(JT808_0x0200.StatusFlag)} ({value.StatusFlag}>>28) !=1");
                }
                writer.WriteInt32(value.Lat);
            }
            if (((value.StatusFlag >> 27) & 1) == 1)
            {
                uint lng = (uint)value.Lng;
                writer.WriteUInt32(lng);
            }
            else
            {
                if (value.Lng < 0)
                {
                    throw new JT808Exception(JT808ErrorCode.LatOrLngError, $"Lng {nameof(JT808_0x0200.StatusFlag)} ({value.StatusFlag}>>29) !=1");
                }
                writer.WriteInt32(value.Lng);
            }
            writer.WriteUInt16(value.Altitude);
            writer.WriteUInt16(value.Speed);
            writer.WriteUInt16(value.Direction);
            writer.WriteDateTime_yyMMddHHmmss(value.GPSTime);
            if (value.BasicLocationAttachData != null && value.BasicLocationAttachData.Count > 0)
            {
                foreach (var item in value.BasicLocationAttachData)
                {
                    try
                    {
                        item.Value.SerializeExt(ref writer, item.Value, config);
                    }
                    catch
                    {

                    }
                }
            }
            if (value.CustomLocationAttachData != null && value.CustomLocationAttachData.Count > 0)
            {
                foreach (var item in value.CustomLocationAttachData)
                {
                    item.Value.SerializeExt(ref writer, item.Value, config);
                }
            }
            if (value.CustomLocationAttachData2 != null && value.CustomLocationAttachData2.Count > 0)
            {
                foreach (var item in value.CustomLocationAttachData2)
                {
                    item.Value.SerializeExt(ref writer, item.Value, config);
                }
            }
            if (value.CustomLocationAttachData3 != null && value.CustomLocationAttachData3.Count > 0)
            {
                foreach (var item in value.CustomLocationAttachData3)
                {
                    item.Value.SerializeExt(ref writer, item.Value, config);
                }
            }
            if (value.CustomLocationAttachData4 != null && value.CustomLocationAttachData4.Count > 0)
            {
                foreach (var item in value.CustomLocationAttachData4)
                {
                    item.Value.SerializeExt(ref writer, item.Value, config);
                }
            }
            if (value.UnknownLocationAttachData!=null && value.UnknownLocationAttachData.Count > 0)
            {
                foreach (var item in value.UnknownLocationAttachData)
                {
                    if(item.Value!=null && item.Value.Length >= 2)
                    {
                        writer.WriteArray(item.Value);
                    }
                }
            }
            if (value.ExceptionLocationAttachOriginalData != null && value.ExceptionLocationAttachOriginalData.Count > 0)
            {
                foreach (var item in value.ExceptionLocationAttachOriginalData)
                {
                    if (item != null && item.Length >= 2)
                    {
                        writer.WriteArray(item);
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0200 value = new ();
            value.AlarmFlag = reader.ReadUInt32();
            writer.WriteNumber($"[{value.AlarmFlag.ReadBinary().ToString()}]报警标志", value.AlarmFlag);
            value.StatusFlag = reader.ReadUInt32();
            var alarmFlagBits =Convert.ToString(value.AlarmFlag, 2).PadLeft(32, '0').AsSpan();
            writer.WriteStartObject("报警标志对象");
            if (reader.Version == JT808Version.JTT2019)
            {
                writer.WriteString($"[bit31]保留", $"{alarmFlagBits[0]}");
            }
            else
            {
                writer.WriteString($"[bit31]非法开门报警", $"{alarmFlagBits[0]}");
            }
            writer.WriteString($"[bit30]侧翻预警", $"{alarmFlagBits[1]}");
            writer.WriteString($"[bit29]碰撞预警", $"{alarmFlagBits[2]}");
            writer.WriteString($"[bit28]车辆非法位移", $"{alarmFlagBits[3]}");
            writer.WriteString($"[bit27]车辆非法点火", $"{alarmFlagBits[4]}");
            writer.WriteString($"[bit26]车辆被盗(通过车辆防盗器)", $"{alarmFlagBits[5]}");
            writer.WriteString($"[bit25]车辆油量异常", $"{alarmFlagBits[6]}");
            writer.WriteString($"[bit24]车辆VSS故障", $"{alarmFlagBits[7]}");
            writer.WriteString($"[bit23]路线偏离报警", $"{alarmFlagBits[8]}");
            writer.WriteString($"[bit22]路段行驶时间不足/过长", $"{alarmFlagBits[9]}");
            writer.WriteString($"[bit21]进出路线", $"{alarmFlagBits[10]}");
            writer.WriteString($"[bit20]进出区域", $"{alarmFlagBits[11]}");
            writer.WriteString($"[bit19]超时停车", $"{alarmFlagBits[12]}");
            writer.WriteString($"[bit18]当天累计驾驶超时", $"{alarmFlagBits[13]}");
            if (reader.Version == JT808Version.JTT2019)
            {
                writer.WriteString($"[bit17]右转盲区异常报警", $"{alarmFlagBits[14]}");
                writer.WriteString($"[bit16]胎压预警", $"{alarmFlagBits[15]}");
                writer.WriteString($"[bit15]违规行驶报警", $"{alarmFlagBits[16]}");
            }
            else
            {
                writer.WriteString($"[bit15~bit17]保留", alarmFlagBits.Slice(14, 3).ToString());
            }
            writer.WriteString($"[bit14]疲劳驾驶预警", $"{alarmFlagBits[17]}");
            writer.WriteString($"[bit13]超速预警", $"{alarmFlagBits[18]}");
            writer.WriteString($"[bit12]道路运输证IC卡模块故障", $"{alarmFlagBits[19]}");
            writer.WriteString($"[bit11]摄像头故障", $"{alarmFlagBits[20]}");
            writer.WriteString($"[bit10]TTS模块故障", $"{alarmFlagBits[21]}");
            writer.WriteString($"[bit9]终端LCD或显示器故障", $"{alarmFlagBits[22]}");
            writer.WriteString($"[bit8]终端主电源掉电", $"{alarmFlagBits[23]}");
            writer.WriteString($"[bit7]终端主电源欠压", $"{alarmFlagBits[24]}");
            writer.WriteString($"[bit6]GNSS天线短路", $"{alarmFlagBits[25]}");
            writer.WriteString($"[bit5]GNSS天线未接或被剪断", $"{alarmFlagBits[26]}");
            writer.WriteString($"[bit4]GNSS模块发生故障", $"{alarmFlagBits[27]}");
            writer.WriteString($"[bit3]危险预警", $"{alarmFlagBits[28]}");
            writer.WriteString($"[bit2]疲劳驾驶", $"{alarmFlagBits[29]}");
            writer.WriteString($"[bit1]超速报警", $"{alarmFlagBits[30]}");
            writer.WriteString($"[bit0]紧急报警,触动报警开关后触发", $"{alarmFlagBits[31]}");
            writer.WriteEndObject();
            writer.WriteNumber($"[{value.StatusFlag.ReadBinary().ToString()}]状态位标志", value.StatusFlag);
            var StatusFlagBits = Convert.ToString(value.StatusFlag, 2).PadLeft(32, '0').AsSpan();
            writer.WriteStartObject("状态标志对象");
            if (reader.Version == JT808Version.JTT2019)
            {
                writer.WriteString($"[bit23~bit31]保留", StatusFlagBits.Slice(0, 9).ToString());
                writer.WriteString($"[{StatusFlagBits[9]}]bit22", StatusFlagBits[9] == '0' ? "车辆处于停止状态" : "车辆处于行驶状态");
            }
            else
            {
                writer.WriteString($"[bit22~bit31]保留", StatusFlagBits.Slice(0, 10).ToString());
            }
            writer.WriteString($"[{StatusFlagBits[10]}]bit21", StatusFlagBits[10] == '0' ? "未使用Galileo卫星进行定位" : "使用Galileo卫星进行定位");
            writer.WriteString($"[{StatusFlagBits[11]}]bit20", StatusFlagBits[11] == '0' ? "未使用GLONASS卫星进行定位" : "使用GLONASS卫星进行定位");
            writer.WriteString($"[{StatusFlagBits[12]}]bit19", StatusFlagBits[12] == '0' ? "未使用北斗卫星进行定位" : "使用北斗卫星进行定位");
            writer.WriteString($"[{StatusFlagBits[13]}]bit18", StatusFlagBits[13] == '0' ? "未使用GPS卫星进行定位" : "使用GPS卫星进行定位");
            writer.WriteString($"[{StatusFlagBits[14]}]bit17", StatusFlagBits[14] == '0' ? "门5关" : "门5开");
            writer.WriteString($"[{StatusFlagBits[15]}]bit16", StatusFlagBits[15] == '0' ? "门4关" : "门4开");
            writer.WriteString($"[{StatusFlagBits[16]}]bit15", StatusFlagBits[16] == '0' ? "门3关" : "门3开");
            writer.WriteString($"[{StatusFlagBits[17]}]bit14", StatusFlagBits[17] == '0' ? "门2关" : "门2开");
            writer.WriteString($"[{StatusFlagBits[18]}]bit13", StatusFlagBits[18] == '0' ? "门1关" : "门1开");
            writer.WriteString($"[{StatusFlagBits[19]}]bit12", StatusFlagBits[19] == '0' ? "车门解锁" : "车门加锁");
            writer.WriteString($"[{StatusFlagBits[20]}]bit11", StatusFlagBits[20] == '0' ? "车辆电路正常" : "车辆电路断开");
            writer.WriteString($"[{StatusFlagBits[21]}]bit10", StatusFlagBits[21] == '0' ? "车辆油路正常" : "车辆油路断开");
            var bit8And9 = StatusFlagBits.Slice(22, 2).ToString();
            switch (bit8And9)
            {
                case "00":
                    writer.WriteString($"[{bit8And9}]bit8~bit9", "空车");
                    break;
                case "01":
                    writer.WriteString($"[{bit8And9}]bit8~bit9", "半载");
                    break;
                case "10":
                    writer.WriteString($"[{bit8And9}]bit8~bit9", "保留");
                    break;
                case "11":
                    writer.WriteString($"[{bit8And9}]bit8~bit9", "满载");
                    break;
            }
            writer.WriteString($"[bit6~bit7]保留", StatusFlagBits.Slice(24, 2).ToString());
            writer.WriteString($"[{StatusFlagBits[26]}]bit5", StatusFlagBits[26] == '0' ? "经纬度未经保密插件加密" : "经纬度已经保密插件加密");
            writer.WriteString($"[{StatusFlagBits[27]}]bit4", StatusFlagBits[27] == '0' ? "运营状态" : "停运状态");
            writer.WriteString($"[{StatusFlagBits[28]}]bit3", StatusFlagBits[28] == '0' ? "东经" : "西经");
            writer.WriteString($"[{StatusFlagBits[29]}]bit2", StatusFlagBits[29] == '0' ? "北纬" : "南纬");
            writer.WriteString($"[{StatusFlagBits[30]}]bit1", StatusFlagBits[30] == '0' ? "未定位" : "定位");
            writer.WriteString($"[{StatusFlagBits[31]}]bit0", StatusFlagBits[31] == '0' ? "ACC关" : "ACC开");
            writer.WriteEndObject();
            if (((value.StatusFlag >> 28) & 1) == 1)
            {   //南纬 268435456 0x10000000
                value.Lat = (int)reader.ReadUInt32();
                writer.WriteNumber($"[{value.Lat.ReadNumber()}]纬度", value.Lat);
            }
            else
            {
                value.Lat = reader.ReadInt32();
                writer.WriteNumber($"[{value.Lat.ReadNumber()}]纬度", value.Lat);
            }
            if (((value.StatusFlag >> 27) & 1) == 1)
            {   //西经 134217728 0x8000000
                value.Lng = (int)reader.ReadUInt32();
                writer.WriteNumber($"[{value.Lng.ReadNumber()}]经度", value.Lng);
            }
            else
            {
                value.Lng = reader.ReadInt32();
                writer.WriteNumber($"[{value.Lng.ReadNumber()}]经度", value.Lng);
            }
            value.Altitude = reader.ReadUInt16();
            writer.WriteNumber($"[{value.Altitude.ReadNumber()}]高程", value.Altitude);
            value.Speed = reader.ReadUInt16();
            writer.WriteNumber($"[{value.Speed.ReadNumber()}]速度", value.Speed);
            value.Direction = reader.ReadUInt16();
            writer.WriteNumber($"[{value.Direction.ReadNumber()}]方向", value.Direction);
            value.GPSTime = reader.ReadDateTime_yyMMddHHmmss();
            writer.WriteString($"[{value.GPSTime:yyMMddHHmmss}]定位时间", value.GPSTime.ToString("yyyy-MM-dd HH:mm:ss"));
            // 位置附加信息
            value.BasicLocationAttachData = new Dictionary<byte, JT808_0x0200_BodyBase>();
            value.CustomLocationAttachData = new Dictionary<byte, JT808_0x0200_CustomBodyBase>();
            value.CustomLocationAttachData2 = new Dictionary<ushort, JT808_0x0200_CustomBodyBase2>();
            value.CustomLocationAttachData3 = new Dictionary<ushort, JT808_0x0200_CustomBodyBase3>();
            value.CustomLocationAttachData4 = new Dictionary<byte, JT808_0x0200_CustomBodyBase4>();
            value.ExceptionLocationAttachOriginalData = new List<byte[]>();
            writer.WriteStartArray("附加信息列表");
            while (reader.ReadCurrentRemainContentLength() > 0)
            {
                try
                {
                    //正常自定义注册、正常数据解析，不支持国标乱序组包
                    //优先国标组包->自定义附加数据注册->异常数据
                    //注意：最坏的是自定义的跟基础标准的附加信息Id冲突了,那么优先使用标准的进行解析
                    //基础标准附加Id、自定义标准附加Id、自定义标准附加Id 4
                    byte attachId = reader.ReadVirtualByte();
                    //自定义标准附加Id2、自定义标准附加Id3
                    ushort attachId2_3 = reader.ReadVirtualUInt16();
                    if (config.JT808_0X0200_Factory.TryGetValue(reader.Version, attachId, out object attachInstance))
                    {
                        if (value.BasicLocationAttachData.ContainsKey(attachId))
                        {
                            //存在重复的就不解析，把数据统一放在异常定位数据里面
                            reader.Skip(1);
                            byte attachLen = reader.ReadByte();
                            writer.WriteNumber($"[{attachId.ReadNumber()}]未知附加信息Id", attachId);
                            writer.WriteNumber($"[{attachLen.ReadNumber()}]未知附加信息长度", attachLen);
                            writer.WriteString($"未知附加信息", reader.ReadArray(reader.ReaderCount - 2, attachLen + 2).ToArray().ToHexString());
                            reader.Skip(attachLen);
                        }
                        else
                        {
                            writer.WriteStartObject();
                            attachInstance.Analyze(ref reader, writer, config);
                            writer.WriteEndObject();
                            value.BasicLocationAttachData.Add(attachId, null);
                        }
                    }
                    else if (config.JT808_0X0200_Custom_Factory.Map.TryGetValue(attachId, out object customAttachInstance))
                    {
                        if (value.CustomLocationAttachData.ContainsKey(attachId))
                        {
                            reader.Skip(1);
                            byte attachLen = reader.ReadByte();
                            writer.WriteNumber($"[{attachId.ReadNumber()}]未知附加信息Id", attachId);
                            writer.WriteNumber($"[{attachLen.ReadNumber()}]未知附加信息长度", attachLen);
                            writer.WriteString($"未知附加信息_{attachId}", reader.ReadArray(reader.ReaderCount - 2, attachLen + 2).ToArray().ToHexString());
                            reader.Skip(attachLen);
                        }
                        else
                        {
                            writer.WriteStartObject();
                            customAttachInstance.Analyze(ref reader, writer, config);
                            writer.WriteEndObject();
                            value.CustomLocationAttachData.Add(attachId, null);
                        }
                    }
                    else if (config.JT808_0X0200_Custom_Factory.Map4.TryGetValue(attachId, out object customAttachInstance4))
                    {
                        if (value.CustomLocationAttachData4.ContainsKey(attachId))
                        {
                            reader.Skip(1);
                            int attachLen = reader.ReadInt32();
                            writer.WriteNumber($"[{attachId.ReadNumber()}]未知附加信息Id", attachId);
                            writer.WriteNumber($"[{attachLen.ReadNumber()}]未知附加信息长度", attachLen);
                            writer.WriteString($"未知附加信息1_4_{attachId}", reader.ReadArray(reader.ReaderCount - 5, attachLen + 5).ToArray().ToHexString());
                            reader.Skip(attachLen);
                        }
                        else
                        {
                            writer.WriteStartObject();
                            customAttachInstance4.Analyze(ref reader, writer, config);
                            writer.WriteEndObject();
                            value.CustomLocationAttachData4.Add(attachId, null);
                        }
                    }
                    else if (config.JT808_0X0200_Custom_Factory.Map2.TryGetValue(attachId2_3, out object customAttachInstance2))
                    {
                        if (value.CustomLocationAttachData2.ContainsKey(attachId2_3))
                        {
                            reader.Skip(2);
                            byte attachLen = reader.ReadByte();
                            writer.WriteNumber($"[{attachId.ReadNumber()}]未知附加信息Id", attachId);
                            writer.WriteNumber($"[{attachLen.ReadNumber()}]未知附加信息长度", attachLen);
                            writer.WriteString($"未知附加信息2_1", reader.ReadArray(reader.ReaderCount - 3, attachLen + 3).ToArray().ToHexString());
                            reader.Skip(attachLen);
                        }
                        else
                        {
                            writer.WriteStartObject();
                            customAttachInstance2.Analyze(ref reader, writer, config);
                            writer.WriteEndObject();
                            value.CustomLocationAttachData2.Add(attachId2_3, null);
                        }
                    }
                    else if (config.JT808_0X0200_Custom_Factory.Map3.TryGetValue(attachId2_3, out object customAttachInstance3))
                    {
                        if (value.CustomLocationAttachData3.ContainsKey(attachId2_3))
                        {
                            reader.Skip(2);
                            ushort attachLen = reader.ReadUInt16();
                            writer.WriteNumber($"[{attachId.ReadNumber()}]未知附加信息Id", attachId);
                            writer.WriteNumber($"[{attachLen.ReadNumber()}]未知附加信息长度", attachLen);
                            writer.WriteString($"未知附加信息2_2_{attachId}", reader.ReadArray(reader.ReaderCount - 4, attachLen + 4).ToArray().ToHexString());
                            reader.Skip(attachLen);
                        }
                        else
                        {
                            writer.WriteStartObject();
                            customAttachInstance3.Analyze(ref reader, writer, config);
                            writer.WriteEndObject();
                            value.CustomLocationAttachData3.Add(attachId2_3, null);
                        }
                    }
                    else
                    {
                        //未知的附加只通过标准的自定义附加信息来解析，其余的通过自己注册，自己实现的方式来解析
                        reader.Skip(1);
                        byte attachLen = reader.ReadByte();
                        int remainLength = reader.ReadCurrentRemainContentLength();
                        writer.WriteStartObject();
                        writer.WriteNumber($"[{attachId.ReadNumber()}]未知附加信息Id", attachId);
                        writer.WriteNumber($"[{attachLen.ReadNumber()}]未知附加信息长度", attachLen);
                        if ((attachLen+2) > remainLength)
                        {
                            writer.WriteString($"未知附加信息[异常解析]", reader.ReadArray(remainLength).ToArray().ToHexString());
                        }
                        else
                        {
                            writer.WriteString($"未知附加信息", reader.ReadArray(reader.ReaderCount - 2, attachLen + 2).ToArray().ToHexString());
                            reader.Skip(attachLen);
                        }
                        writer.WriteEndObject();
                    }
                }
                catch (Exception ex)
                {
                    writer.WriteStartObject();
                    writer.WriteString($"解析外部部未知附加信息报错", ex.StackTrace);
                    try
                    {
                        var remainLength = reader.ReadCurrentRemainContentLength();
                        if (remainLength > 0)
                        {
                            writer.WriteString($"未知附加信息", reader.ReadArray(remainLength).ToArray().ToHexString());
                        }
                        else
                        {
                            writer.WriteStartObject();
                            writer.WriteString($"未知附加信息", "无");
                            writer.WriteEndObject();
                        }
                    }
                    catch (Exception innerEx)
                    {
                        writer.WriteString($"解析内部未知附加信息报错", innerEx.StackTrace);
                    }
                    finally
                    {
                        writer.WriteEndObject();
                    }
                    break;
                }
            }
            writer.WriteEndArray();
        }
    }
}
