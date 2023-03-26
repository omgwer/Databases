namespace Entity;

public partial class CourseModuleStatus
{
    public string EnrollmentId { get; set; } = null!;

    public string ModuleId { get; set; } = null!;

    public decimal? Progress { get; set; }

    public int? Duration { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual CourseStatus Enrollment { get; set; } = null!;

    public virtual CourseModule Module { get; set; } = null!;
}
