using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 电子运单上报
    /// 0x0701
    /// </summary>
    public class JT808_0x0701 : JT808MessagePackFormatter<JT808_0x0701>, JT808Bodies,  IJT808Analyze
    {
        /// <summary>
        /// 0x0701
        /// </summary>
        public ushort MsgId  => 0x0701;
        /// <summary>
        /// 电子运单上报
        /// </summary>
        public string Description => "电子运单上报";
        /// <summary>
        /// 电子运单长度
        /// </summary>
        public uint ElectronicWaybillLength { get; set; }
         /// <summary>
         /// 电子运单内容
         /// </summary>
        public byte[] ElectronicContent { get; set; }
        /// <summary>
        /// 电子运单内容
        /// 注意:需要具体的实现
        /// </summary>
        public JT808_0x0701_CustomBodyBase ElectronicContentObj { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0701 value = new JT808_0x0701();
            value.ElectronicWaybillLength = reader.ReadUInt32();
            writer.WriteNumber($"[{value.ElectronicWaybillLength.ReadNumber()}]电子运单长度", value.ElectronicWaybillLength);
            value.ElectronicContent = reader.ReadArray((int)value.ElectronicWaybillLength).ToArray();
            writer.WriteString($"电子运单", value.ElectronicContent.ToHexString());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x0701 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0701 value = new JT808_0x0701();
            value.ElectronicWaybillLength = reader.ReadUInt32();
            value.ElectronicContent = reader.ReadArray((int)value.ElectronicWaybillLength).ToArray();
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0701 value, IJT808Config config)
        {
            writer.Skip(4, out int skipPosition);
            IJT808MessagePackFormatter formatter = config.GetMessagePackFormatterByType(value.ElectronicContentObj.GetType());
            formatter.Serialize(ref writer, value.ElectronicContentObj, config);
            int contentLength = writer.GetCurrentPosition() - skipPosition - 4;
            writer.WriteInt32Return(contentLength, skipPosition);
        }
    }
}
