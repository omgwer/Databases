using Lab3.Application.Models.Dto;
using Lab3.Infrastructure.Data;
using Lab3.Infrastructure.Data.Common;

namespace Lab3.Infrastructure.Repository;

public class EnrollmentRepository
{
    public void SaveEnrollment(EnrollmentParams enrollmentParams)
    {
        var insertEnrollmentSql = "INSERT INTO course_enrollment VALUES( @enrollmentId , @courseId );";
        var insertCourseStatusSql = "INSERT INTO course_status VALUES( @enrollmentId , @progress , @duration );";
        var connection = ConnectionProvider.GetConnection().OpenConnection().BeginTransaction();
        try
        {
            {
                List<Parameter> insertCourseParameters = new List<Parameter>();
                insertCourseParameters.Add(new Parameter("enrollmentId", enrollmentParams.EnrollmentId));
                insertCourseParameters.Add(new Parameter("progress", "0", "int"));
                insertCourseParameters.Add(new Parameter("duration", "0", "int"));
                connection.Execute(insertCourseStatusSql, insertCourseParameters);
            }
            {
                List<Parameter> insertCourseParameters = new List<Parameter>();
                insertCourseParameters.Add(new Parameter("enrollmentId", enrollmentParams.EnrollmentId));
                insertCourseParameters.Add(new Parameter("courseId", enrollmentParams.CourseId));
                connection.Execute(insertEnrollmentSql, insertCourseParameters);
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