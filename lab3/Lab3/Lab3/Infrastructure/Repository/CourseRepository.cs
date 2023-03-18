using Lab3.Application.Models.Dto;
using Lab3.Infrastructure.Data;
using Lab3.Infrastructure.Data.Common;

namespace Lab3.Infrastructure.Repository;

public class CourseRepository
{
    public void SaveCourse(SaveCourseParams saveCourseParams)
    {
        var requireModuleIds = saveCourseParams.RequiredModuleIds;
        var moduleIds = saveCourseParams.ModuleIds;
        // убираем повторы из moduleIds
        foreach (var requireModule in requireModuleIds)
        {
            moduleIds.Remove(requireModule);
        }
        
        var connection = ConnectionProvider.GetConnection();
        try
        {
            connection.OpenConnection();
            connection.BeginTransaction();
            {
                var insertCourseSql = "INSERT INTO course VALUES( @courseId );";
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
            throw new Exception(ex.Message);
        }
        finally
        {
            connection.CloseConnection();
        }
    }

    public List<List<string>> GetCourseStatus(CourseStatusParams courseStatusParams)
    {
        var sqlCommand = "SELECT * FROM course";
        var connection = ConnectionProvider.GetConnection();
        try
        {
            var result = connection.OpenConnection().Execute(sqlCommand);
            return result.Get();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            connection.CloseConnection();
        }
    }
    
    public void DeleteCourse(string courseId)
    {
    }
}