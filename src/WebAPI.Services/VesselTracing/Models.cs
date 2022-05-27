namespace WebAPI.Services.VesselTracing;

public sealed record AddTraceDto
{
    public int IMO { get; init; }            
    public string VesselName { get; init; }  
    public DateTime TimeStamp { get; init; } 
    public TracePointDto Point { get; init; }
}

public sealed record UpdateTraceDto
{
    public DateTime TimeStamp { get; init; } 
    public TracePointDto Point { get; init; }
}

public sealed record TracePointDto
{
    public double Lat { get; init; }    
    public double Lon { get; init; }   
}

public record UpdateVesselDto(string Name);