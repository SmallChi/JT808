using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 电话回拨
    /// </summary>
    public class JT808_0x8400 : JT808MessagePackFormatter<JT808_0x8400>, JT808Bodies, IJT808Analyze
    {
        /// <summary>
        /// 0x8400
        /// </summary>
        public ushort MsgId =>0x8400;
        /// <summary>
        /// 电话回拨
        /// </summary>
        public string Description => "电话回拨";
        /// <summary>
        /// 0:普通通话；1:监听
        /// </summary>
        public JT808CallBackType CallBack { get; set; }
        /// <summary>
        /// 电话号码 
        /// 最长为 20 字节
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8400 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8400 jT808_0X8400 = new JT808_0x8400();
            jT808_0X8400.CallBack = (JT808CallBackType)reader.ReadByte();
            // 最长为 20 字节
            jT808_0X8400.PhoneNumber = reader.ReadRemainStringContent();
            return jT808_0X8400;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8400 value, IJT808Config config)
        {
            writer.WriteByte((byte)value.CallBack);
            writer.WriteString(value.PhoneNumber.ValiMaxString(nameof(value.PhoneNumber),20));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8400 value = new JT808_0x8400();
            value.CallBack = (JT808CallBackType)reader.ReadByte();
            writer.WriteNumber($"[{((byte)value.CallBack).ReadNumber()}]{value.CallBack.ToString()}", (byte)value.CallBack);
            // 最长为 20 字节
            var pnoBuffer = reader.ReadVirtualArray(reader.ReadCurrentRemainContentLength()).ToArray();
            value.PhoneNumber = reader.ReadRemainStringContent();
            writer.WriteString($"[{pnoBuffer.ToHexString()}]电话号码", value.PhoneNumber);
        }
    }
}
