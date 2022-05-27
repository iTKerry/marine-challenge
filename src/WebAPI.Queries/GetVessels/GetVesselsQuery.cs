using DataAccess.EF.Repositories.Parameters;
using MediatR.Core.Abstractions;

namespace WebAPI.Queries.GetVessels;

public sealed record GetVesselsQuery(
    int Take, 
    int Page, 
    string OrderProperty, 
    OrderBy OrderBy) : IQuery<List<VesselViewModel>>;

public sealed record VesselViewModel
{
    public int IMO { get; init; }
    public string Name { get; init; }
    public DateTime LastTimeStamp { get; init; }
}