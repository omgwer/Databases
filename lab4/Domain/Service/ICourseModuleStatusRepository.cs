namespace Entity.Service;

public interface ICourseModuleStatusRepository
{
    List<CourseModuleStatus> GetList();
    CourseModuleStatus? Get(string enrollmentId, string moduleId);
    void Add(CourseModuleStatus courseStatus);
    void Delete(string enrollmentId, string moduleId);
    void Update(CourseModuleStatus course);
    public List<CourseModuleStatus> GetListByEnrollmentId(string enrollmentId);
    public void Delete(CourseModuleStatus courseModuleStatus);
}