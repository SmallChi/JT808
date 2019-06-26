using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace JT808.Protocol.Interfaces
{
    public interface IJT808ExternalRegister
    {
        void Register(Assembly externalAssembly);
    }
}
