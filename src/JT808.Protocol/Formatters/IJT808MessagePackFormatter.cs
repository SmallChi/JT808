using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters
{
    /// <summary>
    /// 序列化器接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IJT808MessagePackFormatter<T>
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
    public interface IJT808MessagePackFormatter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        void Serialize(ref JT808MessagePackWriter writer, object value, IJT808Config config);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        object Deserialize(ref JT808MessagePackReader reader, IJT808Config config);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class JT808MessagePackFormatter<T> : IJT808MessagePackFormatter<T>, IJT808MessagePackFormatter where T : class, new()
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public abstract T Deserialize(ref JT808MessagePackReader reader, IJT808Config config);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public abstract void Serialize(ref JT808MessagePackWriter writer, T value, IJT808Config config);

        object IJT808MessagePackFormatter.Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            return Deserialize(ref reader, config);
        }

        void IJT808MessagePackFormatter.Serialize(ref JT808MessagePackWriter writer, object value, IJT808Config config)
        {
            var v = (value == null)? default(T) : (T)value;
            Serialize(ref writer, v, config);
        }
    }
}
