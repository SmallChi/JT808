using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Interfaces
{
    public interface IJT808_0x8900_Custom_Factory : IJT808ExternalRegister
    {
        IDictionary<byte, object> Map { get; }
        IJT808_0x8900_Custom_Factory SetMap<TJT808_0x8900_BodyBase>() where TJT808_0x8900_BodyBase : JT808_0x8900_BodyBase;
    }
}
