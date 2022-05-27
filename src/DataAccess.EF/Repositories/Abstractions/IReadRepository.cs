using System.Linq.Expressions;
using DataAccess.EF.Repositories.Parameters;

namespace DataAccess.EF.Repositories.Abstractions;

public interface IReadRepository<TEntity> where TEntity : class
{
    Task<List<TEntity>> GetAllAsync(
        QueryParams<TEntity> viewQueryParams,
        CancellationToken cancellationToken = default);

    Task<List<TProjected>> GetAllAsync<TProjected>(
        ProjectedQueryParams<TEntity, TProjected> viewQueryParams,
        CancellationToken cancellationToken = default);

    Task<int> CountAsync(
        Expression<Func<TEntity, bool>> wherePredicate,
        CancellationToken cancellationToken = default);
}