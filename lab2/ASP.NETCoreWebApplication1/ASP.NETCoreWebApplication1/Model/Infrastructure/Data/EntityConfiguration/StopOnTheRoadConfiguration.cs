using ASP.NETCoreWebApplication1.Model.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASP.NETCoreWebApplication1.Model.Infrastructure.EntityConfiguration;

public class StopOnTheRoadConfiguration : IEntityTypeConfiguration<StopOnTheRoad>
{
    public void Configure(EntityTypeBuilder<StopOnTheRoad> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.IsHavePavilion);
        builder.Property(x => x.BusStopName);
        builder.Property(x => x.RangeFromStart);
        builder.Property(x => x.PlacementAlongTheRoad);
        builder.Property(x => x.RoadId);
    }
}
