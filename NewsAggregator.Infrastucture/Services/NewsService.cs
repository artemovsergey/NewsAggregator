using NewsAggregator.Application.Interfaces;
using NewsAggregator.Domen.Models;
using System.Net;
using System.Security.Cryptography;
using System.ServiceModel.Syndication;
using System.Text;
using System.Xml;

namespace NewsAggregator.Infrastucture.Services;

public class NewsService : INewsService
{

    private readonly INewsRepository _repository;
    public NewsService(INewsRepository repository)
    {
        _repository = repository;      
    }

    public async Task FetchNews(string url)
    {

        var feed = Fetch(url);

        foreach (var item in feed.Items)
        {
            var news = new News()
            {
                Title = item.Title.Text,
                Content = item.Summary.Text,
                DatePublic = item.PublishDate.Date,
                Link = item.Links.First().Uri.ToString(),
                Hash = await HashNews(item.Title.Text)
            };

            if (!await _repository.ExistNews(news.Hash))
            {
                await _repository.SaveNews(news);
            }
            else
            {
                Console.WriteLine("Такая новость уже есть!");
            }
        }
    }
    public async Task<string> HashNews(string title)
    {
        // Реализация хеширования пароля с использованием MD5
        using (MD5 md5 = MD5.Create())
        {
            byte[] inputBytes = Encoding.ASCII.GetBytes(title);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Конвертируем байты обратно в строку
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }

    private SyndicationFeed Fetch(string url)
    {

        using (var client = new WebClient())
        {
            var rssData = client.DownloadString(url);
            using (var reader = XmlReader.Create(new StringReader(rssData)))
            {
                var feed = SyndicationFeed.Load(reader);
                return feed;
            }
        }
    }

    public async Task<IEnumerable<News>> GetNews(int pageIndex,
                                                 int pageSize,
                                                 string sortColumn,
                                                 string sortOrder,
                                                 string filterColumn,
                                                 string filterQuery)
    {
        return await _repository.GetNews(pageIndex,
                                         pageSize,
                                         sortColumn,
                                         sortOrder,
                                         filterColumn,
                                         filterQuery);
    }

}
