using MediatR.Core.Abstractions;

namespace WebAPI.Queries.GetTraces;

public sealed record GetTracesQuery(int IMO) : IQuery<List<TraceViewModel>>;

public sealed record TraceViewModel
{
    public int TraceId { get; set; }
    public DateTime TimeStamp { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}