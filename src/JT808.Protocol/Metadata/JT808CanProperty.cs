using JT808.Protocol.Interfaces;

namespace JT808.Protocol.Metadata
{
    /// <summary>
    /// Can属性
    /// </summary>
    public struct JT808CanProperty: IJT808_2019_Version
    {
        /// <summary>
        /// CAN ID
        /// 4
        /// </summary>
        public uint CanId { get; set; }
        /// <summary>
        /// CAN 数据
        /// 8
        /// </summary>
        public byte[] CanData { get; set; }
    }
}
