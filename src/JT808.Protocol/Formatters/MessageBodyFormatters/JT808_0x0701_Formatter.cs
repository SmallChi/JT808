using JT808.Protocol.Extensions;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x0701_Formatter : IJT808MessagePackFormatter<JT808_0x0701>
    {
        public JT808_0x0701 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0701 jT808_0X0701 = new JT808_0x0701();
            jT808_0X0701.ElectronicWaybillLength = reader.ReadUInt32();
            jT808_0X0701.ElectronicContent = reader.ReadArray((int)jT808_0X0701.ElectronicWaybillLength).ToArray();
            return jT808_0X0701;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0701 value, IJT808Config config)
        {
            writer.Skip(4, out int skipPosition);
            object obj = config.GetMessagePackFormatterByType(value.ElectronicContentObj.GetType());
            JT808MessagePackFormatterResolverExtensions.JT808DynamicSerialize(obj, ref writer, value.ElectronicContentObj, config);
            int contentLength=writer.GetCurrentPosition()- skipPosition-4;
            writer.WriteInt32Return(contentLength, skipPosition);
        }
    }
}
