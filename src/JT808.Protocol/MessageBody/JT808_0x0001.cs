using JT808.Protocol.Enums;
using JT808.Protocol.MessagePack;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using System.Text.Json;
using JT808.Protocol.Extensions;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 终端通用应答
    /// </summary>
    public class JT808_0x0001 : JT808MessagePackFormatter<JT808_0x0001>, JT808Bodies, IJT808Analyze
    {
        /// <summary>
        /// 0x0001
        /// </summary>
        public ushort MsgId => 0x0001;
        /// <summary>
        /// 终端通用应答
        /// </summary>
        public string Description => "终端通用应答";
        /// <summary>
        /// 应答流水号
        /// 对应的平台消息的流水号
        /// </summary>
        public ushort ReplyMsgNum { get; set; }
        /// <summary>
        /// 应答 ID
        /// 对应的平台消息的 ID
        /// <see cref="JT808.Protocol.Enums.JT808MsgId"/>
        /// </summary>
        public ushort ReplyMsgId { get; set; }

        /// <summary>
        /// 结果
        /// 0：成功/确认；1：失败；2：消息有误；3：不支持
        /// </summary>
        public JT808TerminalResult TerminalResult { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x0001 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0001 jT808_0X0001 = new JT808_0x0001();
            jT808_0X0001.ReplyMsgNum = reader.ReadUInt16();
            jT808_0X0001.ReplyMsgId = reader.ReadUInt16();
            jT808_0X0001.TerminalResult = (JT808TerminalResult)reader.ReadByte();
            return jT808_0X0001;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0001 value, IJT808Config config)
        {
            writer.WriteUInt16(value.ReplyMsgNum);
            writer.WriteUInt16(value.ReplyMsgId);
            writer.WriteByte((byte)value.TerminalResult);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            var replyMsgNum = reader.ReadUInt16();
            var replyMsgId = reader.ReadUInt16();
            var terminalResult = reader.ReadByte();
            writer.WriteNumber($"[{replyMsgNum.ReadNumber()}]应答流水号", replyMsgNum);
            writer.WriteNumber($"[{replyMsgId.ReadNumber()}]应答消息Id", replyMsgId);
            writer.WriteString($"[{terminalResult.ReadNumber()}]结果", ((JT808TerminalResult)terminalResult).ToString());
        }
    }
}
