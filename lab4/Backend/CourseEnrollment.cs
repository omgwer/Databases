using System;
using System.Collections.Generic;

namespace lab4;

public partial class CourseEnrollment
{
    public string EnrollmentId { get; set; } = null!;

    public string? CourseId { get; set; }

    public virtual Course? Course { get; set; }

    public virtual CourseStatus Enrollment { get; set; } = null!;
}
