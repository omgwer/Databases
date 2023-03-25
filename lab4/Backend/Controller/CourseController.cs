using Microsoft.AspNetCore.Mvc;
using Service.Model.Dto;

namespace Lab4.Controller;

[ApiController]
[Route("api/")]
public class CourseController : ControllerBase
{
    [HttpPost("saveCourse")]
    public IActionResult SaveCourse(SaveCourseParams saveCourseParams)
    {
        try
        {
     //       new CourseRepository().SaveCourse(saveCourseParams);
        } catch (Exception exception)
        {
            return Problem(exception.Message);
        }

        return Ok("Course with modules saved");
    }

    [HttpPost("deleteCourse")]
    public IActionResult DeleteCourse(string courseId)
    {
        
        try
        {
     //       new CourseRepository().DeleteCourse(courseId);
        } catch (Exception exception)
        {
            return Problem(exception.Message);
        }

        return Ok("Course deleted");
    }

    [HttpPost("saveEnrollment")]
    public IActionResult SaveEnrollment(EnrollmentParams enrollmentParams)
    {
        try
        {
      //      new EnrollmentRepository().SaveEnrollment(enrollmentParams);
        } catch (Exception exception)
        {
            return Problem(exception.Message);
        }
        return Ok("Enrollment with modules saved");
    }

    [HttpPost("saveMaterialStatus")]
    public IActionResult SaveMaterialStatus(SaveMaterialStatusParams saveMaterialStatusParams)
    {
        try
        {
      //      new MaterialRepository().SaveMaterialStatus(saveMaterialStatusParams);
        } catch (Exception exception)
        {
            return Problem(exception.Message);
        }
        return Ok("Material statuses with modules saved");
    }

    [HttpPost("getCourseStatus")]
    public IActionResult GetCourseStatus(CourseStatusParams courseStatusParams)
    {
        List<List<string>> request;
        try
        {
      //      request = new CourseRepository().GetCourseStatus(courseStatusParams);
        } catch (Exception exception)
        {
            return Problem(exception.Message);
        }

        return Ok("request");
    }
}