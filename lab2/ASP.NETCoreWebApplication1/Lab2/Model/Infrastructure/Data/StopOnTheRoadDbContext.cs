using Lab2.Model.Infrastructure.Data.EntityConfiguration;
using Lab2.Model.Infrastructure.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Lab2.Model.Infrastructure.Data;

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