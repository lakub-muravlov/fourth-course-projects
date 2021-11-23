using Bis2.Cryptography.Abstractions;
using Bis2.Cryptography.Algorithms;
using Bis2.Enums;
using Bis2.Options;
using System;
using System.IO;
using System.Security.Cryptography;

namespace Bis2.Cryptography
{
    class EncryptionFacade : IEncryptionFacade
    {
        private readonly CryptoInitOptions initOptions;
        public EncryptionFacade()
        {
            initOptions = new CryptoInitOptions
            {
                PaddingMode = PaddingMode.Zeros,
                CipherMode = CipherMode.ECB
            };
        }

        public void Decrypt<TKey>(Stream input, Stream output, AlgorithmOptions<TKey> options, IKeyGenerator<TKey> keyGenerator)
        {
            var algorithm = GetAlgorithmInstance(options);
            var key = keyGenerator.GenerateKey(options);
            PrintInfo(options, key);
            algorithm.Decrypt(input, output, key);
        }

        public void Encrypt<TKey>(Stream input, Stream output, AlgorithmOptions<TKey> options, IKeyGenerator<TKey> keyGenerator)
        {
            var algorithm = GetAlgorithmInstance(options);
            var key = keyGenerator.GenerateKey(options);
            PrintInfo(options, key);
            algorithm.Encrypt(input, output, key);
        }

        private ICryptoAlgorithm GetAlgorithmInstance<TKey>(AlgorithmOptions<TKey> options)
        {
            ICryptoAlgorithm algorithm;
            switch (options.Algorithm)
            {
                case EncryptionAlgorithm.Des:
                    algorithm = new DesAlgorithm(initOptions);
                    break;
                case EncryptionAlgorithm.TripleDes:
                    algorithm = new TripleDesAlgorithm(initOptions);
                    break;
                default:
                    throw new ArgumentException("Invalid algorithm type");
            }
            return algorithm;
        }

        private void PrintInfo<TKey>(AlgorithmOptions<TKey> options, byte[] key)
        {
            Console.WriteLine($"Generated key for {Enum.GetName(typeof(EncryptionAlgorithm), options.Algorithm).ToUpper()} algorithm");
            Console.WriteLine($"Encryption/Decryption key: {string.Join(" ", Array.ConvertAll(key, x => x.ToString("X2")))}");
            Console.WriteLine($"Key length: {key.Length} bytes");
        }
    }
}
