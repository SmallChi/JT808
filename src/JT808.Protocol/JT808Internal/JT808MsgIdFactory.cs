using JT808.Protocol.Attributes;
using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.JT808Internal
{
    internal static class JT808MsgIdFactory
    {
        private static readonly Dictionary<ushort, Type> map;

        static JT808MsgIdFactory()
        {
            map = new Dictionary<ushort, Type>();
            InitMap();
        }

        internal static Type GetBodiesImplTypeByMsgId(ushort msgId) => map.TryGetValue(msgId, out Type type) ? type : null;


        private static void InitMap()
        {
            foreach (var item in Enum.GetNames(typeof(JT808MsgId)))
            {
                JT808MsgId msgId = item.ToEnum<JT808MsgId>();
                JT808BodiesTypeAttribute jT808BodiesTypeAttribute = msgId.GetAttribute<JT808BodiesTypeAttribute>();
                map.Add((ushort)msgId, jT808BodiesTypeAttribute?.JT808BodiesType);
            }
        }

        internal static void SetMap<TJT808Bodies>(ushort msgId) where TJT808Bodies : JT808Bodies
        {
            if (!map.ContainsKey(msgId))
                map.Add(msgId, typeof(TJT808Bodies));
        }

        internal static void ReplaceMap<TJT808Bodies>(ushort msgId) where TJT808Bodies : JT808Bodies
        {
            if (!map.ContainsKey(msgId))
                map.Add(msgId, typeof(TJT808Bodies));
            else
                map[msgId] = typeof(TJT808Bodies);
        }
    }
}
