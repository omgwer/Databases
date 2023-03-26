namespace Entity.Service;

public interface ICourseRepository
{
    List<Course> GetCoursesList();
    Course GetCourse();
    void AddCourse(Course course);
    void DeleteCourse(string id);
    void Update(Course course);
}