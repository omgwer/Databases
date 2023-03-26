namespace Entity;

public partial class CourseModule
{
    public string ModuleId { get; set; } = null!;

    public string? CourseId { get; set; }

    public string? IsRequired { get; set; }

    public DateTime CreateAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual Course? Course { get; set; }

    public virtual ICollection<CourseModuleStatus> CourseModuleStatuses { get; } = new List<CourseModuleStatus>();
}
