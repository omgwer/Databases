using ASP.NETCoreWebApplication1.Model;
using Lab2.Model.Domain;
using Npgsql;

namespace Lab2.DbWorker;

public class DbWorker
{
    private string CONNECTION_STRING = "Server=localhost;userid=postgres;Password=12345678;Database=Lab2;Port=5432";
    const string SELECT_STOP_ON_THE_ROAD = "SELECT * FROM stop_on_the_road";
    const string SELECT_PLACEMENT_ALONG_THE_ROAD = "SELECT DISTINCT * FROM placement_along_the_road";
    const string SELECT_PLACEMENT_ALONG_THE_ROAD_BY_ID = "SELECT * FROM placement_along_the_road WHERE id = :id";
    const string SELECT_ROAD_BY_ID = "SELECT * FROM road WHERE id = :id";
    const string SELECT_LOCALITY_NAME_BY_ID = "SELECT * FROM locality_name WHERE id = :id";
    const string SELECT_LOCALITY_NAME = "SELECT DISTINCT * FROM locality_name";
    const string SELECT_BUS_STOP_NAME_RESTRICTIIONS = $"SELECT st_r.id,ln_sp.locality_name AS start_point,ln_fp.locality_name AS finish_point,st_r.range_from_start,st_r.bus_stop_name,pl_r.placement_along_the_road,st_r.is_have_pavilion FROM stop_on_the_road AS st_r INNER JOIN placement_along_the_road AS pl_r ON st_r.placement_along_the_road_id = pl_r.id INNER JOIN road AS r ON st_r.road_id = r.id INNER JOIN locality_name AS ln_sp ON ln_sp.id = r.start_point INNER JOIN locality_name AS ln_fp ON ln_fp.id = r.finish_point";
    const string SELECT_IS_HAVE_PAVILION_RESTRICTION = $"SELECT st_r.id,ln_sp.locality_name AS start_point,ln_fp.locality_name AS finish_point,st_r.range_from_start,st_r.bus_stop_name,pl_r.placement_along_the_road,st_r.is_have_pavilion FROM stop_on_the_road AS st_r INNER JOIN placement_along_the_road AS pl_r ON st_r.placement_along_the_road_id = pl_r.id INNER JOIN road AS r ON st_r.road_id = r.id INNER JOIN locality_name AS ln_sp ON ln_sp.id = r.start_point INNER JOIN locality_name AS ln_fp ON ln_fp.id = r.finish_point";

    //public DbWorker(string connectionString)
    //{
    //    CONNECTION_STRING = connectionString;
    //}

    public string GetPlacementAlongTheRoadBy(int id)
    {
        using (NpgsqlConnection npgSqlConnection = new NpgsqlConnection(CONNECTION_STRING))
        {
            npgSqlConnection.Open();
            string query = SELECT_PLACEMENT_ALONG_THE_ROAD_BY_ID.Replace(":id", id.ToString());
            NpgsqlCommand command = new NpgsqlCommand(query, npgSqlConnection);
            var reader = command.ExecuteReader();
            
            string result = "";
            while (reader.Read())
            { 
                result = reader.GetString(1);
            }
            
            npgSqlConnection.Close();
            return result;
        }
        return "";
    }

    public List<string> GetAllPlacementAlongTheRoad()
    {
        List<string> result = new List<string>();
        using (NpgsqlConnection npgSqlConnection = new NpgsqlConnection(CONNECTION_STRING))
        {
            npgSqlConnection.Open();
            NpgsqlCommand command = new NpgsqlCommand(SELECT_PLACEMENT_ALONG_THE_ROAD, npgSqlConnection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                result.Add(reader.GetString(1));
                //Вариант 2
                //result.Add(GetPlacementAlongTheRoadBy(reader.GetInt32(0));
            }

            npgSqlConnection.Close();
        }
        return result;
    }

    public string GetLocalityNameBy(int id)
    {
        using (NpgsqlConnection npgSqlConnection = new NpgsqlConnection(CONNECTION_STRING))
        {
            npgSqlConnection.Open();
            string query = SELECT_LOCALITY_NAME_BY_ID.Replace(":id", id.ToString());
            NpgsqlCommand command = new NpgsqlCommand(query, npgSqlConnection);
            var reader = command.ExecuteReader();

            string result = "";
            while (reader.Read())
            {
                result = reader.GetString(1);
            }

            npgSqlConnection.Close();
            return result;
        }
        return "";
    }

    public RoadDto GetRoadBy(int id)
    {
        RoadDto result = new RoadDto();
        using (NpgsqlConnection npgSqlConnection = new NpgsqlConnection(CONNECTION_STRING))
        {
            npgSqlConnection.Open();
            string query = SELECT_ROAD_BY_ID.Replace(":id", id.ToString());
            NpgsqlCommand command = new NpgsqlCommand(query, npgSqlConnection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                int startPointId = reader.GetInt32(1);
                int endPointId = reader.GetInt32(2);
                result.StartPoint = GetLocalityNameBy(startPointId);
                result.EndPoint = GetLocalityNameBy(endPointId);
            }
            npgSqlConnection.Close();
        }
        return result;
    }

    private StopOnTheRoad ReadRouteDtoFrom(NpgsqlDataReader reader)
    {
        StopOnTheRoad result = new StopOnTheRoad();
        long id = reader.GetInt64(0);
        try
        {
            if (!reader.IsDBNull(1))
                result.IsHavePavilion = reader.GetString(1);
            else
                result.IsHavePavilion = null;
            result.BusStopName = reader.GetString(2);
            result.RangeFromStart = reader.GetDouble(3);
            int placementAlongTheRoadId = reader.GetInt32(4);
            int roadId = reader.GetInt32(5);
            result.Placement = GetPlacementAlongTheRoadBy(placementAlongTheRoadId);

            RoadDto road = GetRoadBy(roadId);
            result.StartPoint = road.StartPoint;
            result.FinishPoint = road.EndPoint;

        }
        catch (Exception ex) 
        {
        }
        return result;
    }

    public List<StopOnTheRoadDto> GetAllRoutes()
    {
        List<StopOnTheRoadDto> result = new List<StopOnTheRoadDto>();
        using (NpgsqlConnection npgSqlConnection = new NpgsqlConnection(CONNECTION_STRING))
        {
            npgSqlConnection.Open();
            NpgsqlCommand command = new NpgsqlCommand(SELECT_STOP_ON_THE_ROAD, npgSqlConnection);
            var reader = command.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    result.Add(ReadRouteDtoFrom(reader));
                }
            }
            catch (Exception e)
            { 
            }

            npgSqlConnection.Close();
        }
        return result;
    }

    public List<string> GetLocalityNameRestriction()
    {
        List<string> result = new List<string>();
        using (NpgsqlConnection npgSqlConnection = new NpgsqlConnection(CONNECTION_STRING))
        {
            npgSqlConnection.Open();
            NpgsqlCommand command = new NpgsqlCommand(SELECT_LOCALITY_NAME, npgSqlConnection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                result.Add(reader.GetString(1));
            }
            npgSqlConnection.Close();
        }
        return result;
    }

    public List<string> GetBusStopNameRestriction()
    {
        List<string> result = new List<string>();
        using (NpgsqlConnection npgSqlConnection = new NpgsqlConnection(CONNECTION_STRING))
        {
            npgSqlConnection.Open();
            NpgsqlCommand command = new NpgsqlCommand(SELECT_BUS_STOP_NAME_RESTRICTIIONS, npgSqlConnection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                result.Add(reader.GetString(1));
            }
            npgSqlConnection.Close();
        }
        return result;
    }

    public List<string> GetIsHavePavilionRestriction()
    {
        List<string> result = new List<string>();
        using (NpgsqlConnection npgSqlConnection = new NpgsqlConnection(CONNECTION_STRING))
        {
            npgSqlConnection.Open();
            NpgsqlCommand command = new NpgsqlCommand(SELECT_IS_HAVE_PAVILION_RESTRICTION, npgSqlConnection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                result.Add(reader.GetString(1));
            }
            npgSqlConnection.Close();
        }
        return result;
    }

    public List<string> SearchSubstring(int offset, int limit, string inputString)
    {
        var searchSubstringRequest = $"WHERE ln_sp.locality_name LIKE '%{inputString}%' OR ln_fp.locality_name LIKE '%{inputString}%' OR st_r.bus_stop_name LIKE '%{inputString}%' OR pl_r.placement_along_the_road LIKE '%{inputString}%' OR st_r.is_have_pavilion LIKE '%{inputString}%'";
        string request =
            $"SELECT st_r.id,ln_sp.locality_name AS start_point,ln_fp.locality_name AS finish_point,st_r.range_from_start,st_r.bus_stop_name,pl_r.placement_along_the_road,st_r.is_have_pavilion FROM stop_on_the_road AS st_r INNER JOIN placement_along_the_road AS pl_r ON st_r.placement_along_the_road_id = pl_r.id INNER JOIN road AS r ON st_r.road_id = r.id INNER JOIN locality_name AS ln_sp ON ln_sp.id = r.start_point INNER JOIN locality_name AS ln_fp ON ln_fp.id = r.finish_point  LIMIT {limit} OFFSET {offset}"; //{searchSubstringRequest}
        List<string> result = new List<string>();
        using (NpgsqlConnection npgSqlConnection = new NpgsqlConnection(CONNECTION_STRING))
        {
            npgSqlConnection.Open();
            NpgsqlCommand command = new NpgsqlCommand(request, npgSqlConnection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                result.Add(reader.GetString(1));
            }
            npgSqlConnection.Close();
        }
        return result;
    }

    public List<StopOnTheRoad> GetStopOnTheRoadList(int offset, int limit)
    {
        var request = $"SELECT st_r.id,ln_sp.locality_name AS start_point,ln_fp.locality_name AS finish_point,st_r.range_from_start,st_r.bus_stop_name,pl_r.placement_along_the_road,st_r.is_have_pavilion FROM stop_on_the_road AS st_r INNER JOIN placement_along_the_road AS pl_r ON st_r.placement_along_the_road_id = pl_r.id INNER JOIN road AS r ON st_r.road_id = r.id INNER JOIN locality_name AS ln_sp ON ln_sp.id = r.start_point INNER JOIN locality_name AS ln_fp ON ln_fp.id = r.finish_point LIMIT {limit} OFFSET {offset}";
        List<StopOnTheRoad> result = new List<StopOnTheRoad>();
        using (NpgsqlConnection npgSqlConnection = new NpgsqlConnection(CONNECTION_STRING))
        {
            npgSqlConnection.Open();
            NpgsqlCommand command = new NpgsqlCommand(request, npgSqlConnection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                result.Add(ReadRouteDtoFrom(reader));
            }
            npgSqlConnection.Close();
        }
        return result;
    }
}