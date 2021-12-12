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

        public void Decrypt<TOptions, TKey>(Stream input, Stream output, AlgorithmOptions<TOptions> options, IKeyGenerator<TOptions, TKey> keyGenerator)
        {
            var algorithm = GetAlgorithmInstance<TOptions, TKey>(options);
            var key = keyGenerator.GenerateKey(options);
            PrintInfo(options, key);
            algorithm.Decrypt(input, output, key);
        }

        public void Encrypt<TOptions, TKey>(Stream input, Stream output, AlgorithmOptions<TOptions> options, IKeyGenerator<TOptions, TKey> keyGenerator)
        {
            var algorithm = GetAlgorithmInstance<TOptions, TKey>(options);
            var key = keyGenerator.GenerateKey(options);
            PrintInfo(options, key);
            algorithm.Encrypt(input, output, key);
        }

        private ICryptoAlgorithm<TKey> GetAlgorithmInstance<TOptions, TKey>(AlgorithmOptions<TOptions> options)
        {
            ICryptoAlgorithm<TKey> algorithm;
            switch (options.Algorithm)
            {
                case EncryptionAlgorithm.Des:
                    algorithm = (ICryptoAlgorithm<TKey>)new DesAlgorithm(initOptions);
                    break;
                case EncryptionAlgorithm.TripleDes:
                    algorithm = (ICryptoAlgorithm<TKey>)new TripleDesAlgorithm(initOptions);
                    break;
                case EncryptionAlgorithm.Rsa:
                    algorithm = (ICryptoAlgorithm<TKey>)new RsaAlgorithm();
                    break;
                default:
                    throw new ArgumentException("Invalid algorithm type");
            }
            return algorithm;
        }

        private void PrintInfo<TOptions,TKey>(AlgorithmOptions<TOptions> options, TKey key)
        {
            Console.WriteLine($"Generated key for {Enum.GetName(typeof(EncryptionAlgorithm), options.Algorithm).ToUpper()} algorithm");
            if(options.Algorithm != EncryptionAlgorithm.Rsa)
            {
                Console.WriteLine($"Encryption/Decryption key: {string.Join(" ", Array.ConvertAll(key as byte[], x => x.ToString()))}");
                Console.WriteLine($"Key length: {(key as byte[]).Length} bytes");
            }

        }
    }
}
