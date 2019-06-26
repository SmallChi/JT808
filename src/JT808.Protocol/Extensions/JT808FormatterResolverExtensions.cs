using JT808.Protocol.Formatters;
using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Extensions
{
    /// <summary>
    /// 
    /// ref http://adamsitnik.com/Span/#span-must-not-be-a-generic-type-argument
    /// ref http://adamsitnik.com/Span/
    /// ref:MessagePack.Formatters.DynamicObjectTypeFallbackFormatter
    /// </summary>
    [Obsolete("使用JT808MessagePackFormatterResolverExtensions")]
    public static class JT808FormatterResolverExtensions
    {
        delegate int JT808SerializeMethod(object dynamicFormatter, ref byte[] bytes, int offset, object value, IJT808Config config);

        delegate dynamic JT808DeserializeMethod(object dynamicFormatter, ReadOnlySpan<byte> bytes, out int readSize, IJT808Config config);

        static readonly ConcurrentDictionary<Type, (object Value, JT808SerializeMethod SerializeMethod)> jT808Serializers = new ConcurrentDictionary<Type, (object Value, JT808SerializeMethod SerializeMethod)>();

        static readonly ConcurrentDictionary<Type, (object Value, JT808DeserializeMethod DeserializeMethod)> jT808Deserializes = new ConcurrentDictionary<Type, (object Value, JT808DeserializeMethod DeserializeMethod)>();

        public static int JT808DynamicSerialize(object objFormatter, ref byte[] bytes, int offset, dynamic value, IJT808Config config)
        {
            Type type = value.GetType();
            var ti = type.GetTypeInfo();
            (object Value, JT808SerializeMethod SerializeMethod) formatterAndDelegate;
            if (!jT808Serializers.TryGetValue(type, out formatterAndDelegate))
            {
                var t = type;
                {
                    var formatterType = typeof(IJT808Formatter<>).MakeGenericType(t);
                    var param0 = Expression.Parameter(typeof(object), "formatter");
                    var param1 = Expression.Parameter(typeof(byte[]).MakeByRefType(), "bytes");
                    var param2 = Expression.Parameter(typeof(int), "offset");
                    var param3 = Expression.Parameter(typeof(object), "value");
                    var param4 = Expression.Parameter(typeof(IJT808Config),"config");
                    var serializeMethodInfo = formatterType.GetRuntimeMethod("Serialize", new[] { typeof(byte[]).MakeByRefType(), typeof(int), t, typeof(IJT808Config) });
                    var body = Expression.Call(
                        Expression.Convert(param0, formatterType),
                        serializeMethodInfo,
                        param1,
                        param2,
                        ti.IsValueType ? Expression.Unbox(param3, t) : Expression.Convert(param3, t),
                        param4);
                    var lambda = Expression.Lambda<JT808SerializeMethod>(body, param0, param1, param2, param3, param4).Compile();
                    formatterAndDelegate = (objFormatter, lambda);
                }
                jT808Serializers.TryAdd(t, formatterAndDelegate);
            }
            return formatterAndDelegate.SerializeMethod(formatterAndDelegate.Value, ref bytes, offset, value, config);
        }

        public static dynamic JT808DynamicDeserialize(object objFormatter, ReadOnlySpan<byte> bytes, out int readSize, IJT808Config config)
        {
            var type = objFormatter.GetType();
            (object Value, JT808DeserializeMethod DeserializeMethod) formatterAndDelegate;
            if (!jT808Deserializes.TryGetValue(type, out formatterAndDelegate))
            {
                var t = type;
                {
                    var formatterType = typeof(IJT808Formatter<>).MakeGenericType(t);
                    ParameterExpression param0 = Expression.Parameter(typeof(object), "formatter");
                    ParameterExpression param1 = Expression.Parameter(typeof(ReadOnlySpan<byte>), "bytes");
                    ParameterExpression param2 = Expression.Parameter(typeof(int).MakeByRefType(), "readSize");
                    ParameterExpression param3 = Expression.Parameter(typeof(IJT808Config), "config");
                    var deserializeMethodInfo = type.GetRuntimeMethod("Deserialize", new[] { typeof(ReadOnlySpan<byte>), typeof(int).MakeByRefType() , typeof(IJT808Config) });
                    var body = Expression.Call(
                        Expression.Convert(param0, type),
                        deserializeMethodInfo,
                        param1,
                        param2,
                        param3
                        );
                    var lambda = Expression.Lambda<JT808DeserializeMethod>(body, param0, param1, param2, param3).Compile();
                    formatterAndDelegate = (objFormatter, lambda);
                }
                jT808Deserializes.TryAdd(t, formatterAndDelegate);
            }
            return formatterAndDelegate.DeserializeMethod(formatterAndDelegate.Value, bytes, out readSize, config);
        }
    }

    /// <summary>
    /// 
    /// ref http://adamsitnik.com/Span/#span-must-not-be-a-generic-type-argument
    /// ref http://adamsitnik.com/Span/
    /// ref:MessagePack.Formatters.DynamicObjectTypeFallbackFormatter
    /// </summary>
    public static class JT808MessagePackFormatterResolverExtensions
    {
        delegate void JT808SerializeMethod(object dynamicFormatter, ref JT808MessagePackWriter writer,object value, IJT808Config config);

        delegate dynamic JT808DeserializeMethod(object dynamicFormatter, ref JT808MessagePackReader reader, IJT808Config config);

        static readonly ConcurrentDictionary<Type, (object Value, JT808SerializeMethod SerializeMethod)> jT808Serializers = new ConcurrentDictionary<Type, (object Value, JT808SerializeMethod SerializeMethod)>();
        
        static readonly ConcurrentDictionary<Type, (object Value, JT808DeserializeMethod DeserializeMethod)> jT808Deserializes = new ConcurrentDictionary<Type, (object Value, JT808DeserializeMethod DeserializeMethod)>();
        public static void JT808DynamicSerialize(object objFormatter, ref JT808MessagePackWriter writer, object value, IJT808Config config)
        {
            Type type = value.GetType();
            var ti = type.GetTypeInfo();
            (object Value, JT808SerializeMethod SerializeMethod) formatterAndDelegate;
            if (!jT808Serializers.TryGetValue(type, out formatterAndDelegate))
            {
                var t = type;
                {
                    var formatterType = typeof(IJT808MessagePackFormatter<>).MakeGenericType(t);
                    var param0 = Expression.Parameter(typeof(object), "formatter");
                    var param1 = Expression.Parameter(typeof(JT808MessagePackWriter).MakeByRefType(), "writer");
                    var param2 = Expression.Parameter(typeof(object), "value");
                    var param3 = Expression.Parameter(typeof(IJT808Config), "config");
                    var serializeMethodInfo = formatterType.GetRuntimeMethod("Serialize", new[] { typeof(JT808MessagePackWriter).MakeByRefType(), t, typeof(IJT808Config) });
                    var body = Expression.Call(
                        Expression.Convert(param0, formatterType),
                        serializeMethodInfo,
                        param1,
                        ti.IsValueType ? Expression.Unbox(param2, t) : Expression.Convert(param2, t),
                        param3);
                    var lambda = Expression.Lambda<JT808SerializeMethod>(body, param0, param1, param2, param3).Compile();
                    formatterAndDelegate = (objFormatter, lambda);
                }
                jT808Serializers.TryAdd(t, formatterAndDelegate);
            }
            formatterAndDelegate.SerializeMethod(formatterAndDelegate.Value, ref writer, value, config);
        }
        public static dynamic JT808DynamicDeserialize(object objFormatter, ref JT808MessagePackReader reader, IJT808Config config)
        {
            var type = objFormatter.GetType();
            (object Value, JT808DeserializeMethod DeserializeMethod) formatterAndDelegate;
            if (!jT808Deserializes.TryGetValue(type, out formatterAndDelegate))
            {
                var t = type;
                {
                    var formatterType = typeof(IJT808MessagePackFormatter<>).MakeGenericType(t);
                    ParameterExpression param0 = Expression.Parameter(typeof(object), "formatter");
                    ParameterExpression param1 = Expression.Parameter(typeof(JT808MessagePackReader).MakeByRefType(), "reader");
                    ParameterExpression param2 = Expression.Parameter(typeof(IJT808Config), "config");
                    var deserializeMethodInfo = type.GetRuntimeMethod("Deserialize", new[] { typeof(JT808MessagePackReader).MakeByRefType(), typeof(IJT808Config) });
                    var body = Expression.Call(
                        Expression.Convert(param0, type),
                        deserializeMethodInfo,
                        param1,
                        param2
                        );
                    var lambda = Expression.Lambda<JT808DeserializeMethod>(body, param0, param1, param2).Compile();
                    formatterAndDelegate = (objFormatter, lambda);
                }
                jT808Deserializes.TryAdd(t, formatterAndDelegate);
            }
            return formatterAndDelegate.DeserializeMethod(formatterAndDelegate.Value,ref reader, config);
        }
    }
}
