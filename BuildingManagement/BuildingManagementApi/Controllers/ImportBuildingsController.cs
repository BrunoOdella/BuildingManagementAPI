using LogicInterface.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.In;
using Models.Out;

namespace BuildingManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            //var adminGuid = new Guid(_httpContextAccessor.HttpContext.Items["userID"] as string);
            var adminGuid = new System.Guid("0C68F751-225B-4BF5-A3F9-08DC801A0E05");
            _buildingImportLogic.ImportBuilding(adminGuid, importBuilding.AssemblyPath);
            return StatusCode(201);
        }
    }
}