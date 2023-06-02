namespace RunLengthAlgo;

public static class RunLengthCompression
{
    public static byte[] Compress(byte[] data)
    {
        using (var outputStream = new MemoryStream())
        {
            for (var i = 0; i < data.Length; i++)
            {
                var count = 1;
                while (i + 1 < data.Length && data[i] == data[i + 1])
                {
                    count++;
                    i++;
                }

                outputStream.WriteByte(data[i]);
                outputStream.WriteByte((byte)count);
            }

            return outputStream.ToArray();
        }
    }

    public static byte[] Decompress(byte[] data)
    {
        using (var outputStream = new MemoryStream())
        {
            for (var i = 0; i < data.Length; i += 2)
            {
                var b = data[i];
                int count = data[i + 1];

                for (var j = 0; j < count; j++)
                {
                    outputStream.WriteByte(b);
                }
            }

            return outputStream.ToArray();
        }
    }
}