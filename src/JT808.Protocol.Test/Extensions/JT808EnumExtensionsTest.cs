using JT808.Protocol.Extensions;
using Xunit;

namespace JT808.Protocol.Test.Extensions
{
    public class JT808EnumExtensionsTest
    {
        [Fact]
        public void Test1()
        {
            var list0 = JT808EnumExtensions.GetEnumTypes<JT808.Protocol.Enums.JT808Alarm>(5, 32);
            var list1 = JT808EnumExtensions.GetEnumTypes<JT808.Protocol.Enums.JT808Alarm>(16, 32);
            var list2 = JT808EnumExtensions.GetEnumTypes<JT808.Protocol.Enums.JT808Alarm>(18, 32);
            var list3 = JT808EnumExtensions.GetEnumTypes<JT808.Protocol.Enums.JT808Alarm>(24, 32);
            var list4 = JT808EnumExtensions.GetEnumTypes<JT808.Protocol.Enums.JT808Alarm>(31, 32);
        }
    }
}
