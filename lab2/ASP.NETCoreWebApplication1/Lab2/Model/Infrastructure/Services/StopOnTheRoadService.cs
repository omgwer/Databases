using Lab2.Model.Domain;
using Lab2.Model.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Lab2.Model.Infrastructure.Services;

public class StopOnTheRoadService
{
    private readonly DbSet<StopOnTheRoad> _dbSet;

    public StopOnTheRoadService(StopOnTheRoadDbContext dbContext)
    {
        _dbSet = dbContext.Set<StopOnTheRoad>();
    }

    public IEnumerable<StopOnTheRoad> GetStopOnTheRoadList(int offset, int limit)
    {
        var stopOnTheRoadList = _dbSet.FromSql($"SELECT st_r.id,ln_sp.locality_name AS start_point,ln_fp.locality_name AS finish_point,st_r.range_from_start,st_r.bus_stop_name,pl_r.placement_along_the_road,st_r.is_have_pavilion FROM stop_on_the_road AS st_r INNER JOIN placement_along_the_road AS pl_r ON st_r.placement_along_the_road_id = pl_r.id INNER JOIN road AS r ON st_r.road_id = r.id INNER JOIN locality_name AS ln_sp ON ln_sp.id = r.start_point INNER JOIN locality_name AS ln_fp ON ln_fp.id = r.finish_point LIMIT {limit} OFFSET {offset}").ToList();
        return stopOnTheRoadList;
    }
}