using Lab3.Application.Models.Dto;
using Lab3.Infrastructure.Data;
using Lab3.Infrastructure.Data.Common;

namespace Lab3.Infrastructure.Repository;

public class CourseRepository
{
    public void SaveCourse(SaveCourseParams saveCourseParams)
    {
        var insertCourseSql = "INSERT INTO course VALUES( @courseId );";

        var moduleIds = saveCourseParams.ModuleIds;
        var requireModuleIds = saveCourseParams.RequiredModuleIds;
        // убираем повторы из moduleIds
        foreach (var requireModule in requireModuleIds)
        {
            moduleIds.Remove(requireModule);
        }

        // TODO Репозиторий открывает и закрывает транзакции
        var connection = new Connection();
        try
        {
            connection.OpenConnection();
            connection.BeginTransaction();
            {
                List<Parameter> insertCourseParameters = new List<Parameter>
                    {new Parameter("courseId", saveCourseParams.CourseId)};
                connection.Execute(insertCourseSql, insertCourseParameters);
            }
            
            for(var i = 0; i < moduleIds.Count; i++)
            {
                var insertModuleSql = "INSERT INTO course_module VALUES(moduleId, courseId, isRequired);";
                
                insertModuleSql = insertModuleSql.Replace("moduleId", "@moduleId" + i);
                insertModuleSql = insertModuleSql.Replace("courseId", "@courseId" + i);
                insertModuleSql = insertModuleSql.Replace("isRequired", "@isRequired" + i);
                
                List<Parameter> insertModuleParameters = new List<Parameter>();
                insertModuleParameters.Add(new Parameter("moduleId" + i, moduleIds[i]));
                insertModuleParameters.Add(new Parameter("courseId" + i, saveCourseParams.CourseId));
                insertModuleParameters.Add(new Parameter("isRequired" + i, "false"));
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
            connection.CloseConnection();
        }
    }

    public List<List<string>> GetCourseStatus(CourseStatusParams courseStatusParams)
    {
        var sqlCommand = "SELECT * FROM course";
        var connection = new Connection();
        var result = connection.Execute(sqlCommand);
        connection.Commit();
        return result.Get();
    }
    
    public void DeleteCourse(string courseId)
    {
    }
}