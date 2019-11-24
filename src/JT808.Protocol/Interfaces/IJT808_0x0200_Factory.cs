using JT808.Protocol.MessageBody;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Interfaces
{
   public interface IJT808_0x0200_Factory: IJT808ExternalRegister
    {
        IDictionary<byte, object> Map { get; set; }

        IJT808_0x0200_Factory SetMap<TJT808_0x0200_Body>() where TJT808_0x0200_Body : JT808_0x0200_BodyBase;
    }
}
