using Lab3.Application.Models.Dto;
using Lab3.Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Lab3.Controller;

[ApiController]
[Route("api/")]
public class CourseController : ControllerBase
{
    [HttpPost("saveCourse")]
    public IActionResult SaveCourse(SaveCourseParams saveCourseParams)
    {
        try
        {
            new CourseRepository().SaveCourse(saveCourseParams);
        } catch (Exception exception)
        {
            return Problem(exception.Message);
        }

        return Ok("Course with modules saved");
    }

    [HttpPost("deleteCourse")]
    public string DeleteCourse()
    {
        return "deleteCourse";
    }

    [HttpPost("saveEnrollment")]
    public IActionResult SaveEnrollment(EnrollmentParams enrollmentParams)
    {
        try
        {
            new EnrollmentRepository().SaveEnrollment(enrollmentParams);
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
            new MaterialRepository().SaveMaterialStatus(saveMaterialStatusParams);
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
            request = new CourseRepository().GetCourseStatus(courseStatusParams);
        } catch (Exception exception)
        {
            return Problem("Error with save course");
        }

        return Ok(request);
    }
}