using BuildingManagementApi.Filters;
using LogicInterface.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.In;
using Models.Out;

namespace BuildingManagementApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(AuthenticationFilter))]
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
            string managerId = _httpContextAccessor.HttpContext.Items["userID"] as string;
            var response = _buildingLogic.CreateBuilding(managerId, request.ToEntity());
            return CreatedAtAction(nameof(CreateBuilding), new { id = response.BuildingId }, new BuildingResponse(response));
        }

        [HttpDelete("{BuildingId}")]
        public IActionResult DeleteBuilding(Guid BuildingId)
        {
            string managerId = _httpContextAccessor.HttpContext.Items["userID"] as string;
            _buildingLogic.DeleteBuilding(managerId, BuildingId);
            return NoContent();
        }
    }
}
