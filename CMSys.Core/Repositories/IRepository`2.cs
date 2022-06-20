using CMSys.Core.Entities;

namespace CMSys.Core.Repositories;

public interface IRepository<TEntity, in TKey> : IRepository<TEntity> where TEntity : Entity<TKey>
{
    TEntity Find(TKey id);
}