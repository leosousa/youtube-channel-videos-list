using System.Linq.Expressions;
using YoutubeData.Domain.Entities.Base;

namespace YoutubeData.Domain.Interfaces.Services.Base;

public interface IDomainService<TEntity> where TEntity : Entity
{
    int CountResult { get; set; }

    Task<TEntity> Create(TEntity entity);
    Task<List<TEntity>> CreateAll(List<TEntity> entities);
    IEnumerable<TEntity> ListAll();
    Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> filters);
    Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> filters, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);
    Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> filters, int? page, int? pagesize);
    Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> filters, int? page, int? pagesize, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);
    Task<TEntity> GetById(long id);
    int GetCount(Expression<Func<TEntity, bool>> filters);
    int GetPageSize();
    Task<TEntity> Update(TEntity entity);
    Task<TEntity> Remove(TEntity entity);
}
