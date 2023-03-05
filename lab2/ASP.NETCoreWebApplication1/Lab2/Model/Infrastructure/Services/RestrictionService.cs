using Lab2.Model.Domain;
using Lab2.Model.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Lab2.Model.Infrastructure.Services;

public class RestrictionService
{
    private readonly DbSet<StopOnTheRoad> _dbSet;
    private readonly DbSet<PlacementAlongTheRoad> _dbPlacementSet;
    private readonly DbSet<LocalityName> _dbLocalitySet;
    private readonly DbSet<Road> _dbRoadSet;
    private readonly string connection = "Host=localhost; Database=ips_labs; Username=postgres; Password=12345678; Port= 5432";

    public RestrictionService(StopOnTheRoadDbContext dbContext)
    {
        _dbSet = dbContext.Set<StopOnTheRoad>();
        _dbPlacementSet = dbContext.Set<PlacementAlongTheRoad>();
        _dbLocalitySet = dbContext.Set<LocalityName>();
        _dbRoadSet = dbContext.Set<Road>();
    }
    
    public IEnumerable<string> GetPlacementOfTheRoadRestriction()
    {
        var placementAlongTheRoads =_dbPlacementSet.FromSql($"SELECT DISTINCT * FROM placement_along_the_road").ToList();  // вот это дело нужно распарсить
        
        var test = 


        var placementRestrictionList = new List<string>();
        placementAlongTheRoads.ForEach(x => placementRestrictionList.Add(x.Placement));
        return placementRestrictionList;
    }

    // public IEnumerable<string> GetPlacementOfTheRoadRestriction()
    // {
    //     var placementAlongTheRoads =
    //         _dbPlacementSet.FromSql($"SELECT DISTINCT * FROM placement_along_the_road").ToList();
    //     var placementRestrictionList = new List<string>();
    //     placementAlongTheRoads.ForEach(x => placementRestrictionList.Add(x.Placement));
    //     return placementRestrictionList;
    // }

    public IEnumerable<string> GetLocalityNameRestriction()
    {
        var localityNameResponse = _dbLocalitySet.FromSql($"SELECT DISTINCT * FROM locality_name").ToList();
        var localityNameList = new List<string>();
        localityNameResponse.ForEach(x => localityNameList.Add(x.Locality));
        return localityNameList;
    }

    public IEnumerable<string> GetBusStopNameRestriction()
    {
        var localityNameResponse = _dbSet.FromSql($"SELECT st_r.id,ln_sp.locality_name AS start_point,ln_fp.locality_name AS finish_point,st_r.range_from_start,st_r.bus_stop_name,pl_r.placement_along_the_road,st_r.is_have_pavilion FROM stop_on_the_road AS st_r INNER JOIN placement_along_the_road AS pl_r ON st_r.placement_along_the_road_id = pl_r.id INNER JOIN road AS r ON st_r.road_id = r.id INNER JOIN locality_name AS ln_sp ON ln_sp.id = r.start_point INNER JOIN locality_name AS ln_fp ON ln_fp.id = r.finish_point").ToList();
        var localityNameSet = new SortedSet<string>();
        localityNameResponse.ForEach(x => localityNameSet.Add(x.BusStopName));
        return localityNameSet.ToList();
    }

    public IEnumerable<string> GetIsHavePavilionRestriction()
    {
        var localityNameResponse = _dbSet.FromSql($"SELECT st_r.id,ln_sp.locality_name AS start_point,ln_fp.locality_name AS finish_point,st_r.range_from_start,st_r.bus_stop_name,pl_r.placement_along_the_road,st_r.is_have_pavilion FROM stop_on_the_road AS st_r INNER JOIN placement_along_the_road AS pl_r ON st_r.placement_along_the_road_id = pl_r.id INNER JOIN road AS r ON st_r.road_id = r.id INNER JOIN locality_name AS ln_sp ON ln_sp.id = r.start_point INNER JOIN locality_name AS ln_fp ON ln_fp.id = r.finish_point").ToList();
        var localityNameSet = new SortedSet<string>();
        localityNameResponse.ForEach(x => localityNameSet.Add(x.IsHavePavilion));
        return localityNameSet.ToList();
    }
}

// public PlacementAlongTheRoad? GetRecipe(int recipeId)
// {
//     var recipe = _dbSetTest.FromSql($"SELECT DISTINCT * FROM placement_along_the_road");
//     var recipe1 = _dbSetTest.FromSql($"SELECT DISTINCT * FROM placement_along_the_road").ToList();
//     var test = "kek";
//     return null;
// }