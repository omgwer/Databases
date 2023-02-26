using ASP.NETCoreWebApplication1.Model.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASP.NETCoreWebApplication1.Model.Infrastructure.EntityConfiguration;

public class PlacementAlongTheRoadConfiguration : IEntityTypeConfiguration<PlacementAlongTheRoad>
{
    public void Configure(EntityTypeBuilder<PlacementAlongTheRoad> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Placement);
    }
}