namespace Lab3.Domain;

public class Course
{
    public int Id { get; set; } // dbContext
    public int Version { get; set; } // dbContext
    public string CourseId { get; set; }
    public string CreatedAt { get; set; }
    public string UpdatedAt { get; set; }
}