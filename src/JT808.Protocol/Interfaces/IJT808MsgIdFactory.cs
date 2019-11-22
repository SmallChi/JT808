using System;

namespace JT808.Protocol.Interfaces
{
    public interface IJT808MsgIdFactory
    {
        object GetBodiesImplInstanceByMsgId(ushort msgId, string terminalPhoneNo);
        IJT808MsgIdFactory SetMap<TJT808Bodies>(ushort msgId, string terminalPhoneNo) 
            where TJT808Bodies : JT808Bodies;
        IJT808MsgIdFactory SetMap(ushort msgId, string terminalPhoneNo, Type bodiesImplType);
        IJT808MsgIdFactory ReplaceMap<TJT808Bodies>(ushort msgId, string terminalPhoneNo)
            where TJT808Bodies : JT808Bodies;
        IJT808MsgIdFactory CustomSetMap<TJT808Bodies>(ushort msgId, string terminalPhoneNo)
            where TJT808Bodies : JT808Bodies;
        IJT808MsgIdFactory CustomSetMap(ushort msgId, string terminalPhoneNo, Type bodiesImplType);
    }
}
