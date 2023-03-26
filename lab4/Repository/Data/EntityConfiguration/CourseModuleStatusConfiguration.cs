using Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Data.EntityConfiguration;

public class CourseModuleStatusConfiguration : IEntityTypeConfiguration<CourseModuleStatus>
{
    public void Configure(EntityTypeBuilder<CourseModuleStatus> builder)
    {
        builder.HasKey( x => x.EnrollmentId );
        builder.HasKey(x => x.CourseModuleId);
        builder.Property( x => x.Progress );
        builder.Property( x => x.Duration );
        builder.Property( x => x.DeletedAt );
    }
}