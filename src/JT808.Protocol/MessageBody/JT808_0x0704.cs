using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;
using System.Collections.Generic;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 定位数据批量上传
    /// </summary>
    [JT808Formatter(typeof(JT808_0x0704_Formatter))]
    public class JT808_0x0704 : JT808Bodies
    {
        /// <summary>
        /// 数据项个数
        /// </summary>
        public ushort Count { get; set; }

        /// <summary>
        /// 位置数据类型
        /// </summary>
        public BatchLocationType LocationType { get; set; }

        /// <summary>
        /// 位置汇报数据集合
        /// </summary>
        public IList<JT808_0x0200> Positions { get; set; }

        /// <summary>
        /// 位置数据类型
        /// </summary>
        public enum BatchLocationType : byte
        {
            正常位置批量汇报 = 0x00,
            盲区补报 = 0x01
        }
    }
}
