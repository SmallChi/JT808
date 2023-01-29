using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 信息点播/取消
    /// 0x0303
    /// 2019版本已作删除
    /// </summary>
    public class JT808_0x0303 : JT808MessagePackFormatter<JT808_0x0303>, JT808Bodies,  IJT808_2019_Version, IJT808Analyze
    {
        /// <summary>
        /// 0x0303
        /// </summary>
        public ushort MsgId  => 0x0303;
        /// <summary>
        /// 信息点播_取消
        /// </summary>
        public string Description => "信息点播_取消";
        /// <summary>
        /// 信息类型
        /// </summary>
        public byte InformationType { get; set; }
        /// <summary>
        /// 点播/取消标志
        /// </summary>
        public byte Flag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0303 value = new JT808_0x0303();
            value.InformationType = reader.ReadByte();
            value.Flag = reader.ReadByte();
            writer.WriteNumber($"[{value.InformationType.ReadNumber()}]信息类型", value.InformationType);
            writer.WriteNumber($"[{value.Flag.ReadNumber()}]{(value.Flag==1? "点播" : "取消")}", value.Flag);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x0303 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0303 value = new JT808_0x0303();
            value.InformationType = reader.ReadByte();
            value.Flag = reader.ReadByte();
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0303 value, IJT808Config config)
        {
            writer.WriteByte(value.InformationType);
            writer.WriteByte(value.Flag);
        }
    }
}
