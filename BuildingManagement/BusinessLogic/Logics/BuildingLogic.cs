using Domain;
using IDataAccess;
using LogicInterface.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Logics
{
    public class BuildingLogic : IBuildingLogic
    {
        private IBuildingRepository _buildingRepository;

        public BuildingLogic(IBuildingRepository buildingRepository)
        {
            _buildingRepository = buildingRepository;
        }

        public Building CreateBuilding(string managerId, Building building)
        {
            if (!Guid.TryParse(managerId, out Guid parsedManagerId))
            {
                throw new ArgumentException("Invalid manager ID");
            }

            // Agregar validaciones
            return _buildingRepository.AddBuilding(building);
        }
    }


}
