using System.Linq.Expressions;
using CMSys.Common.Paging;
using CMSys.Core.Entities.Membership;

namespace CMSys.Core.Repositories.Membership;

public interface IUserRepository : IRepository<User, Guid>
{
    User FindByEmail(string email);
    PagedList<User> GetPagedList(PageInfo pageInfo, string filter, Expression<Func<User, bool>> predicate = null);
}