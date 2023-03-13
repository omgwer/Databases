using Lab3.Infrastructure.Data;
using Lab3.Infrastructure.Data.Common;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace Lab3.Controller;

[ApiController]
[Route("api/")]
public class CourseController : ControllerBase
{
    [HttpPost("saveCourse")]
    public string SaveCourse()
    {
        // string sqlCommand = "INSERT INTO course VALUES('test1', 1), ('testr2', 1);";
        // var test = new Connection();
        // var sone = test.Prepare(sqlCommand).Execute(sqlCommand, new List<Parameter>());
        // test.Commit();
        //
        // return "saveCourse";
        
        var conn = new NpgsqlConnection("Host=localhost; Database=ips_labs_3; Username=postgres; Password=12345678; Port= 5432");
        conn.Open();
        var transaction = conn.BeginTransaction();
        var command = new NpgsqlCommand(@"INSERT INTO course VALUES ('test1', 1), ('testr2', 1);", conn);
        //command.ExecuteReader();
        // если запрос инсерт, то нужно нижний метод вызывать
        command.ExecuteNonQuery();
        transaction.Commit();
        return "kek";
    }
    
    [HttpPost("deleteCourse")]
    public string DeleteCourse()
    {
        return "deleteCourse";
    }
    
    [HttpPost("saveEnrollment")]
    public string DeleteCourse(int courseId)
    {
        return "saveEnrollment";
    }
    
    [HttpPost("saveMaterialStatus")]
    public string SaveMaterialStatus(int courseId)
    {
        return "saveEnrollment";
    }
    
    [HttpPost("getCourseStatus")]
    public string GetCourseStatus(int courseId)
    {
        return "getCourseStatus";
    }
}