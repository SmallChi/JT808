using System.Text.Json;

using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// GNSS 定位模式，定义如下：
    /// bit0，0：禁用 GPS 定位， 1：启用 GPS 定位；
    /// bit1，0：禁用北斗定位， 1：启用北斗定位；
    /// bit2，0：禁用 GLONASS 定位， 1：启用 GLONASS 定位；
    /// bit3，0：禁用 Galileo 定位， 1：启用 Galileo 定位。
    /// </summary>
    public class JT808_0x8103_0x0090 : JT808MessagePackFormatter<JT808_0x8103_0x0090>, JT808_0x8103_BodyBase, IJT808Analyze
    {
        /// <summary>
        /// 0x0090
        /// </summary>
        public  uint ParamId { get; set; } = 0x0090;
        /// <summary>
        /// 数据长度
        /// 1 byte
        /// </summary>
        public  byte ParamLength { get; set; } = 1;
        /// <summary>
        /// GNSS 定位模式，定义如下：
        /// bit0，0：禁用 GPS 定位， 1：启用 GPS 定位；
        /// bit1，0：禁用北斗定位， 1：启用北斗定位；
        /// bit2，0：禁用 GLONASS 定位， 1：启用 GLONASS 定位；
        /// bit3，0：禁用 Galileo 定位， 1：启用 Galileo 定位。
        /// </summary>
        public byte ParamValue { get; set; }
        /// <summary>
        /// GNSS 定位模式
        /// </summary>
        public  string Description => "GNSS定位模式";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8103_0x0090 jT808_0x8103_0x0090 = new JT808_0x8103_0x0090();
            jT808_0x8103_0x0090.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x0090.ParamLength = reader.ReadByte();
            jT808_0x8103_0x0090.ParamValue = reader.ReadByte();
            writer.WriteNumber($"[{ jT808_0x8103_0x0090.ParamId.ReadNumber()}]参数ID", jT808_0x8103_0x0090.ParamId);
            writer.WriteNumber($"[{jT808_0x8103_0x0090.ParamLength.ReadNumber()}]参数长度", jT808_0x8103_0x0090.ParamLength);
            writer.WriteStartArray($"[{ jT808_0x8103_0x0090.ParamValue.ReadNumber()}]参数值[GNSS定位模式]");
            writer.WriteStringValue((jT808_0x8103_0x0090.ParamValue & 01) > 0 ? "启用GPS定位" : "禁用GPS定位");
            writer.WriteStringValue((jT808_0x8103_0x0090.ParamValue & 02) > 0 ? "启用北斗定位" : "禁用北斗定位");
            writer.WriteStringValue((jT808_0x8103_0x0090.ParamValue & 04) > 0 ? "启用GLONASS定位" : "禁用GLONASS定位");
            writer.WriteStringValue((jT808_0x8103_0x0090.ParamValue & 08) > 0 ? "启用Galileo定位" : "禁用Galileo定位");
            writer.WriteEndArray();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8103_0x0090 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x0090 jT808_0x8103_0x0090 = new JT808_0x8103_0x0090();
            jT808_0x8103_0x0090.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x0090.ParamLength = reader.ReadByte();
            jT808_0x8103_0x0090.ParamValue = reader.ReadByte();
            return jT808_0x8103_0x0090;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x0090 value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteByte(value.ParamValue);
        }
    }
}
