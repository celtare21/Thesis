using CompAlgosProject.CompressionAlgorithmsFactory.Entities;

namespace CompAlgosProject.CompressionAlgorithmsFactory;

public static class CompressionAlgorithmFactory
{
    public static ICompressionAlgorithm GetAlgorithm(string name)
    {
        return name.ToLowerInvariant() switch
        {
            "lz4" => new Lz4Algorithm(),
            "lzo" => new LzoAlgorithm(),
            "zstd" => new ZstdAlgorithm(),
            "gzip" => new GzipAlgorithm(),
            "bzip2" => new Bzip2Algorithm(),
            "xz" => new XzAlgorithm(),
            "rle" => new CustomAlgorithm(),
            "mp3" => new WavToMp3(),
            "png" => new PngToJpeg(),
            var _ => throw new ArgumentException($"Unsupported compression algorithm: {name}")
        };
    }
}