using DataAccess.EF.Models.Base;
using Domain.ValueObjects;

namespace DataAccess.EF.Models;

public class VesselDbo : Modifiable<IMO>
{
    public string Name { get; set; }
    
    public List<TraceDbo> Traces { get; set; }
}