using ASP.NETCoreWebApplication1.Model.Domain;
using ASP.NETCoreWebApplication1.Model.Infrastructure.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace ASP.NETCoreWebApplication1.Model.Infrastructure.Services;

public class StopOnTheRoadService
{
    private readonly DbSet<StopOnTheRoad> _dbSet;
    
    public StopOnTheRoadService( StopOnTheRoadDbContext dbContext )
    {
        _dbSet = dbContext.Set<StopOnTheRoad>();
    }
    
    public StopOnTheRoad? GetRecipe( int recipeId )
    {
        var recipe = _dbSet.FromSql($"SELECT * FROM placement_along_the_road");
        var test = "kek";
        return null;
    }
}