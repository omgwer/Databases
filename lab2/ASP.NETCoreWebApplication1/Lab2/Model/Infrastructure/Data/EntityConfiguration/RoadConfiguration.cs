using Lab2.Model.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lab2.Model.Infrastructure.Data.EntityConfiguration;

public class RoadConfiguration : IEntityTypeConfiguration<Road>
{
    public void Configure(EntityTypeBuilder<Road> builder)
    {
        builder.HasKey(x => x.Id).HasName("id");

        builder
            .HasOne(x => x.StartPoint)
            .WithMany().HasConstraintName("start_point");
        builder
            .HasOne(x => x.FinishPoint)
            .WithMany().HasConstraintName("finish_point");
    }
}