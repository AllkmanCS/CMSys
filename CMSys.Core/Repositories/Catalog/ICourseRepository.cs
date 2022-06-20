using System.Linq.Expressions;
using CMSys.Common.Paging;
using CMSys.Core.Entities.Catalog;

namespace CMSys.Core.Repositories.Catalog;

public interface ICourseRepository : IRepository<Course, Guid>
{
    PagedList<Course> GetPagedList(PageInfo pageInfo, Expression<Func<Course, bool>> predicate = null);
}