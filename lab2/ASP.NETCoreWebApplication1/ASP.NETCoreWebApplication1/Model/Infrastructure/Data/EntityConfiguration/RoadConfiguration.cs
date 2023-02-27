using ASP.NETCoreWebApplication1.Model.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASP.NETCoreWebApplication1.Model.Infrastructure.EntityConfiguration;

public class RoadConfiguration : IEntityTypeConfiguration<Road>
{
    public void Configure(EntityTypeBuilder<Road> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasOne(x => x.StartPoint)
            .WithMany();
        builder
            .HasOne(x => x.FinishPoint)
            .WithMany();
    }
}