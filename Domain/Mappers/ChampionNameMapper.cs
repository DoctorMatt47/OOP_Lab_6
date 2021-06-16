using System.Linq;

namespace OOP_Lab_6.Domain.Mappers
{
    /// <summary>
    /// Represents method for name map for the champions.
    /// </summary>
    public static class ChampionNameMapper
    {
        /// <summary>
        /// Gets name of the image of champion.
        /// Transfers champion name to the champion image name.
        /// </summary>
        /// <param name="name">Champion name to be transfered</param>
        /// <returns></returns>
        public static string GetImageName(string name)
        {
            if (name == "Wukong")
                return "MonkeyKing";
            var charsToRemove = new string[] { ".", " " };
            foreach (var c in charsToRemove)
            {
                name = name.Replace(c, string.Empty);
            }
            if (name.Contains("\'"))
            {
                name = name.Replace("\'", string.Empty);
                name = name.ToLower();
                name = name.First().ToString().ToUpper() + name[1..];
            }
            var index = name.IndexOf("&");
            if (index != -1)
            {
                name = name.Remove(index);
            }
            return name;
        }
    }
}
