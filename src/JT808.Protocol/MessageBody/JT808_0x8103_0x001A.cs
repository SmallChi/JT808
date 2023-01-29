using System.Text.Json;

using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 道路运输证 IC 卡认证主服务器 IP 地址或域名
    /// </summary>
    public class JT808_0x8103_0x001A : JT808MessagePackFormatter<JT808_0x8103_0x001A>, JT808_0x8103_BodyBase, IJT808Analyze
    {
        /// <summary>
        /// 0x001A
        /// </summary>
        public  uint ParamId { get; set; } = 0x001A;
        /// <summary>
        /// 数据长度
        /// </summary>
        public  byte ParamLength { get; set; }
        /// <summary>
        /// 道路运输证 IC 卡认证主服务器 IP 地址或域名
        /// </summary>
        public string ParamValue { get; set; }
        /// <summary>
        /// 道路运输证IC卡认证主服务器IP地址或域名
        /// </summary>
        public  string Description => "道路运输证IC卡认证主服务器IP地址或域名";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8103_0x001A jT808_0x8103_0x001A = new JT808_0x8103_0x001A();
            jT808_0x8103_0x001A.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x001A.ParamLength = reader.ReadByte();
            var paramValue = reader.ReadVirtualArray(jT808_0x8103_0x001A.ParamLength);
            jT808_0x8103_0x001A.ParamValue = reader.ReadString(jT808_0x8103_0x001A.ParamLength);
            writer.WriteNumber($"[{ jT808_0x8103_0x001A.ParamId.ReadNumber()}]参数ID", jT808_0x8103_0x001A.ParamId);
            writer.WriteNumber($"[{jT808_0x8103_0x001A.ParamLength.ReadNumber()}]参数长度", jT808_0x8103_0x001A.ParamLength);
            writer.WriteString($"[{paramValue.ToArray().ToHexString()}]参数值[道路运输证IC卡认证主服务器 IP]", jT808_0x8103_0x001A.ParamValue);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8103_0x001A Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x001A jT808_0x8103_0x001A = new JT808_0x8103_0x001A();
            jT808_0x8103_0x001A.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x001A.ParamLength = reader.ReadByte();
            jT808_0x8103_0x001A.ParamValue = reader.ReadString(jT808_0x8103_0x001A.ParamLength);
            return jT808_0x8103_0x001A;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x001A value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.Skip(1, out int skipPosition);
            writer.WriteString(value.ParamValue);
            int length = writer.GetCurrentPosition() - skipPosition - 1;
            writer.WriteByteReturn((byte)length, skipPosition);
        }
    }
}
