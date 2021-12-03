using System.Security.Cryptography;

namespace Bis2.Options
{
    class CryptoInitOptions
    {
        public CipherMode CipherMode { get; set; }
        public PaddingMode PaddingMode { get; set; }
    }
}
