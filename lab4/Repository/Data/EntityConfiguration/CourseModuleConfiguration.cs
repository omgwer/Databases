using Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Data.EntityConfiguration;

public class CourseModuleConfiguration : IEntityTypeConfiguration<CourseModule>
{
    public void Configure( EntityTypeBuilder<CourseModule> builder )
    {
        builder.HasKey( x => x.ModuleId );
        builder.HasMany(x => x.Courses).WithOne();
        builder.Property(x => x.IsRequired);
        builder.Property( x => x.CreatedAt );
        builder.Property( x => x.UpdatedAt );
        builder.Property( x => x.DeletedAt );
    }
}