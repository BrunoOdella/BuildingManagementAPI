using Domain;

namespace IDataAccess
{
    public interface IBuildingRepository
    {
        Building CreateBuilding(Building building);
        bool DeleteBuilding(Guid buildingId);
        Building GetBuilding(Guid managerId, Guid buildingId);
        Building UpdateBuilding(Building existingBuilding);
    }
}
