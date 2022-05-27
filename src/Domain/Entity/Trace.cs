using Domain.Entity.Abstractions;
using Domain.ValueObjects;

namespace Domain.Entity;

public sealed record Trace : Entity<int>
{
    public DateTime TimeStamp { get; init; }
    public Coordinates Point { get; init; }
}