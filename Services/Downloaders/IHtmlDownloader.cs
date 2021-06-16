using System;
using System.Threading.Tasks;

namespace OOP_Lab_6.Services.Downloaders
{
    /// <summary>
    /// Represents html page downloader.
    /// </summary>
    public interface IHtmlDownloader
    {
        /// <summary>
        /// Gets html page of passed uri.
        /// </summary>
        /// <param name="uri">Uri of html page to be gotten.</param>
        /// <returns>Task that returns string with html code.</returns>
        Task<string> GetHtmlAsync(Uri uri);
    }
}
