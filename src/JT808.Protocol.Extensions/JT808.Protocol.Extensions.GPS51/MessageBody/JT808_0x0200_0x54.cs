using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json;
using JT808.Protocol.Extensions.GPS51.Metadata;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Extensions.GPS51.MessageBody
{
    /// <summary>
    /// 1+7*N
    /// Wifi数据：第1个字节wifi个数，后面为n个wifi数据；WIFI数据：6字节 wifiMac 1字节 信号强度
    /// </summary>
    public class JT808_0x0200_0x54 : JT808MessagePackFormatter<JT808_0x0200_0x54>, JT808_0x0200_CustomBodyBase, IJT808Analyze
    {
        /// <summary>
        /// 
        /// </summary>
        public byte AttachInfoId { get; set; } = JT808_GPS51_Constants.JT808_0x0200_0x54;
        /// <summary>
        /// 
        /// </summary>
        public byte AttachInfoLength { get; set; }
        /// <summary>
        /// wifi个数
        /// </summary>
        public byte Count { get; set; }
        /// <summary>
        /// wifi信息
        /// </summary>
        public List<WifiInfo> WifiInfos { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0200_0x54 value = new JT808_0x0200_0x54();
            value.AttachInfoId = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoId.ReadNumber()}]附加信息Id", value.AttachInfoId);
            value.AttachInfoLength = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoLength.ReadNumber()}]附加信息长度", value.AttachInfoLength);
            value.Count = reader.ReadByte();
            for (int i = 0; i < value.Count; i++)
            {

                // writer.WriteString($"[{value.Direction.ReadNumber()}]正反转", "未知");

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x0200_0x54 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x54 value = new JT808_0x0200_0x54();
            value.AttachInfoId = reader.ReadByte();
            value.AttachInfoLength = reader.ReadByte();
            value.Count = reader.ReadByte();
            for (int i = 0; i < value.Count; i++)
            {

                // 

            }
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x54 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(1);
            writer.WriteByte((byte)value.WifiInfos.Count);
            foreach (var wifi in value.WifiInfos)
            {

            }
        }
    }

    public class WifiInfo {

        /// <summary>
        /// 信号轻度
        /// </summary>
        public byte SingnalStrength { get; set; }
    }
}
