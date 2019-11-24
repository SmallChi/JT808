using JT808.Protocol.Internal;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xunit;

namespace JT808.Protocol.Test.Internal
{
    public class JT808_0x0200_Custom_FactoryTest
    {
        [Fact]
        public void Test1()
        {
            Assert.Throws<ArgumentException>(()=> 
            {
                new JT808_0x0200_Custom_Factory().Register(Assembly.GetExecutingAssembly());
            });
        }
    }
}
