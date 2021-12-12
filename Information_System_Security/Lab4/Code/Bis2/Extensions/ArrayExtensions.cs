using System.Linq;

namespace Bis2.Extensions
{
    public static class ArrayExtensions
    {
        public static string ToHexString(this byte[] array)
        {
            return string.Join(" ", array.Select(elem => elem.ToString("X2")));
        }
    }
}
