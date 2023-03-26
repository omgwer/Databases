namespace Entity;

public partial class CourseEnrollment
{
    public string EnrollmentId { get; set; } = null!;

    public string? CourseId { get; set; }

    public virtual Course? Course { get; set; }

    public virtual CourseStatus Enrollment { get; set; } = null!;
}
