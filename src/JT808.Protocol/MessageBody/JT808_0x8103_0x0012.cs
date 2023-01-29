using System.Text.Json;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 主服务器无线通信拨号密码
    /// </summary>
    public class JT808_0x8103_0x0012 : JT808MessagePackFormatter<JT808_0x8103_0x0012>, JT808_0x8103_BodyBase,  IJT808Analyze
    {
        /// <summary>
        /// 0x0012
        /// </summary>
        public  uint ParamId { get; set; } = 0x0012;
        /// <summary>
        /// 数据长度
        /// n byte
        /// </summary>
        public  byte ParamLength { get; set; }
        /// <summary>
        /// 主服务器无线通信拨号密码
        /// </summary>
        public string ParamValue { get; set; }
        /// <summary>
        /// 主服务器无线通信拨号密码
        /// </summary>
        public  string Description => "主服务器无线通信拨号密码";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8103_0x0012 jT808_0x8103_0x0012 = new JT808_0x8103_0x0012();
            jT808_0x8103_0x0012.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x0012.ParamLength = reader.ReadByte();
            var paramValue = reader.ReadVirtualArray(jT808_0x8103_0x0012.ParamLength);
            jT808_0x8103_0x0012.ParamValue = reader.ReadString(jT808_0x8103_0x0012.ParamLength);
            writer.WriteNumber($"[{ jT808_0x8103_0x0012.ParamId.ReadNumber()}]参数ID", jT808_0x8103_0x0012.ParamId);
            writer.WriteNumber($"[{jT808_0x8103_0x0012.ParamLength.ReadNumber()}]参数长度", jT808_0x8103_0x0012.ParamLength);
            writer.WriteString($"[{paramValue.ToArray().ToHexString()}]参数值[主服务器无线通信拨号密码]", jT808_0x8103_0x0012.ParamValue);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8103_0x0012 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x0012 jT808_0x8103_0x0012 = new JT808_0x8103_0x0012();
            jT808_0x8103_0x0012.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x0012.ParamLength = reader.ReadByte();
            jT808_0x8103_0x0012.ParamValue = reader.ReadString(jT808_0x8103_0x0012.ParamLength);
            return jT808_0x8103_0x0012;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x0012 value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.Skip(1, out int skipPosition);
            writer.WriteString(value.ParamValue);
            int length = writer.GetCurrentPosition() - skipPosition - 1;
            writer.WriteByteReturn((byte)length, skipPosition);
        }
    }
}
