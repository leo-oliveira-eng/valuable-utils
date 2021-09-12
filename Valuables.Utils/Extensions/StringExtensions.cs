using System.Linq;

namespace Valuables.Utils.Extensions
{
    public static class StringExtensions
    {
        public static string ToNumbersOnly(this string text)
            => string.IsNullOrEmpty(text)
                ? string.Empty
                : new string(text.Where(char.IsDigit).ToArray());
    }
}
