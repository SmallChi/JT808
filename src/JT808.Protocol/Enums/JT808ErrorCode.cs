namespace JT808.Protocol.Enums
{
    public enum JT808ErrorCode
    {
        /// <summary>
        /// 校验和不相等
        /// </summary>
        CheckCodeNotEqual = 1001,
        /// <summary>
        /// 消息头解析错误
        /// </summary>
        HeaderParseError = 1003,
        /// <summary>
        /// 消息体解析错误
        /// </summary>
        BodiesParseError = 1004,
        GetAttributeError = 1005,
        /// <summary>
        /// 没有实现对应的类型
        /// </summary>
        NotImplType = 1006,
        /// <summary>
        /// 长度不够
        /// </summary>
        NotEnoughLength = 1007,
        /// <summary>
        /// 没有全局注册格式化器
        /// <see cref="JT808.Protocol.Formatters.IJT808MessagePackFormatter<T>"/>
        /// </summary>
        NotGlobalRegisterFormatterAssembly = 1008,        
        /// <summary>
        /// 经纬度错误
        /// </summary>
        LatOrLngError = 1009
    }
}
