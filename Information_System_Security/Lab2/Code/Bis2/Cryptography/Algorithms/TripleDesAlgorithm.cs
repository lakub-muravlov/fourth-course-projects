using Bis2.Cryptography.Abstractions;
using Bis2.Options;
using System.IO;
using System.Security.Cryptography;

namespace Bis2.Cryptography.Algorithms
{
    class TripleDesAlgorithm : ICryptoAlgorithm
    {
        private TripleDESCryptoServiceProvider tripleDes;
        public TripleDesAlgorithm(CryptoInitOptions options)
        {
            tripleDes = new TripleDESCryptoServiceProvider();
            tripleDes.Mode = options.CipherMode;
            tripleDes.Padding = options.PaddingMode;
        }
        public void Decrypt(Stream input, Stream output, byte[] key)
        {
            tripleDes.Key = key;
            using (var cryptoStream = new CryptoStream(input, tripleDes.CreateDecryptor(tripleDes.Key, tripleDes.IV), CryptoStreamMode.Read))
            {
                using (var streamReader = new StreamReader(cryptoStream))
                {
                    string text = streamReader.ReadToEnd();
                    using (var streamWriter = new StreamWriter(output))
                    {
                        streamWriter.Write(text);
                    }
                }
            }
        }

        public void Encrypt(Stream input, Stream output, byte[] key)
        {
            tripleDes.Key = key;
            byte[] inArray;
            using (var memoryStream = new MemoryStream())
            {
                input.CopyTo(memoryStream);
                inArray = memoryStream.ToArray();
            }

            using (var cryptoStream = new CryptoStream(output, tripleDes.CreateEncryptor(tripleDes.Key, tripleDes.IV), CryptoStreamMode.Write))
            {
                cryptoStream.Write(inArray, 0, inArray.Length);
            }
        }
    }
}
