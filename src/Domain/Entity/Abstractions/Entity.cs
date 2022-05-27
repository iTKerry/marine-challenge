namespace Domain.Entity.Abstractions;

public abstract record Entity<TId>
{
    public TId Id { get; set; } = default!;
}