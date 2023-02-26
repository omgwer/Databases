using Microsoft.EntityFrameworkCore;

namespace ASP.NETCoreWebApplication1.Model.Infrastructure.EntityConfiguration;

public class StopOnTheRoadDbContext : DbContext
{
    public StopOnTheRoadDbContext( DbContextOptions options ) : base( options )
    {
    }
    
    protected override void OnModelCreating( ModelBuilder modelBuilder )
    {
        modelBuilder.ApplyConfiguration( new LocalityNameConfiguration() );
        modelBuilder.ApplyConfiguration( new PlacementAlongTheRoadConfiguration() );
        modelBuilder.ApplyConfiguration( new RoadConfiguration() );
        modelBuilder.ApplyConfiguration( new StopOnTheRoadConfiguration() );

        // для каждой таблицы добавлять в билдер конфиг.
    }
}