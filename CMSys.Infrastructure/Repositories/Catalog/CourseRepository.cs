using System.Linq.Expressions;
using CMSys.Common;
using CMSys.Common.Paging;
using CMSys.Core.Entities.Catalog;
using CMSys.Core.Exceptions;
using CMSys.Core.Repositories.Catalog;
using CMSys.Infrastructure.Helpers;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CMSys.Infrastructure.Repositories.Catalog;

internal sealed class CourseRepository : Repository<Course, Guid>, ICourseRepository
{
    internal CourseRepository(DbContext context) : base(context)
    {
    }

    public PagedList<Course> GetPagedList(PageInfo pageInfo, Expression<Func<Course, bool>> predicate = null)
    {
        Check.ArgumentNotNull(pageInfo, nameof(pageInfo));

        try
        {
            var query = MakeInclusions();
            if (predicate is not null)
            {
                query = query.Where(predicate);
            }

            var items = query.OrderBy(x => x.CourseGroup.VisualOrder)
                .ThenBy(x => x.VisualOrder)
                .SelectPage(pageInfo).ToList();
            var total = query.Count();

            return new PagedList<Course>(items, total, pageInfo);
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
    protected override IEnumerable<Course> InternalFilter(Expression<Func<Course, bool>> predicate)
    {
        return MakeInclusions().Where(predicate)
            .OrderBy(x => x.CourseGroup.VisualOrder)
            .ThenBy(x => x.VisualOrder)
            .ToList();
    }

    protected override IQueryable<Course> MakeInclusions()
    {
        return DbSet.Include(x => x.CourseType)
            .Include(x => x.CourseGroup)
            .Include(x => x.Trainers.OrderBy(t => t.VisualOrder))
            .ThenInclude(x => x.Trainer)
            .ThenInclude(x => x.User);
    }
}