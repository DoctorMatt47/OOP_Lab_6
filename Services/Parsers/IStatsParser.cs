using System.Collections.Generic;
using System.Threading.Tasks;
using OOP_Lab_6.Models;

namespace OOP_Lab_6.Services.Parsers
{
    public interface IStatsParser
    {
        Task<IEnumerable<ChampionStats>> GetChampionStatsAsync();
    }
}
