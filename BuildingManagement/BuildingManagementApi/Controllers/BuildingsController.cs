using BuildingManagementApi.Filters;
using LogicInterface.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.In;
using Models.Out;

namespace BuildingManagementApi.Controllers
{
    [Route("api/v2/[controller]")]
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

        [HttpPut("{buildingId}")]
        public IActionResult UpdateBuilding(Guid buildingId, [FromBody] UpdateBuildingRequest request)
        {
            string ccadminId = _httpContextAccessor.HttpContext.Items["userID"] as string;
            var buildingToUpdate = request.ToEntity();
            buildingToUpdate.BuildingId = buildingId;

            var updatedBuilding = _buildingLogic.UpdateBuilding(ccadminId, buildingToUpdate, buildingId);
            return Ok(new BuildingResponse(updatedBuilding));
        }

        [HttpGet]
        public IActionResult GetBuildings()
        {
            string adminId = _httpContextAccessor.HttpContext.Items["userID"] as string;
            var buildings = _buildingLogic.GetBuildings(adminId);
            var response = buildings.Select(b => new BuildingResponse(b)).ToList();
            return Ok(response);
        }

        [HttpGet("{buildingId}/apartments")]
        public IActionResult GetApartments(Guid buildingId)
        {
            string managerId = _httpContextAccessor.HttpContext.Items["userID"] as string;
            var apartments = _buildingLogic.GetApartments(managerId, buildingId);
            var response = apartments.Select(b => new ApartmentResponse(b)).ToList();
            return Ok(response);
        }

    }
}
