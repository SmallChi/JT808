using System.Text.Json;
using JT808.Protocol.Attributes;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// GNSS 模块详细定位数据上传设置：
    /// 上传方式为 0x01 时，单位为秒；
    /// 上传方式为 0x02 时，单位为米；
    /// 上传方式为 0x0B 时，单位为秒；
    /// 上传方式为 0x0C 时，单位为米；
    /// 上传方式为 0x0D 时，单位为条。
    /// </summary>
    public class JT808_0x8103_0x0095 : JT808_0x8103_BodyBase, IJT808MessagePackFormatter<JT808_0x8103_0x0095>, IJT808Analyze
    {
        public override uint ParamId { get; set; } = 0x0095;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; }
        /// <summary>
        /// GNSS 模块详细定位数据上传设置：
        /// 上传方式为 0x01 时，单位为秒；
        /// 上传方式为 0x02 时，单位为米；
        /// 上传方式为 0x0B 时，单位为秒；
        /// 上传方式为 0x0C 时，单位为米；
        /// 上传方式为 0x0D 时，单位为条。
        /// </summary>
        public uint ParamValue { get; set; }

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8103_0x0095 jT808_0x8103_0x0095 = new JT808_0x8103_0x0095();
            jT808_0x8103_0x0095.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x0095.ParamLength = reader.ReadByte();
            jT808_0x8103_0x0095.ParamValue = reader.ReadUInt32();
            writer.WriteNumber($"[{ jT808_0x8103_0x0095.ParamId.ReadNumber()}]参数ID", jT808_0x8103_0x0095.ParamId);
            writer.WriteNumber($"[{jT808_0x8103_0x0095.ParamLength.ReadNumber()}]参数长度", jT808_0x8103_0x0095.ParamLength);
            writer.WriteNumber($"[{ jT808_0x8103_0x0095.ParamValue.ReadNumber()}]参数值[GNSS模块详细定位数据上传设置]", jT808_0x8103_0x0095.ParamValue);
        }

        public JT808_0x8103_0x0095 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x0095 jT808_0x8103_0x0095 = new JT808_0x8103_0x0095();
            jT808_0x8103_0x0095.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x0095.ParamLength = reader.ReadByte();
            jT808_0x8103_0x0095.ParamValue = reader.ReadUInt32();
            return jT808_0x8103_0x0095;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x0095 value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteUInt32(value.ParamValue);
        }
    }
}
