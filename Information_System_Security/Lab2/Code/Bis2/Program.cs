using Bis2.Cryptography;
using Bis2.Cryptography.Abstractions;
using Bis2.Cryptography.KeyBuilders;
using Bis2.Enums;
using Bis2.Options;
using System;
using System.IO;

namespace Bis2
{
    class Program
    {
        private static string resourcesPath;
        private static string password;
        private static IEncryptionFacade encryptionFacade;
        private static IKeyGenerator<string> keyGenerator;
        private static AlgorithmOptions<string> desOptions;
        private static AlgorithmOptions<string> tripleDesOptions;
        static Program()
        {
            resourcesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../", "Resources");
            password = "Муравльов Андрій Дмитрович";
            encryptionFacade = new EncryptionFacade();
            keyGenerator = new StringKeyGenerator();

            desOptions = new AlgorithmOptions<string>
            {
                Algorithm = EncryptionAlgorithm.Des,
                Key = password
            };
            tripleDesOptions = new AlgorithmOptions<string>
            {
                Algorithm = EncryptionAlgorithm.TripleDes,
                Key = password
            };
        }
        static void Main(string[] args)
        {
            using (FileStream input = File.Open(Path.Combine(resourcesPath, "plain-text.txt"), FileMode.Open))
            {
                using (FileStream desEncrypted = File.Open(Path.Combine(resourcesPath, "encrypted-des.hex"), FileMode.OpenOrCreate))
                {
                    encryptionFacade.Encrypt(input, desEncrypted, desOptions, keyGenerator);
                }

                input.Seek(0, SeekOrigin.Begin);

                using (FileStream tripleDesEncrypted = File.Open(Path.Combine(resourcesPath, "encrypted-triple-des.hex"), FileMode.OpenOrCreate))
                {
                    encryptionFacade.Encrypt(input, tripleDesEncrypted, tripleDesOptions, keyGenerator);
                }
            }

            using (FileStream desEncrypted = File.Open(Path.Combine(resourcesPath, "encrypted-des.hex"), FileMode.Open))
            {
                using (FileStream desDecrypted = File.Open(Path.Combine(resourcesPath, "decrypted-des.txt"), FileMode.OpenOrCreate))
                {
                    encryptionFacade.Decrypt(desEncrypted, desDecrypted, desOptions, keyGenerator);
                }
            }

            using (FileStream tripleDesEncrypted = File.Open(Path.Combine(resourcesPath, "encrypted-triple-des.hex"), FileMode.Open))
            {
                using (FileStream tripleDesDecrypted = File.Open(Path.Combine(resourcesPath, "decrypted-triple-des.txt"), FileMode.OpenOrCreate))
                {
                    encryptionFacade.Decrypt(tripleDesEncrypted, tripleDesDecrypted, tripleDesOptions, keyGenerator);
                }
            }
        }
    }
}
