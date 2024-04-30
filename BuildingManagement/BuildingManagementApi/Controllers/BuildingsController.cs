using LogicInterface.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.In;
using Models.Out;

namespace BuildingManagementApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BuildingsController : ControllerBase
    {
        private readonly IBuildingLogic _buildingLogic;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BuildingsController(IBuildingLogic buildingLogic, IHttpContextAccessor httpContextAccessor)
        {
            _buildingLogic = buildingLogic;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        public IActionResult CreateBuilding([FromBody] CreateBuildingRequest request)
        {
            string managerId = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            var response = _buildingLogic.CreateBuilding(managerId, request.ToEntity());
            return CreatedAtAction(nameof(CreateBuilding), new { id = response.BuildingId }, new BuildingResponse(response));
        }
    }
}
