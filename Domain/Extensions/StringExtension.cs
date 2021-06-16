using System.Linq;

namespace OOP_Lab_6.Domain.Extensions
{
    /// <summary>
    /// Represents extension methods for string
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// Cleans string of non digits.
        /// </summary>
        /// <param name="str">This string.</param>
        /// <returns>String only with digits and dots.</returns>
        public static string CleanOfNonDigits(this string str)
        {
            if (string.IsNullOrEmpty(str)) return str;
            string cleaned = new string(str.Where(x => char.IsDigit(x) || x == '.').ToArray());
            return cleaned;
        }

        /// <summary>
        /// Cuts last n characters of string.
        /// </summary>
        /// <param name="str">This string.</param>
        /// <param name="countOfChars">Count of characters to be cut.</param>
        /// <returns>String without last n characters.</returns>
        public static string CutOffEnd(this string str, int countOfChars)
        {
            if (string.IsNullOrEmpty(str)) return str;
            string cleaned = new string(str.Substring(0, str.Length - countOfChars).ToArray());
            return cleaned;
        }

        /// <summary>
        /// Transforms html codes to characters.
        /// </summary>
        /// <param name="str">This string</param>
        /// <returns>String without html codes.</returns>
        public static string ParseHtmlCodes(this string str)
        {
            if (string.IsNullOrEmpty(str)) return str;
            var found = str.IndexOf("&#x27;");
            if (found != -1)
            {
                str = str.Remove(found, 6).Insert(found, "\'");
            }
            return str;
        }
    }
}
