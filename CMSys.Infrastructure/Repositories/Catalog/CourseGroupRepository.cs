using System.Linq.Expressions;
using CMSys.Core.Entities.Catalog;
using CMSys.Core.Repositories.Catalog;
using Microsoft.EntityFrameworkCore;

namespace CMSys.Infrastructure.Repositories.Catalog;

internal sealed class CourseGroupRepository : Repository<CourseGroup, Guid>, ICourseGroupRepository
{
    internal CourseGroupRepository(DbContext context) : base(context)
    {
    }

    protected override IEnumerable<CourseGroup> InternalFilter(Expression<Func<CourseGroup, bool>> predicate)
    {
        return DbSet.Where(predicate).OrderBy(x => x.VisualOrder).ToList();
    }
}