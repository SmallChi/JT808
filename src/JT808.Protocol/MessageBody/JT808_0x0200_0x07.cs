
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System.Collections.Generic;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 卫星状态数据
    ///  2019版本
    /// </summary>
    public class JT808_0x0200_0x07 : JT808MessagePackFormatter<JT808_0x0200_0x07>, JT808_0x0200_BodyBase,  IJT808Analyze, IJT808_2019_Version
    {
        /// <summary>
        /// 
        /// </summary>
        public JT808_0x0200_0x07()
        {
            BeiDou = new List<SatelliteStatusInformation>();
            GPS = new List<SatelliteStatusInformation>();
            GLONASS = new List<SatelliteStatusInformation>();
            Galileo = new List<SatelliteStatusInformation>();
        }
        /// <summary>
        /// JT808_0x0200_0x07
        /// </summary>
        public byte AttachInfoId { get; set; } = JT808Constants.JT808_0x0200_0x07;
        /// <summary>
        /// 4 的倍数
        /// </summary>
        public byte AttachInfoLength { get; set; }
        /// <summary>
        /// BeiDou
        /// 最小值是 0，最大值 12，CN 值大于等于 20 的卫星数量
        /// </summary>
        public List<SatelliteStatusInformation> BeiDou { get; set; }
        /// <summary>
        /// GPS
        /// 最小值是 0，最大值 12，CN 值大于等于 20 的卫星数量
        /// </summary>
        public List<SatelliteStatusInformation> GPS { get; set; }
        /// <summary>
        /// GLONASS
        /// 最小值是 0，最大值 12，CN 值大于等于 20 的卫星数量
        /// </summary>
        public List<SatelliteStatusInformation> GLONASS { get; set; }
        /// <summary>
        /// Galileo
        /// 最小值是 0，最大值 12，CN 值大于等于 20 的卫星数量
        /// </summary>
        public List<SatelliteStatusInformation> Galileo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0200_0x07 value = new JT808_0x0200_0x07();
            value.AttachInfoId = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoId.ReadNumber()}]附加信息Id", value.AttachInfoId);
            value.AttachInfoLength = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoLength.ReadNumber()}]附加信息长度", value.AttachInfoLength);
            //BeiDou
            byte beidouCount = reader.ReadByte();
            writer.WriteNumber($"[{beidouCount.ReadNumber()}]北斗卫星数量", beidouCount);
            writer.WriteStartArray();
            for (int i = 0; i < beidouCount; i++)
            {
                SatelliteStatusInformation ssi = new SatelliteStatusInformation();
                ssi.No = reader.ReadByte();
                ssi.Elevation = reader.ReadByte();
                ssi.AzimuthAngle = reader.ReadUInt16();
                ssi.Analyze("BeiDou", writer);
            }
            writer.WriteEndArray();
            //GPS
            byte gpsCount = reader.ReadByte();
            writer.WriteNumber($"[{gpsCount.ReadNumber()}]GPS卫星数量", gpsCount);
            writer.WriteStartArray();
            for (int i = 0; i < gpsCount; i++)
            {
                SatelliteStatusInformation ssi = new SatelliteStatusInformation();
                ssi.No = reader.ReadByte();
                ssi.Elevation = reader.ReadByte();
                ssi.AzimuthAngle = reader.ReadUInt16();
                ssi.Analyze("GPS", writer);
            }
            writer.WriteEndArray();
            //GLONASS
            byte glonassCount = reader.ReadByte();
            writer.WriteNumber($"[{glonassCount.ReadNumber()}]GLONASS卫星数量", glonassCount);
            writer.WriteStartArray();
            for (int i = 0; i < glonassCount; i++)
            {
                SatelliteStatusInformation ssi = new SatelliteStatusInformation();
                ssi.No = reader.ReadByte();
                ssi.Elevation = reader.ReadByte();
                ssi.AzimuthAngle = reader.ReadUInt16();
                ssi.Analyze("GLONASS", writer);
            }
            writer.WriteEndArray();
            //Galileo
            byte galileoCount = reader.ReadByte();
            writer.WriteNumber($"[{galileoCount.ReadNumber()}]Galileo卫星数量", galileoCount);
            writer.WriteStartArray();
            for (int i = 0; i < galileoCount; i++)
            {
                SatelliteStatusInformation ssi = new SatelliteStatusInformation();
                ssi.No = reader.ReadByte();
                ssi.Elevation = reader.ReadByte();
                ssi.AzimuthAngle = reader.ReadUInt16();
                ssi.Analyze("Galileo", writer);
            }
            writer.WriteEndArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x0200_0x07 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x07 value = new JT808_0x0200_0x07();
            value.AttachInfoId = reader.ReadByte();
            value.AttachInfoLength = reader.ReadByte();
            //BeiDou
            byte beidouCount = reader.ReadByte();
            for(int i = 0; i < beidouCount; i++)
            {
                SatelliteStatusInformation ssi = new SatelliteStatusInformation();
                ssi.No = reader.ReadByte();
                ssi.Elevation = reader.ReadByte();
                ssi.AzimuthAngle = reader.ReadUInt16();
                value.BeiDou.Add(ssi);
            }
            //GPS
            byte gpsCount = reader.ReadByte();
            for (int i = 0; i < gpsCount; i++)
            {
                SatelliteStatusInformation ssi = new SatelliteStatusInformation();
                ssi.No = reader.ReadByte();
                ssi.Elevation = reader.ReadByte();
                ssi.AzimuthAngle = reader.ReadUInt16();
                value.GPS.Add(ssi);
            }
            //GLONASS
            byte glonassCount = reader.ReadByte();
            for (int i = 0; i < glonassCount; i++)
            {
                SatelliteStatusInformation ssi = new SatelliteStatusInformation();
                ssi.No = reader.ReadByte();
                ssi.Elevation = reader.ReadByte();
                ssi.AzimuthAngle = reader.ReadUInt16();
                value.GLONASS.Add(ssi);
            }
            //Galileo
            byte galileoCount = reader.ReadByte();
            for (int i = 0; i < galileoCount; i++)
            {
                SatelliteStatusInformation ssi = new SatelliteStatusInformation();
                ssi.No = reader.ReadByte();
                ssi.Elevation = reader.ReadByte();
                ssi.AzimuthAngle = reader.ReadUInt16();
                value.Galileo.Add(ssi);
            }
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x07 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            //各个类型的卫星数量为1个字节
            int attachInfoLength = 4;
            if (value.BeiDou != null)
            {
                attachInfoLength +=value.BeiDou.Count * 4;
            }
            if (value.GPS != null)
            {
                attachInfoLength += value.GPS.Count * 4;
            }
            if (value.GLONASS != null)
            {
                attachInfoLength += value.GLONASS.Count * 4;
            }
            if (value.Galileo != null)
            {
                attachInfoLength += value.Galileo.Count * 4;
            }
            writer.WriteByte((byte)attachInfoLength);
            if (value.BeiDou != null)
            {
                writer.WriteByte((byte)value.BeiDou.Count);
                foreach (var item in value.BeiDou)
                {
                    writer.WriteByte(item.No);
                    writer.WriteByte(item.Elevation);
                    writer.WriteUInt16(item.AzimuthAngle);
                }
            }
            else
            {
                writer.WriteByte(0);
            }
            if (value.GPS != null)
            {
                writer.WriteByte((byte)value.GPS.Count);
                foreach (var item in value.GPS)
                {
                    writer.WriteByte(item.No);
                    writer.WriteByte(item.Elevation);
                    writer.WriteUInt16(item.AzimuthAngle);
                }
            }
            else
            {
                writer.WriteByte(0);
            }
            if (value.GLONASS != null)
            {
                writer.WriteByte((byte)value.GLONASS.Count);
                foreach (var item in value.GLONASS)
                {
                    writer.WriteByte(item.No);
                    writer.WriteByte(item.Elevation);
                    writer.WriteUInt16(item.AzimuthAngle);
                }
            }
            else
            {
                writer.WriteByte(0);
            }
            if (value.Galileo != null)
            {
                writer.WriteByte((byte)value.Galileo.Count);
                foreach (var item in value.Galileo)
                {
                    writer.WriteByte(item.No);
                    writer.WriteByte(item.Elevation);
                    writer.WriteUInt16(item.AzimuthAngle);
                }
            }
            else
            {
                writer.WriteByte(0);
            }
        }

        /// <summary>
        /// 卫星状态信息
        /// </summary>
        public class SatelliteStatusInformation
        {
            /// <summary>
            /// 卫星编号
            /// 1-200
            /// </summary>
            public byte No { get; set; }
            /// <summary>
            /// 仰角
            /// 0-90
            /// </summary>
            public byte Elevation { get; set; }
            /// <summary>
            /// 方位角
            /// </summary>
            public ushort AzimuthAngle { get; set; }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="type"></param>
            /// <param name="writer"></param>
            public void Analyze(string type,Utf8JsonWriter writer)
            {
                writer.WriteStartObject();
                writer.WriteNumber($"[{type}-{No}]卫星编号", No);
                writer.WriteNumber($"[{type}-{Elevation}]仰角", Elevation);
                writer.WriteNumber($"[{type}-{AzimuthAngle}]方位角", AzimuthAngle);
                writer.WriteEndObject();
            }
        }
    }
}
