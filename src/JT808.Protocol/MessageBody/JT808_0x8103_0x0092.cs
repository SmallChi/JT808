using System.Text.Json;

using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// GNSS 模块详细定位数据输出频率，定义如下：
    /// 0x00：500ms；0x01：1000ms（默认值）；
    /// 0x02：2000ms；0x03：3000ms；
    /// 0x04：4000ms。
    /// </summary>
    public class JT808_0x8103_0x0092 : JT808MessagePackFormatter<JT808_0x8103_0x0092>, JT808_0x8103_BodyBase,  IJT808Analyze
    {
        /// <summary>
        /// 0x0092
        /// </summary>
        public  uint ParamId { get; set; } = 0x0092;
        /// <summary>
        /// 数据长度
        /// 1 byte
        /// </summary>
        public  byte ParamLength { get; set; } = 1;
        /// <summary>
        /// GNSS 模块详细定位数据输出频率，定义如下：
        /// 0x00：500ms；0x01：1000ms（默认值）；
        /// 0x02：2000ms；0x03：3000ms；
        /// 0x04：4000ms。
        /// </summary>
        public byte ParamValue { get; set; }
        /// <summary>
        /// GNSS模块详细定位数据输出频率
        /// </summary>
        public  string Description => "GNSS模块详细定位数据输出频率";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8103_0x0092 jT808_0x8103_0x0092 = new JT808_0x8103_0x0092();
            jT808_0x8103_0x0092.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x0092.ParamLength = reader.ReadByte();
            jT808_0x8103_0x0092.ParamValue = reader.ReadByte();
            writer.WriteNumber($"[{ jT808_0x8103_0x0092.ParamId.ReadNumber()}]参数ID", jT808_0x8103_0x0092.ParamId);
            writer.WriteNumber($"[{jT808_0x8103_0x0092.ParamLength.ReadNumber()}]参数长度", jT808_0x8103_0x0092.ParamLength);
            writer.WriteNumber($"[{ jT808_0x8103_0x0092.ParamValue.ReadNumber()}]GNSS模块详细定位数据输出频率ms", jT808_0x8103_0x0092.ParamValue==0?500: jT808_0x8103_0x0092.ParamValue*1000);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8103_0x0092 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x0092 jT808_0x8103_0x0092 = new JT808_0x8103_0x0092();
            jT808_0x8103_0x0092.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x0092.ParamLength = reader.ReadByte();
            jT808_0x8103_0x0092.ParamValue = reader.ReadByte();
            return jT808_0x8103_0x0092;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x0092 value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteByte(value.ParamValue);
        }
    }
}
