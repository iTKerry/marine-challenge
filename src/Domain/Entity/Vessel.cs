using Domain.Entity.Abstractions;
using Domain.ValueObjects;

namespace Domain.Entity;

public sealed record Vessel : Entity<IMO>
{
    public string Name { get; init; } = default!;
    public List<Trace> Traces { get; set; } = new();
}