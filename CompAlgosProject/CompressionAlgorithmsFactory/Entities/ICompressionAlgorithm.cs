namespace CompAlgosProject.CompressionAlgorithmsFactory.Entities;

public interface ICompressionAlgorithm
{
    public byte[] Compress(byte[] data, int level);
    public byte[] Decompress(byte[] data);
    public string Name { get; }
}