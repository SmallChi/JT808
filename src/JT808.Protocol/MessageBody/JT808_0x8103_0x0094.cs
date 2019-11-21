using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// GNSS 模块详细定位数据上传方式
    /// 0x00，本地存储，不上传（默认值）；
    /// 0x01，按时间间隔上传；
    /// 0x02，按距离间隔上传；
    /// 0x0B，按累计时间上传，达到传输时间后自动停止上传；
    /// 0x0C，按累计距离上传，达到距离后自动停止上传；
    /// 0x0D，按累计条数上传，达到上传条数后自动停止上传。
    /// </summary>
    public class JT808_0x8103_0x0094 : JT808_0x8103_BodyBase, IJT808MessagePackFormatter<JT808_0x8103_0x0094>
    {
        public override uint ParamId { get; set; } = 0x0094;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; }
        /// <summary>
        /// GNSS 模块详细定位数据上传方式
        /// 0x00，本地存储，不上传（默认值）；
        /// 0x01，按时间间隔上传；
        /// 0x02，按距离间隔上传；
        /// 0x0B，按累计时间上传，达到传输时间后自动停止上传；
        /// 0x0C，按累计距离上传，达到距离后自动停止上传；
        /// 0x0D，按累计条数上传，达到上传条数后自动停止上传。
        /// </summary>
        public byte ParamValue { get; set; }
        public JT808_0x8103_0x0094 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x0094 jT808_0x8103_0x0094 = new JT808_0x8103_0x0094();
            jT808_0x8103_0x0094.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x0094.ParamLength = reader.ReadByte();
            jT808_0x8103_0x0094.ParamValue = reader.ReadByte();
            return jT808_0x8103_0x0094;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x0094 value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteByte(value.ParamValue);
        }
    }
}
