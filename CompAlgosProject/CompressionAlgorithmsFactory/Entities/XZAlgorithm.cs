using System.Runtime.InteropServices;
using Joveler.Compression.XZ;

namespace CompAlgosProject.CompressionAlgorithmsFactory.Entities;

public class XzAlgorithm : ICompressionAlgorithm
{
    static XzAlgorithm()
    {
        string libDir = "runtimes";
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            libDir = Path.Combine(libDir, "win-");
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            libDir = Path.Combine(libDir, "linux-");
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            libDir = Path.Combine(libDir, "osx-");

        // ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
        switch (RuntimeInformation.ProcessArchitecture)
        {
            case Architecture.X86:
                libDir += "x86";
                break;
            case Architecture.X64:
                libDir += "x64";
                break;
            case Architecture.Arm:
                libDir += "arm";
                break;
            case Architecture.Arm64:
                libDir += "arm64";
                break;
        }

        libDir = Path.Combine(libDir, "native");

        string libPath = null!;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            libPath = Path.Combine(libDir, "liblzma.dll");
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            libPath = Path.Combine(libDir, "liblzma.so");
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            libPath = Path.Combine(libDir, "liblzma.dylib");

        if (libPath == null)
            throw new PlatformNotSupportedException("Unable to find native library.");
        if (!File.Exists(libPath))
            throw new PlatformNotSupportedException($"Unable to find native library [{libPath}].");

        XZInit.GlobalInit(libPath);
    }

    public byte[] Compress(byte[] data, int level)
    {
        var enumLevel = level switch
        {
            1 => LzmaCompLevel.Level0,
            2 => LzmaCompLevel.Level6,
            3 => LzmaCompLevel.Level9,
            var _ => (LzmaCompLevel)level
        };

        using var outputStream = new MemoryStream();
        using var dataStream = new MemoryStream(data);
        using var xzStream = new XZStream(outputStream, new XZCompressOptions { Level = enumLevel, LeaveOpen = false });

        dataStream.CopyTo(xzStream);

        return outputStream.ToArray();
    }

    public byte[] Decompress(byte[] data)
    {
        using var outputStream = new MemoryStream();
        using var compressedStream = new MemoryStream(data);
        using var xzStream = new XZStream(compressedStream, new XZDecompressOptions { LeaveOpen = false });

        try
        {
            xzStream.CopyTo(outputStream);
        }
        catch
        {
            // lib fails for no reason, just ignore
        }

        return outputStream.ToArray();
    }

    public string Name => "XZ";
}