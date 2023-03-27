using Service.Model.Dto;

namespace Service;

public interface ICourseEnrollmentService
{
    public void SaveEnrollment(SaveEnrollmentParams saveEnrollmentParams);
}