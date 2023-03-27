namespace Entity.Service;

public interface ICourseEnrollmentRepository
{
    List<CourseEnrollment> GetList();
    CourseEnrollment? Get(string id);
    void Add(CourseEnrollment courseStatus);
    void Delete(string id);
    void Update(CourseEnrollment course);
}