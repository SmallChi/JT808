using System;
using System.Buffers.Binary;
using System.Text.Json;

using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// CAN 总线 ID 单独采集设置：
    /// bit63-bit32 表示此 ID 采集时间间隔(ms)，0 表示不采集；
    /// bit31 表示 CAN 通道号，0：CAN1，1：CAN2；
    /// bit30 表示帧类型，0：标准帧，1：扩展帧；
    /// bit29 表示数据采集方式，0：原始数据，1：采集区间的计算值；
    /// bit28-bit0 表示 CAN 总线 ID。
    /// </summary>
    public class JT808_0x8103_0x0110 : JT808MessagePackFormatter<JT808_0x8103_0x0110>, JT808_0x8103_BodyBase, IJT808Analyze
    {
        /// <summary>
        /// 0x0110
        /// </summary>
        public uint ParamId { get; set; } = 0x0110;
        /// <summary>
        /// 数据长度
        /// 8 byte
        /// </summary>
        public byte ParamLength { get; set; } = 8;
        /// <summary>
        /// bit63-bit32 表示此 ID 采集时间间隔(ms)，0 表示不采集；
        /// </summary>
        public uint CollectTimeInterval { get; set; }
        /// <summary>
        /// bit31 表示 CAN 通道号，0：CAN1，1：CAN2；
        /// </summary>
        public byte ChannelNo { get; set; }
        /// <summary>
        /// bit30 表示帧类型，0：标准帧，1：扩展帧；
        /// </summary>
        public byte FrameType { get; set; }
        /// <summary>
        ///  bit29 表示数据采集方式，0：原始数据，1：采集区间的计算值；
        /// </summary>
        public byte CollectWay { get; set; }
        /// <summary>
        /// bit28-bit0 表示 CAN 总线 ID。
        /// </summary>
        public long BusId { get; set; }
        /// <summary>
        /// CAN总线ID单独采集设置
        /// </summary>
        public string Description => "CAN总线ID单独采集设置";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8103_0x0110 jT808_0x8103_0x0110 = new JT808_0x8103_0x0110();
            jT808_0x8103_0x0110.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x0110.ParamLength = reader.ReadByte();

            jT808_0x8103_0x0110.CollectTimeInterval = reader.ReadUInt32();
            var temp = reader.ReadUInt32();
            jT808_0x8103_0x0110.ChannelNo = (byte)((temp >> 31) & 0x01);
            jT808_0x8103_0x0110.FrameType = (byte)((temp >> 30) & 0x01);
            jT808_0x8103_0x0110.CollectWay = (byte)((temp >> 29) & 0x01);
            jT808_0x8103_0x0110.BusId = temp & 0x01FFFFFF;
            writer.WriteNumber($"[{jT808_0x8103_0x0110.ParamId.ReadNumber()}]参数ID", jT808_0x8103_0x0110.ParamId);
            writer.WriteNumber($"[{jT808_0x8103_0x0110.ParamLength.ReadNumber()}]参数长度", jT808_0x8103_0x0110.ParamLength);
            writer.WriteStartObject($"CAN总线ID单独采集设置");
            writer.WriteNumber($"[{ jT808_0x8103_0x0110.CollectTimeInterval.ReadNumber()}]此 ID 采集时间间隔(ms)", jT808_0x8103_0x0110.CollectTimeInterval) ;
            writer.WriteString($"[{ jT808_0x8103_0x0110.ChannelNo.ReadBinary()[0]}]CAN通道号", jT808_0x8103_0x0110.ChannelNo == 0? "CAN1" : "CAN2");
            writer.WriteString($"[{ jT808_0x8103_0x0110.FrameType.ReadBinary()[0]}]帧类型", jT808_0x8103_0x0110.FrameType == 0 ? "标准帧" : "扩展帧");
            writer.WriteString($"[{ jT808_0x8103_0x0110.CollectWay.ReadBinary()[0]}]数据采集方式", jT808_0x8103_0x0110.CollectWay== 0 ? "原始数据" : "采集区间的计算值");
            writer.WriteNumber($"[{ jT808_0x8103_0x0110.BusId.ReadNumber()}]CAN 总线 ID", jT808_0x8103_0x0110.BusId);
            writer.WriteEndObject();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8103_0x0110 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x0110 jT808_0x8103_0x0110 = new JT808_0x8103_0x0110();
            jT808_0x8103_0x0110.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x0110.ParamLength = reader.ReadByte();
            jT808_0x8103_0x0110.CollectTimeInterval= reader.ReadUInt32();
            var temp= reader.ReadUInt32();
            jT808_0x8103_0x0110.ChannelNo =(byte)( (temp >> 31) & 0x01);
            jT808_0x8103_0x0110.FrameType = (byte)((temp >> 30) & 0x01);
            jT808_0x8103_0x0110.CollectWay = (byte)((temp >> 29) & 0x01);
            jT808_0x8103_0x0110.BusId = temp & 0x01FFFFFF;
            return jT808_0x8103_0x0110;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x0110 value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteUInt32(value.CollectTimeInterval);
            var temp = (uint)(value.ChannelNo << 31 | value.FrameType << 30 | value.CollectWay << 29 | (int)(value.BusId& 0x01FFFFFF));
            writer.WriteUInt32(temp);
        }
    }
}
