﻿using Domain;

namespace IDataAccess
{
    public interface IBuildingRepository
    {
        Building CreateBuilding(Building building);
        Building GetBuilding(Guid managerId, Guid buildingId);
    }
}
