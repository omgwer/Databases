using Service.Model.Dto;

namespace Service;

public interface ICourseService
{
    public void SaveCourse(SaveCourseParams saveCourseParams);
    public CourseStatusData GetCourseStatus(CourseStatusParams courseStatusParams);
    public void DeleteCourse(string courseId);
}