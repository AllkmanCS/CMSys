using System.Linq.Expressions;
using CMSys.Core.Entities;

namespace CMSys.Core.Repositories;

public interface IRepository<TEntity> : IRepository where TEntity : Entity
{
    TEntity Find(Expression<Func<TEntity, bool>> predicate);
    IEnumerable<TEntity> All();
    IEnumerable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate);

    void Add(TEntity entity);
    void Remove(TEntity entity);
}