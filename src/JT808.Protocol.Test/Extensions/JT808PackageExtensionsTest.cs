using JT808.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using JT808.Protocol.Extensions;
using System.Reflection;
using System.Linq;
using JT808.Protocol.Formatters;

namespace JT808.Protocol.Test.Extensions
{
    public class JT808PackageExtensionsTest
    {
        [Fact]
        public void CreatePackage()
        {
            var package = JT808MsgId._0x0002.Create_终端心跳("123456789", new Protocol.MessageBody.JT808_0x0002
            {

            });
        }


        [Fact]
        public void Get()
        {
            var a = Assembly.GetAssembly(typeof(JT808Package)).GetTypes().Where(w => w.FullName.EndsWith("Formatter")).ToList();
        }
    }
}
