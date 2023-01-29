using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessagePack;
using System.Collections.Generic;
using System.Text.Json;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 视频相关报警屏蔽字
    /// 0x8103_0x007A
    /// </summary>
    public class JT808_0x8103_0x007A : JT808MessagePackFormatter<JT808_0x8103_0x007A>, JT808_0x8103_BodyBase, IJT808Analyze
    {
        /// <summary>
        /// 
        /// </summary>
        public uint ParamId { get; set; } = 0x007A;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public byte ParamLength { get; set; } = 4;
        /// <summary>
        /// 视频相关屏蔽报警字
        /// </summary>
        public uint AlarmShielding { get; set; }
        /// <summary>
        /// 视频相关报警屏蔽字
        /// </summary>
        public string Description => "视频相关报警屏蔽字";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8103_0x007A value = new JT808_0x8103_0x007A();
            value.ParamId = reader.ReadUInt32();
            writer.WriteNumber($"[{value.ParamId.ReadNumber()}]参数 ID", value.ParamId);
            value.ParamLength = reader.ReadByte();
            writer.WriteNumber($"[{value.ParamLength.ReadNumber()}]数据长度", value.ParamLength);
            value.AlarmShielding = reader.ReadUInt32();
            writer.WriteString($"[{value.AlarmShielding.ReadNumber()}]视频相关屏蔽报警字", AlarmShieldingDisplay(value.AlarmShielding));
            string AlarmShieldingDisplay(uint AlarmShielding)
            {
                switch (AlarmShielding)
                {
                    case 0x14:
                        return "视频相关报警";
                    case 0x15:
                        return "视频信号丢失报警状态";
                    case 0x16:
                        return "视频信号遮挡报警状态";
                    case 0x17:
                        return "存储器故障报警状态";
                    case 0x18:
                        return "异常驾驶行为详细描述";
                    default:
                        break;
                }
                return "未知";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8103_0x007A Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x007A jT808_0x8103_0x007A = new JT808_0x8103_0x007A();
            jT808_0x8103_0x007A.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x007A.ParamLength = reader.ReadByte();
            jT808_0x8103_0x007A.AlarmShielding = reader.ReadUInt32();
            return jT808_0x8103_0x007A;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x007A value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteUInt32(value.AlarmShielding);
        }
    }
}
