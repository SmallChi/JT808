namespace JT808.Protocol.Enums
{
    public enum JT808TerminalRegisterResult : byte
    {
        成功 = 0x00,
        车辆已被注册 = 0x01,
        数据库中无该车辆 = 0x02,
        终端已被注册 = 0x03,
        数据库中无该终端 = 0x04
    }
}
