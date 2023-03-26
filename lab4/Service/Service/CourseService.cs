using Entity;
using Entity.Service;
using Service.Model.Dto;

namespace Service.Service;

public class CourseService : ICourseService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseRepository _courseRepository;
    private readonly ICourseModuleService _courseModuleService;

    //TODO - как правильнее, в одном сервисе использоватьь другие сервисы, или репозитории
    public CourseService(ICourseRepository courseRepository, ICourseModuleService courseModuleService,
        IUnitOfWork unitOfWork)
    {
        _courseRepository = courseRepository;
        _unitOfWork = unitOfWork;
        _courseModuleService = courseModuleService;
    }

    public void SaveCourse(SaveCourseParams saveCourseParams)
    {
        var course = _courseRepository.GetCourse(saveCourseParams.CourseId);
        if (course != null)
        {
            throw new InvalidDataException($"Course with id =' {saveCourseParams.CourseId}' is exists");
        }

        _courseRepository.AddCourse(new Course() {CourseId = saveCourseParams.CourseId});

        var requireModuleIds = saveCourseParams.RequiredModuleIds;
        var moduleIds = saveCourseParams.ModuleIds;
        // убираем повторы из moduleIds
        foreach (var requireModule in requireModuleIds)
        {
            moduleIds.Remove(requireModule);
        }

        requireModuleIds.ForEach(e => _courseModuleService
            .SaveModule(new CourseModuleParams()
            {
                ModuleId = e, CourseId = saveCourseParams.CourseId, IsRequired = true
            }));
        
        moduleIds.ForEach(e => _courseModuleService
            .SaveModule(new CourseModuleParams()
            {
                ModuleId = e, CourseId = saveCourseParams.CourseId, IsRequired = false
            }));

        _unitOfWork.Commit();
    }

    public void DeleteCourse(string courseId)
    {
        throw new NotImplementedException();
    }

    public CourseStatusData GetCourseStatus(CourseStatusParams courseStatusParams)
    {
        throw new NotImplementedException();
    }
}