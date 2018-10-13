using JT808.Protocol.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace JT808.Protocol.Test.Extensions
{
    public class JT808EnumExtensionsTest
    {
        [Fact]
        public void Test1()
        {
            var list0 = JT808EnumExtensions.ParseAlarmStatus(5);
            var list1 = JT808EnumExtensions.ParseAlarmStatus(16);
            var list2 = JT808EnumExtensions.ParseAlarmStatus(18);
            var list3 = JT808EnumExtensions.ParseAlarmStatus(24);
            var list4 = JT808EnumExtensions.ParseAlarmStatus(31);
        }
    }
}
