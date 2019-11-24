using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Interfaces
{
    public interface IJT808_0x0200_Custom_Factory: IJT808ExternalRegister
    {
        IDictionary<byte, object> Map { get; }
        IJT808_0x0200_Custom_Factory SetMap<TJT808_0x0200_CustomBody>() where TJT808_0x0200_CustomBody : JT808_0x0200_CustomBodyBase;
    }
}
