using System.Linq.Expressions;

namespace DataAccess.EF.Repositories.Parameters;

public struct ProjectedQueryParams<TEntity, TProjected>
{
    public int Skip { get; init; }
    public int Take { get; init; }
    public OrderBy OrderBy { get; init; }
    public string OrderByProperty { get; init; }
    public Expression<Func<TEntity, bool>> WherePredicate { get; init; }
    public Expression<Func<TEntity, TProjected>> Projection { get; init; }
}