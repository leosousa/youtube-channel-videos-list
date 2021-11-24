using System.Linq.Expressions;
using YoutubeData.Domain.Entities.Base;

namespace YoutubeData.Domain.Interfaces.Repositories.Base;

public interface IRepository<TEntity> where TEntity : Entity
{
    void Create(TEntity entity);
    void Createll(IEnumerable<TEntity> entities);
    TEntity GetById(int id);
    TEntity Get(Expression<Func<TEntity, bool>> filter);
    IEnumerable<TEntity> GetAll();
    IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter);
    IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter);
    int Count(Expression<Func<TEntity, bool>> filter = null);
    void Update(TEntity entity);
    void UpdateAll(IEnumerable<TEntity> entities);
    void Remove(TEntity entity);
    void RemoveAll(IEnumerable<TEntity> entities);
}
