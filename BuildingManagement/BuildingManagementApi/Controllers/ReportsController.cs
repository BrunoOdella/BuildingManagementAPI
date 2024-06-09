using BuildingManagementApi.Filters;
using LogicInterface.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.Out;

namespace BuildingManagementApi.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(AuthenticationFilter))]
    public class ReportsController : ControllerBase
    {
        private readonly IReportLogicByMaintenanceStaff _logicByMaintenanceStaff;
        private readonly IReportLogicByBuilding _logicByBuilding;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReportsController(IReportLogicByMaintenanceStaff logicByMaintenanceStaff, IReportLogicByBuilding logicByBuilding, IHttpContextAccessor httpContextAccessor)
        {
            _logicByMaintenanceStaff = logicByMaintenanceStaff;
            _logicByBuilding = logicByBuilding;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("request_by_building")]
        public ObjectResult GetReport_RequestByBuilding([FromQuery] string? BuildingID)
        {
            var managerID = new Guid(_httpContextAccessor.HttpContext.Items["userID"] as string);

            if (string.IsNullOrEmpty(BuildingID))
                return Ok(new Report_RequestByBuildingResponse(_logicByBuilding.RequestByBuilding(managerID)));

            return Ok(new Report_RequestByBuildingResponse(_logicByBuilding.RequestByBuilding(managerID, new Guid(BuildingID))));
        }

        [HttpGet("request_by_maintenance_staff")]
        public ObjectResult GetReport_RequestByMaintenanceStaff([FromQuery] string? MaintenanceStaffID)
        {
            var managerID = new Guid(_httpContextAccessor.HttpContext.Items["userID"] as string);

            if (string.IsNullOrEmpty(MaintenanceStaffID))
                return Ok(new Report_RequestByMaintenanceStaffResponse(_logicByMaintenanceStaff.RequestByMaintenanceStaff(managerID)));

            return Ok(new Report_RequestByMaintenanceStaffResponse(_logicByMaintenanceStaff.RequestByMaintenanceStaff(managerID, new Guid(MaintenanceStaffID))));
        }
    }
}
