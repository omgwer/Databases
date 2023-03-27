using Service.Model.Dto;

namespace Service;

public interface ICourseModuleService
{
    public void SaveModule(CourseModuleParams courseModuleParams);
}