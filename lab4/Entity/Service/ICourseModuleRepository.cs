namespace Entity.Service;

public interface ICourseModuleRepository
{
    List<CourseModule> GetCoursesModulesList();
    CourseModule? GetCourseModule(string id);
    void AddCourseModule(CourseModule courseModule);
    void DeleteCourseModule(string id);
    void UpdateCourseModule(Course course);
}