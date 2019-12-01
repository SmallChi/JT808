using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 摄像头立即拍摄命令
    /// 0x8801
    /// </summary>
    public class JT808_0x8801 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x8801>, IJT808_2019_Version
    {
        public override ushort MsgId { get; } = 0x8801;
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
        /// </summary>
        public byte SaveFlag { get; set; }
        /// <summary>
        /// 分辨率
        /// 0x01:320*240；
        /// 0x02:640*480；
        /// 0x03:800*600；
        /// 0x04:1024*768;
        /// 0x05:176*144;[Qcif];
        /// 0x06:352*288;[Cif];
        /// 0x07:704*288;[HALF D1];
        /// 0x08:704*576;[D1];
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
        public JT808_0x8801 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
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

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8801 value, IJT808Config config)
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
    }
}
