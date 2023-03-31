namespace Entity.Service;

public interface ICourseModuleRepository
{
    List<CourseModule> GetCoursesModulesList();
    CourseModule? GetCourseModule(string id);
    List<CourseModule> GetCourseModulesListByCourseId(string courseId);
    void AddCourseModule(CourseModule courseModule);
    void DeleteCourseModule(string moduleId);
    void DeleteCourseModule(CourseModule courseModule);
    void UpdateCourseModule(Course course);
}