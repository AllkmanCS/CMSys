using System.Collections.ObjectModel;

namespace CMSys.Common.Paging;

public class PagedList<T> : PagedListBase
{
    public IReadOnlyList<T> Items { get; }

    public int From => (Page - 1) * PerPage + 1;
    public int To => From + Items.Count - 1;

    public PagedList(IList<T> items, int total, int page = 1, int perPage = int.MaxValue) : this(items, total,
        new PageInfo(page, perPage))
    {
    }

    public PagedList(IList<T> items, int total, PageInfo pageInfo) : base(total, pageInfo)
    {
        Check.ArgumentNotNull(items, nameof(items));

        Items = new ReadOnlyCollection<T>(items);
    }

    public PagedList<TResult> Convert<TResult>(Func<T, TResult> func)
    {
        Check.ArgumentNotNull(func, nameof(func));

        return new PagedList<TResult>(Items.Select(func).ToList(), Total, Page, PerPage);
    }
}