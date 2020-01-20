using System.Text.Json;
using JT808.Protocol.Attributes;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 报警拍摄存储标志，与位置信息汇报消息中的报警标志相对应，相应位为 1 则对相应报警时拍的照片进行存储，否则实时上传
    /// </summary>
    public class JT808_0x8103_0x0053 : JT808_0x8103_BodyBase, IJT808MessagePackFormatter<JT808_0x8103_0x0053>, IJT808Analyze
    {
        public override uint ParamId { get; set; } = 0x0053;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; } = 4;
        /// <summary>
        /// 报警拍摄存储标志，与位置信息汇报消息中的报警标志相对应，相应位为 1 则对相应报警时拍的照片进行存储，否则实时上传
        /// </summary>
        public uint ParamValue { get; set; }

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8103_0x0053 jT808_0x8103_0x0053 = new JT808_0x8103_0x0053();
            jT808_0x8103_0x0053.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x0053.ParamLength = reader.ReadByte();
            jT808_0x8103_0x0053.ParamValue = reader.ReadUInt32();
            writer.WriteNumber($"[{ jT808_0x8103_0x0053.ParamId.ReadNumber()}]参数ID", jT808_0x8103_0x0053.ParamId);
            writer.WriteNumber($"[{jT808_0x8103_0x0053.ParamLength.ReadNumber()}]参数长度", jT808_0x8103_0x0053.ParamLength);
            writer.WriteNumber($"[{ jT808_0x8103_0x0053.ParamValue.ReadNumber()}]参数值[报警拍摄存储标志]", jT808_0x8103_0x0053.ParamValue);
        }

        public JT808_0x8103_0x0053 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x0053 jT808_0x8103_0x0053 = new JT808_0x8103_0x0053();
            jT808_0x8103_0x0053.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x0053.ParamLength = reader.ReadByte();
            jT808_0x8103_0x0053.ParamValue = reader.ReadUInt32();
            return jT808_0x8103_0x0053;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x0053 value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteUInt32(value.ParamValue);
        }
    }
}
