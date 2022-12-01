using AutoMapper;

namespace DynamoPackageService.Infrastructure;

public class PageModel<T>
{
    public PageModel()
    {

    }
    public PageModel(int pageIndex, int pageCount, int dataCount, int pageSize, IList<T> data)
    {
        Page = pageIndex;
        PageCount = pageCount;
        DataCount = dataCount;
        PageSize = pageSize;
        Data = data;
    }

    public int Page { get; set; }

    public int PageCount { get; set; }
  
    public int DataCount { get; set; }

    public int PageSize { get; set; }
    
    public IList<T> Data { get; set; }


    public PageModel<TResult> ConvertTo<TResult>()
    {
        return new PageModel<TResult>(Page, PageCount, DataCount, PageSize, default);
    }

    public PageModel<TResult> ConvertTo<TResult>(IMapper mapper)
    {
        var result = ConvertTo<TResult>();
        if (Data != null)
        {
            result.Data = mapper.Map<List<TResult>>(Data);
        }
        return result;
    }
}