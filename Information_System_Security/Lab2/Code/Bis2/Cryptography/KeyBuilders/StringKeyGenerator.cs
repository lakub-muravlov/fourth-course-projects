using Bis2.Cryptography.Abstractions;
using Bis2.Enums;
using Bis2.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Bis2.Cryptography.KeyBuilders
{
    class StringKeyGenerator : IKeyGenerator<string>
    {
        public byte[] GenerateKey(AlgorithmOptions<string> options)
        {
            var keyBytes = Encoding.UTF8.GetBytes(options.Key);
            int keyLength = options.Algorithm == EncryptionAlgorithm.Des ? 8 : 16;
            
            byte[] hash;
            using (MD5 md5 = MD5.Create())
            {
                hash = md5.ComputeHash(keyBytes);
            }
            
            var hashLength = hash.Length;
            if (keyLength <= hashLength)
            {
                return hash.Take(keyLength).ToArray();
            }
            
            var remainder = keyLength % hashLength;
            var nFits = (keyLength - remainder) / hashLength;
            var result = new List<byte>();
            
            for (int i = 0; i < nFits; i++)
            {
                result.AddRange(hash);
            }
            
            result.AddRange(hash.Take(remainder));
            var key = result.ToArray();

            return key;
        }
    }
}
