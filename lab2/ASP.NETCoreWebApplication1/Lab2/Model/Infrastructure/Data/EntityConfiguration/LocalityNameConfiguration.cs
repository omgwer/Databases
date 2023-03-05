using Lab2.Model.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lab2.Model.Infrastructure.EntityConfiguration;

public class LocalityNameConfiguration : IEntityTypeConfiguration<LocalityName>
{
    public void Configure(EntityTypeBuilder<LocalityName> builder)
    {
        builder.HasKey(x => x.Id).HasName("id");
        builder.Property(x => x.Locality).HasColumnName("locality_name");
    }
}