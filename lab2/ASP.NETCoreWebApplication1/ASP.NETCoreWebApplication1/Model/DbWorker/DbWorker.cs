using Npgsql;

namespace ASP.NETCoreWebApplication1.Model.DbWorker;

public class DbWorker
{
    const string CONNECTION_STRING = "Server=localhost;userid=testuser;Password=12345678;Database=ips_labs;Port=5432";

    public int executeRequest(string request)
    {
        // NpgsqlConnection npgSqlConnection = new NpgsqlConnection(CONNECTION_STRING);
        // npgSqlConnection.Open();
        //
        // NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM  placement_along_the_road", npgSqlConnection);
        //
        // var reader = command.ExecuteReader();
        // while (reader.Read())
        // {
        //     reader.
        //     string test4 = reader.GetString(1);
        //     
        // }
        //
         return 1;
    }
}