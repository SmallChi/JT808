using System.Text.Json;

using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// GNSS 模块详细定位数据上传方式
    /// 0x00，本地存储，不上传（默认值）；
    /// 0x01，按时间间隔上传；
    /// 0x02，按距离间隔上传；
    /// 0x0B，按累计时间上传，达到传输时间后自动停止上传；
    /// 0x0C，按累计距离上传，达到距离后自动停止上传；
    /// 0x0D，按累计条数上传，达到上传条数后自动停止上传。
    /// </summary>
    public class JT808_0x8103_0x0094 : JT808MessagePackFormatter<JT808_0x8103_0x0094>, JT808_0x8103_BodyBase,  IJT808Analyze
    {
        /// <summary>
        /// 0x0094
        /// </summary>
        public uint ParamId { get; set; } = 0x0094;
        /// <summary>
        /// 数据长度
        /// 1 byte
        /// </summary>
        public byte ParamLength { get; set; } = 1;
        /// <summary>
        /// GNSS 模块详细定位数据上传方式
        /// 0x00，本地存储，不上传（默认值）；
        /// 0x01，按时间间隔上传；
        /// 0x02，按距离间隔上传；
        /// 0x0B，按累计时间上传，达到传输时间后自动停止上传；
        /// 0x0C，按累计距离上传，达到距离后自动停止上传；
        /// 0x0D，按累计条数上传，达到上传条数后自动停止上传。
        /// </summary>
        public byte ParamValue { get; set; }
        /// <summary>
        /// GNSS 模块详细定位数据上传方式
        /// </summary>
        public string Description => "GNSS模块详细定位数据上传方式";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8103_0x0094 jT808_0x8103_0x0094 = new JT808_0x8103_0x0094();
            jT808_0x8103_0x0094.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x0094.ParamLength = reader.ReadByte();
            jT808_0x8103_0x0094.ParamValue = reader.ReadByte();
            writer.WriteNumber($"[{ jT808_0x8103_0x0094.ParamId.ReadNumber()}]参数ID", jT808_0x8103_0x0094.ParamId);
            writer.WriteNumber($"[{jT808_0x8103_0x0094.ParamLength.ReadNumber()}]参数长度", jT808_0x8103_0x0094.ParamLength);
            writer.WriteString($"[{ jT808_0x8103_0x0094.ParamValue.ReadNumber()}]参数值[GNSS模块详细定位数据上传方式]", GetUploadType( jT808_0x8103_0x0094.ParamValue));
            string GetUploadType(byte num) {
                switch (num)
                {
                    case 0x00:
                        return "本地存储，不上传（默认值）";
                    case 0x01:
                        return "按时间间隔上传";
                    case 0x02:
                        return "按距离间隔上传";
                    case 0x0B:
                        return "按累计时间上传，达到传输时间后自动停止上传";
                    case 0x0C:
                        return "按累计距离上传，达到距离后自动停止上传";
                    case 0x0D:
                        return "按累计条数上传，达到上传条数后自动停止上传";
                    default:
                        return "未识别";
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8103_0x0094 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x0094 jT808_0x8103_0x0094 = new JT808_0x8103_0x0094();
            jT808_0x8103_0x0094.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x0094.ParamLength = reader.ReadByte();
            jT808_0x8103_0x0094.ParamValue = reader.ReadByte();
            return jT808_0x8103_0x0094;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x0094 value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteByte(value.ParamValue);
        }
    }
}
