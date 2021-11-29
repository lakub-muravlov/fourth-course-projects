using Bis2.Cryptography.Abstractions;
using Bis2.Extensions;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Bis2.Cryptography.Algorithms
{
    public class RsaAlgorithm : ICryptoAlgorithm<X509Certificate2>
    {
        public void Decrypt(Stream input, Stream output, X509Certificate2 key)
        {
            int blockSize = key.PrivateKey.KeySize / 8;
            byte[] inputBuffer = input.ReadAllBytes();
            using (var rsa = key.GetRSAPrivateKey())
            {
                for (int i = 0; i < Math.Ceiling((float)inputBuffer.Length / blockSize); i++)
                {
                    byte[] block = inputBuffer.Skip(i * blockSize).Take(blockSize).ToArray();
                    var decryptedBlock = rsa.Decrypt(block, RSAEncryptionPadding.Pkcs1);
                    output.Write(decryptedBlock, 0, decryptedBlock.Length);
                }
            }
        }

        public void Encrypt(Stream input, Stream output, X509Certificate2 key)
        {
            int blockSize = (key.PrivateKey.KeySize / 8) - 11;
            byte[] inputBuffer = input.ReadAllBytes();
            using (var rsa = key.GetRSAPublicKey())
            {
                for (int i = 0; i < Math.Ceiling((float)inputBuffer.Length / blockSize); i++)
                {
                    byte[] block = inputBuffer.Skip(i * blockSize).Take(blockSize).ToArray();
                    var decryptedBlock = rsa.Encrypt(block, RSAEncryptionPadding.Pkcs1);
                    output.Write(decryptedBlock, 0, decryptedBlock.Length);
                }
            }
        }
    }
}
