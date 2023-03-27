namespace Entity.Service;

public interface ICourseModuleRepository
{
    List<CourseModule> GetCoursesModulesList();
    CourseModule? GetCourseModule(string id);
    List<CourseModule> getCourseModulesListByCourseId(string courseId);
    void AddCourseModule(CourseModule courseModule);
    void DeleteCourseModule(string moduleId);
    void DeleteCourseModule(CourseModule courseModule);
    void UpdateCourseModule(Course course);
}