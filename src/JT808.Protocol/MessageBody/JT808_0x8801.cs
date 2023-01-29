using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 摄像头立即拍摄命令
    /// 0x8801
    /// </summary>
    public class JT808_0x8801 : JT808MessagePackFormatter<JT808_0x8801>, JT808Bodies,IJT808Analyze, IJT808_2019_Version
    {
        /// <summary>
        /// 0x8801
        /// </summary>
        public ushort MsgId => 0x8801;
        /// <summary>
        /// 摄像头立即拍摄命令
        /// </summary>
        public string Description => "摄像头立即拍摄命令";
        /// <summary>
        /// 通道 ID
        /// </summary>
        public byte ChannelId { get; set; }
        /// <summary>
        /// 拍摄命令 
        /// 0 表示停止拍摄；0xFFFF 表示录像；其它表示拍照张数
        /// </summary>
        public ushort ShootingCommand { get; set; }
        /// <summary>
        /// 拍照间隔/录像时间
        /// 秒，0 表示按最小间隔拍照或一直录像
        /// </summary>
        public ushort VideoTime { get; set; }
        /// <summary>
        /// 保存标志 
        /// 1：保存；0：实时上传
        /// 仅主机拍照时有效
        /// </summary>
        public byte SaveFlag { get; set; }
        /// <summary>
        /// 分辨率
        /// <see cref="JT808.Protocol.Enums.JT808CameraResolutionType"/>
        /// </summary>
        public byte Resolution { get; set; }
        /// <summary>
        /// 图像/视频质量
        /// 1-10，1 代表质量损失最小，10 表示压缩比最大
        /// </summary>
        public byte VideoQuality { get; set; }
        /// <summary>
        /// 亮度
        /// 0-255
        /// </summary>
        public byte Lighting { get; set; }
        /// <summary>
        /// 对比度
        /// 0-127
        /// </summary>
        public byte Contrast { get; set; }
        /// <summary>
        /// 饱和度
        /// 0-127
        /// </summary>
        public byte Saturability { get; set; }
        /// <summary>
        /// 色度
        /// 0-255
        /// </summary>
        public byte Chroma { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8801 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8801 jT808_0X8801 = new JT808_0x8801();
            jT808_0X8801.ChannelId = reader.ReadByte();
            jT808_0X8801.ShootingCommand = reader.ReadUInt16();
            jT808_0X8801.VideoTime = reader.ReadUInt16();
            jT808_0X8801.SaveFlag = reader.ReadByte();
            jT808_0X8801.Resolution = reader.ReadByte();
            jT808_0X8801.VideoQuality = reader.ReadByte();
            jT808_0X8801.Lighting = reader.ReadByte();
            jT808_0X8801.Contrast = reader.ReadByte();
            jT808_0X8801.Saturability = reader.ReadByte();
            jT808_0X8801.Chroma = reader.ReadByte();
            return jT808_0X8801;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8801 value, IJT808Config config)
        {
            writer.WriteByte(value.ChannelId);
            writer.WriteUInt16(value.ShootingCommand);
            writer.WriteUInt16(value.VideoTime);
            writer.WriteByte(value.SaveFlag);
            writer.WriteByte(value.Resolution);
            writer.WriteByte(value.VideoQuality);
            writer.WriteByte(value.Lighting);
            writer.WriteByte(value.Contrast);
            writer.WriteByte(value.Saturability);
            writer.WriteByte(value.Chroma);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8801 value = new JT808_0x8801();
            value.ChannelId = reader.ReadByte();
            value.ShootingCommand = reader.ReadUInt16();
            value.VideoTime = reader.ReadUInt16();
            value.SaveFlag = reader.ReadByte();
            value.Resolution = reader.ReadByte();
            value.VideoQuality = reader.ReadByte();
            value.Lighting = reader.ReadByte();
            value.Contrast = reader.ReadByte();
            value.Saturability = reader.ReadByte();
            value.Chroma = reader.ReadByte();
            JT808CameraResolutionType jT808CameraResolutionType = (JT808CameraResolutionType)value.Resolution;
            writer.WriteNumber($"[{ value.ChannelId.ReadNumber()}]通道ID", value.ChannelId);
            writer.WriteNumber($"[{ value.ShootingCommand.ReadNumber()}]拍摄命令", value.ShootingCommand);
            writer.WriteNumber($"[{ value.VideoTime.ReadNumber()}]拍照间隔_录像时间", value.VideoTime);
            writer.WriteString($"[{ value.SaveFlag.ReadNumber()}]保存标志-{value.SaveFlag}", value.SaveFlag==1? "保存" : "实时上传");
            writer.WriteNumber($"[{ value.Resolution.ReadNumber()}]分辨率-{jT808CameraResolutionType.ToString()}", value.Resolution);
            writer.WriteNumber($"[{ value.VideoQuality.ReadNumber()}]图像_视频质量", value.VideoQuality);
            writer.WriteNumber($"[{ value.Lighting.ReadNumber()}]亮度", value.Lighting);
            writer.WriteNumber($"[{ value.Contrast.ReadNumber()}]对比度", value.Contrast);
            writer.WriteNumber($"[{ value.Saturability.ReadNumber()}]饱和度", value.Saturability);
            writer.WriteNumber($"[{ value.Chroma.ReadNumber()}]色度", value.Chroma);
        }
    }
}
