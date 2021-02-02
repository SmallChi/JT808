using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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
            var list5= JT808EnumExtensions.GetEnumTypes<JT808.Protocol.Enums.JT808Alarm>(2147483679, 33); 
            Assert.Equal(list0, new List<JT808Alarm>() { JT808Alarm.紧急报警_触动报警开关后触发,JT808Alarm.疲劳驾驶 } );
            Assert.Equal(list1, new List<JT808Alarm>() { JT808Alarm.GNSS模块发生故障} );
            Assert.Equal(list2, new List<JT808Alarm>() { JT808Alarm.超速报警, JT808Alarm.GNSS模块发生故障 });
            Assert.Equal(list3, new List<JT808Alarm>() { JT808Alarm.危险预警, JT808Alarm.GNSS模块发生故障 });
            Assert.Equal(list3, new List<JT808Alarm>() { JT808Alarm.危险预警, JT808Alarm.GNSS模块发生故障 });
            Assert.Equal(list4, new List<JT808Alarm>() { JT808Alarm.紧急报警_触动报警开关后触发, JT808Alarm.超速报警,JT808Alarm.疲劳驾驶, JT808Alarm.危险预警, JT808Alarm.GNSS模块发生故障 });
            Assert.Equal(list5, new List<JT808Alarm>() { JT808Alarm.紧急报警_触动报警开关后触发, JT808Alarm.超速报警, JT808Alarm.疲劳驾驶, JT808Alarm.危险预警, JT808Alarm.GNSS模块发生故障, JT808Alarm.非法开门报警 });
        }

        [Fact]
        public void Test2()
        {
            var types = Enum.GetNames(typeof(JT808MsgId));
            var bodyTypes = Assembly.GetAssembly(typeof(JT808Package)).GetTypes().Where(w => w.BaseType == typeof(JT808Bodies)).ToList();
            Assert.Equal(types.Length, bodyTypes.Count);
        }

        [Fact]
        public void Test3()
        {
            var types = Enum.GetNames(typeof(JT808MsgId));
            var bodyTypes = Assembly.GetAssembly(typeof(JT808Package)).GetTypes().Where(w => w.BaseType == typeof(JT808Bodies)).ToList();
            foreach (var type in bodyTypes)
            {
                var instance = Activator.CreateInstance(type);
                if(instance is JT808Bodies jT808Bodies)
                {
                    string code = $"0x{jT808Bodies.MsgId.ToString("X2").PadLeft(4, '0')}";
                    string name = jT808Bodies.Description;
                    Debug.WriteLine(type.FullName);
                }
            }
            Assert.Equal(types.Length, bodyTypes.Count);
        }
    }
}
