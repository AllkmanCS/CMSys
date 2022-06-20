namespace CMSys.Common.Paging;

public class PageInfo
{
    public int Page { get; }
    public int PerPage { get; }

    public PageInfo(int page = 1, int perPage = int.MaxValue)
    {
        Check.ArgumentSatisfies(page, x => x > 0, nameof(page) + " must be > 0.", nameof(page));
        Check.ArgumentSatisfies(perPage, x => x > 0, nameof(perPage) + " must be > 0.", nameof(perPage));

        Page = page;
        PerPage = perPage;
    }
}