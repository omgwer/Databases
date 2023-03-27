using Entity;
using Entity.Service;
using Service.Model.Dto;

namespace Service.Service;

public class CourseModuleService : ICourseModuleService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseModuleRepository _courseModuleRepository;
     
    public CourseModuleService(ICourseModuleRepository courseModuleRepository, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _courseModuleRepository = courseModuleRepository;
    }
    
    public void SaveModule(CourseModuleParams courseModuleParams)
    {
        var courseModule = _courseModuleRepository.GetCourseModule(courseModuleParams.ModuleId);
        if (courseModule != null)
        {
            throw new InvalidDataException($"Module with id =' {courseModuleParams.ModuleId}' is exists");
        }
        _courseModuleRepository.AddCourseModule(new CourseModule(){CourseId = courseModuleParams.CourseId, ModuleId = courseModuleParams.ModuleId, IsRequired = courseModuleParams.IsRequired});
    }
}