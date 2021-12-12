using System;
using System.Linq;

namespace Bis4
{
    class TranspositionAlgorithm
    {
        private readonly byte[] transpositions;
        private readonly int chunkLength;


        public TranspositionAlgorithm(byte[] transpositions)
        {
            this.transpositions = transpositions;
            chunkLength = transpositions.Length;
        }

        public TranspositionAlgorithm(int chunkLength)
        {
            this.chunkLength = chunkLength;
            transpositions = Enumerable.Range(0, chunkLength).Select(x => (byte)x).OrderBy(x => new Random().Next()).ToArray();
        }

        public byte[] GetTranspositions()
        {
            return transpositions;
        }

        public byte[] Decrypt(byte[] input)
        {
            int inputLength = input.Length;
            byte[] result = null;
            int chunks = (inputLength / chunkLength) == 0 ? 1 : inputLength / chunkLength;
            for (int i = 0; i < chunks; i++)
            {
                byte[] chunk = input.Skip(i * chunkLength).Take(chunkLength).ToArray();
                chunk = chunk.Concat(new byte[chunkLength - chunk.Length]).ToArray();
                byte[] encrypted = new byte[chunkLength];
                for (int j = 0; j < chunk.Length; j++)
                {
                    byte pos = transpositions[j];
                    encrypted[pos] = chunk[j];
                }
                result = result == null ? encrypted : result.Concat(encrypted).ToArray();
            }
            return result;
        }

        public byte[] Encrypt(byte[] input)
        {
            int inputLength = input.Length;
            byte[] result = null;
            int chunks = (inputLength / chunkLength) == 0 ? 1 : inputLength / chunkLength;
            for (int i = 0; i <= chunks; i++)
            {
                byte[] chunk = input.Skip(i * chunkLength).Take(chunkLength).ToArray();
                chunk = chunk.Concat(new byte[chunkLength - chunk.Length]).ToArray();
                byte[] encrypted = new byte[chunkLength];
                for (int j = 0; j < chunkLength; j++)
                {
                    byte encryptedPos = transpositions[j];
                    encrypted[j] = chunk[encryptedPos % chunk.Length];
                }
                result = result == null ? encrypted : result.Concat(encrypted).ToArray();
            }
            return result.ToArray();
        }
    }
}
