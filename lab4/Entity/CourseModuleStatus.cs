namespace Entity;

public class CourseModuleStatus
{
    public string EnrollmentId { get; set; }       // PK
    public CourseModule CourseModule { get; set; } // PK
    public int Progress { get; set; }
    public int Duration { get; set; }
    public string DeletedAt { get; set; } = string.Empty;
}