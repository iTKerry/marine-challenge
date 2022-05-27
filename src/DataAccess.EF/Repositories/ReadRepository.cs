using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using DataAccess.EF.Repositories.Abstractions;
using DataAccess.EF.Repositories.Parameters;

namespace DataAccess.EF.Repositories;

public sealed class ReadRepository<TEntity> : IReadRepository<TEntity> where TEntity : class
{
    private readonly DbContext _dbContext;

    public ReadRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<TEntity>> GetAllAsync(
        QueryParams<TEntity> viewQueryParams, 
        CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Set<TEntity>()
            .AsQueryable()
            .Where(viewQueryParams.WherePredicate);

        var orderedProp = viewQueryParams.OrderByProperty;
        query = viewQueryParams.OrderBy switch
        {
            OrderBy.None => query,
            OrderBy.Descending => query.OrderBy($"{orderedProp} desc"),
            OrderBy.Ascending => query.OrderBy(orderedProp),
            _ => throw new ArgumentOutOfRangeException(nameof(viewQueryParams.OrderBy))
        };

        if (viewQueryParams.Skip > 0) 
            query = query.Skip(viewQueryParams.Skip);

        if (viewQueryParams.Take > 0) 
            query = query.Take(viewQueryParams.Skip);

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<List<TProjected>> GetAllAsync<TProjected>(
        ProjectedQueryParams<TEntity, TProjected> viewQueryParams, 
        CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Set<TEntity>()
            .AsQueryable()
            .Where(viewQueryParams.WherePredicate);
            
        var projected = query.Select(viewQueryParams.Projection);
                
        var orderedProp = viewQueryParams.OrderByProperty;
        projected = viewQueryParams.OrderBy switch
        {
            OrderBy.None => projected,
            OrderBy.Descending => projected.OrderBy($"{orderedProp} desc"),
            OrderBy.Ascending => projected.OrderBy(orderedProp),
            _ => throw new ArgumentOutOfRangeException(nameof(viewQueryParams.OrderBy))
        };

        if (viewQueryParams.Skip > 0) 
            projected = projected.Skip(viewQueryParams.Skip);

        if (viewQueryParams.Take > 0) 
            projected = projected.Take(viewQueryParams.Take);

        return await projected.ToListAsync(cancellationToken);
    }

    public async Task<int> CountAsync(
        Expression<Func<TEntity, bool>> wherePredicate, 
        CancellationToken cancellationToken = default)
    {
        var result = await _dbContext.Set<TEntity>()
            .Where(wherePredicate)
            .CountAsync(cancellationToken);
                
        return result;
    }
}