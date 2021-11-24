using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using YoutubeData.Domain.Entities.Base;
using YoutubeData.Domain.Interfaces.Repositories.Base;
using YoutubeData.Infra.Data.Contexts;

namespace YoutubeData.Infra.Data.Repositories.Base;

public class Repository<TDatabaseContext, TEntity> : IRepository<TEntity>
        where TDatabaseContext : YoutubeDataContext
        where TEntity : Entity
{
    protected readonly YoutubeDataContext _database;

    public Repository(TDatabaseContext database)
    {
        _database = database;
    }

    public int Count(Expression<Func<TEntity, bool>>? filter = null)
    {
        throw new NotImplementedException();
    }

    public void Create(TEntity entity)
    {
        _database.Set<TEntity>().Add(entity);
        _database.SaveChanges();
    }

    public virtual void CreateAll(IEnumerable<TEntity> entities)
    {
        throw new NotImplementedException();
    }

    public virtual TEntity? Get(Expression<Func<TEntity, bool>> filter)
    {
        return _database.Set<TEntity>().FirstOrDefault(filter);
    }

    public virtual TEntity? GetById(long id)
    {
        return _database.Set<TEntity>().Find(id);
    }

    public virtual int GetPageSize()
    {
        throw new NotImplementedException();
    }

    public virtual IEnumerable<TEntity> List()
    {
        return _database.Set<TEntity>().ToList();
    }

    public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter)
    {
        return _database.Set<TEntity>().Where(filter);
    }

    public virtual void Remove(TEntity entity)
    {
        _database.Set<TEntity>().Remove(entity);
        _database.SaveChanges();
    }

    public virtual void RemoveAll(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            _database.Set<TEntity>().Remove(entity);
        }
        _database.SaveChanges();
    }

    public virtual IEnumerable<TEntity> Search(Expression<Func<TEntity, bool>> filters)
    {
        return _database.Set<TEntity>().Where(filters).ToList();
    }

    public virtual IEnumerable<TEntity> Search(Expression<Func<TEntity, bool>> filters, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null)
    {
        var query = this.Query(filters);

        try
        {
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query;
        }
        catch (Exception)
        {
            return query;
        }
    }

    public virtual IEnumerable<TEntity> Search(Expression<Func<TEntity, bool>> filters, int? page, int? pagesize)
    {
        return this.Search(filters, page, pagesize, o => o.OrderBy(e => e.Id));
    }

    public virtual IEnumerable<TEntity> Search(Expression<Func<TEntity, bool>> filters, int? page, int? pagesize, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null)
    {
        var query = this.Query(filters);

        if (orderBy != null)
        {
            query = orderBy(query);
        }
        
        if ((page.HasValue && page > 0) && (pagesize.HasValue && pagesize > 0))
        {
            query = query.Skip((page.Value - 1) * pagesize.Value).Take(pagesize.Value);
        }

        return query;
    }

    public virtual void Update(TEntity entity)
    {
        _database.Entry(entity).State = EntityState.Modified;
        _database.SaveChanges();
    }

    public virtual void UpdateAll(IEnumerable<TEntity> entities)
    {
        foreach (var obj in entities)
        {
            _database.Entry(obj).State = EntityState.Modified;
        }
        _database.SaveChanges();
    }
}
