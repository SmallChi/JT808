
using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 信息点播/取消
    /// </summary>
    public class JT808_0x0304 : JT808MessagePackFormatter<JT808_0x0304>, JT808Bodies,  IJT808Analyze
    {
        /// <summary>
        /// 0x0304
        /// </summary>
        public ushort MsgId  => 0x0304;
        /// <summary>
        /// 
        /// </summary>
        public bool SkipSerialization => false;
        /// <summary>
        /// 信息点播/取消
        /// </summary>
        public string Description => "信息点播/取消";
        /// <summary>
        /// 应答流水号
        /// 对应的平台消息的流水号
        /// </summary>
        public ushort ReplyMsgNum { get; set; }
        /// <summary>
        /// 消息类型
        /// 0x4E 英文短信 
        /// 0x4F 中文短信
        /// </summary>
        public byte MessageType { get; set; }
        /// <summary>
        /// 应答信息长度
        /// </summary>
        public ushort MessageLength { get; set; }
        /// <summary>
        /// 消息
        /// 0x4E ASCII
        /// 0x4F GBK
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x0304 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0304 jT808_0x0304 = new JT808_0x0304();
            jT808_0x0304.ReplyMsgNum = reader.ReadUInt16();
            jT808_0x0304.MessageType = reader.ReadByte();
            jT808_0x0304.MessageLength= reader.ReadUInt16();
            if (jT808_0x0304.MessageLength > 0) {
                var messageBytes = reader.ReadArray(jT808_0x0304.MessageLength).ToArray();
                switch (jT808_0x0304.MessageType)
                {
                    case 0x4e:
                        jT808_0x0304.Message = Encoding.ASCII.GetString(messageBytes);
                        break;
                    case 0x4F:
                        jT808_0x0304.Message = config.Encoding.GetString(messageBytes);
                        break;
                }
            }
            return jT808_0x0304;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0304 value, IJT808Config config)
        {
            writer.WriteUInt16(value.ReplyMsgNum);
            writer.WriteByte(value.MessageType);
            var messageBytes= Array.Empty<byte>();
            switch (value.MessageType)
            {
                case 0x4e:
                    messageBytes= Encoding.ASCII.GetBytes(value.Message);
                    break;
                case 0x4F:
                    messageBytes= config.Encoding.GetBytes(value.Message);
                    break;
            }
            writer.WriteUInt16((ushort)messageBytes.Length);
            writer.WriteArray(messageBytes);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0304 jT808_0x0304 = new JT808_0x0304();
            jT808_0x0304.ReplyMsgNum = reader.ReadUInt16();
            writer.WriteNumber($"[{jT808_0x0304.ReplyMsgNum.ReadNumber()}]应答流水号", jT808_0x0304.ReplyMsgNum);
            jT808_0x0304.MessageType = reader.ReadByte();
            switch (jT808_0x0304.MessageType)
            {
                case 0x4e:
                    writer.WriteString($"[{jT808_0x0304.MessageType.ReadNumber()}]消息类型", "ASCII");
                    break;
                case 0x4F:
                    writer.WriteString($"[{jT808_0x0304.MessageType.ReadNumber()}]消息类型", "GBK");
                    break;
            }
            jT808_0x0304.MessageLength = reader.ReadUInt16();
            writer.WriteNumber($"[{jT808_0x0304.MessageLength.ReadNumber()}]消息长度", jT808_0x0304.MessageLength);
            if (jT808_0x0304.MessageLength > 0)
            {
                var messageBytes = reader.ReadArray(jT808_0x0304.MessageLength).ToArray();
                switch (jT808_0x0304.MessageType)
                {
                    case 0x4e:
                        jT808_0x0304.Message = Encoding.ASCII.GetString(messageBytes);
                        break;
                    case 0x4F:
                        jT808_0x0304.Message = config.Encoding.GetString(messageBytes);
                        break;
                }
                writer.WriteString($"[{messageBytes.ToHexString()}]消息", jT808_0x0304.Message);
            }
        }
    }
}
