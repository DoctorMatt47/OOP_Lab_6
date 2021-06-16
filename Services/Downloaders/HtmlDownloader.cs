using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace OOP_Lab_6.Services.Downloaders
{
    /// <summary>
    /// Represents html page downloader that uses HttpClient instance.
    /// </summary>
    public class HtmlDownloader : IHtmlDownloader
    {
        /// <summary>
        /// Gets html page of passed uri.
        /// </summary>
        /// <param name="uri">Uri of html page to be gotten.</param>
        /// <returns>Task that returns string with html code.</returns>
        public async Task<string> GetHtmlAsync(Uri uri)
        {
            using var client = new HttpClient();
            return await client.GetStringAsync(uri);
        }
    }
}
