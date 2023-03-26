namespace Service.Model.Dto;

public class CourseModuleParams
{
    public string ModuleId { get; set; } 
    public string CourseId { get; set; }
    public bool IsRequired { get; set; } = false;
}