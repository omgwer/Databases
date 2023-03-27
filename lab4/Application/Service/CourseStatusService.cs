using Entity;
using Entity.Service;
using Service.Model.Dto;

namespace Service.Service;

public class CourseStatusService : ICourseStatusService
{
    private readonly IUnitOfWork _unitOfWork;
    
    private readonly ICourseRepository _courseRepository;
    private readonly ICourseEnrollmentRepository _courseEnrollmentRepository;
    private readonly ICourseModuleRepository _courseModuleRepository;
    private readonly ICourseModuleStatusRepository _courseModuleStatusRepository;
    private readonly ICourseStatusRepository _courseStatusRepository;
    

    public CourseStatusService(ICourseRepository courseRepository, 
        IUnitOfWork unitOfWork, ICourseEnrollmentRepository courseEnrollmentRepository, ICourseModuleRepository courseModuleRepository,
        ICourseModuleStatusRepository courseModuleStatusRepository, ICourseStatusRepository courseStatusRepository)
    {
        _courseRepository = courseRepository;
        _unitOfWork = unitOfWork;
        _courseEnrollmentRepository = courseEnrollmentRepository;
        _courseModuleRepository = courseModuleRepository;
        _courseModuleStatusRepository = courseModuleStatusRepository;
        _courseStatusRepository = courseStatusRepository;
    }

    public CourseStatusData GetCourseStatus(CourseStatusParams courseStatusParams)
    {
        CourseStatusData courseStatusData = new CourseStatusData();
        CourseEnrollment? courseEnrollment = _courseEnrollmentRepository.Get(courseStatusParams.EnrollmentId);
        var courseStatus = _courseStatusRepository.GetCourseStatus(courseStatusParams.EnrollmentId)!;
        //var courseModules = _courseModuleRepository.getCourseModulesListByCourseId(courseStatusParams.CourseId);
        var courseModuleStatusList = _courseModuleStatusRepository.GetListByEnrollmentId(courseEnrollment!.EnrollmentId);
        
        courseStatusData.EnrollmentId = courseStatusParams.EnrollmentId;
        courseModuleStatusList.ForEach(e=> courseStatusData.Modules
            .Add(new ModuleStatusData()
            {
                ModuleId = e.ModuleId, Progress = e.Progress
            }));
        courseStatusData.Progress = courseStatus.Progress;
        courseStatusData.Duration = courseStatus.Duration;
        return courseStatusData;
    }
}