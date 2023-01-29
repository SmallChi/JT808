
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 数据上行透传
    /// </summary>
    public class JT808_0x0900 : JT808MessagePackFormatter<JT808_0x0900>, JT808Bodies, IJT808Analyze, IJT808_2019_Version
    {
        /// <summary>
        /// 0x0900
        /// </summary>
        public ushort MsgId => 0x0900;
        /// <summary>
        /// 数据上行透传
        /// </summary>
        public string Description => "数据上行透传";
        /// <summary>
        /// 透传消息类型
        /// </summary>
        public byte PassthroughType { get; set; }

        /// <summary>
        /// 透传数据
        /// </summary>
        public byte[] PassthroughData { get; set; }

        /// <summary>
        /// 透传消息内容
        /// </summary>
        public JT808_0x0900_BodyBase JT808_0x0900_BodyBase { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0900 value = new JT808_0x0900();
            value.PassthroughType = reader.ReadByte();
            writer.WriteNumber($"[{value.PassthroughType.ReadNumber()}]透传消息类型", value.PassthroughType);
            if (config.JT808_0x0900_Custom_Factory.Map.TryGetValue(value.PassthroughType, out var instance))
            {
                writer.WriteStartObject("数据上行对象");
                try
                {
                    instance.Analyze(ref reader, writer, config);
                }
                catch (System.Exception ex)
                {
                    writer.WriteString("错误信息", $"{ex.Message}-{ex.StackTrace}");
                }
                writer.WriteEndObject();
            }
            else
            {
                value.PassthroughData = reader.ReadContent().ToArray();
                writer.WriteString("透传消息内容", value.PassthroughData.ToHexString());
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x0900 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0900 value = new JT808_0x0900();
            value.PassthroughType = reader.ReadByte();
            if(config.JT808_0x0900_Custom_Factory.Map.TryGetValue(value.PassthroughType,out var instance))
            {
                value.JT808_0x0900_BodyBase = instance.DeserializeExt<JT808_0x0900_BodyBase>(ref reader, config);
            }
            else
            {
                value.PassthroughData = reader.ReadContent().ToArray();
            }
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0900 value, IJT808Config config)
        {
            writer.WriteByte(value.PassthroughType);

            if ( value.JT808_0x0900_BodyBase != null )
            {
                IJT808MessagePackFormatter formatter = config.GetMessagePackFormatterByType( value.JT808_0x0900_BodyBase.GetType() );
                formatter.Serialize(ref writer, value.JT808_0x0900_BodyBase, config );
            }
            else
            {
                writer.WriteArray( value.PassthroughData );
            }
        }
    }
}
