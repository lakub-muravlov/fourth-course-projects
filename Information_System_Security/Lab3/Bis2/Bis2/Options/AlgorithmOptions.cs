using Bis2.Enums;

namespace Bis2.Options
{
    class AlgorithmOptions<T>
    {
        public EncryptionAlgorithm Algorithm {get; set; }
        public T Value { get; set; }
    }
}
