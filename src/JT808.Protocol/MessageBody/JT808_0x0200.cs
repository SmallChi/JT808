using JT808.Protocol.Enums;
using JT808.Protocol.Exceptions;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 位置信息汇报
    /// </summary>
    public class JT808_0x0200 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x0200>
    {
        public override ushort MsgId { get; } = 0x0200;
        /// <summary>
        /// 报警标志 
        /// </summary>
        public uint AlarmFlag { get; set; }
        /// <summary>
        /// 状态位标志
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
        /// 存储自定义附加信息源数据
        /// </summary>
        public Dictionary<byte, byte[]> JT808CustomLocationAttachOriginalData { get; set; }
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
    }
}
