using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Interfaces
{
   public interface IJT808_0x0200_Factory
   {
        IDictionary<byte, Type> JT808LocationAttachMethod { get;  set; }
   }
}
