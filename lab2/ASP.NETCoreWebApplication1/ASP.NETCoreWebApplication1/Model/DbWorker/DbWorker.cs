using Npgsql;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Internal;
using System;

namespace ASP.NETCoreWebApplication1.Model.DbWorker;

public class DbWorker
{
    const string CONNECTION_STRING = "Server=localhost;userid=postgres;Password=12345678;Database=Lab2;Port=5432";
    const string SELECT_STOP_ON_THE_ROAD = "SELECT * FROM stop_on_the_road";
    const string SELECT_PLACEMENT_ALONG_THE_ROAD_BY_ID = "SELECT * FROM placement_along_the_road WHERE id = :id";
    const string SELECT_ROAD_BY_ID = "SELECT * FROM road WHERE id = :id";
    const string SELECT_LOCALITY_NAME_BY_ID = "SELECT * FROM locality_name WHERE id = :id";

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

    private StopOnTheRoadDto ReadRouteDtoFrom(NpgsqlDataReader reader)
    {
        StopOnTheRoadDto result = new StopOnTheRoadDto();
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
            result.PlacementAlongTheRoad = GetPlacementAlongTheRoadBy(placementAlongTheRoadId);

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
}