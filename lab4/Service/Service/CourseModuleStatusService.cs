using Entity;
using Entity.Service;
using Service.Model.Dto;

namespace Service.Service;

public class CourseModuleStatusService : ICourseModuleStatusService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseModuleStatusRepository _courseModuleStatusRepository;
    private readonly ICourseStatusRepository _courseStatusRepository;
    private readonly ICourseModuleRepository _courseModuleRepository;
    
    
    public CourseModuleStatusService(ICourseModuleStatusRepository courseModuleStatusRepository, ICourseStatusRepository courseEnrollmentRepository, ICourseModuleRepository courseModuleRepository, IUnitOfWork unitOfWork)
    {
        _courseModuleStatusRepository = courseModuleStatusRepository;
        _courseModuleRepository = courseModuleRepository;
        _unitOfWork = unitOfWork;
        
        _courseStatusRepository = courseEnrollmentRepository;
    }
    
    
    // public string EnrollmentId { get; set; } = String.Empty;
    // public string ModuleId { get; set; } = String.Empty;
    // public int Progress { get; set; }
    // public int Duration { get; set; }
    public void SaveMaterialStatus(SaveMaterialStatusParams saveMaterialStatusParams)
    {
        CourseModule? courseModule = _courseModuleRepository.GetCourseModule(saveMaterialStatusParams.ModuleId);
        if (courseModule == null)
            throw new Exception($"Module with id {saveMaterialStatusParams.ModuleId} is not exist");

        CourseModuleStatus? courseModuleStatus = _courseModuleStatusRepository.Get(saveMaterialStatusParams.EnrollmentId, saveMaterialStatusParams.ModuleId);
        if (courseModuleStatus == null)
        {
            _courseModuleStatusRepository.Add(
                new CourseModuleStatus(){ 
                    EnrollmentId = saveMaterialStatusParams.EnrollmentId, 
                    ModuleId = saveMaterialStatusParams.ModuleId, 
                    Duration = saveMaterialStatusParams.Duration, 
                    Progress = saveMaterialStatusParams.Progress}); }
        else
        {
            courseModuleStatus.Duration += saveMaterialStatusParams.Duration;
            if (!courseModule.IsRequired.Equals(null) && courseModule.IsRequired == true)
            {
                courseModuleStatus.Progress = 100;
            }
            else
            {
                courseModuleStatus.Progress = saveMaterialStatusParams.Progress;
            }

            _courseModuleStatusRepository.Update(courseModuleStatus);
        }

        _unitOfWork.Commit();
    }
}