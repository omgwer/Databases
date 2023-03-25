using Service.Model.Dto;

namespace Service;

public interface IModuleService
{
    public void SaveMaterialStatus(SaveMaterialStatusParams saveMaterialStatusParams);
}