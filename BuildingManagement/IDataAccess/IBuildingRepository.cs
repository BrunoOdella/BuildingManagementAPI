using Domain;

namespace IDataAccess
{
    public interface IBuildingRepository
    {
        Building CreateBuilding(Building building);
        bool DeleteBuilding(Guid buildingId);
        IEnumerable<Building> GetAll(Guid managerId);
        Apartment GetApartment(Guid managerId, Guid apartmentId, Guid buildingId);
        List<Apartment> GetAllApartments(Guid managerId, Guid buildingId);
        Building GetBuilding(Guid managerId, Guid buildingId);
        Building UpdateBuilding(Building existingBuilding);
    }
}
