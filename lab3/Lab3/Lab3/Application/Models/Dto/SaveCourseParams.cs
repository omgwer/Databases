namespace Lab3.Application.Models.Dto;

public class SaveCourseParams
{
    public string CourseId { get; set; }
    public List<string> ModuleIds { get; set; }
    public List<string> RequiredModuleIds { get; set; }
}