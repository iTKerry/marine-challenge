namespace WebAPP.Data.API;

public sealed record AddTraceRequest
{
    public int IMO { get; init; }            
    public string VesselName { get; init; }  
    public DateTime TimeStamp { get; init; } 
    public TracePoint Point { get; init; }
}

public sealed record UpdateTraceRequest
{
    public DateTime TimeStamp { get; init; } 
    public TracePoint Point { get; init; }
}

public sealed record TracePoint
{
    public double Lat { get; init; }    
    public double Lon { get; init; }   
}

public sealed record UpdateVesselRequest
{
    public string Name { get; init; }
}