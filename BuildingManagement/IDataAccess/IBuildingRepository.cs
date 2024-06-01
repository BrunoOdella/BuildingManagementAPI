using Domain;

namespace IDataAccess
{
    public interface IBuildingRepository
    {
        Building CreateBuilding(Building building);
        bool DeleteBuilding(Guid buildingId);
        IEnumerable<Building> GetAll(Guid managerId);
        public Apartment GetApartment(Guid managerId, Guid apartmentId);
        List<Apartment> GetAllApartments(Guid managerId, Guid buildingId);
        Building GetBuilding(Guid managerId, Guid buildingId);
        Building GetBuilding(Guid buildingId);
        Building GetBuildingByAdmin(Guid adminId, Guid buildingId);
        Building UpdateBuilding(Building existingBuilding);
        public Building GetBuildingByLocation(double latitude, double longitude);
        IEnumerable<Building> GetBuildingsByConstructionCompanyAdminId(Guid constructionCompanyAdminId);
        IEnumerable<Building> GetBuildingsByManagerId(Guid managerId);
    }
}
