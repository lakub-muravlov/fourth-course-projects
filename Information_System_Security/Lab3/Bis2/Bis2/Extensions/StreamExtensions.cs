using System.IO;

namespace Bis2.Extensions
{
    public static class StreamExtensions
    {
        public static byte[] ReadAllBytes(this Stream inputStream)
        {
            long inputLength = inputStream.Length;
            byte[] inputBuffer = new byte[inputLength];
            inputStream.Read(inputBuffer, 0, (int)inputLength);
            return inputBuffer;
        }
    }
}
