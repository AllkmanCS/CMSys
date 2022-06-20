using CMSys.Core.Entities;
using CMSys.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CMSys.Infrastructure.Repositories;

internal abstract class Repository<TEntity, TKey> : Repository<TEntity>, IRepository<TEntity, TKey>
    where TEntity : Entity<TKey>
{
    protected Repository(DbContext context) : base(context)
    {
    }

    public virtual TEntity Find(TKey id) => Find(x => x.Id.Equals(id));
}