using System.Runtime.InteropServices.JavaScript;
using Lab3.Application.Models.Dto;
using Lab3.Infrastructure.Data;
using Lab3.Infrastructure.Data.Common;

namespace Lab3.Infrastructure.Repository;

public class CourseRepository
{
    public void SaveCourse(SaveCourseDto saveCourseDto)
    {
        var insertCourseSql = "INSERT INTO course VALUES( @courseId )";
        var insertModuleSql = "INSERT INTO course_module VALUES( @moduleId, @courseId, @isRequired)";

        var moduleIds = saveCourseDto.ModuleIds;
        var requireModuleIds = saveCourseDto.RequiredModuleIds;
        // убираем повторы из moduleIds
        foreach (var requireModule in requireModuleIds)
        {
            moduleIds.Remove(requireModule);
        }

        var connection = new Connection();
        try
        {
            {
                List<Parameter> insertCourseParameters = new List<Parameter> { new Parameter("courseId", saveCourseDto.CourseId) };
                connection.Execute(insertCourseSql, insertCourseParameters);
            }
            foreach (var moduleId in moduleIds)
            {
                List<Parameter> insertModuleParameters = new List<Parameter>();
                insertModuleParameters.Add( new Parameter("moduleId", moduleId));
                insertModuleParameters.Add( new Parameter("courseId", saveCourseDto.CourseId));
                insertModuleParameters.Add( new Parameter("isRequired", "false"));
                connection.Execute(insertModuleSql, insertModuleParameters);
            }
           
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