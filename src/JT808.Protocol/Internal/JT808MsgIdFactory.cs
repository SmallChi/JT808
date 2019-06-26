using JT808.Protocol.Attributes;
using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.Interfaces;
using System;
using System.Collections.Generic;

namespace JT808.Protocol.Internal
{
    internal class JT808MsgIdFactory: IJT808MsgIdFactory
    {
        private Dictionary<ushort, Type> map;

        private Dictionary<string, Dictionary<ushort, Type>> customMap;

        internal JT808MsgIdFactory()
        {
            map = new Dictionary<ushort, Type>();
            customMap = new Dictionary<string, Dictionary<ushort, Type>>(StringComparer.OrdinalIgnoreCase);
            InitMap();
        }

        private void InitMap()
        {
            foreach (var item in Enum.GetNames(typeof(JT808MsgId)))
            {
                JT808MsgId msgId = item.ToEnum<JT808MsgId>();
                if (!map.ContainsKey((ushort)msgId))
                {
                    JT808BodiesTypeAttribute jT808BodiesTypeAttribute = msgId.GetAttribute<JT808BodiesTypeAttribute>();
                    if (jT808BodiesTypeAttribute != null)
                    {                
                        map.Add((ushort)msgId, jT808BodiesTypeAttribute.JT808BodiesType);
                    }
                }
            }
        }

        public Type GetBodiesImplTypeByMsgId(ushort msgId, string terminalPhoneNo)
        {
            //判断有无自定义消息Id类型
            if (customMap.TryGetValue(terminalPhoneNo, out var dict))
            {
                if (dict != null)
                {
                    return dict.TryGetValue(msgId, out Type bodiesImptType)? bodiesImptType:null;
                }
            }
            return map.TryGetValue(msgId, out Type type) ? type : null;
        }

        public IJT808MsgIdFactory SetMap<TJT808Bodies>(ushort msgId, string terminalPhoneNo) 
            where TJT808Bodies : JT808Bodies
        {
            if (!map.ContainsKey(msgId))
            {
                map.Add(msgId, typeof(TJT808Bodies));
            }
            return this;
        }

        public IJT808MsgIdFactory SetMap(ushort msgId, string terminalPhoneNo, Type bodiesImplType)
        {
            if (!map.ContainsKey(msgId))
            {
                map.Add(msgId, bodiesImplType);
            }
            return this;
        }

        public IJT808MsgIdFactory ReplaceMap<TJT808Bodies>(ushort msgId, string terminalPhoneNo) where TJT808Bodies : JT808Bodies
        {
            if (!map.ContainsKey(msgId))
            {
                map.Add(msgId, typeof(TJT808Bodies));
            }
            else
            {
                map[msgId] = typeof(TJT808Bodies);
            }
            return this;
        }

        public IJT808MsgIdFactory CustomSetMap<TJT808Bodies>(ushort msgId, string terminalPhoneNo) where TJT808Bodies : JT808Bodies
        {
            if (!string.IsNullOrEmpty(terminalPhoneNo))
            {
                if (!customMap.TryGetValue(terminalPhoneNo, out var dict))
                {
                    if (dict == null)
                    {
                        Dictionary<ushort, Type> tmp = new Dictionary<ushort, Type>();
                        tmp.Add(msgId, typeof(TJT808Bodies));
                        customMap.Add(terminalPhoneNo, tmp);
                    }
                    else
                    {
                        if (!dict.ContainsKey(msgId))
                        {
                            dict.Add(msgId, typeof(TJT808Bodies));
                        }
                    }
                }
            }
            return this;
        }

        public IJT808MsgIdFactory CustomSetMap(ushort msgId, string terminalPhoneNo, Type bodiesImplType)
        {
            if (!string.IsNullOrEmpty(terminalPhoneNo))
            {
                if (!customMap.TryGetValue(terminalPhoneNo, out var dict))
                {
                    if (dict == null)
                    {
                        Dictionary<ushort, Type> tmp = new Dictionary<ushort, Type>();
                        tmp.Add(msgId, bodiesImplType);
                        customMap.Add(terminalPhoneNo, tmp);
                        return this;
                    }
                    else
                    {
                        if (!dict.ContainsKey(msgId))
                        {
                            dict.Add(msgId, bodiesImplType);
                        }
                    }
                }
            }
            return this;
        }
    }
}
