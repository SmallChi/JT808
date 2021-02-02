using JT808.Protocol.Enums;
using JT808.Protocol.Exceptions;
using JT808.Protocol.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JT808.Protocol.Test.Extensions
{
    public class JT808ValidationExtensionsTest
    {
        [Fact]
        public void Test1()
        {
            string str = "SmallChi";
            str.ValiString(nameof(str), 8);
        }

        [Fact]
        public void Test2()
        {
            string str = "SmallChi";
            var ex=Assert.Throws<JT808Exception>(() => 
            {
                str.ValiString(nameof(str),4);
            });
            Assert.Equal(JT808ErrorCode.VailLength, ex.ErrorCode);
            Assert.Equal("str:8>fixed[4]", ex.Message);
        }

        [Fact]
        public void Test3()
        {
            string str = "SmallChi";
            var ex = Assert.Throws<JT808Exception>(() =>
            {
                str.ValiString(nameof(str), 16);
            });
            Assert.Equal(JT808ErrorCode.NotEnoughLength, ex.ErrorCode);
            Assert.Equal("str:8<fixed[16]", ex.Message);
        }

        [Fact]
        public void Test4()
        {
            byte[] arr = new byte[7];
            arr.ValiBytes(nameof(arr), 7);
        }

        [Fact]
        public void Test5()
        {
            byte[] arr = new byte[7];
            var ex = Assert.Throws<JT808Exception>(() =>
            {
                arr.ValiBytes(nameof(arr), 6);
            });
            Assert.Equal(JT808ErrorCode.VailLength, ex.ErrorCode);
            Assert.Equal("arr:7>fixed[6]", ex.Message);
        }

        [Fact]
        public void Test6()
        {
            byte[] arr = new byte[7];
            var ex = Assert.Throws<JT808Exception>(() =>
            {
                arr.ValiBytes(nameof(arr), 8);
            });
            Assert.Equal(JT808ErrorCode.NotEnoughLength, ex.ErrorCode);
            Assert.Equal("arr:7<fixed[8]", ex.Message);
        }
    }
}
