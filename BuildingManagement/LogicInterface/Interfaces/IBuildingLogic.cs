namespace LogicInterface.Interfaces
{
    public interface IBuildingLogic
    {
        Building CreateBuilding(string managerId, Building building);
        void DeleteBuilding(string? managerId, Guid buildingId);
        Building UpdateBuilding(string managerId, Building building, Guid buildingId);
        IEnumerable<Building> GetBuildings(string personId);
        IEnumerable<Apartment> GetApartments(string managerId, Guid buildingId);
    }
}
