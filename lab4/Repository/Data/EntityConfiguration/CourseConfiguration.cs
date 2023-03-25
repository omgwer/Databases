using Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Data.EntityConfiguration;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure( EntityTypeBuilder<Course> builder )
    {
        builder.HasKey( x => x.CourseId );
        builder.Property( x => x.CreatedAt );
        builder.Property( x => x.UpdatedAt );
        builder.Property( x => x.DeletedAt );
    }
}