using BuildingManagementApi.Filters;
using LogicInterface.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.In;
using Models.Out;

namespace BuildingManagementApi.Controllers
{
    [Route("api/v2/maintenancestaff")]
    [ApiController]
    [ServiceFilter(typeof(AuthenticationFilter))]
    public class MaintenanceStaffController : ControllerBase
    {
        private readonly IMaintenanceStaffLogic _maintenanceStaffLogic;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MaintenanceStaffController(IMaintenanceStaffLogic maintenanceStaffLogic, IHttpContextAccessor httpContextAccessor)
        {
            _maintenanceStaffLogic = maintenanceStaffLogic;
            _httpContextAccessor = httpContextAccessor;

        }

        [HttpPost]
        public IActionResult CreateMaintenanceStaff([FromBody] CreateMaintenanceStaffRequest request)
        {
            string managerId = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            var maintenanceStaff = request.ToEntity();
            var createdStaff = _maintenanceStaffLogic.AddMaintenanceStaff(managerId, maintenanceStaff);
            var response = new CreateMaintenanceStaffResponse(createdStaff);

            return new CreatedResult(string.Empty, response);
        }

        [HttpGet]
        public IActionResult GetAllMaintenanceStaff()
        {
            string managerId = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            var maintenanceStaff = _maintenanceStaffLogic.GetAllMaintenanceStaff(managerId);
            var response = maintenanceStaff.Select(b => new CreateMaintenanceStaffResponse(b)).ToList();
            return Ok(response);
        }
    }
}
