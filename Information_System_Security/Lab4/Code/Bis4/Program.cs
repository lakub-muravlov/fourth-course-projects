using Bis2.Cryptography.Algorithms;
using Bis2.Extensions;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Bis4
{
    class Program
    {
        static void Main(string[] args)
        {
            string keysDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../", "Keys");

            HashingFunction hashing = new HashingFunction();
            int permutationTableLength = 10 + (18 + 82) % 7;
            X509Certificate2 key = new X509Certificate2(Path.Combine(keysDir, "cert.p12"), "andrey");
            RsaAlgorithm rsa = new RsaAlgorithm();
            TranspositionAlgorithm senderPermutation = new TranspositionAlgorithm(permutationTableLength);
            byte[] message = Encoding.UTF8.GetBytes("Muravlyov Andrey");
            Console.WriteLine($"message: {Encoding.UTF8.GetString(message)}");
            byte[] encryptedMessage = senderPermutation.Encrypt(message);
            byte[] originalHash = hashing.CalculateHash(encryptedMessage);
            Console.WriteLine($"Message hash after symmetric encryption: {originalHash.ToHexString()}");

            MemoryStream symmetricKeyStream = new MemoryStream(senderPermutation.GetTranspositions());
            MemoryStream encryptedSymmetricKey = new MemoryStream();
            rsa.Encrypt(symmetricKeyStream, encryptedSymmetricKey, key);

            MemoryStream decryptedSymmetricKey = new MemoryStream();
            rsa.Decrypt(encryptedSymmetricKey, decryptedSymmetricKey, key);
            TranspositionAlgorithm receiverPermutation = new TranspositionAlgorithm(decryptedSymmetricKey.ToArray());
            byte[] decryptedMessage = receiverPermutation.Decrypt(encryptedMessage);
            byte[] receivedHash = hashing.CalculateHash(encryptedMessage);
            Console.WriteLine($"Received message: {Encoding.UTF8.GetString(decryptedMessage)}");
            Console.WriteLine($"Received message hash: {receivedHash.ToHexString()}");
        }
    }
}
