namespace Lab2.Model.Domain;

public class StopOnTheRoad
{
    public int Id { get; set; } // dbContext
    public string? IsHavePavilion { get; set; }  = string.Empty;
    public string? BusStopName { get; set; } = string.Empty;
    public double? RangeFromStart { get; set; }
   // public PlacementAlongTheRoad PlacementAlongTheRoad { get; set; } //dbContext
    public string? Placement { get; set; } = String.Empty;
    public string? StartPoint { get; set; }
    public string? FinishPoint { get; set; }
   // public Road Road { get; set; } //dbContext
}
