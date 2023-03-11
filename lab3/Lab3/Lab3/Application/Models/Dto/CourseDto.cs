using Lab3.Domain;

namespace Lab3.Application.Models.Dto;

public class CourseDto
{
    public string CourseId { get; set; }
    public List<string> ModuleIds { get; set; }
    public List<string> RequiredModuleIds { get; set; }

    public Course ToCourse(CourseDto dto)
    {
        return new Course
        {
            Id = 0,
            CourseId = dto.CourseId,
            ModuleIds = dto.ModuleIds,
            RequiredModuleIds = dto.RequiredModuleIds
        };
    }

}