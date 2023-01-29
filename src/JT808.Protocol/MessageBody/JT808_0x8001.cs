using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 平台通用应答
    /// </summary>
    public class JT808_0x8001 : JT808MessagePackFormatter<JT808_0x8001>, JT808Bodies, IJT808Analyze
    {
        /// <summary>
        /// 0x8001
        /// </summary>
        public ushort MsgId  => 0x8001;
        /// <summary>
        /// 平台通用应答
        /// </summary>
        public string Description => "平台通用应答";
        /// <summary>
        /// 应答消息流水号
        /// </summary>
        public ushort MsgNum { get; set; }
        /// <summary>
        /// 应答消息Id
        /// <see cref="JT808.Protocol.Enums.JT808MsgId"/>
        /// </summary>
        public ushort AckMsgId { get; set; }
        /// <summary>
        /// 返回结果
        /// <see cref="JT808.Protocol.Enums.JT808PlatformResult"/>
        /// </summary>
        public JT808PlatformResult JT808PlatformResult { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8001 value = new JT808_0x8001();
            value.MsgNum = reader.ReadUInt16();
            writer.WriteNumber($"[{value.MsgNum.ReadNumber()}]应答消息流水号", value.MsgNum);
            value.AckMsgId = reader.ReadUInt16();
            writer.WriteNumber($"[{value.AckMsgId.ReadNumber()}]应答消息Id", value.AckMsgId);
            value.JT808PlatformResult = (JT808PlatformResult)reader.ReadByte();
            writer.WriteNumber($"[{((byte)value.JT808PlatformResult).ReadNumber()}]结果-{value.JT808PlatformResult.ToString()}", (byte)value.JT808PlatformResult);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8001 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8001 value = new JT808_0x8001();
            value.MsgNum = reader.ReadUInt16();
            value.AckMsgId = reader.ReadUInt16();
            value.JT808PlatformResult = (JT808PlatformResult)reader.ReadByte();
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8001 value, IJT808Config config)
        {
            writer.WriteUInt16(value.MsgNum);
            writer.WriteUInt16(value.AckMsgId);
            writer.WriteByte((byte)value.JT808PlatformResult);
        }
    }
}
