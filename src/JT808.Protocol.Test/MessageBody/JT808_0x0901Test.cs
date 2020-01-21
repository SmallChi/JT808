using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System.Runtime.InteropServices;
using System.Text;
using Xunit;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x0901Test
    {
        JT808Serializer JT808Serializer = new JT808Serializer();
        const string UserName = "smallchismallchismallchismallchismallchismallchismallchismallchismallchismallchismallchi";

        [Fact]
        public void Test1()
        {
            JT808_0x0901 jT808_0X0901 = new JT808_0x0901();
            var data = Encoding.UTF8.GetBytes(UserName);
            jT808_0X0901.UnCompressMessage = data;
            var hex = JT808Serializer.Serialize(jT808_0X0901).ToHexString();
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Assert.Equal("0000001F1F8B08000000000000032BCE4DCCC949CEC82CA6320D0027F897E258000000", hex);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Assert.Equal("0000001F1F8B080000000000000A2BCE4DCCC949CEC82CA6320D0027F897E258000000", hex);
            }

        }

        [Fact]
        public void Test2()
        {
            byte[] bytes = "0000001F1F8B080000000000000B2BCE4DCCC949CEC82CA6320D0027F897E258000000".ToHexBytes();
            JT808_0x0901 jT808_0X8600 = JT808Serializer.Deserialize<JT808_0x0901>(bytes);
            Assert.Equal((uint)88, jT808_0X8600.UnCompressMessageLength);
            Assert.Equal(Encoding.UTF8.GetBytes(UserName), jT808_0X8600.UnCompressMessage);
        }

        [Fact]
        public void Test3()
        {
            byte[] bytes = "0000001F1F8B080000000000000B2BCE4DCCC949CEC82CA6320D0027F897E258000000".ToHexBytes();
            string json = JT808Serializer.Analyze<JT808_0x0901>(bytes);
        }
    }
}
