using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using JT808.Protocol.Extensions.GPS51.Metadata;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Extensions.GPS51.MessageBody
{
    /// <summary>
    /// 正反转
    /// </summary>
    public class JT808_0x0200_0x52 : JT808MessagePackFormatter<JT808_0x0200_0x52>, JT808_0x0200_CustomBodyBase, IJT808Analyze
    {
        /// <summary>
        /// 
        /// </summary>
        public byte AttachInfoId { get; set; } = JT808_GPS51_Constants.JT808_0x0200_0x52;
        /// <summary>
        /// 
        /// </summary>
        public byte AttachInfoLength { get; set; }
        /// <summary>
        /// 正反转值
        ///  0:未知；1：正转（空车）2:反转（重车）；3：停转 例子解析为：03
        /// </summary>
        public byte Direction { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0200_0x52 value = new JT808_0x0200_0x52();
            value.AttachInfoId = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoId.ReadNumber()}]附加信息Id", value.AttachInfoId);
            value.AttachInfoLength = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoLength.ReadNumber()}]附加信息长度", value.AttachInfoLength);
            value.Direction = reader.ReadByte();
            if (value.Direction == 0)
            {
                writer.WriteString($"[{value.Direction.ReadNumber()}]正反转", "未知");
            }
            else if(value.Direction==1)
            {
                writer.WriteString($"[{value.Direction.ReadNumber()}]正反转", "正转(空车)");
            }
            else if (value.Direction == 2)
            {
                writer.WriteString($"[{value.Direction.ReadNumber()}]正反转", "反转(重车)");
            }
            else if (value.Direction == 3)
            {
                writer.WriteString($"[{value.Direction.ReadNumber()}]正反转", "停转");
            }
            else
            {
                writer.WriteString($"[{value.Direction.ReadNumber()}]正反转", "未知2");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x0200_0x52 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x52 value = new JT808_0x0200_0x52();
            value.AttachInfoId = reader.ReadByte();
            value.AttachInfoLength = reader.ReadByte();
            value.Direction = reader.ReadByte();
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x52 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(1);
            writer.WriteByte(value.Direction);
        }
    }
}
