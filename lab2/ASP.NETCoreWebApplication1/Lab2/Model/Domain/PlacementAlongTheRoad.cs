using System.ComponentModel.DataAnnotations.Schema;

namespace Lab2.Model.Domain;

public class PlacementAlongTheRoad
{
    [Column("id")] public int? Id { get; set; }
    [Column("placement_along_the_road")] public string Placement { get; set; } = String.Empty;
}