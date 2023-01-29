using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System.Text.Json;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 音视频通道列表设置
    /// 0x8103_0x0076_AVChannelRefTable
    /// </summary>
    public class JT808_0x8103_0x0076_AVChannelRefTable: JT808MessagePackFormatter<JT808_0x8103_0x0076_AVChannelRefTable>, IJT808Analyze
    {
        /// <summary>
        /// 物理通道号
        /// </summary>
        public byte PhysicalChannelNo { get; set; }
        /// <summary>
        /// 逻辑通道号
        /// </summary>
        public byte LogicChannelNo { get; set; }
        /// <summary>
        /// 通道类型
        /// </summary>
        public byte ChannelType { get; set; }
        /// <summary>
        /// 是否链接云台
        /// </summary>
        public byte IsConnectCloudPlat { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8103_0x0076_AVChannelRefTable value = new JT808_0x8103_0x0076_AVChannelRefTable();
            value.PhysicalChannelNo = reader.ReadByte();
            writer.WriteNumber($"[{value.PhysicalChannelNo.ReadNumber()}]物理通道号", value.PhysicalChannelNo);
            value.LogicChannelNo = reader.ReadByte();
            writer.WriteString($"[{value.LogicChannelNo.ReadNumber()}]逻辑通道号", LogicalChannelNoDisplay(value.LogicChannelNo));
            value.ChannelType = reader.ReadByte();
            writer.WriteString($"[{value.ChannelType.ReadNumber()}]通道类型", ChannelTypeDisplay(value.ChannelType));
            value.IsConnectCloudPlat = reader.ReadByte();
            writer.WriteString($"[{value.IsConnectCloudPlat.ReadNumber()}]是否链接云台", IsConnectCloudPlatDisplay(value.IsConnectCloudPlat));
            string LogicalChannelNoDisplay(byte LogicalChannelNo)
            {
                switch (LogicalChannelNo)
                {
                    case 1:
                        return "驾驶员";
                    case 2:
                        return "车辆正前方";
                    case 3:
                        return "车前门";
                    case 4:
                        return "车厢前部";
                    case 5:
                        return "车厢后部";
                    case 7:
                        return "行李舱";
                    case 8:
                        return "车辆左侧";
                    case 9:
                        return "车辆右侧";
                    case 10:
                        return "车辆正后方";
                    case 11:
                        return "车厢中部";
                    case 12:
                        return "车中门";
                    case 13:
                        return "驾驶席车门";
                    case 33:
                        return "驾驶员";
                    case 36:
                        return "车厢前部";
                    case 37:
                        return "车厢后部";
                    default:
                        return "预留";
                }
            }
            string ChannelTypeDisplay(byte ChannelType) {
                switch (ChannelType)
                {
                    case 0:
                        return "音视频";
                    case 1:
                        return "音频";
                    case 2:
                        return "视频";
                    default:
                        return "未知";
                }
            }
            string IsConnectCloudPlatDisplay(byte IsConnectCloudPlat) {
                switch (IsConnectCloudPlat)
                {
                    case 0:
                        return "未连接";
                    case 1:
                        return "连接";
                    default:
                        return "未知";
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8103_0x0076_AVChannelRefTable Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x0076_AVChannelRefTable jT808_0X8103_0X0076_AVChannelRefTable = new JT808_0x8103_0x0076_AVChannelRefTable();
            jT808_0X8103_0X0076_AVChannelRefTable.PhysicalChannelNo = reader.ReadByte();
            jT808_0X8103_0X0076_AVChannelRefTable.LogicChannelNo = reader.ReadByte();
            jT808_0X8103_0X0076_AVChannelRefTable.ChannelType = reader.ReadByte();
            jT808_0X8103_0X0076_AVChannelRefTable.IsConnectCloudPlat = reader.ReadByte();
            return jT808_0X8103_0X0076_AVChannelRefTable;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x0076_AVChannelRefTable value, IJT808Config config)
        {
            writer.WriteByte(value.PhysicalChannelNo);
            writer.WriteByte(value.LogicChannelNo);
            writer.WriteByte(value.ChannelType);
            writer.WriteByte(value.IsConnectCloudPlat);
        }
    }
}
