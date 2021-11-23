using System.IO;

namespace Bis2.Cryptography.Abstractions
{
    interface ICryptoAlgorithm
    {
        public void Encrypt(Stream input, Stream output, byte[] key);
        public void Decrypt(Stream input, Stream output, byte[] key);
    }
}
