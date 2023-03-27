namespace Service.Model.Dto;

public class CourseStatusData
{
    public string EnrollmentId { get; set; }
    public List<ModuleStatusData> Modules { get; set; }
    public decimal? Progress { get; set; }
    public decimal? Duration { get; set; }
}