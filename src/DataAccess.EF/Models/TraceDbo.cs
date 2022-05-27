using DataAccess.EF.Models.Base;

namespace DataAccess.EF.Models;

public class TraceDbo : Modifiable<int>
{
    public VesselDbo Vessel { get; set; }
    public DateTime TimeStamp { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}