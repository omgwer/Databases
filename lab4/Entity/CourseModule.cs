namespace Entity;

public class CourseModule
{
    public string ModuleId { get; set; } = string.Empty;
    public List<Course>? Courses { get; set; }
    public bool IsRequired { get; set; }
    public string CreatedAt { get; set; } = string.Empty;
    public string UpdatedAt { get; set; } = string.Empty;
    public string DeletedAt { get; set; } = string.Empty;
}