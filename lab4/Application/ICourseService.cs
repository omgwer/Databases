using Service.Model.Dto;

namespace Service;

public interface ICourseService
{
    public void SaveCourse(SaveCourseParams saveCourseParams);
    public void DeleteCourse(string courseId);
    public CourseStatusData GetCourseStatus(CourseStatusParams courseStatusParams);
}