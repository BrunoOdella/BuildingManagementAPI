using BuildingManagementApi.Filters;
using LogicInterface.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.In;
using Models.Out;

namespace BuildingManagementApi.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(AuthenticationFilter))]
    public class ImportBuildingsController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IBuildingImportLogic _buildingImportLogic;

        public ImportBuildingsController(IBuildingImportLogic buildingImportLogic,
            IHttpContextAccessor httpContextAccessor)
        {
            _buildingImportLogic = buildingImportLogic;
            _httpContextAccessor = httpContextAccessor;
        }


        [HttpPost]
        public IActionResult ImportBuildings([FromBody] ImportBuildingRequest importBuilding)
        {
            var adminGuid = new Guid(_httpContextAccessor.HttpContext.Items["userID"] as string);
            _buildingImportLogic.ImportBuilding(adminGuid, importBuilding.AssemblyPath);
            return StatusCode(201, "Buildings Created");
        }
    }
}