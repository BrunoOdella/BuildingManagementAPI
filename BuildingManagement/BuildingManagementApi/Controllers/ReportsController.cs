using BuildingManagementApi.Filters;
using LogicInterface.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Out;

namespace BuildingManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(AuthenticationFilter))]
    public class ReportsController : ControllerBase
    {
        private readonly IReportLogic _logic;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReportsController(IReportLogic logic, IHttpContextAccessor httpContextAccessor)
        {
            _logic = logic;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("request_by_building")]
        public ObjectResult GetReport_RequestByBuilding()
        {
            var managerID = new Guid(_httpContextAccessor.HttpContext.Items["userID"] as string);
            return Ok(_logic.RequestByBuilding(managerID)
                .Select(request => new Report_RequestByBuildingResponse(request)).ToList());
            return null;
        }
    }
}
