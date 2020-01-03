using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using JT808.Protocol.Extensions;

namespace JT808.Protocol.Test.Extensions
{
    public class JT808HexExtensionsTest
    {
        [Fact]
        public void Test1()
        {
            ushort a1 = 512;
            string hex1 = a1.ReadNumber();
            Assert.Equal("0200", hex1);

            byte a2 = 126;
            string hex2 = a2.ReadNumber();
            Assert.Equal("7E", hex2);

            ushort a3 = 126;
            string hex3 = a3.ReadNumber();
            Assert.Equal("007E", hex3);

            short a4 = 126;
            string hex4 = a4.ReadNumber();
            Assert.Equal("007E", hex4);

            int a5 = 126;
            string hex5 = a5.ReadNumber();
            Assert.Equal("0000007E", hex5);

            uint a6 = 126;
            string hex6 = a6.ReadNumber();
            Assert.Equal("0000007E", hex6);


            long a7 = 126;
            string hex7 = a7.ReadNumber();
            Assert.Equal("000000000000007E", hex7);

            ulong a8 = 126;
            string hex8 = a8.ReadNumber();
            Assert.Equal("000000000000007E", hex8);

            ulong a9 = 191204091818;
            string hex9 = a9.ReadNumber("D12");
            Assert.Equal("191204091818", hex9);
        }
    }
}
