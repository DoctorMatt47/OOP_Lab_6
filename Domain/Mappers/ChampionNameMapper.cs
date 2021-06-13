using System.Linq;

namespace OOP_Lab_6.Domain.Mappers
{
    public static class ChampionNameMapper
    {
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
