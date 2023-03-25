using Service.Model.Dto;

namespace Service;

public interface IEnrollmentService
{
    public void SaveEnrollment(EnrollmentParams enrollmentParams);
}