using CMSys.Core.Entities.Membership;
using CMSys.Core.Repositories.Membership;
using Microsoft.EntityFrameworkCore;

namespace CMSys.Infrastructure.Repositories.Membership;

internal sealed class RoleRepository : Repository<Role, Guid>, IRoleRepository
{
    internal RoleRepository(DbContext context) : base(context)
    {
    }
}