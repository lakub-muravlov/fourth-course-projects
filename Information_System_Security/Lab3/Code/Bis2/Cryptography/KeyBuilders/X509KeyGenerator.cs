using Bis2.Cryptography.Abstractions;
using Bis2.Options;
using System.Security.Cryptography.X509Certificates;

namespace Bis2.Cryptography.KeyBuilders
{
    class X509KeyGenerator : IKeyGenerator<X509CertOptions, X509Certificate2>
    {
        public X509Certificate2 GenerateKey(AlgorithmOptions<X509CertOptions> options)
        {
            return new X509Certificate2(options.Value.PathToCert, options.Value.CertKey);
        }
    }
}
