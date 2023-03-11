using Microsoft.AspNetCore.Mvc;

namespace Lab3.Controller;

[ApiController]
[Route("api/")]
public class CourseController : ControllerBase
{
    [HttpPost("saveCourse")]
    public string SaveCourse()
    {
        return "saveCourse";
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