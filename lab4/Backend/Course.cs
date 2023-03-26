using System;
using System.Collections.Generic;

namespace lab4;

public partial class Course
{
    public string CourseId { get; set; } = null!;

    public int Version { get; set; }

    public DateTime CreateAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<CourseEnrollment> CourseEnrollments { get; } = new List<CourseEnrollment>();

    public virtual ICollection<CourseModule> CourseModules { get; } = new List<CourseModule>();
}
