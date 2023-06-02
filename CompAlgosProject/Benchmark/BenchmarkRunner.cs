using CompAlgosProject.CompressionAlgorithmsFactory;
using CompAlgosProject.CompressionAlgorithmsFactory.Entities;

namespace CompAlgosProject.Benchmark;

public static class BenchmarkRunner
{
    public static void Run(IEnumerable<string> algorithmNames, byte[] data, int iterations, int level,
        Dictionary<string, Dictionary<string, double>> benchmarkData)
    {
        var algorithms = algorithmNames.Select(CompressionAlgorithmFactory.GetAlgorithm).ToArray();

        foreach (var algorithm in algorithms)
        {
            if ((CompressionLevel)level != CompressionLevel.Default && AlgoHasOnlyDefaultLevel(algorithm))
                continue;

            var testValueDic = new Dictionary<string, double>();

            Console.WriteLine($"Running benchmark for {algorithm.Name}...");

            // Warm up
            var compressedData = algorithm.Compress(data, level);
            algorithm.Decompress(compressedData);

            // Measure compression time
            var compressionWatch = System.Diagnostics.Stopwatch.StartNew();
            for (var i = 0; i < iterations; i++)
            {
                algorithm.Compress(data, level);
            }

            compressionWatch.Stop();

            // Measure decompression time
            compressedData = algorithm.Compress(data, level);
            var decompressionWatch = System.Diagnostics.Stopwatch.StartNew();
            for (var i = 0; i < iterations; i++)
            {
                algorithm.Decompress(compressedData);
            }

            decompressionWatch.Stop();

            // Calculate compression ratio
            var compressedLength = compressedData.Length;
            var uncompressedLength = data.Length;
            var compressionRatio = (double)uncompressedLength / compressedLength;
            var sizeDiff = uncompressedLength - compressedLength;

            Console.WriteLine($"Compression time: {compressionWatch.ElapsedMilliseconds}ms");
            Console.WriteLine($"Decompression time: {decompressionWatch.ElapsedMilliseconds}ms");
            Console.WriteLine($"Compressed size: {compressedLength} bytes");
            Console.WriteLine($"Uncompressed size: {uncompressedLength} bytes");
            Console.WriteLine($"Size difference: {sizeDiff} bytes");
            Console.WriteLine($"Compression ratio: {compressionRatio:F2}");
            Console.WriteLine($"Compression level: {((CompressionLevel)level).ToString()}");
            Console.WriteLine();

            testValueDic.Add("Compression time", compressionWatch.ElapsedMilliseconds);
            testValueDic.Add("Decompression time", decompressionWatch.ElapsedMilliseconds);
            testValueDic.Add("Compressed size", compressedLength);
            testValueDic.Add("Uncompressed size", uncompressedLength);
            testValueDic.Add("Size difference", sizeDiff);
            testValueDic.Add("Compression ratio", Math.Truncate(compressionRatio * 100) / 100);

            benchmarkData.Add($"{algorithm.Name} {level}", testValueDic);
        }
    }

    private static bool AlgoHasOnlyDefaultLevel(ICompressionAlgorithm algorithm)
    {
        return algorithm.Name.Contains("mp3") || algorithm.Name.Contains("lzo") ||
               algorithm.Name.Contains("rle");
    }
}