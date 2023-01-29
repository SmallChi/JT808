using System.Text.Json;

using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 每次最长通话时间，单位为秒（s），0 为不允许通话，0xFFFFFFFF 为不限制
    /// </summary>
    public class JT808_0x8103_0x0046 : JT808MessagePackFormatter<JT808_0x8103_0x0046>, JT808_0x8103_BodyBase,  IJT808Analyze
    {
        /// <summary>
        /// 0x0046
        /// </summary>
        public uint ParamId { get; set; } = 0x0046;
        /// <summary>
        /// 数据长度
        /// 4 byte
        /// </summary>
        public byte ParamLength { get; set; } = 4;
        /// <summary>
        /// 每次最长通话时间，单位为秒（s），0 为不允许通话，0xFFFFFFFF 为不限制
        /// </summary>
        public uint ParamValue { get; set; }
        /// <summary>
        /// 每次最长通话时间
        /// </summary>
        public string Description => "每次最长通话时间";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8103_0x0046 jT808_0x8103_0x0046 = new JT808_0x8103_0x0046();
            jT808_0x8103_0x0046.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x0046.ParamLength = reader.ReadByte();
            jT808_0x8103_0x0046.ParamValue = reader.ReadUInt32();
            writer.WriteNumber($"[{ jT808_0x8103_0x0046.ParamId.ReadNumber()}]参数ID", jT808_0x8103_0x0046.ParamId);
            writer.WriteNumber($"[{jT808_0x8103_0x0046.ParamLength.ReadNumber()}]参数长度", jT808_0x8103_0x0046.ParamLength);
            writer.WriteNumber($"[{ jT808_0x8103_0x0046.ParamValue.ReadNumber()}]参数值[每次最长通话时间s]", jT808_0x8103_0x0046.ParamValue);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8103_0x0046 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x0046 jT808_0x8103_0x0046 = new JT808_0x8103_0x0046();
            jT808_0x8103_0x0046.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x0046.ParamLength = reader.ReadByte();
            jT808_0x8103_0x0046.ParamValue = reader.ReadUInt32();
            return jT808_0x8103_0x0046;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x0046 value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteUInt32(value.ParamValue);
        }
    }
}
