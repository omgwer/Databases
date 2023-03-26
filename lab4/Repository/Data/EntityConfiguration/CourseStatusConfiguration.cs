using Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Data.EntityConfiguration;

public class CourseStatusConfiguration : IEntityTypeConfiguration<CourseStatus>
{
    public void Configure(EntityTypeBuilder<CourseStatus> builder)
    {
        builder.HasKey( x => x.EnrollmentId );
        builder.HasOne(x => x.Course).WithMany();
        builder.Property( x => x.Progress );
        builder.Property( x => x.Duration );
        builder.Property( x => x.DeletedAt );
    }
}