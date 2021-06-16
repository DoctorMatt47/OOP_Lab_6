using System.Collections.Generic;
using System.Threading.Tasks;
using OOP_Lab_6.Models;

namespace OOP_Lab_6.Services.Parsers
{
    /// <summary>
    /// Represents interface for stats parser
    /// </summary>
    public interface IStatsParser
    {
        /// <summary>
        /// Parses html page and returns enumerable of champion stats.
        /// </summary>
        /// <returns>Task that returns enumerable of champion stats.</returns>
        Task<IEnumerable<ChampionStats>> GetChampionStatsAsync();
    }
}
