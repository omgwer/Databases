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
	                progress = @progress, duration = course_status.duration + @duration;
                ";

        // не тот upsert нужно добавить для course_module_data

        var upsertCourseModuleStatus = @"
                    INSERT INTO course_module_status (enrollment_id, module_id, progress, duration) VALUES (@enrollmentId , @moduleId , @progress , @duration)
                    ON CONFLICT (enrollment_id, module_id)
                    DO UPDATE SET
                    progress = @progress, duration = course_module_status.duration + @duration;
                ";

        var updateCourseModuleProgress = @"
                    WITH course_module AS
                    (
                            SELECT module_id, is_required
                            FROM course_module
                            WHERE module_id = @moduleId
                    )
                    UPDATE course_module_status AS cms 
                    SET
                        progress = CASE WHEN cm.is_required = 'false' THEN 100 ELSE progress END
                    FROM course_module AS cm WHERE cm.module_id = cms.module_id;
                ";
        
        var connection = ConnectionProvider.GetConnection().OpenConnection().BeginTransaction();
        try
        {
            {
                List<Parameter> insertCourseParameters = new List<Parameter>();
                insertCourseParameters.Add(new Parameter("enrollmentId", saveMaterialStatusParams.EnrollmentId));
                insertCourseParameters.Add(new Parameter("moduleId", saveMaterialStatusParams.ModuleId));
                insertCourseParameters.Add(new Parameter("progress", saveMaterialStatusParams.Progress.ToString(), "int"));
                insertCourseParameters.Add(new Parameter("duration", saveMaterialStatusParams.Duration.ToString(), "int"));
                connection.Execute(upsertCourseModuleStatus, insertCourseParameters);
            }
            {
                List<Parameter> insertCourseParameters = new List<Parameter>();
                insertCourseParameters.Add(new Parameter("moduleId", saveMaterialStatusParams.ModuleId));
                connection.Execute(updateCourseModuleProgress, insertCourseParameters);
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