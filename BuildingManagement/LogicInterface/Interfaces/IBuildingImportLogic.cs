using Domain;

namespace LogicInterface.Interfaces;

public interface IBuildingImportLogic
{
    Building ImportBuilding(Guid adminGuid, string assemblyPath);
}