namespace JT808.Protocol.Interfaces
{
    /// <summary>
    /// JT808分布式自增流水号
    /// </summary>
    public interface IJT808MsgSNDistributed
    {
        /// <summary>
        /// 根据终端SIM号自增
        /// </summary>
        /// <param name="terminalPhoneNo"></param>
        /// <returns></returns>
        ushort Increment(string terminalPhoneNo);
    }
}
