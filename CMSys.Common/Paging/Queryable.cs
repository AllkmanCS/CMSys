namespace CMSys.Common.Paging;

public static class Queryable
{
    public static IQueryable<T> SelectPage<T>(this IQueryable<T> query, PageInfo pageInfo)
    {
        Check.ArgumentNotNull(query, nameof(query));
        Check.ArgumentNotNull(pageInfo, nameof(pageInfo));

        return query.Skip((pageInfo.Page - 1) * pageInfo.PerPage).Take(pageInfo.PerPage);
    }
}