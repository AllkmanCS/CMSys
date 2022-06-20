using System.Linq.Expressions;
using CMSys.Core.Entities.Catalog;
using CMSys.Core.Repositories.Catalog;
using Microsoft.EntityFrameworkCore;

namespace CMSys.Infrastructure.Repositories.Catalog;

internal sealed class TrainerRepository : Repository<Trainer, Guid>, ITrainerRepository
{
    internal TrainerRepository(DbContext context) : base(context)
    {
    }

    protected override IEnumerable<Trainer> InternalFilter(Expression<Func<Trainer, bool>> predicate)
    {
        return MakeInclusions().Where(predicate)
            .OrderBy(x => x.TrainerGroup.VisualOrder)
            .ThenBy(x => x.VisualOrder)
            .ToList();
    }

    protected override IQueryable<Trainer> MakeInclusions()
    {
        return DbSet.Include(x => x.User).Include(x => x.TrainerGroup);
    }
}