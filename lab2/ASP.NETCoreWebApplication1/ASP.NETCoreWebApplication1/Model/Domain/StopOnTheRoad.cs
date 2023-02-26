namespace ASP.NETCoreWebApplication1.Model.Domain;

public class StopOnTheRoad
{
    public int Id { get; set; } // dbContext
    public string IsHavePavilion { get; set; }  = string.Empty;
    public string BusStopName { get; set; } = string.Empty;
    public double RangeFromStart { get; set; }
    public PlacementAlongTheRoad PlacementAlongTheRoad { get; set; } //dbContext
    public Road RoadId { get; set; } //dbContext
}
