using Domain;

namespace IDataAccess
{
    public interface IBuildingRepository
    {
        Building CreateBuilding(Building building);
        Apartment GetApartment(Guid managerId, Guid apartmentId, Guid buildingId);
        Building GetBuilding(Guid managerId, Guid buildingId);
    }
}
