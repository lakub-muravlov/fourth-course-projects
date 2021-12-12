using System.Security.Cryptography;

namespace Bis4
{
    public class HashingFunction
    {
        public byte[] CalculateHash(byte[] input)
        {
            using (SHA1Managed sha = new SHA1Managed())
            {
                return sha.ComputeHash(input);
            }
        }
    }
}
