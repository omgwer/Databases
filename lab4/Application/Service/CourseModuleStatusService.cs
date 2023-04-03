using Entity;
using Entity.Service;
using Service.Model.Dto;

namespace Service.Service;

public class CourseModuleStatusService : ICourseModuleStatusService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseModuleStatusRepository _courseModuleStatusRepository;
    private readonly ICourseModuleRepository _courseModuleRepository;
    private readonly ICourseEnrollmentRepository _courseEnrollmentRepository;
    private readonly ICourseStatusRepository _courseStatusRepository;

    public CourseModuleStatusService(ICourseModuleStatusRepository courseModuleStatusRepository,
        ICourseModuleRepository courseModuleRepository, IUnitOfWork unitOfWork,
        ICourseEnrollmentRepository courseEnrollmentRepository, ICourseStatusRepository courseStatusRepository)
    {
        _courseModuleStatusRepository = courseModuleStatusRepository;
        _courseModuleRepository = courseModuleRepository;
        _unitOfWork = unitOfWork;
        _courseEnrollmentRepository = courseEnrollmentRepository;
        _courseStatusRepository = courseStatusRepository;
    }

    //TODO обновлять courseStatus
    public void SaveMaterialStatus(SaveMaterialStatusParams saveMaterialStatusParams)
    {
        CourseModule? courseModule = _courseModuleRepository.GetCourseModule(saveMaterialStatusParams.ModuleId);
        if (courseModule == null)
            throw new Exception($"Module with id {saveMaterialStatusParams.ModuleId} is not exist");

        CourseModuleStatus? courseModuleStatus =
            _courseModuleStatusRepository.Get(saveMaterialStatusParams.EnrollmentId, saveMaterialStatusParams.ModuleId);

        var progress = courseModule.IsRequired == true ? 100 : saveMaterialStatusParams.Progress;

        if (courseModuleStatus == null)
        {
            _courseModuleStatusRepository.Add(
                new CourseModuleStatus()
                {
                    EnrollmentId = saveMaterialStatusParams.EnrollmentId,
                    ModuleId = saveMaterialStatusParams.ModuleId,
                    Duration = saveMaterialStatusParams.Duration,
                    Progress = progress
                });
        }
        else
        {
            courseModuleStatus.Duration += saveMaterialStatusParams.Duration;
            courseModuleStatus.Progress = progress;
            _courseModuleStatusRepository.Update(courseModuleStatus);
        }

        // нужно обойти весь список с модулями, и высчитать прогресс
        var courseModuleStatusList =
            _courseModuleStatusRepository.GetListByEnrollmentId(saveMaterialStatusParams.EnrollmentId);
        bool areAllModulesOptional = true;
        foreach (var courseModuleStatusElement in courseModuleStatusList)
        {
            var module = _courseModuleRepository.GetCourseModule(courseModuleStatusElement.ModuleId);
            if ((bool)module.IsRequired)
                areAllModulesOptional = false;
        }

        if (areAllModulesOptional)
        {
            var course = _courseStatusRepository.GetCourseStatus(saveMaterialStatusParams.EnrollmentId);
            course!.Progress = 100;
            _courseStatusRepository.UpdateCourseStatus(course);
        }

        _unitOfWork.Commit();
    }
}