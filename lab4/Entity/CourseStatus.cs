﻿namespace Entity;

public class CourseStatus
{
    public string EnrollmentId { get; set; } = string.Empty;
    public Course? Course { get; set; }
    public int Progress { get; set; }
    public int Duration { get; set; }
    public string DeletedAt { get; set; } = string.Empty;
}