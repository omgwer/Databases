using Lab2.Model.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lab2.Model.Infrastructure.EntityConfiguration;

public class PlacementAlongTheRoadConfiguration : IEntityTypeConfiguration<PlacementAlongTheRoad>
{
    public void Configure(EntityTypeBuilder<PlacementAlongTheRoad> builder)
    {
        builder.HasKey(x => x.Id).HasName("id");
        builder.Property(x => x.Placement).HasColumnName("placement_along_the_road");
    }
}