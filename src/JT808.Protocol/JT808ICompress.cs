namespace JT808.Protocol
{
    /// <summary>
    /// 压缩接口
    /// </summary>
    public interface IJT808ICompress
    {
        byte[] Compress(byte[] data);

        byte[] Decompress(byte[] compressData);
    }
}
