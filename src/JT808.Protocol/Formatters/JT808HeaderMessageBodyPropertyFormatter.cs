using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;

namespace JT808.Protocol.Formatters
{
    /// <summary>
    /// 头部消息体属性的格式化器
    /// </summary>
    public class JT808HeaderMessageBodyPropertyFormatter : IJT808MessagePackFormatter<JT808HeaderMessageBodyProperty>
    {
        public static readonly JT808HeaderMessageBodyPropertyFormatter Instance = new JT808HeaderMessageBodyPropertyFormatter();
        public JT808HeaderMessageBodyProperty Deserialize(ref JT808MessagePackReader writer, IJT808Config config)
        {
            JT808HeaderMessageBodyProperty messageBodyProperty = new JT808HeaderMessageBodyProperty();
            messageBodyProperty.Unwrap(writer.ReadUInt16(), config);
            if (messageBodyProperty.IsPackge)
            {
                messageBodyProperty.PackgeCount = writer.ReadUInt16();
                messageBodyProperty.PackageIndex = writer.ReadUInt16();
            }
            return messageBodyProperty;
        }
        public void Serialize(ref JT808MessagePackWriter writer, JT808HeaderMessageBodyProperty value, IJT808Config config)
        {   
            writer.WriteUInt16(value.Wrap(config));
        }
    }
}
