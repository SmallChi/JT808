using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters
{
    /// <summary>
    /// 序列化器接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IJT808MessagePackFormatter<T> : IJT808Formatter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        void Serialize(ref JT808MessagePackWriter writer, T value, IJT808Config config);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        T Deserialize(ref JT808MessagePackReader reader, IJT808Config config);
    }
    /// <summary>
    /// 
    /// </summary>
    public interface IJT808Formatter
    {

    }
}
