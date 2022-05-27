using System.Linq.Expressions;

namespace DataAccess.EF.Repositories.Parameters;

public struct QueryParams<TEntity>
{
    public int Skip { get; private set; }
    public int Take { get; private set; }
    public OrderBy OrderBy { get; private set; }
    public string OrderByProperty { get; private set; }
    public Expression<Func<TEntity, bool>> WherePredicate { get; private set; }
}