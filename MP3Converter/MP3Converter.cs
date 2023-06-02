using NAudio.Lame;
using NAudio.Wave;

namespace MP3Converter;

public static class MP3Converter
{
    public static byte[] Compress(byte[] data)
    {
        using (var inputStream = new MemoryStream(data))
        using (var outputStream = new MemoryStream())
        {
            using (var reader = new WaveFileReader(inputStream))
            using (var writer = new LameMP3FileWriter(outputStream, reader.WaveFormat, 128))
            {
                reader.CopyTo(writer);
            }

            return outputStream.ToArray();
        }
    }
}