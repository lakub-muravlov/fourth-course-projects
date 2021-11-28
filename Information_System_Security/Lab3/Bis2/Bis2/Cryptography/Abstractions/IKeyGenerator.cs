using Bis2.Options;

namespace Bis2.Cryptography.Abstractions
{
    interface IKeyGenerator<TOptions, TKey>
    {
        TKey GenerateKey(AlgorithmOptions<TOptions> options);
    }
}
