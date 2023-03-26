using Microsoft.AspNetCore.Mvc;
using Service;
using Service.Model.Dto;
using Service.Service;

namespace Lab4.Controller;

[ApiController]
[Route("api/")]
public class CourseController : ControllerBase
{
    private readonly ICourseEnrollmentService _courseEnrollmentService;
    private readonly ICourseModuleService _courseModuleService;
    private readonly ICourseModuleStatusService _courseModuleStatusService;
    private readonly ICourseService _courseService;
    private readonly ICourseStatusService _courseStatusService;

    public CourseController(ICourseEnrollmentService courseEnrollmentService, ICourseModuleService courseModuleService, 
        ICourseModuleStatusService courseModuleStatusService, ICourseService courseService, ICourseStatusService courseStatusService)
    {
        _courseEnrollmentService = courseEnrollmentService;
        _courseModuleService = courseModuleService;
        _courseModuleStatusService = courseModuleStatusService;
        _courseService = courseService;
        _courseStatusService = courseStatusService;
    }

    [HttpPost("saveCourse")]
    public IActionResult SaveCourse(SaveCourseParams saveCourseParams)
    {
        try
        {
            _courseService.SaveCourse(saveCourseParams);
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