using System.Data;
using Lab3.Infrastructure.Data.Common;
using Npgsql;

namespace Lab3.Infrastructure.Data;

public class Connection 
{
    private const string CONNECTION_STRING =  "Host=localhost; Database=ips_labs_3; Username=postgres; Password=12345678; Port= 5432";
    private NpgsqlConnection connection = null;
    private NpgsqlTransaction transaction = null;
    private NpgsqlCommand command = null;

    // init connection object for execute
    public Connection()
    {
        connection = new NpgsqlConnection(CONNECTION_STRING);
        connection.Open();
        command = new NpgsqlCommand();
        command.Connection = connection;
        command.Transaction = transaction;
        command.CommandType = CommandType.Text;
        command.CommandText = string.Empty;
    }

    
    // TODO разделить connection на два класса connection connectionProvider
    // явно вызывать Open 
    // Ленивое создание создание connection
    public Connection Open()
    {
        return this;
    }

    public Connection BeginTransaction()
    {
        transaction = connection.BeginTransaction();
        return this;
    }

    public DatabaseDto Execute(string sqlRequest)
    {
        return Execute(sqlRequest, new List<Parameter>());
    }

    public DatabaseDto Execute(string sqlRequest, List<Parameter> parametersList)
    {
        // TODO ленивое создание connection
        if (parametersList.Count != 0)
        {
            foreach (var tmpParameter in parametersList)
            {
                NpgsqlParameter parameter = new NpgsqlParameter();
                parameter.ParameterName = tmpParameter.Name;
                parameter.Value = tmpParameter.Value;
                command.Parameters.Add(parameter);
            }
        }
        Prepare(sqlRequest);
        NpgsqlDataReader dataReader = command.ExecuteReader();
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
    
    public Connection Commit()
    {
        transaction.Commit();
        return this;
    }
    
    public Connection Rollback()
    {
        if (transaction != null)
            transaction.Rollback();
        return this;
    }

    public Connection Close()
    {
        if (transaction != null)
            connection.Close();
        return this;
    }
    
    // подготовка к выполнению запроса
    private Connection Prepare(string sqlRequest)
    {
        command.CommandText = sqlRequest;
        command.Prepare();
        return this;
    }
}



