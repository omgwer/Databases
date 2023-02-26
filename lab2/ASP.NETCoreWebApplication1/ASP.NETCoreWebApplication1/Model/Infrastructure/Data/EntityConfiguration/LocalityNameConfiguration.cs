using ASP.NETCoreWebApplication1.Model.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASP.NETCoreWebApplication1.Model.Infrastructure.EntityConfiguration;

public class LocalityNameConfiguration : IEntityTypeConfiguration<LocalityName>
{
    public void Configure(EntityTypeBuilder<LocalityName> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Locality);
    }
}