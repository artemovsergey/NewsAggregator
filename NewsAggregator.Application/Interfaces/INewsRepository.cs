using NewsAggregator.Domen.Models;
namespace NewsAggregator.Application.Interfaces;
public interface INewsRepository
{
    Task<IEnumerable<News>> GetNews(int pageIndex,
                                    int pageSize,
                                    string sortColumn,
                                    string sortOrder,
                                    string filterColumn,
                                    string filterQuery);

    Task SaveNews(News n);
    Task<bool> ExistNews(string hash);
}
