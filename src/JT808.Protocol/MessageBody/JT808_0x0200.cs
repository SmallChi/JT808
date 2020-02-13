using JT808.Protocol.Enums;
using JT808.Protocol.Exceptions;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 位置信息汇报
    /// </summary>
    public class JT808_0x0200 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x0200>, IJT808Analyze
    {
        public override ushort MsgId { get; } = 0x0200;
        public override string Description => "位置信息汇报";
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
        /// 位置附加信息
        /// </summary>
        public Dictionary<byte, JT808_0x0200_BodyBase> JT808LocationAttachData { get; set; }
        /// <summary>
        /// 存储未知的附加信息源数据
        /// </summary>
        public Dictionary<byte, byte[]> JT808UnknownLocationAttachOriginalData { get; set; }
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
        public Dictionary<byte, JT808_0x0200_CustomBodyBase> JT808CustomLocationAttachData { get; set; }

        public JT808_0x0200 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200 jT808_0X0200 = new JT808_0x0200();
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
            {   //西经 ‭134217728‬ 0x8000000
                jT808_0X0200.Lng = (int)reader.ReadUInt32();
            }
            else
            {
                jT808_0X0200.Lng = reader.ReadInt32();
            }
            jT808_0X0200.Altitude = reader.ReadUInt16();
            jT808_0X0200.Speed = reader.ReadUInt16();
            jT808_0X0200.Direction = reader.ReadUInt16();
            jT808_0X0200.GPSTime = reader.ReadDateTime6();
            // 位置附加信息
            jT808_0X0200.JT808LocationAttachData = new Dictionary<byte, JT808_0x0200_BodyBase>();
            jT808_0X0200.JT808CustomLocationAttachData = new Dictionary<byte, JT808_0x0200_CustomBodyBase>();
            jT808_0X0200.JT808UnknownLocationAttachOriginalData = new Dictionary<byte, byte[]>();
            while (reader.ReadCurrentRemainContentLength() > 0)
            {
                try
                {
                    ReadOnlySpan<byte> attachSpan = reader.GetVirtualReadOnlySpan(2);
                    byte attachId = attachSpan[0];
                    byte attachLen = attachSpan[1];
                    if (config.JT808_0X0200_Factory.Map.TryGetValue(attachId, out object jT808LocationAttachInstance))
                    {
                        dynamic attachImpl = JT808MessagePackFormatterResolverExtensions.JT808DynamicDeserialize(jT808LocationAttachInstance, ref reader, config);
                        jT808_0X0200.JT808LocationAttachData.Add(attachImpl.AttachInfoId, attachImpl);
                    }
                    else if (config.JT808_0X0200_Custom_Factory.Map.TryGetValue(attachId,out object customAttachInstance))
                    {
                        dynamic attachImpl = JT808MessagePackFormatterResolverExtensions.JT808DynamicDeserialize(customAttachInstance, ref reader, config);
                        jT808_0X0200.JT808CustomLocationAttachData.Add(attachImpl.AttachInfoId, attachImpl);
                    }
                    else
                    {
                        reader.Skip(2);
                        jT808_0X0200.JT808UnknownLocationAttachOriginalData.Add(attachId, reader.ReadArray(reader.ReaderCount - 2, attachLen + 2).ToArray());
                        reader.Skip(attachLen);
                    }
                }
                catch
                {
                    try
                    {
                        byte attachId = reader.ReadByte();
                        byte attachLen = reader.ReadByte();
                        jT808_0X0200.JT808UnknownLocationAttachOriginalData.Add(attachId, reader.ReadArray(reader.ReaderCount - 2, attachLen + 2).ToArray());
                        reader.Skip(attachLen);
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
            return jT808_0X0200;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200 value, IJT808Config config)
        {
            writer.WriteUInt32(value.AlarmFlag);
            writer.WriteUInt32(value.StatusFlag);
            //0x10000000 南纬 134217728
            //0x8000000  西经 ‭‬268435456
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
            writer.WriteDateTime6(value.GPSTime);
            if (value.JT808LocationAttachData != null && value.JT808LocationAttachData.Count > 0)
            {
                foreach (var item in value.JT808LocationAttachData)
                {
                    try
                    {
                        JT808MessagePackFormatterResolverExtensions.JT808DynamicSerialize(item.Value, ref writer, item.Value, config);
                    }
                    catch(Exception ex)
                    {

                    }
                }
            }
            if (value.JT808CustomLocationAttachData != null && value.JT808CustomLocationAttachData.Count > 0)
            {
                foreach (var item in value.JT808CustomLocationAttachData)
                {
                    JT808MessagePackFormatterResolverExtensions.JT808DynamicSerialize(item.Value, ref writer, item.Value, config);
                }
            }
        }

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0200 value = new JT808_0x0200();
            value.AlarmFlag = reader.ReadUInt32();
            writer.WriteNumber($"[{value.AlarmFlag.ReadNumber()}]报警标志", value.AlarmFlag);
            value.StatusFlag = reader.ReadUInt32();
            var alarmFlagBits = Convert.ToString(value.AlarmFlag, 2).PadLeft(32, '0').AsSpan();
            writer.WriteStartObject("报警标志对象");
            if (reader.Version == JT808Version.JTT2019)
            {
                writer.WriteString($"[bit31]保留", $"{alarmFlagBits[31]}");
            }
            else
            {
                writer.WriteString($"[bit31]非法开门报警", $"{alarmFlagBits[31]}");
            }
            writer.WriteString($"[bit30]侧翻预警", $"{alarmFlagBits[30]}");
            writer.WriteString($"[bit29]碰撞预警", $"{alarmFlagBits[29]}");
            writer.WriteString($"[bit28]车辆非法位移", $"{alarmFlagBits[28]}");
            writer.WriteString($"[bit27]车辆非法点火", $"{alarmFlagBits[27]}");
            writer.WriteString($"[bit26]车辆被盗(通过车辆防盗器)", $"{alarmFlagBits[26]}");
            writer.WriteString($"[bit25]车辆油量异常", $"{alarmFlagBits[25]}");
            writer.WriteString($"[bit24]车辆VSS故障", $"{alarmFlagBits[24]}");
            writer.WriteString($"[bit23]路线偏离报警", $"{alarmFlagBits[23]}");
            writer.WriteString($"[bit22]路段行驶时间不足/过长", $"{alarmFlagBits[22]}");
            writer.WriteString($"[bit21]进出路线", $"{alarmFlagBits[21]}");
            writer.WriteString($"[bit20]进出区域", $"{alarmFlagBits[20]}");
            writer.WriteString($"[bit19]超时停车", $"{alarmFlagBits[19]}");
            writer.WriteString($"[bit18]当天累计驾驶超时", $"{alarmFlagBits[18]}");
            if (reader.Version == JT808Version.JTT2019)
            {
                writer.WriteString($"[bit17]右转盲区异常报警", $"{alarmFlagBits[17]}");
                writer.WriteString($"[bit16]胎压预警", $"{alarmFlagBits[16]}");
                writer.WriteString($"[bit15]违规行驶报警", $"{alarmFlagBits[15]}");
            }
            else
            {
                writer.WriteString($"[bit15~bit17]保留", alarmFlagBits.Slice(15, 3).ToString());
            }
            writer.WriteString($"[bit14]疲劳驾驶预警", $"{alarmFlagBits[14]}");
            writer.WriteString($"[bit13]超速预警", $"{alarmFlagBits[13]}");
            writer.WriteString($"[bit12]道路运输证IC卡模块故障", $"{alarmFlagBits[12]}");
            writer.WriteString($"[bit11]摄像头故障", $"{alarmFlagBits[11]}");
            writer.WriteString($"[bit10]TTS模块故障", $"{alarmFlagBits[10]}");
            writer.WriteString($"[bit9]终端LCD或显示器故障", $"{alarmFlagBits[9]}");
            writer.WriteString($"[bit8]终端主电源掉电", $"{alarmFlagBits[8]}");
            writer.WriteString($"[bit7]终端主电源欠压", $"{alarmFlagBits[7]}");
            writer.WriteString($"[bit6]GNSS天线短路", $"{alarmFlagBits[6]}");
            writer.WriteString($"[bit5]GNSS天线未接或被剪断", $"{alarmFlagBits[5]}");
            writer.WriteString($"[bit4]GNSS模块发生故障", $"{alarmFlagBits[4]}");
            writer.WriteString($"[bit3]危险预警", $"{alarmFlagBits[3]}");
            writer.WriteString($"[bit2]疲劳驾驶", $"{alarmFlagBits[2]}");
            writer.WriteString($"[bit1]超速报警", $"{alarmFlagBits[1]}");
            writer.WriteString($"[bit0]紧急报警,触动报警开关后触发", $"{alarmFlagBits[0]}");
            writer.WriteEndObject();
            writer.WriteNumber($"[{value.StatusFlag.ReadNumber()}]状态位标志", value.StatusFlag);
            var StatusFlagBits = Convert.ToString(value.StatusFlag, 2).PadLeft(32, '0').AsSpan();
            writer.WriteStartObject("状态标志对象");
            if (reader.Version == JT808Version.JTT2019)
            {
                writer.WriteString($"[bit23~bit31]保留", StatusFlagBits.Slice(23, 9).ToString());
                writer.WriteString($"[{StatusFlagBits[22]}]bit22", StatusFlagBits[22] == '0' ? "车辆处于停止状态" : "车辆处于行驶状态");
            }
            else
            {
                writer.WriteString($"[bit22~bit31]保留", StatusFlagBits.Slice(22, 10).ToString());
            }
            writer.WriteString($"[{StatusFlagBits[21]}]bit21", StatusFlagBits[21] == '0' ? "未使用Galileo卫星进行定位" : "使用Galileo卫星进行定位");
            writer.WriteString($"[{StatusFlagBits[20]}]bit20", StatusFlagBits[20] == '0' ? "未使用GLONASS卫星进行定位" : "使用GLONASS卫星进行定位");
            writer.WriteString($"[{StatusFlagBits[19]}]bit19", StatusFlagBits[19] == '0' ? "未使用北斗卫星进行定位" : "使用北斗卫星进行定位");
            writer.WriteString($"[{StatusFlagBits[18]}]bit18", StatusFlagBits[18] == '0' ? "未使用GPS卫星进行定位" : "使用GPS卫星进行定位");
            writer.WriteString($"[{StatusFlagBits[17]}]bit17", StatusFlagBits[17] == '0' ? "门5关" : "门5开");
            writer.WriteString($"[{StatusFlagBits[16]}]bit16", StatusFlagBits[16] == '0' ? "门4关" : "门4开");
            writer.WriteString($"[{StatusFlagBits[15]}]bit15", StatusFlagBits[15] == '0' ? "门3关" : "门3开");
            writer.WriteString($"[{StatusFlagBits[14]}]bit14", StatusFlagBits[14] == '0' ? "门2关" : "门2开");
            writer.WriteString($"[{StatusFlagBits[13]}]bit13", StatusFlagBits[13] == '0' ? "门1关" : "门1开");
            writer.WriteString($"[{StatusFlagBits[12]}]bit12", StatusFlagBits[12] == '0' ? "车门解锁" : "车门加锁");
            writer.WriteString($"[{StatusFlagBits[11]}]bit11", StatusFlagBits[11] == '0' ? "车辆电路正常" : "车辆电路断开");
            writer.WriteString($"[{StatusFlagBits[10]}]bit10", StatusFlagBits[10] == '0' ? "车辆油路正常" : "车辆油路断开");
            var bit8And9 = StatusFlagBits.Slice(8, 2).ToString();
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
            writer.WriteString($"[bit6~bit7]保留", StatusFlagBits.Slice(6, 2).ToString());
            writer.WriteString($"[{StatusFlagBits[5]}]bit5", StatusFlagBits[5] == '0' ? "经纬度未经保密插件加密" : "经纬度已经保密插件加密");
            writer.WriteString($"[{StatusFlagBits[4]}]bit4", StatusFlagBits[4] == '0' ? "运营状态" : "停运状态");
            writer.WriteString($"[{StatusFlagBits[3]}]bit3", StatusFlagBits[3] == '0' ? "东经" : "西经");
            writer.WriteString($"[{StatusFlagBits[2]}]bit2", StatusFlagBits[2] == '0' ? "北纬" : "南纬");
            writer.WriteString($"[{StatusFlagBits[1]}]bit1", StatusFlagBits[1] == '0' ? "未定位" : "定位");
            writer.WriteString($"[{StatusFlagBits[0]}]bit0", StatusFlagBits[0] == '0' ? "ACC关" : "ACC开");
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
            {   //西经 ‭134217728‬ 0x8000000
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
            value.GPSTime = reader.ReadDateTime6();
            writer.WriteString($"[{value.GPSTime.ToString("yyMMddHHmmss")}]定位时间", value.GPSTime.ToString("yyyy-MM-dd HH:mm:ss"));
            // 位置附加信息
            writer.WriteStartArray("附加信息列表");
            while (reader.ReadCurrentRemainContentLength() > 0)
            {
                try
                {
                    ReadOnlySpan<byte> attachSpan = reader.GetVirtualReadOnlySpan(2);
                    byte attachId = attachSpan[0];
                    byte attachLen = attachSpan[1];
                    if (config.JT808_0X0200_Factory.Map.TryGetValue(attachId, out object jT808LocationAttachInstance))
                    {
                        writer.WriteStartObject();
                        jT808LocationAttachInstance.Analyze(ref reader, writer, config);
                        writer.WriteEndObject();
                    }
                    else if (config.JT808_0X0200_Custom_Factory.Map.TryGetValue(attachId, out object customAttachInstance))
                    {
                        writer.WriteStartObject();
                        customAttachInstance.Analyze(ref reader, writer, config);
                        writer.WriteEndObject();
                    }
                    else
                    {
                        writer.WriteStartObject();
                        reader.Skip(2);
                        writer.WriteNumber($"[{attachId.ReadNumber()}]未知附加信息Id", attachId);
                        writer.WriteNumber($"[{attachLen.ReadNumber()}]未知附加信息长度", attachLen);
                        writer.WriteString($"未知附加信息", reader.ReadArray(reader.ReaderCount - 2, attachLen + 2).ToArray().ToHexString());
                        reader.Skip(attachLen);
                        writer.WriteEndObject();
                    }
                }
                catch
                {
                    writer.WriteStartObject();
                    try
                    {
                        byte attachId = reader.ReadByte();
                        byte attachLen = reader.ReadByte();
                        writer.WriteNumber($"[{attachId.ReadNumber()}]未知附加信息Id", attachId);
                        writer.WriteNumber($"[{attachLen.ReadNumber()}]未知附加信息长度", attachLen);
                        writer.WriteString($"未知附加信息", reader.ReadArray(reader.ReaderCount - 2, attachLen + 2).ToArray().ToHexString());   
                        reader.Skip(attachLen);
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    finally
                    {
                        writer.WriteEndObject();
                    }
                }
            }
            writer.WriteEndArray();
        }
    }
}
