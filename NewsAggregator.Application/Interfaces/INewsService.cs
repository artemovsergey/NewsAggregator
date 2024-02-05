using NewsAggregator.Domen.Models;

namespace NewsAggregator.Application.Interfaces;

public interface INewsService
{
    Task FetchNews(string url);
    Task<string> HashNews(string title);

    Task<IEnumerable<News>> GetNews(int pageIndex,
                                    int pageSize,
                                    string sortColumn,
                                    string sortOrder,
                                    string filterColumn,
                                    string filterQuery);
}

