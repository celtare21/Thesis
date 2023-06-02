namespace CompAlgosProject.CompressionAlgorithmsFactory.Entities;

public class WavToMp3 : ICompressionAlgorithm
{
    public byte[] Compress(byte[] data, int level)
    {
        return MP3Converter.MP3Converter.Compress(data);
    }

    public byte[] Decompress(byte[] data)
    {
        return Array.Empty<byte>();
    }

    public string Name => "mp3";
}