using System.Linq.Expressions;
using YoutubeData.Domain.Entities.Base;
using YoutubeData.Domain.Interfaces.Repositories.Base;
using YoutubeData.Domain.Interfaces.Services.Base;

namespace YoutubeData.Domain.Services.Base;

public class DomainService<TEntity> : IDomainService<TEntity> where TEntity : Entity
{
    private readonly IRepository<TEntity> _repository;

    public int CountResult { get; set; }

    public DomainService(IRepository<TEntity> repository)
    {
        _repository = repository;
    }

    public void Create(TEntity entity)
    {
        _repository.Create(entity);
    }

    public void CreateAll(List<TEntity> entities)
    {
        _repository.CreateAll(entities);
    }

    public async Task<IEnumerable<TEntity>> List()
    {
        return await Task.FromResult(_repository.List());
    }

    public async Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> filters)
    {
        return await Task.FromResult(_repository.Search(filters));
    }

    public async Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> filters, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null)
    {
        return await Task.FromResult(_repository.Search(filters, orderBy));
    }

    public async Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> filters, int? page, int? pagesize)
    {
        return await Task.FromResult(_repository.Search(filters, page, pagesize));
    }

    public async Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> filters, int? page, int? pagesize, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null)
    {
        return await Task.FromResult(_repository.Search(filters, page, pagesize, orderBy));
    }

    public async Task<TEntity?> GetById(long id)
    {
        return await Task.FromResult(_repository.GetById(id));
    }

    public int Count(Expression<Func<TEntity, bool>> filters)
    {
        return _repository.Count(filters);
    }

    public int GetPageSize()
    {
        return _repository.GetPageSize();
    }

    public void Update(TEntity entity)
    {
        _repository.Update(entity);
    }

    public void Remove(TEntity entity)
    {
        _repository.Remove(entity);
    }
}
