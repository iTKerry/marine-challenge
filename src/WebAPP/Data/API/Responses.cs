using System.Text.Json.Serialization;

namespace WebAPP.Data.API;

public sealed class Vessel
{
    public int IMO { get; set; }
    public string Name { get; set; }
    public DateTime LastTimeStamp { get; set; }
    
    [JsonIgnore]
    public bool IsEditing { get; set; }
}

public sealed class Trace
{
    public int TraceId { get; set; }
    public DateTime TimeStamp { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    
    [JsonIgnore]
    public bool IsEditing { get; set; }
}
