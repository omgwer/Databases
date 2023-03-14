using Lab3.Application.Models.Dto;
using Lab3.Infrastructure.Data;
using Lab3.Infrastructure.Data.Common;

namespace Lab3.Infrastructure.Repository;

public class CourseRepository
{
    public void SaveCourse(SaveCourseDto saveCourseDto)
    {
        var insertCourseId = "INSERT INTO course VALUES( @courseId )";
        List<Parameter> insertCourseParameters = new List<Parameter>();
        if (saveCourseDto.CourseId != "")
        {
            var parameter = new Parameter()
            {
                Name = "courseId",
                Value = saveCourseDto.CourseId
            };
            insertCourseParameters.Add(parameter);
        }
        
        var connection = new Connection();
        try
        {
            var result = connection.Execute(insertCourseId, insertCourseParameters);
            // some execute commands
            connection.Commit();
        }
        catch (Exception ex)
        {
            connection.Rollback();
            throw new Exception("fail");
        }
        finally
        {
            connection.Close();
        }
    }

    public void DeleteCourse(string courseId)
    {
    }

    public void GetCourseStatus()
    {
        var sqlCommand = "SELECT * FROM course";
        var connection = new Connection();
        var result = connection.Execute(sqlCommand);
        connection.Commit();
        result.Get();
    }
}