using System.Linq;
using System.Text;
using Xunit;

namespace JT808.Protocol.Test
{
    public class JT808GlobalConfigTest
    {
        [Fact]
        public void Test1()
        {
            string str = string.Join(',', Enumerable.Range(0, 100000).Select(s => s.ToString()));
            byte[] data = Encoding.UTF8.GetBytes(str);
            var data1 = JT808GlobalConfig.Instance.Compress.Compress(data);
            Assert.True(data.Length > data1.Length);
        }
    }
}
