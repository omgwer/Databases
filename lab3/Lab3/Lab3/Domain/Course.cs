namespace Lab3.Domain;

public class Course
{
    public int Id { get; set; } // dbContext
    public string CourseId { get; set; }
    public List<string> ModuleIds { get; set; }
    public List<string> RequiredModuleIds { get; set; }
}