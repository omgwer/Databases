using System.Data;
using Lab3.Infrastructure.Data.Common;
using Npgsql;

namespace Lab3.Infrastructure.Data;

public class Connection
{
    private const string CONNECTION_STRING =
        "Host=localhost; Database=ips_labs_3; Username=postgres; Password=12345678; Port= 5432";

    private NpgsqlConnection connection;
    private NpgsqlTransaction transaction;
    private NpgsqlCommand command;

    // TODO разделить connection на два класса connection connectionProvider
    // явно вызывать Open 
    // Ленивое создание создание connection
    public Connection OpenConnection()
    {
        GetConnection().connection.Open();
        return this;
    }

    public DatabaseDto Execute(string sqlRequest)
    {
        return Execute(sqlRequest, new List<Parameter>());
    }

    public DatabaseDto Execute(string sqlRequest, List<Parameter> parametersList)
    {
        GetConnection();
        command = new NpgsqlCommand();
        command.Connection = connection;
        command.Transaction = transaction;
        command.CommandType = CommandType.Text;
        command.CommandText = string.Empty;

        Prepare(sqlRequest, parametersList);
        var dataReader = command.ExecuteReader();
        var databaseDto = new DatabaseDto();
        while (dataReader.Read())
        {
            var stringInDatabaseResponse = new List<string>();
            for (var i = 0; i < dataReader.FieldCount; i++)
            {
                stringInDatabaseResponse.Add(dataReader.GetValue(i).ToString());
            }

            databaseDto.Add(stringInDatabaseResponse);
        }

        dataReader.Close();
        return databaseDto;
    }

    public Connection BeginTransaction()
    {
        transaction = GetConnection().connection.BeginTransaction();
        return this;
    }


    public Connection Commit()
    {
        GetConnection().transaction.Commit();
        return this;
    }

    public Connection Rollback()
    {
        GetConnection().transaction.Rollback();
        return this;
    }

    public Connection CloseConnection()
    {
        GetConnection().connection.Close();
        return this;
    }

    // подготовка к выполнению запроса
    private Connection Prepare(string sqlRequest, List<Parameter> parametersList)
    {
        command.CommandText = sqlRequest;
        // command.Prepare();
        if (parametersList.Count == 0) return this;
        foreach (var tmpParameter in parametersList)
        {
            var parameter = new NpgsqlParameter
            {
                ParameterName = tmpParameter.Name,
                Value = tmpParameter.Value
            };
            command.Parameters.Add(parameter);
        }

        return this;
    }

    private Connection GetConnection()
    {
        if (Equals(connection, null))
            connection = new NpgsqlConnection(CONNECTION_STRING);
        return this;
    }
}