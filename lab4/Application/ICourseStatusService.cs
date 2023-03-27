using Service.Model.Dto;

namespace Service;

public interface ICourseStatusService
{
    public CourseStatusData GetCourseStatus(CourseStatusParams courseStatusParams);
}