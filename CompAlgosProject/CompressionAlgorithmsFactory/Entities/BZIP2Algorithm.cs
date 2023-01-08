using ICSharpCode.SharpZipLib.BZip2;

namespace CompAlgosProject.CompressionAlgorithmsFactory.Entities;

public class Bzip2Algorithm : ICompressionAlgorithm
{
    public byte[] Compress(byte[] data, int level)
    {
        var inStream = new MemoryStream(data);
        var outStream = new MemoryStream();

        level = level switch
        {
            1 => 1,
            2 => 5,
            3 => 9,
            var _ => level
        };

        BZip2.Compress(inStream, outStream, true, level);

        return outStream.ToArray();
    }

    public byte[] Decompress(byte[] data)
    {
        var inStream = new MemoryStream(data);
        var outStream = new MemoryStream();

        BZip2.Decompress(inStream, outStream, true);

        return outStream.ToArray();
    }

    public string Name => "BZIP2";
}