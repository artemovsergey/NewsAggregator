using Microsoft.EntityFrameworkCore;
using NewsAggregator.Application.Interfaces;
using NewsAggregator.Domen.Models;
using System.Linq;
using System.Reflection;
using System.Linq.Dynamic.Core;

namespace NewsAggregator.Infrastucture.Data;

public class NewsRepository : INewsRepository
{
    private readonly NewsAggregationContext _db;
    public NewsRepository(NewsAggregationContext db)
    {
        _db = db;            
    }

    public Task DeleteNews()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ExistNews(string hash)
    {
        var news = await _db.NewsList.Where(n => n.Hash == hash).FirstOrDefaultAsync();
        
        return news != null ? true : false;

    }

    public async Task<IEnumerable<News>> GetNews(int pageIndex,
                                                int pageSize,
                                                string sortColumn,
                                                string sortOrder,
                                                string filterColumn,
                                                string filterQuery)
    {

        if (!string.IsNullOrEmpty(sortColumn) && IsValidProperty(sortColumn))
        {
            sortOrder = !string.IsNullOrEmpty(sortOrder) && sortOrder.ToUpper() == "ASC"
            ? "ASC"
            : "DESC";
        }

        IQueryable<News> newsList = _db.NewsList;

        if (!string.IsNullOrEmpty(filterColumn)
            && !string.IsNullOrEmpty(filterQuery)
            && IsValidProperty(filterColumn))
        {
            //users = users.Where(u => $"{filterColumn}".Contains($"{filterQuery})"));
            newsList = newsList.Where($"{filterColumn}.Contains(@0)", filterQuery);

            Console.WriteLine($"Фильтрация: {newsList.Count()}");
        }

        return await newsList.OrderBy($"{sortColumn} {sortOrder}")
                              .Skip(pageIndex * pageSize)
                              .Take(pageSize)

                              .ToListAsync();
    }

    public async Task SaveNews(News n)
    {
        _db.NewsList.Add(n);
        await _db.SaveChangesAsync();
    }

    public static bool IsValidProperty(string propertyName,
                                  bool throwExceptionIfNotFound = true)
    {
        var prop = typeof(News).GetProperty(
        propertyName,

        BindingFlags.IgnoreCase |
        BindingFlags.Public |
        BindingFlags.Instance);
        if (prop == null && throwExceptionIfNotFound)
            throw new NotSupportedException(
            string.Format($"ERROR: Property '{propertyName}' does not exist.")
         );
        return prop != null;

    }


}
