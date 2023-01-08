using System.IO.Compression;

namespace CompAlgosProject.CompressionAlgorithmsFactory.Entities;

public class GzipAlgorithm : ICompressionAlgorithm
{
    public byte[] Compress(byte[] data, int level)
    {
        var enumLevel = level switch
        {
            1 => System.IO.Compression.CompressionLevel.Fastest,
            2 => System.IO.Compression.CompressionLevel.Optimal,
            3 => System.IO.Compression.CompressionLevel.SmallestSize,
            var _ => (System.IO.Compression.CompressionLevel)level
        };

        using var outputStream = new MemoryStream();
        using var zipStream = new GZipStream(outputStream, enumLevel);

        zipStream.Write(data, 0, data.Length);

        return outputStream.ToArray();
    }

    public byte[] Decompress(byte[] data)
    {
        using var compressedStream = new MemoryStream(data);
        using var zipStream = new GZipStream(compressedStream, CompressionMode.Decompress);
        using var outputStream = new MemoryStream();

        zipStream.CopyTo(outputStream);

        return outputStream.ToArray();
    }

    public string Name => "GZIP";
}