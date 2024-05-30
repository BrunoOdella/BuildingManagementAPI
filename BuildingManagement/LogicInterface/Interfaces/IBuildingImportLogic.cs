using Domain;

namespace LogicInterface.Interfaces;

public interface IBuildingImportLogic
{
    void ImportBuilding(Guid adminGuid, string assemblyPath);
}