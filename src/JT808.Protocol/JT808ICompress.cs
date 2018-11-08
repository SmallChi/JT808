using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol
{
    /// <summary>
    /// 压缩接口
    /// </summary>
    public interface JT808ICompress
    {
        byte[] Compress(byte[] data);

        byte[] Decompress(byte[] compressData);
    }
}
