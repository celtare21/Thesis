using EasyCompressor;

namespace CompAlgosProject.CompressionAlgorithmsFactory.Entities;

public class ZstdAlgorithm : ICompressionAlgorithm
{
    public byte[] Compress(byte[] data, int level)
    {
        level = level switch
        {
            1 => -7,
            2 => 8,
            3 => 22,
            var _ => level
        };

        var compressor = new ZstdCompressor(level: level);

        return compressor.Compress(data);
    }

    public byte[] Decompress(byte[] data)
    {
        var compressor = new ZstdCompressor();

        return compressor.Decompress(data);
    }

    public string Name => "ZSTD";
}