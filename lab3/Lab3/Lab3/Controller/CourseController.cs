using Lab3.Application.Models.Dto;
using Lab3.Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Lab3.Controller;

[ApiController]
[Route("api/")]
public class CourseController : ControllerBase
{
    [HttpPost("saveCourse")]
    public IActionResult SaveCourse(SaveCourseDto saveCourseDto)
    {
        try
        {
            new CourseRepository().SaveCourse(saveCourseDto);
        } catch (Exception exception)
        {
            return Problem(exception.Message);
        }

        return Ok("success");
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