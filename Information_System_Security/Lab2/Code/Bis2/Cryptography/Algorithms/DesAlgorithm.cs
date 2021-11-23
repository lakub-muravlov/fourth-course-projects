using Bis2.Cryptography.Abstractions;
using Bis2.Options;
using System.IO;
using System.Security.Cryptography;

namespace Bis2.Cryptography.Algorithms
{
    class DesAlgorithm : ICryptoAlgorithm
    {
        private readonly DES des;

        public DesAlgorithm(CryptoInitOptions options)
        {
            des = DES.Create();
            des.Mode = options.CipherMode;
            des.Padding = options.PaddingMode;
        }

        public void Decrypt(Stream input, Stream output, byte[] key)
        {
            des.Key = key;
            using (var cryptoStream = new CryptoStream(input, des.CreateDecryptor(des.Key, des.IV), CryptoStreamMode.Read))
            {
                using (var streamReader = new StreamReader(cryptoStream))
                {
                    var decryptedText = streamReader.ReadToEnd();
                    using (StreamWriter streamWriter = new StreamWriter(output))
                    {
                        streamWriter.Write(decryptedText);
                    }
                }
            }
        }

        public void Encrypt(Stream input, Stream output, byte[] key)
        {
            des.Key = key;
            using (var cryptoStream = new CryptoStream(output, des.CreateEncryptor(des.Key, des.IV), CryptoStreamMode.Write))
            {
                byte[] inArray;
                using (var memoryStream = new MemoryStream())
                {
                    input.CopyTo(memoryStream);
                    inArray = memoryStream.ToArray();
                }
                cryptoStream.Write(inArray);
            }
        }
    }
}
