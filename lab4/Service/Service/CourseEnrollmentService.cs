using Entity;
using Entity.Service;
using Service.Model.Dto;

namespace Service.Service;

public class CourseEnrollmentService : ICourseEnrollmentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseStatusRepository _courseStatusRepository;
    private readonly ICourseEnrollmentRepository _courseEnrollmentRepository;
    
    public CourseEnrollmentService(ICourseStatusRepository courseStatusRepository, ICourseEnrollmentRepository courseEnrollmentRepository, IUnitOfWork unitOfWork)
    {
        _courseStatusRepository = courseStatusRepository;
        _courseEnrollmentRepository = courseEnrollmentRepository;
        _unitOfWork = unitOfWork;
    }

    public void SaveEnrollment(SaveEnrollmentParams saveCourseParams)
    {
        _courseEnrollmentRepository.Add(new CourseEnrollment(){EnrollmentId = saveCourseParams.EnrollmentId, CourseId = saveCourseParams.CourseId});
        _courseStatusRepository.Add(new CourseStatus(){EnrollmentId = saveCourseParams.EnrollmentId, Duration = 0, Progress = 0});
        _unitOfWork.Commit();
    }
}