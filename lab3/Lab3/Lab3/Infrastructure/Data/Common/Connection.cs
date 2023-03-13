using Npgsql;

namespace Lab3.Infrastructure.Data.Common;

public class Connection
{
    private const string CONNECTION_STRING =  "Host=localhost; Database=ips_labs_3; Username=postgres; Password=12345678; Port= 5432";
    NpgsqlConnection connection = null;
    NpgsqlTransaction transaction = null;
    NpgsqlCommand command = null;   
    
}