namespace DataAccess.EF.Models.Base;

public abstract class Modifiable<TId> : Creatable<TId>, IModifiable
{
    public DateTime Modified { get; set; }
}