using Bis2.Options;
using System.IO;

namespace Bis2.Cryptography.Abstractions
{
    interface IEncryptionFacade
    {
        void Decrypt<TKey>(Stream input, Stream output, AlgorithmOptions<TKey> options, IKeyGenerator<TKey> keyGenerator);
        void Encrypt<TKey>(Stream input, Stream output, AlgorithmOptions<TKey> options, IKeyGenerator<TKey> keyGenerator);
    }
}
