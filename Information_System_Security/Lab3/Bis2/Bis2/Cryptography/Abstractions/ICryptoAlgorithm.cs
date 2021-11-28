using System.IO;

namespace Bis2.Cryptography.Abstractions
{
    interface ICryptoAlgorithm<TKey>
    {
        public void Encrypt(Stream input, Stream output, TKey key);
        public void Decrypt(Stream input, Stream output, TKey key);
    }
}
