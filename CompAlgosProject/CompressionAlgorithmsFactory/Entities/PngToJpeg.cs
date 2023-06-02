namespace CompAlgosProject.CompressionAlgorithmsFactory.Entities;

public class PngToJpeg : ICompressionAlgorithm
{
    public byte[] Compress(byte[] data, int level)
    {
        var enumLevel = level switch
        {
            0 => 100,
            1 => 75,
            2 => 50,
            3 => 25,
            var _ => level
        };

        return PNGConverter.PNGConverter.ConvertPngToJpeg(data, enumLevel);
    }

    public byte[] Decompress(byte[] data)
    {
        throw new NotImplementedException();
    }

    public string Name => "png";
}