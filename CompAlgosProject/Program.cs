using CompAlgosProject.Benchmark;
using CompAlgosProject.CompressionAlgorithmsFactory.Entities;

var compressionOnWords = new Dictionary<string, Dictionary<string, double>>();
var compressionOnWav = new Dictionary<string, Dictionary<string, double>>();
var compressionOnPng = new Dictionary<string, Dictionary<string, double>>();
var wordsDic = File.ReadAllBytes("words.txt");
var wavFile = File.ReadAllBytes("california.wav");
var pngFile = File.ReadAllBytes("4k.png");

foreach (var level in Enum.GetValues(typeof(CompressionLevel)))
{
    BenchmarkRunner.Run(new[] { "lz4", "zstd", "gzip", "bzip2", "xz", "rle", "lzo" }, wordsDic, 1, (int)level, compressionOnWords);
    BenchmarkRunner.Run(new[] { "lz4", "zstd", "gzip", "bzip2", "xz", "rle", "lzo", "mp3" }, wavFile, 1, (int)level, compressionOnWav);
    BenchmarkRunner.Run(new[] { "lz4", "zstd", "gzip", "bzip2", "xz", "rle", "lzo", "png" }, pngFile, 1, (int)level, compressionOnPng);
}

Console.WriteLine();

ExcelProcessor.ExcelProcessor.ProcessData(compressionOnWords, "words");
ExcelProcessor.ExcelProcessor.ProcessData(compressionOnWav, "wav");
ExcelProcessor.ExcelProcessor.ProcessData(compressionOnWav, "png");