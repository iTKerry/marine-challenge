namespace DataAccess.EF.Models.Base;

public interface IModifiable : ICreatable
{
    public DateTime Modified { get; set; }
}