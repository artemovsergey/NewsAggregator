namespace NewsAggregator.API.Controllers;

public class ApiResult<T>
{

    public List<T> Data { get; private set; }
    public int PageIndex { get; private set; }
    public int PageSize { get; private set; }
    public int TotalCount { get; private set; }
    public int TotalPages { get; private set; }
    public bool HasPreviousPage
    {
        get
        {
            return (PageIndex > 0);
        }
    }
    public bool HasNextPage
    {
        get
        {
            return ((PageIndex + 1) < TotalPages);
        }
    }
    public string? SortColumn { get; set; }
    public string? SortOrder { get; set; }
    public string FilterColumn { get; set; }
    public string FilterQuery { get; set; }

    public ApiResult(List<T> data,
                     int count,
                     int pageIndex,
                     int pageSize,
                     string? sortColumn,
                     string? sortOrder,
                     string filterColumn,
                     string filterQuery)
    {
        Data = data;
        PageIndex = pageIndex;
        PageSize = pageSize;
        TotalCount = count;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        SortColumn = sortColumn;
        SortOrder = sortOrder;
        FilterColumn = filterColumn;
        FilterQuery = filterQuery;
    }
}
