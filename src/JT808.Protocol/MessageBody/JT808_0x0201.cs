using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 位置信息查询应答
    /// </summary>
    public class JT808_0x0201 : JT808MessagePackFormatter<JT808_0x0201>, JT808Bodies, IJT808Analyze
    {
        /// <summary>
        /// 0x0201
        /// </summary>
        public ushort MsgId  => 0x0201;
        /// <summary>
        /// 位置信息查询应答
        /// </summary>
        public string Description => "位置信息查询应答";
        /// <summary>
        /// 应答流水号
        /// 对应的终端注册消息的流水号
        /// </summary>
        public ushort ReplyMsgNum { get; set; }

        /// <summary>
        /// 位置信息汇报见 8.12
        /// </summary>
        public JT808_0x0200 Position { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0201 value = new JT808_0x0201();
            value.ReplyMsgNum = reader.ReadUInt16();
            writer.WriteNumber($"[{value.ReplyMsgNum.ReadNumber()}]", value.ReplyMsgNum);
            writer.WriteStartObject("位置基本信息");
            config.GetAnalyze<JT808_0x0200>().Analyze(ref reader, writer, config);
            writer.WriteEndObject();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x0201 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0201 jT808_0X0201 = new JT808_0x0201();
            jT808_0X0201.ReplyMsgNum = reader.ReadUInt16();
            jT808_0X0201.Position = config.GetMessagePackFormatter<JT808_0x0200>().Deserialize(ref reader, config);
            return jT808_0X0201;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0201 value, IJT808Config config)
        {
            writer.WriteUInt16(value.ReplyMsgNum);
            config.GetMessagePackFormatter<JT808_0x0200>().Serialize(ref writer, value.Position, config);
        }
    }
}
