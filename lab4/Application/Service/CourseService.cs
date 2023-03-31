using Entity;
using Entity.Service;
using Service.Model.Dto;

namespace Service.Service;

public class CourseService : ICourseService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseModuleService _courseModuleService;
    
    private readonly ICourseRepository _courseRepository;
    private readonly ICourseEnrollmentRepository _courseEnrollmentRepository;
    private readonly ICourseModuleRepository _courseModuleRepository;
    private readonly ICourseModuleStatusRepository _courseModuleStatusRepository;
    private readonly ICourseStatusRepository _courseStatusRepository;
    

    public CourseService(ICourseRepository courseRepository, ICourseModuleService courseModuleService,
        IUnitOfWork unitOfWork, ICourseEnrollmentRepository courseEnrollmentRepository, ICourseModuleRepository courseModuleRepository,
        ICourseModuleStatusRepository courseModuleStatusRepository, ICourseStatusRepository courseStatusRepository)
    {
        _courseRepository = courseRepository;
        _unitOfWork = unitOfWork;
        _courseModuleService = courseModuleService;
        _courseEnrollmentRepository = courseEnrollmentRepository;
        _courseModuleRepository = courseModuleRepository;
        _courseModuleStatusRepository = courseModuleStatusRepository;
        _courseStatusRepository = courseStatusRepository;
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
        var courseModules = _courseModuleRepository.GetCourseModulesListByCourseId(courseId);
        var courseEnrollment = _courseEnrollmentRepository.GetByCourseId(courseId)!;
        
        _courseEnrollmentRepository.Delete(courseEnrollment.EnrollmentId);
        courseModules.ForEach( e => _courseModuleRepository.DeleteCourseModule(e));
        _courseRepository.DeleteCourse(courseId);
        _courseStatusRepository.DeleteCourseStatus(courseEnrollment.EnrollmentId);
        var courseModuleStatusList = _courseModuleStatusRepository.GetListByEnrollmentId(courseEnrollment.EnrollmentId);
        courseModuleStatusList.ForEach( e => _courseModuleStatusRepository.Delete(e));
        _unitOfWork.Commit();
    }

    public CourseStatusData GetCourseStatus(CourseStatusParams courseStatusParams)
    {
        throw new NotImplementedException();
    }
}