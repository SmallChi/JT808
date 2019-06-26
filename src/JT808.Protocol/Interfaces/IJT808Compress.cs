namespace JT808.Protocol.Interfaces
{
    /// <summary>
    /// 压缩接口
    /// </summary>
    public interface IJT808Compress
    {
        byte[] Compress(byte[] data);

        byte[] Decompress(byte[] compressData);
    }
}
