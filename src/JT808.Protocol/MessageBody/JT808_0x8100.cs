using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 终端注册应答
    /// </summary>
    public class JT808_0x8100 : JT808MessagePackFormatter<JT808_0x8100>, JT808Bodies, IJT808Analyze
    {
        /// <summary>
        /// 0x8100
        /// </summary>
        public ushort MsgId  => 0x8100;
        /// <summary>
        /// 终端注册应答
        /// </summary>
        public string Description => "终端注册应答";
        /// <summary>
        /// 应答流水号
        /// 对应的终端注册消息的流水号
        /// </summary>
        public ushort AckMsgNum { get; set; }

        /// <summary>
        /// 结果
        /// </summary>
        public JT808TerminalRegisterResult JT808TerminalRegisterResult { get; set; }

        /// <summary>
        /// 鉴权码
        /// 只有在成功后才有该字段
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8100 value = new JT808_0x8100();
            value.AckMsgNum = reader.ReadUInt16();
            writer.WriteNumber($"[{ value.AckMsgNum.ReadNumber()}]应答流水号", value.AckMsgNum);
            value.JT808TerminalRegisterResult = (JT808TerminalRegisterResult)reader.ReadByte();
            writer.WriteNumber($"[{ ((byte)value.JT808TerminalRegisterResult).ReadNumber()}]结果-{value.JT808TerminalRegisterResult.ToString()}", (byte)value.JT808TerminalRegisterResult);
            // 只有在成功后才有该字段
            if (value.JT808TerminalRegisterResult == JT808TerminalRegisterResult.success)
            {
                var codeBuffer = reader.ReadVirtualArray(reader.ReadCurrentRemainContentLength()).ToArray();
                value.Code = reader.ReadRemainStringContent();
                writer.WriteString($"[{codeBuffer.ToHexString()}]鉴权码", value.Code);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8100 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8100 value = new JT808_0x8100();
            value.AckMsgNum = reader.ReadUInt16();
            value.JT808TerminalRegisterResult = (JT808TerminalRegisterResult)reader.ReadByte();
            // 只有在成功后才有该字段
            if (value.JT808TerminalRegisterResult == JT808TerminalRegisterResult.success)
            {
                value.Code = reader.ReadRemainStringContent();
            }
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8100 value, IJT808Config config)
        {
            writer.WriteUInt16(value.AckMsgNum);
            writer.WriteByte((byte)value.JT808TerminalRegisterResult);
            // 只有在成功后才有该字段
            if (value.JT808TerminalRegisterResult == JT808TerminalRegisterResult.success)
            {
                writer.WriteString(value.Code);
            }
        }
    }
}
