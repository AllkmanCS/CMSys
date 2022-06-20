using System.Linq.Expressions;
using CMSys.Core.Entities.Catalog;
using CMSys.Core.Repositories.Catalog;
using Microsoft.EntityFrameworkCore;

namespace CMSys.Infrastructure.Repositories.Catalog;

internal sealed class TrainerGroupRepository : Repository<TrainerGroup, Guid>, ITrainerGroupRepository
{
    internal TrainerGroupRepository(DbContext context) : base(context)
    {
    }

    protected override IEnumerable<TrainerGroup> InternalFilter(Expression<Func<TrainerGroup, bool>> predicate)
    {
        return DbSet.Where(predicate).OrderBy(x => x.VisualOrder).ToList();
    }
}