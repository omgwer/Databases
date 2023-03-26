using Microsoft.EntityFrameworkCore;
using Repository.Data.EntityConfiguration;

namespace Repository.Data;

public class CourseDbContext : DbContext
{
    public CourseDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CourseConfiguration());
        modelBuilder.ApplyConfiguration(new CourseModuleConfiguration());
        modelBuilder.ApplyConfiguration(new CourseModuleStatusConfiguration());
        modelBuilder.ApplyConfiguration(new CourseStatusConfiguration());
        // для каждой таблицы добавлять в билдер конфиг.
    }
}