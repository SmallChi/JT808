using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessagePack;
using System.Text.Json;

namespace JT808.Protocol.Extensions.YueBiao.MessageBody
{
    /// <summary>
    /// 智能视频协议版本信息
    /// </summary>
    public class JT808_0x8103_0xF370 : JT808MessagePackFormatter<JT808_0x8103_0xF370>, JT808_0x8103_BodyBase, IJT808Analyze, IJT808_2019_Version
    {
        /// <summary>
        /// 系统参数Id
        /// </summary>
        public uint ParamId { get; set; } = JT808_YueBiao_Constants.JT808_0X8103_0xF370;
        /// <summary>
        /// 参数长度
        /// </summary>
        public byte ParamLength { get; set; } = 1;
        /// <summary>
        /// 智能视频协议版本信息
        /// 引入此智能视频协议版本信息方便平台进行版本控制初始版本是 
        /// 1，每次修订版本号都会递增* 
        /// 注：只支持获取，不支持设置
        /// </summary>
        public byte SmartVideoProtocolVersion { get; set; }
        /// <summary>
        /// 智能视频协议版本信息
        /// </summary>
        public string Description => "智能视频协议版本信息";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8103_0xF370 value = new JT808_0x8103_0xF370();
            value.ParamId = reader.ReadUInt32();
            value.ParamLength = reader.ReadByte();
            writer.WriteNumber($"[{ value.ParamId.ReadNumber()}]参数ID", value.ParamId);
            writer.WriteNumber($"[{value.ParamLength.ReadNumber()}]参数长度", value.ParamLength);
            value.SmartVideoProtocolVersion = reader.ReadByte();
            writer.WriteNumber($"[{value.SmartVideoProtocolVersion.ReadNumber()}]智能视频协议版本信息", value.SmartVideoProtocolVersion);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8103_0xF370 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0xF370 value = new JT808_0x8103_0xF370();
            value.ParamId = reader.ReadUInt32();
            value.ParamLength = reader.ReadByte();
            value.SmartVideoProtocolVersion = reader.ReadByte();
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0xF370 value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteByte(value.SmartVideoProtocolVersion);
        }
    }
}
