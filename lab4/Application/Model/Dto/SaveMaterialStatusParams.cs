namespace Service.Model.Dto;

public class SaveMaterialStatusParams
{
    public string EnrollmentId { get; set; } = String.Empty;
    public string ModuleId { get; set; } = String.Empty;
    public int Progress { get; set; }
    public int Duration { get; set; }
}