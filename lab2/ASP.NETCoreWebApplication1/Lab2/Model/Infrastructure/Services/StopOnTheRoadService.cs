using System.Data;
using Lab2.Model.Domain;
using Lab2.Model.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Lab2.Model.Infrastructure.Services;

public class StopOnTheRoadService
{
    private readonly DbSet<StopOnTheRoad> _dbSet;
    //TODO -  prepare statements add - защита от SQL injection
    // NPG SQL command 
    // Npgsql 
    
    private readonly string CONNECTION_STRING =
        "Host=localhost; Database=ips_labs; Username=postgres; Password=12345678; Port= 5432";

    private readonly string BASE_REQUEST = 
        @"SELECT ST_R.ID,
	        LN_SP.LOCALITY_NAME AS START_POINT,
	        LN_FP.LOCALITY_NAME AS FINISH_POINT,
	        ST_R.RANGE_FROM_START,
	        ST_R.BUS_STOP_NAME,
	        PL_R.PLACEMENT_ALONG_THE_ROAD,
	        ST_R.IS_HAVE_PAVILION
        FROM STOP_ON_THE_ROAD AS ST_R
            INNER JOIN PLACEMENT_ALONG_THE_ROAD AS PL_R ON ST_R.PLACEMENT_ALONG_THE_ROAD_ID = PL_R.ID
            INNER JOIN ROAD AS R ON ST_R.ROAD_ID = R.ID
            INNER JOIN LOCALITY_NAME AS LN_SP ON LN_SP.ID = R.START_POINT
            INNER JOIN LOCALITY_NAME AS LN_FP ON LN_FP.ID = R.FINISH_POINT";

    public StopOnTheRoadService(StopOnTheRoadDbContext dbContext)
    {
        _dbSet = dbContext.Set<StopOnTheRoad>();
    }
    
    public IEnumerable<StopOnTheRoad> GetStopOnTheRoadList(int offset, int limit)
    {
        using NpgsqlConnection npgSqlConnection = new NpgsqlConnection(CONNECTION_STRING);
        npgSqlConnection.Open();
        using NpgsqlCommand command = new NpgsqlCommand(BASE_REQUEST + "WHERE ST_R.ID > @offset LIMIT @limit;" , npgSqlConnection);
        command.Parameters.AddWithValue("limit", limit);
        command.Parameters.AddWithValue("offset", offset);
        var reader = command.ExecuteReader();
        List<StopOnTheRoad> stopOnTheRoadList = new List<StopOnTheRoad>();
        StopOnTheRoad stopOnTheRoad = new StopOnTheRoad();
        while (reader.Read())   // convert base object
        {
            stopOnTheRoad.StartPoint = reader.GetString(1);
            stopOnTheRoad.FinishPoint = reader.GetString(2);
            stopOnTheRoad.RangeFromStart = reader.GetFloat(3);
            stopOnTheRoad.BusStopName = reader.GetString(4);
            stopOnTheRoad.Placement = reader.GetString(5);
            stopOnTheRoad.IsHavePavilion = reader.GetString(6);
            stopOnTheRoadList.Add(stopOnTheRoad);
        }
        return stopOnTheRoadList;
    }

    // public IEnumerable<StopOnTheRoad> GetStopOnTheRoadList(int offset, int limit)
    // {
    //     //TODO -  prepare statements add - защита от SQL injection
    //     // NPG SQL command 
    //     // Npgsql 
    //     System.FormattableString request = $"SELECT st_r.id,ln_sp.locality_name AS start_point,ln_fp.locality_name AS finish_point,st_r.range_from_start,st_r.bus_stop_name,pl_r.placement_along_the_road,st_r.is_have_pavilion FROM stop_on_the_road AS st_r INNER JOIN placement_along_the_road AS pl_r ON st_r.placement_along_the_road_id = pl_r.id INNER JOIN road AS r ON st_r.road_id = r.id INNER JOIN locality_name AS ln_sp ON ln_sp.id = r.start_point INNER JOIN locality_name AS ln_fp ON ln_fp.id = r.finish_point LIMIT {limit} OFFSET {offset}";
    //     var stopOnTheRoadList = _dbSet.FromSql(request).ToList();
    //     return stopOnTheRoadList;
    // }

    public IEnumerable<StopOnTheRoad> SearchSubstring(int offset, int limit, string inputString)
    {
        var searchSubstringRequest = $"WHERE ln_sp.locality_name LIKE '%{inputString}%' OR ln_fp.locality_name LIKE '%{inputString}%' OR st_r.bus_stop_name LIKE '%{inputString}%' OR pl_r.placement_along_the_road LIKE '%{inputString}%' OR st_r.is_have_pavilion LIKE '%{inputString}%'";
        System.FormattableString request =
            $"SELECT st_r.id,ln_sp.locality_name AS start_point,ln_fp.locality_name AS finish_point,st_r.range_from_start,st_r.bus_stop_name,pl_r.placement_along_the_road,st_r.is_have_pavilion FROM stop_on_the_road AS st_r INNER JOIN placement_along_the_road AS pl_r ON st_r.placement_along_the_road_id = pl_r.id INNER JOIN road AS r ON st_r.road_id = r.id INNER JOIN locality_name AS ln_sp ON ln_sp.id = r.start_point INNER JOIN locality_name AS ln_fp ON ln_fp.id = r.finish_point  LIMIT {limit} OFFSET {offset}"; //{searchSubstringRequest}
        var stopOnTheRoadList = _dbSet.FromSql(request);
        var test2 = stopOnTheRoadList.ToList();
        return stopOnTheRoadList;
    }
}