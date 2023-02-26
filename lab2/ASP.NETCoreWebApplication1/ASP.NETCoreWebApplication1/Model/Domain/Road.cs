namespace ASP.NETCoreWebApplication1.Model.Domain;

public class Road
{
    public int Id { get; set; } // dbContext
    public LocalityName StartPoint { get; set; }
    public LocalityName FinishPoint { get; set; }
}