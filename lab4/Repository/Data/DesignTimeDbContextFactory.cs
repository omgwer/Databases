using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Repository.Data;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CourseDbContext>
{
    public CourseDbContext CreateDbContext(string[] args)
    {
        IConfigurationBuilder builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory().Replace("Repository", "Backend"))
            .AddJsonFile("appsettings.json");
        
        var config = builder.Build();
        var connectionString = config.GetConnectionString("DefaultConnection");
        var optionsBuilder = new DbContextOptionsBuilder<CourseDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new CourseDbContext(optionsBuilder.Options);
    }
}