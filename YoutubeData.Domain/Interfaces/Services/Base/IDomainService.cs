using System.Linq.Expressions;
using YoutubeData.Domain.Entities.Base;

namespace YoutubeData.Domain.Interfaces.Services.Base;

public interface IDomainService<TEntity> where TEntity : Entity
{
    int CountResult { get; set; }

    void Create(TEntity entity);
    void CreateAll(List<TEntity> entities);
    Task<IEnumerable<TEntity>> List();
    Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> filters);
    Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> filters, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null);
    Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> filters, int? page, int? pagesize);
    Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> filters, int? page, int? pagesize, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null);
    Task<TEntity?> GetById(long id);
    int Count(Expression<Func<TEntity, bool>> filters);
    int GetPageSize();
    void Update(TEntity entity);
    void Remove(TEntity entity);
}
