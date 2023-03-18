namespace Lab3.Infrastructure.Data;

public static class ConnectionProvider
{
    public static Connection GetConnection()
    {
        return new Connection("Host=localhost; Database=ips_labs_3; Username=postgres; Password=12345678; Port= 5432");
    }
}