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
        transaction = connection.BeginTransaction();
        command = new NpgsqlCommand();
        command.Connection = connection;
        command.Transaction = transaction;
        command.CommandType = CommandType.Text;
        command.CommandText = string.Empty;
    }

    ~Connection()
    {
        connection.Close();
    }

    public Connection Prepare(string sqlRequest)
    {
        command.CommandText = sqlRequest;
        return this;
    }

    public List<DataTable> Execute(string sqlRequest, List<Parameter> parametersList)
    {
        if (parametersList.Count != 0)
        {
            foreach (var tmpParameter in parametersList)
            {
                NpgsqlParameter parameter = new NpgsqlParameter();
                //  parameter.Direction = ParameterDirection.Input;
                parameter.ParameterName = tmpParameter.Name;
                parameter.Value = tmpParameter.Value;
            }
        }

        NpgsqlDataReader dataReader = command.ExecuteReader();
        transaction.Commit();
        List<DataTable> dtRtn = new List<DataTable>();
        // while (dataReader.Read())
        // {
        //     DataTable dt = new DataTable();
        //     command = new NpgsqlCommand("FETCH ALL IN " + "\"" + dataReader[0].ToString() + "\"",
        //         connection); //use plpgsql fetch command to get data back
        //     NpgsqlDataAdapter da = new NpgsqlDataAdapter(command);
        //     da.Fill(dt);
        //     dtRtn.Add(dt); //all the data will save in the List<DataTable> ,no matter the connection is closed or returned multiple refcursors
        // }
        
        return dtRtn;
    }
    
    public Connection Commit()
    {
        transaction.Commit();
        return this;
    }
    
    public Connection Rollback()
    {
        transaction.Rollback();
        return this;
    }

    public Connection Close()
    {
        connection.Close();
        return this;
    }
}



