using System.Text.Json;
using JT808.Protocol.Attributes;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 道路运输证 IC 卡认证备份服务器 IP 地址或域名，端口同主服务器
    /// </summary>
    public class JT808_0x8103_0x001D : JT808_0x8103_BodyBase, IJT808MessagePackFormatter<JT808_0x8103_0x001D>, IJT808Analyze
    {
        public override uint ParamId { get; set; } = 0x001D;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; }
        /// <summary>
        /// 道路运输证 IC 卡认证备份服务器 IP 地址或域名，端口同主服务器
        /// </summary>
        public string ParamValue { get; set; }

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8103_0x001D jT808_0x8103_0x001D = new JT808_0x8103_0x001D();
            jT808_0x8103_0x001D.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x001D.ParamLength = reader.ReadByte();
            var paramValue = reader.ReadVirtualArray(jT808_0x8103_0x001D.ParamLength);
            jT808_0x8103_0x001D.ParamValue = reader.ReadString(jT808_0x8103_0x001D.ParamLength);
            writer.WriteNumber($"[{ jT808_0x8103_0x001D.ParamId.ReadNumber()}]参数ID", jT808_0x8103_0x001D.ParamId);
            writer.WriteNumber($"[{jT808_0x8103_0x001D.ParamLength.ReadNumber()}]参数长度", jT808_0x8103_0x001D.ParamLength);
            writer.WriteString($"[{paramValue.ToArray().ToHexString()}]参数值[道路运输证IC卡认证备份服务器IP]", jT808_0x8103_0x001D.ParamValue);
        }

        public JT808_0x8103_0x001D Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x001D jT808_0x8103_0x001D = new JT808_0x8103_0x001D();
            jT808_0x8103_0x001D.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x001D.ParamLength = reader.ReadByte();
            jT808_0x8103_0x001D.ParamValue = reader.ReadString(jT808_0x8103_0x001D.ParamLength);
            return jT808_0x8103_0x001D;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x001D value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.Skip(1, out int skipPosition);
            writer.WriteString(value.ParamValue);
            int length = writer.GetCurrentPosition() - skipPosition - 1;
            writer.WriteByteReturn((byte)length, skipPosition);
        }
    }
}
