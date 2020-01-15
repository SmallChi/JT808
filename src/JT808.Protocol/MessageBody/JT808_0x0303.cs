using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 信息点播/取消
    /// 0x0303
    /// </summary>
    [Obsolete("2019版本已作删除")]
    public class JT808_0x0303 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x0303>, IJT808_2019_Version, IJT808Analyze
    {
        public override ushort MsgId { get; } = 0x0303;
        public override string Description => "信息点播_取消";
        /// <summary>
        /// 信息类型
        /// </summary>
        public byte InformationType { get; set; }
        /// <summary>
        /// 点播/取消标志
        /// </summary>
        public byte Flag { get; set; }

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0303 jT808_0X0303 = new JT808_0x0303();
            jT808_0X0303.InformationType = reader.ReadByte();
            jT808_0X0303.Flag = reader.ReadByte();
            writer.WriteNumber($"[{jT808_0X0303.InformationType.ReadNumber()}]信息类型", jT808_0X0303.InformationType);
            writer.WriteNumber($"[{jT808_0X0303.Flag.ReadNumber()}]{(jT808_0X0303.Flag==1? "点播" : "取消")}", jT808_0X0303.Flag);
        }

        public JT808_0x0303 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0303 jT808_0X0303 = new JT808_0x0303();
            jT808_0X0303.InformationType = reader.ReadByte();
            jT808_0X0303.Flag = reader.ReadByte();
            return jT808_0X0303;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0303 value, IJT808Config config)
        {
            writer.WriteByte(value.InformationType);
            writer.WriteByte(value.Flag);
        }
    }
}
