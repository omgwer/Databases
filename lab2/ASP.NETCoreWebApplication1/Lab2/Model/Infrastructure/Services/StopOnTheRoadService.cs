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
       // _dbSet = dbContext.Set<StopOnTheRoad>();
    }
    
    public IEnumerable<StopOnTheRoad> GetStopOnTheRoadList(SearchParameters searchParameters)
    {
        string limit = " LIMIT @limit";
        string parameters = RequestParametersBuilder(searchParameters);
        string direction = "";
        if (searchParameters.Direction == "ASC")
        {
            string databaseTable = getDatabaseTableName(searchParameters);
            direction = $" ORDER BY {databaseTable}  ASC  ";
        } else if (searchParameters.Direction == "DESC")
        {
            string databaseTable = getDatabaseTableName(searchParameters);
            direction = $" ORDER BY {databaseTable} DESC ";
        }
        
        using NpgsqlConnection npgSqlConnection = new NpgsqlConnection(CONNECTION_STRING);
        npgSqlConnection.Open();
        using NpgsqlCommand command = new NpgsqlCommand(BASE_REQUEST + " WHERE ST_R.ID > @offset " + parameters + direction +  limit , npgSqlConnection);
        command.Parameters.AddWithValue("offset", searchParameters.Offset);
        if (searchParameters.StartPoint != null) 
            command.Parameters.AddWithValue("startPoint", searchParameters.StartPoint);
        if (searchParameters.FinishPoint != null)
            command.Parameters.AddWithValue("finishPoint", searchParameters.FinishPoint);
        if (searchParameters.MinRange != null)
            command.Parameters.AddWithValue("minRange", searchParameters.MinRange);
        if (searchParameters.MaxRange != null)
            command.Parameters.AddWithValue("maxRange", searchParameters.MaxRange);
        if (searchParameters.BusStopName != null)
            command.Parameters.AddWithValue("busStopName", searchParameters.BusStopName);
        if (searchParameters.Placement != null)
            command.Parameters.AddWithValue("placement", searchParameters.Placement);
        if (searchParameters.IsHavePavilion != null)
            command.Parameters.AddWithValue("isHavePavilion", searchParameters.IsHavePavilion);
      //  if (searchParameters.Order != null)
        //    command.Parameters.AddWithValue("orders", searchParameters.Order);
        command.Parameters.AddWithValue("limit", 10);
        var reader = command.ExecuteReader();
        List<StopOnTheRoad> stopOnTheRoadList = new List<StopOnTheRoad>();
        if (reader.HasRows) // если есть данные
        {
            while (reader.Read()) // convert base object
            {
                StopOnTheRoad stopOnTheRoad = new StopOnTheRoad();
                stopOnTheRoad.StartPoint = reader.GetString(1);
                stopOnTheRoad.FinishPoint = reader.GetString(2);
                stopOnTheRoad.RangeFromStart = Math.Round(reader.GetFloat(3), 3);
                stopOnTheRoad.BusStopName = reader.GetString(4);
                stopOnTheRoad.Placement = reader.GetString(5);
                stopOnTheRoad.IsHavePavilion = reader.GetString(6);
                stopOnTheRoadList.Add(stopOnTheRoad);
            }
        }
        return stopOnTheRoadList;
    }

    public IEnumerable<StopOnTheRoad> SearchSubstring(int offset, int limit, string inputString)
    {
        var searchSubstringRequest = " WHERE ln_sp.locality_name LIKE @searchText OR ln_fp.locality_name LIKE @searchText OR st_r.bus_stop_name LIKE @searchText OR pl_r.placement_along_the_road LIKE @searchText OR st_r.is_have_pavilion LIKE @searchText ";
        using NpgsqlConnection npgSqlConnection = new NpgsqlConnection(CONNECTION_STRING);
        npgSqlConnection.Open();
        using NpgsqlCommand command = new NpgsqlCommand(BASE_REQUEST + searchSubstringRequest + " AND ST_R.ID > @offset LIMIT @limit" , npgSqlConnection);
        command.Parameters.AddWithValue("offset", offset);
        command.Parameters.AddWithValue("limit", 10);
        command.Parameters.AddWithValue("searchText", '%' + inputString + '%');
        var reader = command.ExecuteReader();
        List<StopOnTheRoad> stopOnTheRoadList = new List<StopOnTheRoad>();
        if (reader.HasRows) // если есть данные
        {
            while (reader.Read()) // convert base object
            {
                StopOnTheRoad stopOnTheRoad = new StopOnTheRoad();
                stopOnTheRoad.StartPoint = reader.GetString(1);
                stopOnTheRoad.FinishPoint = reader.GetString(2);
                stopOnTheRoad.RangeFromStart = Math.Round(reader.GetFloat(3), 3);
                stopOnTheRoad.BusStopName = reader.GetString(4);
                stopOnTheRoad.Placement = reader.GetString(5);
                stopOnTheRoad.IsHavePavilion = reader.GetString(6);
                stopOnTheRoadList.Add(stopOnTheRoad);
            }
        }
        else
        {
            //throw new Exception("somekek");
        }
        return stopOnTheRoadList;
    }

    private string RequestParametersBuilder(SearchParameters searchParameters)
    {
        string parameters = "";
        if (searchParameters.StartPoint != null)
            parameters += " AND LN_SP.LOCALITY_NAME = @startPoint ";
        if (searchParameters.FinishPoint != null)
            parameters += " AND LN_FP.LOCALITY_NAME = @finishPoint ";
        if (searchParameters.MinRange != null)
            parameters += " AND ST_R.RANGE_FROM_START >  @minRange ";
        if (searchParameters.MaxRange != null)
            parameters += " AND ST_R.RANGE_FROM_START <  @maxRange ";
        if (searchParameters.BusStopName != null)
            parameters += " AND ST_R.BUS_STOP_NAME = @busStopName ";
        if (searchParameters.Placement != null)
            parameters += " AND PL_R.PLACEMENT_ALONG_THE_ROAD = @placement ";
        if (searchParameters.IsHavePavilion != null)
            parameters += " AND ST_R.IS_HAVE_PAVILION = @isHavePavilion ";
      //  if (searchParameters.Order != null )
       //     parameters += " ORDER BY @orders ";
        return parameters;
    }

    private string getDatabaseTableName(SearchParameters searchParameters)
    {
        string resultString = "";
        switch (searchParameters.Order)
        {
            case "startPoint": {}
            {
                resultString = "LN_SP.LOCALITY_NAME";
                break;
            }
            case "finishPoint":
            {
                resultString = "LN_FP.LOCALITY_NAME";
                break;
            }
            case "range":
            {
                resultString = "ST_R.RANGE_FROM_START";
                break;
            }
            case "busStopName":
            {
                resultString = "ST_R.BUS_STOP_NAME";
                break;
            }
            case "placementAlongTheRoad":
            {
                resultString = "PL_R.PLACEMENT_ALONG_THE_ROAD";
                break;
            }
            case "isHavePavilion":
            {
                resultString = "ST_R.IS_HAVE_PAVILION";
                break;
            }
        }

        if (resultString.Equals(""))
            throw new Exception("Error");
        return resultString;
    }
}