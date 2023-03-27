namespace Entity.Service;

public interface ICourseStatusRepository
{
    List<CourseStatus> GetCourseStatusList();
    CourseStatus? GetCourseStatus(string id);
    void Add(CourseStatus courseStatus);
    void DeleteCourseStatus(string id);
    void UpdateCourseStatus(CourseStatus course);
}