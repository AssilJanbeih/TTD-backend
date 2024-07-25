using System.Collections.Generic;

namespace Domain.Paging;

public class PagingResponse<T> where T : class
{
    public PagingResponse(int pageIndex, int pageSize, int count, IReadOnlyList<T> data)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        Count = count;
        Data = data;
    }

    public int PageIndex { get; set; }

    public int PageSize { get; set; }

    public int Count { get; set; }

    public IReadOnlyList<T> Data { get; set; }
}