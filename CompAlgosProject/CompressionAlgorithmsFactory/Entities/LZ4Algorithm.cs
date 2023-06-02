using EasyCompressor;
using K4os.Compression.LZ4;

namespace CompAlgosProject.CompressionAlgorithmsFactory.Entities;

public class Lz4Algorithm : ICompressionAlgorithm
{
    public byte[] Compress(byte[] data, int level)
    {
        var enumLevel = level switch
        {
            1 => LZ4Level.L00_FAST,
            2 => LZ4Level.L06_HC,
            3 => LZ4Level.L12_MAX,
            var _ => (LZ4Level)level
        };

        var compressor = new LZ4Compressor(level: enumLevel);

        return compressor.Compress(data);
    }

    public byte[] Decompress(byte[] data)
    {
        var compressor = new LZ4Compressor();

        return compressor.Decompress(data);
    }

    public string Name => "LZ4";
}