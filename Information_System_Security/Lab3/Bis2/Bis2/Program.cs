using Bis2.Cryptography;
using Bis2.Cryptography.Abstractions;
using Bis2.Cryptography.KeyBuilders;
using Bis2.Enums;
using Bis2.Options;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace Bis2
{
    class Program
    {
        private static string resourcesPath;
        private static string password;
        private static IEncryptionFacade encryptionFacade;
        private static IKeyGenerator<string, byte[]> stringKeyGenerator;
        private static IKeyGenerator<X509CertOptions, X509Certificate2> x509KeyGenerator;
        private static AlgorithmOptions<string> desOptions;
        private static AlgorithmOptions<string> tripleDesOptions;
        private static AlgorithmOptions<X509CertOptions> rsaOptions;
        static Program()
        {
            resourcesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../", "Resources");
            password = "Муравльов Андрій Дмитрович";

            encryptionFacade = new EncryptionFacade();
            stringKeyGenerator = new StringKeyGenerator();
            x509KeyGenerator = new X509KeyGenerator();

            desOptions = new AlgorithmOptions<string>
            {
                Algorithm = EncryptionAlgorithm.Des,
                Value = password
            };
            tripleDesOptions = new AlgorithmOptions<string>
            {
                Algorithm = EncryptionAlgorithm.TripleDes,
                Value = password
            };
            rsaOptions = new AlgorithmOptions<X509CertOptions>
            {
                Algorithm = EncryptionAlgorithm.Rsa,
                Value = new X509CertOptions
                {
                    PathToCert = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../", "Keys/cert.p12"),
                    CertKey = "andrey"
                }
            };
        }
        static void Main(string[] args)
        {
            using (FileStream input = File.Open(Path.Combine(resourcesPath, "plain-text.txt"), FileMode.Open))
            {
                using (FileStream desEncrypted = File.Open(Path.Combine(resourcesPath, "encrypted-des.hex"), FileMode.OpenOrCreate))
                {
                    encryptionFacade.Encrypt(input, desEncrypted, desOptions, stringKeyGenerator);
                }

                input.Seek(0, SeekOrigin.Begin);

                using (FileStream tripleDesEncrypted = File.Open(Path.Combine(resourcesPath, "encrypted-triple-des.hex"), FileMode.OpenOrCreate))
                {
                    encryptionFacade.Encrypt(input, tripleDesEncrypted, tripleDesOptions, stringKeyGenerator);
                }

                input.Seek(0, SeekOrigin.Begin);

                using (FileStream rsaEncrypted = File.Open(Path.Combine(resourcesPath, "encrypted-rsa.hex"), FileMode.OpenOrCreate))
                {
                    encryptionFacade.Encrypt(input, rsaEncrypted, rsaOptions, x509KeyGenerator);
                }
            }

            using (FileStream desEncrypted = File.Open(Path.Combine(resourcesPath, "encrypted-des.hex"), FileMode.Open))
            {
                using (FileStream desDecrypted = File.Open(Path.Combine(resourcesPath, "decrypted-des.txt"), FileMode.OpenOrCreate))
                {
                    encryptionFacade.Decrypt(desEncrypted, desDecrypted, desOptions, stringKeyGenerator);
                }
            }

            using (FileStream tripleDesEncrypted = File.Open(Path.Combine(resourcesPath, "encrypted-triple-des.hex"), FileMode.Open))
            {
                using (FileStream tripleDesDecrypted = File.Open(Path.Combine(resourcesPath, "decrypted-triple-des.txt"), FileMode.OpenOrCreate))
                {
                    encryptionFacade.Decrypt(tripleDesEncrypted, tripleDesDecrypted, tripleDesOptions, stringKeyGenerator);
                }
            }

            using (FileStream rsaEncrypted = File.Open(Path.Combine(resourcesPath, "encrypted-rsa.hex"), FileMode.Open))
            {
                using (FileStream rsaDecrypted = File.Open(Path.Combine(resourcesPath, "decrypted-rsa.txt"), FileMode.OpenOrCreate))
                {
                    encryptionFacade.Decrypt(rsaEncrypted, rsaDecrypted, rsaOptions, x509KeyGenerator);
                }
            }
        }
    }
}
