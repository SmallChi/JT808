using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System.Collections.Generic;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 摄像头立即拍摄命令应答
    /// 0x0805
    /// </summary>
    public class JT808_0x0805 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x0805>, IJT808_2019_Version
    {
        public override ushort MsgId { get; } = 0x0805;
        /// <summary>
        /// 应答流水号
        /// 对应平台摄像头立即拍摄命令的消息流水号
        /// </summary>
        public ushort ReplyMsgNum { get; set; }
        /// <summary>
        /// 结果
        /// 0：成功；1：失败；2：通道不支持。以下字段在结果=0 时才有效。
        /// </summary>
        public byte Result { get; set; }
        /// <summary>
        /// 多媒体ID个数
        /// 拍摄成功的多媒体个数
        /// </summary>
        public ushort MultimediaIdCount { get; set; }
        /// <summary>
        /// 多媒体ID列表
        /// </summary>
        public List<uint> MultimediaIds { get; set; }

        public JT808_0x0805 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0805 jT808_0X0805 = new JT808_0x0805();
            jT808_0X0805.ReplyMsgNum = reader.ReadUInt16();
            jT808_0X0805.Result = reader.ReadByte();
            if (jT808_0X0805.Result == 0)
            {
                jT808_0X0805.MultimediaIdCount = reader.ReadUInt16();
                jT808_0X0805.MultimediaIds = new List<uint>();
                for (var i = 0; i < jT808_0X0805.MultimediaIdCount; i++)
                {
                    uint id = reader.ReadUInt32();
                    jT808_0X0805.MultimediaIds.Add(id);
                }
            }
            return jT808_0X0805;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0805 value, IJT808Config config)
        {
            writer.WriteUInt16(value.ReplyMsgNum);
            writer.WriteByte(value.Result);
            if (value.Result == 0)
            {
                writer.WriteUInt16((ushort)value.MultimediaIds.Count);
                foreach (var item in value.MultimediaIds)
                {
                    writer.WriteUInt32(item);
                }
            }
        }
    }
}
