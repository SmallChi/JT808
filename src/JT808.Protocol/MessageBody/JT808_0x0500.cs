using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 车辆控制应答
    /// </summary>
    public class JT808_0x0500 : JT808MessagePackFormatter<JT808_0x0500>, JT808Bodies, IJT808Analyze
    {
        /// <summary>
        /// 0x0500
        /// </summary>
        public ushort MsgId  => 0x0500;
        /// <summary>
        /// 车辆控制应答
        /// </summary>
        public string Description => "车辆控制应答";
        /// <summary>
        /// 应答流水号
        /// 对应的终端注册消息的流水号
        /// </summary>
        public ushort MsgNum { get; set; }
        /// <summary>
        /// 位置信息汇报消息体
        /// </summary>
        public JT808_0x0200 JT808_0x0200 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0500 value = new JT808_0x0500();
            value.MsgNum = reader.ReadUInt16();
            writer.WriteNumber($"[{value.MsgNum.ReadNumber()}]应答流水号", value.MsgNum);
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
        public override JT808_0x0500 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0500 value = new JT808_0x0500();
            value.MsgNum = reader.ReadUInt16();
            value.JT808_0x0200 = config.GetMessagePackFormatter<JT808_0x0200>().Deserialize(ref reader, config);
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0500 value, IJT808Config config)
        {
            writer.WriteUInt16(value.MsgNum);
            config.GetMessagePackFormatter<JT808_0x0200>().Serialize(ref writer, value.JT808_0x0200, config);
        }
    }
}
