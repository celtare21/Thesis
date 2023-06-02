using RunLengthAlgo;

namespace CompAlgosProject.CompressionAlgorithmsFactory.Entities;

public class CustomAlgorithm : ICompressionAlgorithm
{
    public byte[] Compress(byte[] data, int level = 0)
    {
        return RunLengthCompression.Compress(data);
    }

    public byte[] Decompress(byte[] data)
    {
        // Implement BZIP2 decompression here
        return RunLengthCompression.Decompress(data);
    }

    public string Name => "RLE";
}