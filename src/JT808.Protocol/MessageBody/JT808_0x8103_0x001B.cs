using System.Text.Json;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 道路运输证 IC 卡认证主服务器 TCP 端口
    /// </summary>
    public class JT808_0x8103_0x001B : JT808MessagePackFormatter<JT808_0x8103_0x001B>, JT808_0x8103_BodyBase,  IJT808Analyze
    {
        /// <summary>
        /// 0x001B
        /// </summary>
        public uint ParamId { get; set; } = 0x001B;
        /// <summary>
        /// 数据长度
        /// 4 byte
        /// </summary>
        public byte ParamLength { get; set; } = 4;
        /// <summary>
        ///道路运输证 IC 卡认证主服务器 TCP 端口
        /// </summary>
        public uint ParamValue { get; set; }
        /// <summary>
        /// 道路运输证IC卡认证主服务器TCP端口
        /// </summary>
        public string Description => "道路运输证IC卡认证主服务器TCP端口";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8103_0x001B jT808_0x8103_0x001B = new JT808_0x8103_0x001B();
            jT808_0x8103_0x001B.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x001B.ParamLength = reader.ReadByte();
            jT808_0x8103_0x001B.ParamValue = reader.ReadUInt32();
            writer.WriteNumber($"[{ jT808_0x8103_0x001B.ParamId.ReadNumber()}]参数ID", jT808_0x8103_0x001B.ParamId);
            writer.WriteNumber($"[{jT808_0x8103_0x001B.ParamLength.ReadNumber()}]参数长度", jT808_0x8103_0x001B.ParamLength);
            writer.WriteNumber($"[{ jT808_0x8103_0x001B.ParamValue.ReadNumber()}]参数值[道路运输证IC卡认证主服务器TCP端口]", jT808_0x8103_0x001B.ParamValue);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8103_0x001B Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x001B jT808_0x8103_0x001B = new JT808_0x8103_0x001B();
            jT808_0x8103_0x001B.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x001B.ParamLength = reader.ReadByte();
            jT808_0x8103_0x001B.ParamValue = reader.ReadUInt32();
            return jT808_0x8103_0x001B;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x001B value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteUInt32(value.ParamValue);
        }
    }
}
