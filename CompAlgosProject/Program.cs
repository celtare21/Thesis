using CompAlgosProject.Benchmark;
using CompAlgosProject.CompressionAlgorithmsFactory.Entities;

static byte[] GetByteArray(int sizeInKb)
{
    var rnd = new Random();
    var b = new byte[sizeInKb * 1024]; // convert kb to byte
    rnd.NextBytes(b);
    return b;
}

var benchmarkData = new Dictionary<string, Dictionary<string, double>>();
var data = File.ReadAllBytes("words.txt");
//BenchmarkRunner.Run(new[] { "rle", "lz4" }, data, 1, (int)CompressionLevel.Default, benchmarkData);
BenchmarkRunner.Run(new[] { "rle", "lzo" }, data, 1, (int)CompressionLevel.Default, benchmarkData);
BenchmarkRunner.Run(new[] { "lz4", "zstd", "gzip", "bzip2", "xz"}, data, 1, (int)CompressionLevel.Low, benchmarkData);
BenchmarkRunner.Run(new[] { "lz4", "zstd", "gzip", "bzip2", "xz"}, data, 1, (int)CompressionLevel.Medium, benchmarkData);
BenchmarkRunner.Run(new[] { "lz4", "zstd", "gzip", "bzip2", "xz"}, data, 1, (int)CompressionLevel.High, benchmarkData);
Console.WriteLine();
ExcelProcessor.ExcelProcessor.ProcessData(benchmarkData);