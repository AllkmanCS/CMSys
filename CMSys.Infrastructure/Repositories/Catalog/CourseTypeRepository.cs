using System.Linq.Expressions;
using CMSys.Core.Entities.Catalog;
using CMSys.Core.Repositories.Catalog;
using Microsoft.EntityFrameworkCore;

namespace CMSys.Infrastructure.Repositories.Catalog;

internal sealed class CourseTypeRepository : Repository<CourseType, Guid>, ICourseTypeRepository
{
    internal CourseTypeRepository(DbContext context) : base(context)
    {
    }

    protected override IEnumerable<CourseType> InternalFilter(Expression<Func<CourseType, bool>> predicate)
    {
        return DbSet.Where(predicate).OrderBy(x => x.VisualOrder).ToList();
    }
}