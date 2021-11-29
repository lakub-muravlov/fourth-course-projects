using Bis2.Options;

namespace Bis2.Cryptography.Abstractions
{
    interface IKeyGenerator<T>
    {
        byte[] GenerateKey(AlgorithmOptions<T> options);
    }
}
