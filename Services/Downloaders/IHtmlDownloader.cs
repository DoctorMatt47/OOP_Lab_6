using System;
using System.Threading.Tasks;

namespace OOP_Lab_6.Services.Downloaders
{
    public interface IHtmlDownloader
    {
        Task<string> GetHtmlAsync(Uri uri);

        string GetHtml(Uri uri);
    }
}
