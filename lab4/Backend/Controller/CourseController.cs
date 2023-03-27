using Microsoft.AspNetCore.Mvc;
using Service;
using Service.Model.Dto;

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
        ICourseModuleStatusService courseModuleStatusService, ICourseService courseService,
        ICourseStatusService courseStatusService)
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
        }
        catch (Exception exception)
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
            _courseService.DeleteCourse(courseId);
        }
        catch (Exception exception)
        {
            return Problem(exception.Message);
        }

        return Ok("Course deleted");
    }

    [HttpPost("saveEnrollment")]
    public IActionResult SaveEnrollment(SaveEnrollmentParams enrollmentParams)
    {
        try
        {
            _courseEnrollmentService.SaveEnrollment(enrollmentParams);
        }
        catch (Exception exception)
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
            _courseModuleStatusService.SaveMaterialStatus(saveMaterialStatusParams);
        }
        catch (Exception exception)
        {
            return Problem(exception.Message);
        }

        return Ok("Material statuses with modules saved");
    }

    [HttpPost("getCourseStatus")]
    public IActionResult GetCourseStatus(CourseStatusParams courseStatusParams)
    {
        CourseStatusData request;
        try
        {
            request = _courseStatusService.GetCourseStatus(courseStatusParams);
        }
        catch (Exception exception)
        {
            return Problem(exception.Message);
        }

        return Ok(request);
    }
}