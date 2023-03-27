using Service.Model.Dto;

namespace Service;

public interface ICourseModuleStatusService
{
    public void SaveMaterialStatus(SaveMaterialStatusParams saveMaterialStatusParams);
}