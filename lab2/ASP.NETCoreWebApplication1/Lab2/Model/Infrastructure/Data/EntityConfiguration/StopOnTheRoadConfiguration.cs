using Lab2.Model.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lab2.Model.Infrastructure.EntityConfiguration;

public class StopOnTheRoadConfiguration : IEntityTypeConfiguration<StopOnTheRoad>
{
    public void Configure(EntityTypeBuilder<StopOnTheRoad> builder)
    {
        builder.HasKey(x => x.Id).HasName("id");
        builder.Property(x => x.IsHavePavilion).HasColumnName("is_have_pavilion");
        builder.Property(x => x.BusStopName).HasColumnName("bus_stop_name");
        builder.Property(x => x.RangeFromStart).HasColumnName("range_from_start");

        // builder
        //     .HasOne(x => x.PlacementAlongTheRoad)
        //     .WithMany().HasConstraintName("placement_along_the_road");
        // builder
        //     .HasOne(x => x.Road)
        //     .WithMany().HasConstraintName("road");
        builder.Property(x => x.StartPoint).HasColumnName("start_point");
        builder.Property(x => x.FinishPoint).HasColumnName("finish_point");
        builder.Property(x => x.Placement).HasColumnName("placement_along_the_road");
    }
}
