using System.Linq.Expressions;
using CMSys.Common;
using CMSys.Common.Paging;
using CMSys.Core.Entities.Membership;
using CMSys.Core.Exceptions;
using CMSys.Core.Repositories.Membership;
using CMSys.Infrastructure.Helpers;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CMSys.Infrastructure.Repositories.Membership;

internal sealed class UserRepository : Repository<User, Guid>, IUserRepository
{
    internal UserRepository(DbContext context) : base(context)
    {
    }

    public User FindByEmail(string email)
    {
        Check.ArgumentNotNull(email, nameof(email));

        email = email.ToUpper();
        return Find(x => x.Email.ToUpper() == email);
    }

    public PagedList<User> GetPagedList(PageInfo pageInfo, string filter, Expression<Func<User, bool>> predicate = null)
    {
        Check.ArgumentNotNull(pageInfo, nameof(pageInfo));

        try
        {
            var query = MakeInclusions();
            if (predicate is not null)
            {
                query = query.Where(predicate);
            }

            query = ApplyFilter(query, filter);
            var items = query.SelectPage(pageInfo).ToList();
            var total = query.Count();

            return new PagedList<User>(items, total, pageInfo);
        }
        catch (SqlException ex)
        {
            throw ex.ToRepositoryException();
        }
        catch (Exception ex)
        {
            throw new RepositoryException(ex.Message, ex);
        }
    }

    private static IQueryable<User> ApplyFilter(IQueryable<User> query, string filter)
    {
        query = query.OrderBy(x => x.FirstName).ThenBy(x => x.LastName);

        if (string.IsNullOrEmpty(filter))
        {
            return query;
        }

        filter = filter.ToUpper();

        if (filter.Contains(' '))
        {
            var parts = filter.Split(" ");
            var first = parts[0];
            var second = parts[1];
            return query.Where(x => x.FirstName.ToUpper().Contains(first) && x.LastName.ToUpper().Contains(second));
        }

        return query.Where(x => x.FirstName.ToUpper().Contains(filter) || x.LastName.ToUpper().Contains(filter));
    }

    protected override IQueryable<User> MakeInclusions()
    {
        return DbSet.Include(x => x.Roles);
    }
}