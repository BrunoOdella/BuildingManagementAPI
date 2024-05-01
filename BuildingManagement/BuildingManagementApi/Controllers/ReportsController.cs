using BuildingManagementApi.Filters;
using Domain;
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
        private readonly IReportLogicByMaintenanceStaff _logicByMaintenanceStaff;
        private readonly IReportLogicByBuilding _logicByBuilding;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReportsController(IReportLogicByMaintenanceStaff logicByMaintenanceStaff, IReportLogicByBuilding logicByBuilding,IHttpContextAccessor httpContextAccessor)
        {
            _logicByMaintenanceStaff = logicByMaintenanceStaff;
            _logicByBuilding = logicByBuilding;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("request_by_building/{BuildingID}")]
        public ObjectResult GetReport_RequestByBuilding([FromQuery] string? buildingID)
        {
            var managerID = new Guid(_httpContextAccessor.HttpContext.Items["userID"] as string);

            if (string.IsNullOrEmpty(buildingID))
                return Ok(new Report_RequestByBuildingResponse(_logicByBuilding.RequestByBuilding(managerID)));

            return Ok(new Report_RequestByBuildingResponse(_logicByBuilding.RequestByBuilding(managerID, new Guid(buildingID))));
        }

        [HttpGet("request_by_maintenance_staff/{MaintenanceStaffID}")]
        public ObjectResult GetReport_RequestByMaintenanceStaff([FromQuery] string? buildingID)
        {
            var managerID = new Guid(_httpContextAccessor.HttpContext.Items["userID"] as string);

            if (string.IsNullOrEmpty(buildingID))
                return Ok(new Report_RequestByBuildingResponse(_logicByMaintenanceStaff.RequestByMaintenanceStaff(managerID)));

            return Ok(new Report_RequestByBuildingResponse(_logicByMaintenanceStaff.RequestByMaintenanceStaff(managerID, new Guid(buildingID))));
        }
    }
}
