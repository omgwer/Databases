using Lab2.Model.Domain;
using Lab2.Model.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Lab2.Model.Infrastructure.Services;

public class RestrictionService
{
    private readonly DbSet<StopOnTheRoad> _dbSet;
    private readonly DbSet<PlacementAlongTheRoad> _dbPlacementSet;
    private readonly DbSet<LocalityName> _dbLocalitySet;
    private readonly DbSet<Road> _dbRoadSet;

    private readonly string CONNECTION_STRING =
        "Host=localhost; Database=ips_labs; Username=postgres; Password=12345678; Port= 5432";

    public RestrictionService(StopOnTheRoadDbContext dbContext)
    {
        _dbSet = dbContext.Set<StopOnTheRoad>();
        _dbPlacementSet = dbContext.Set<PlacementAlongTheRoad>();
        _dbLocalitySet = dbContext.Set<LocalityName>();
        _dbRoadSet = dbContext.Set<Road>();
    }

    public IEnumerable<string> GetPlacementOfTheRoadRestriction()
    {
        using NpgsqlConnection npgSqlConnection = new NpgsqlConnection(CONNECTION_STRING);
        npgSqlConnection.Open();
        using NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM placement_along_the_road", npgSqlConnection);
        var reader = command.ExecuteReader();

        List<string> placementRestrictionList = new List<string>();
        while (reader.Read())
        {
            placementRestrictionList.Add(reader.GetString(1));
        }
        reader.Close();
        return placementRestrictionList;
    }

    public IEnumerable<string> GetLocalityNameRestriction()
    {
        using NpgsqlConnection npgSqlConnection = new NpgsqlConnection(CONNECTION_STRING);
        npgSqlConnection.Open();
        using NpgsqlCommand command = new NpgsqlCommand("SELECT DISTINCT * FROM locality_name", npgSqlConnection);
        var reader = command.ExecuteReader();

        var localityNameList = new List<string>();
        while (reader.Read())
        {
            localityNameList.Add(reader.GetString(1));
        }
        reader.Close();
        return localityNameList;
    }

    public IEnumerable<string> GetBusStopNameRestriction()
    {
        const string request = @"SELECT DISTINCT ST_R.BUS_STOP_NAME FROM STOP_ON_THE_ROAD AS ST_R";
        using NpgsqlConnection npgSqlConnection = new NpgsqlConnection(CONNECTION_STRING);
        npgSqlConnection.Open();
        using NpgsqlCommand command = new NpgsqlCommand(request, npgSqlConnection);
        var reader = command.ExecuteReader();

        var localityNameSet = new SortedSet<string>();
        while (reader.Read())
        {
            localityNameSet.Add(reader.GetString(0));
        }
        reader.Close();
        return localityNameSet.ToList();
    }

    public IEnumerable<string> GetIsHavePavilionRestriction()
    {
        const string request = @"SELECT DISTINCT ST_R.IS_HAVE_PAVILION FROM STOP_ON_THE_ROAD AS ST_R;";
        using NpgsqlConnection npgSqlConnection = new NpgsqlConnection(CONNECTION_STRING);
        npgSqlConnection.Open();
        using NpgsqlCommand command = new NpgsqlCommand(request, npgSqlConnection);
        var reader = command.ExecuteReader();

        var localityNameSet = new SortedSet<string>();
        while (reader.Read())
        {
            localityNameSet.Add(reader.GetString(0));
        }
        reader.Close();
        return localityNameSet.ToList();
    }
}