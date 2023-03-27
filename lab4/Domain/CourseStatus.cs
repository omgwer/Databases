namespace Entity;

public partial class CourseStatus
{
    public string EnrollmentId { get; set; } = null!;

    public decimal? Progress { get; set; }

    public int? Duration { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual CourseEnrollment? CourseEnrollment { get; set; }

    public virtual ICollection<CourseModuleStatus> CourseModuleStatuses { get; } = new List<CourseModuleStatus>();
}
