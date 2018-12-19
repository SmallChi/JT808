using JT808.Protocol.JT808Formatters;
using JT808.Protocol.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace JT808.Protocol.Test.Extensions
{
    public class JT808EscapeTest:IDisposable
    {
        private byte[] deEscapeBytes;
        private byte[] escapeBytesPoll;
        private byte[] escapeBytesList;
        private int escapeLen = 0;

        public JT808EscapeTest()
        {
            deEscapeBytes = "7E02000026123456789012007D02000000010000000200BA7F0E07E4F11C0028003C00001810151010100104000000640202007D01137E".ToHexBytes();
            escapeBytesPoll = JT808ArrayPool.Rent(1024);
            escapeBytesList = JT808ArrayPool.Rent(1024);
            var tmp = "7E02000026123456789012007E000000010000000200BA7F0E07E4F11C0028003C00001810151010100104000000640202007D137E".ToHexBytes();
            escapeLen = tmp.Length;
            Array.Copy(tmp, 0, escapeBytesPoll, 0, tmp.Length);
            Array.Copy(tmp, 0, escapeBytesList, 0, tmp.Length);
        }

        [Fact]
        public void DeEscapeTest3()
        {
            byte[] buffer1 = JT808PackageFromatter.JT808DeEscape(deEscapeBytes).ToArray();
            byte[] buffer2 = JT808PackageFromatter.JT808DeEscapeOld(deEscapeBytes, 0, deEscapeBytes.Length).ToArray();
            Assert.Equal(buffer1, buffer2);
        }

        [Fact]
        public void DeEscapeTest6()
        {
            var len1 = JT808PackageFromatter.JT808EscapeOld(ref escapeBytesList, escapeLen);
            var len2 = JT808PackageFromatter.JT808Escape(ref escapeBytesPoll, escapeLen);
            Assert.Equal(len1, len2);
        }

        public void Dispose()
        {
            JT808ArrayPool.Return(escapeBytesPoll);
            JT808ArrayPool.Return(escapeBytesList);
        }
    }
}
