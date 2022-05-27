namespace DataAccess.EF.Models.Base;

public abstract class Creatable<TId> : DataEntity<TId>, ICreatable
{
    public DateTime Created { get; set; }
}