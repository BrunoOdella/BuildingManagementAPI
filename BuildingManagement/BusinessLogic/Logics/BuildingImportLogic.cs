using Domain;
using LogicInterface.Interfaces;
using System.Reflection;
using IDataAccess;
using ImporterInterface;

namespace BusinessLogic.Logics;

public class BuildingImportLogic : IBuildingImportLogic
{
    private readonly IBuildingLogic _buildingLogic;

    public BuildingImportLogic(IBuildingLogic buildingLogic)
    {
        _buildingLogic = buildingLogic;
    }

    public void ImportBuilding(Guid adminGuid, string assemblyPath)
    {
        var importer = LoadImporter(assemblyPath);
        var request = importer.ImportBuilding();

        foreach (var buildingDto in request)
        {
            Building building = CreateBuilding(adminGuid.ToString(), buildingDto);

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

    private Building CreateBuilding(string adminGuid, BuildingDTO building)
    {
        return new Building
        {
            Name = building.Name,
            Address = building.Address,
            Location = new Location()
            {
                Latitude = building.Location.Latitude,
                Longitude = building.Location.Longitude
            },
            CommonExpenses = building.CommonExpenses,
            Apartments = building.Apartments.Select(a => new Apartment
            {
                Floor = a.Floor,
                Number = a.Number,
                HasTerrace = a.HasTerrace,
                NumberOfBathrooms = a.NumberOfBathrooms,
                Owner = new Owner { Email = a.Owner.Email }
            }).ToList(),
            Manager = new Manager { Email = building.Manager.Email },
            ConstructionCompany = new ConstructionCompany(){ ConstructionCompanyAdminId = Guid.Parse(adminGuid) }
        };
    }
}