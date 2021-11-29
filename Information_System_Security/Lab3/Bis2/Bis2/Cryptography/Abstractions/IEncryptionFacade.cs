using Bis2.Options;
using System.IO;

namespace Bis2.Cryptography.Abstractions
{
    interface IEncryptionFacade
    {
        void Decrypt<TOptions, TKey>(Stream input, Stream output, AlgorithmOptions<TOptions> options, IKeyGenerator<TOptions, TKey> keyGenerator);
        void Encrypt<TOptions, TKey>(Stream input, Stream output, AlgorithmOptions<TOptions> options, IKeyGenerator<TOptions, TKey> keyGenerator);
    }
}
