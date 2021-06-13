using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace OOP_Lab_6.Services.Downloaders
{
    public class HtmlDownloader : IHtmlDownloader
    {
        public async Task<string> GetHtmlAsync(Uri uri)
        {
            var client = new HttpClient();
            return await client.GetStringAsync(uri);
        }

        public string GetHtml(Uri uri)
        {
            var client = new HttpClient();
            return client.GetStringAsync(uri).Result;
        }
    }
}
