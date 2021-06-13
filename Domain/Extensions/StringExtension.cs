using System.Linq;

namespace OOP_Lab_6.Domain.Extensions
{
    public static class StringExtension
    {
        public static string CleanOfNonDigits(this string str)
        {
            if (string.IsNullOrEmpty(str)) return str;
            string cleaned = new string(str.Where(x => char.IsDigit(x) || x == '.').ToArray());
            return cleaned;
        }

        public static string CutOffEnd(this string str, int countOfChars)
        {
            if (string.IsNullOrEmpty(str)) return str;
            string cleaned = new string(str.Substring(0, str.Length - countOfChars).ToArray());
            return cleaned;
        }

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
