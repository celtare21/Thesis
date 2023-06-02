using SixLabors.ImageSharp.Formats.Jpeg;
using Color = SixLabors.ImageSharp.Color;

namespace PNGConverter;

public static class PNGConverter
{
    public static byte[] ConvertPngToJpeg(byte[] pngBytes, int level)
    {
        using (var pngStream = new MemoryStream(pngBytes))
        using (var image = Image.Load(pngStream))
        {
            using (var jpegStream = new MemoryStream())
            {
                image.Mutate(x => x.BackgroundColor(Color.White));

                var jpegEncoder = new JpegEncoder { Quality = level };

                image.Save(jpegStream, jpegEncoder);

                return jpegStream.ToArray();
            }
        }
    }
}