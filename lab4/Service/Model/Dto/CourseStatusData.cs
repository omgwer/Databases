namespace Service.Model.Dto;

public class CourseStatusData
{
    public string EnrollmentId { get; set; }
    public List<ModuleStatusData> Modules { get; set; }
    public int Progress { get; set; }
    public int Duration { get; set; }
}