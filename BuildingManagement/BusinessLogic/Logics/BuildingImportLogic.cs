using Domain;
using LogicInterface.Interfaces;
using System.Reflection;
using IDataAccess;
using ImporterInterface;

namespace BusinessLogic.Logics;

public class BuildingImportLogic : IBuildingImportLogic
{
    private readonly IBuildingLogic _buildingLogic;

    public void ImportBuilding(Guid adminGuid, string assemblyPath)
    {
        var importer = LoadImporter(assemblyPath);
        List<Building> request = importer.ImportBuilding();

        foreach (var building in request)
        {
            building.ConstructionCompanyAdminId = adminGuid;
            _buildingLogic.CreateBuilding(adminGuid.ToString(), building);
        }
    }

    private IBuildingImporter LoadImporter(string assemblyPath)
    {
        Assembly assembly = Assembly.LoadFrom(assemblyPath);
        foreach (Type type in assembly.GetTypes())
        {
            if (typeof(IBuildingImporter).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
            {
                return (IBuildingImporter)Activator.CreateInstance(type);
            }
        }
        throw new InvalidOperationException("No valid importer found in assembly.");
    }
}