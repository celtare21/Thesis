using LZOAlgo;

namespace CompAlgosProject.CompressionAlgorithmsFactory.Entities;

public class LzoAlgorithm : ICompressionAlgorithm
{
    public byte[] Compress(byte[] data, int level)
    {
        var compressor = new LzoCompressor();

        return compressor.Compress(data);
    }

    public byte[] Decompress(byte[] data)
    {
        var compressor = new LzoCompressor();

        return compressor.Decompress(data);
    }

    public string Name => "LZO";
}