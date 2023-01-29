using System.Text.Json;

using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 最长停车时间，单位为秒（s）
    /// </summary>
    public class JT808_0x8103_0x005A : JT808MessagePackFormatter<JT808_0x8103_0x005A>, JT808_0x8103_BodyBase,  IJT808Analyze
    {
        /// <summary>
        /// 0x005A
        /// </summary>
        public  uint ParamId { get; set; } = 0x005A;
        /// <summary>
        /// 数据长度
        /// 4 byte
        /// </summary>
        public  byte ParamLength { get; set; } = 4;
        /// <summary>
        /// 最长停车时间，单位为秒（s）
        /// </summary>
        public uint ParamValue { get; set; }
        /// <summary>
        /// 最长停车时间
        /// </summary>
        public  string Description => "最长停车时间";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8103_0x005A jT808_0x8103_0x005A = new JT808_0x8103_0x005A();
            jT808_0x8103_0x005A.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x005A.ParamLength = reader.ReadByte();
            jT808_0x8103_0x005A.ParamValue = reader.ReadUInt32();
            writer.WriteNumber($"[{ jT808_0x8103_0x005A.ParamId.ReadNumber()}]参数ID", jT808_0x8103_0x005A.ParamId);
            writer.WriteNumber($"[{jT808_0x8103_0x005A.ParamLength.ReadNumber()}]参数长度", jT808_0x8103_0x005A.ParamLength);
            writer.WriteNumber($"[{ jT808_0x8103_0x005A.ParamValue.ReadNumber()}]参数值[最长停车时间s]", jT808_0x8103_0x005A.ParamValue);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8103_0x005A Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x005A jT808_0x8103_0x005A = new JT808_0x8103_0x005A();
            jT808_0x8103_0x005A.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x005A.ParamLength = reader.ReadByte();
            jT808_0x8103_0x005A.ParamValue = reader.ReadUInt32();
            return jT808_0x8103_0x005A;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x005A value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteUInt32(value.ParamValue);
        }
    }
}
