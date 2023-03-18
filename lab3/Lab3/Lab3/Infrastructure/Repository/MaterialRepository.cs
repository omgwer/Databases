using Lab3.Application.Models.Dto;
using Lab3.Infrastructure.Data;
using Lab3.Infrastructure.Data.Common;

namespace Lab3.Infrastructure.Repository;

public class MaterialRepository
{
    public void SaveMaterialStatus(SaveMaterialStatusParams saveMaterialStatusParams)
    {
        //var upsertCourseStatus = "INSERT INTO course_status VALUES( @enrollmentId , @progress , @duration );";
        
        var upsertCourseStatus = @"
                    INSERT INTO course_status (enrollment_id, progress, duration) VALUES (@enrollmentId , @progress , @duration)
                    ON CONFLICT (enrollment_id)
                    DO UPDATE SET
	                progress = @progress, duration = @duration;
                ";
        
        // не тот upsert нужно добавить для course_module_data
        
        var upsertCourseModuleStatus = "INSERT INTO course_enrollment VALUES( @enrollmentId , @courseId );";
        var connection = ConnectionProvider.GetConnection().OpenConnection().BeginTransaction();
        try
        {
            {
                List<Parameter> insertCourseParameters = new List<Parameter>();
                insertCourseParameters.Add(new Parameter("enrollmentId", saveMaterialStatusParams.EnrollmentId));
                insertCourseParameters.Add(new Parameter("progress", saveMaterialStatusParams.Progress.ToString(), "int"));
                insertCourseParameters.Add(new Parameter("duration", saveMaterialStatusParams.Duration.ToString(), "int"));
                connection.Execute(upsertCourseStatus, insertCourseParameters);
            }
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
}