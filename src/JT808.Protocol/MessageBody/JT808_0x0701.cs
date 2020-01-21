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
    public class JT808_0x0701 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x0701>, IJT808Analyze
    {
        public override ushort MsgId { get; } = 0x0701;
        public override string Description => "电子运单上报";
        /// <summary>
        /// 电子运单长度
        /// </summary>
        public uint ElectronicWaybillLength { get; set; }
         
        public byte[] ElectronicContent { get; set; }

        /// <summary>
        /// 电子运单内容
        /// 注意:需要具体的实现
        /// </summary>
        public JT808_0x0701_CustomBodyBase ElectronicContentObj { get; set; }

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0701 value = new JT808_0x0701();
            value.ElectronicWaybillLength = reader.ReadUInt32();
            writer.WriteNumber($"[{value.ElectronicWaybillLength.ReadNumber()}]电子运单长度", value.ElectronicWaybillLength);
            value.ElectronicContent = reader.ReadArray((int)value.ElectronicWaybillLength).ToArray();
            writer.WriteString($"电子运单", value.ElectronicContent.ToHexString());
        }

        public JT808_0x0701 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0701 value = new JT808_0x0701();
            value.ElectronicWaybillLength = reader.ReadUInt32();
            value.ElectronicContent = reader.ReadArray((int)value.ElectronicWaybillLength).ToArray();
            return value;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0701 value, IJT808Config config)
        {
            writer.Skip(4, out int skipPosition);
            object obj = config.GetMessagePackFormatterByType(value.ElectronicContentObj.GetType());
            JT808MessagePackFormatterResolverExtensions.JT808DynamicSerialize(obj, ref writer, value.ElectronicContentObj, config);
            int contentLength = writer.GetCurrentPosition() - skipPosition - 4;
            writer.WriteInt32Return(contentLength, skipPosition);
        }
    }
}
