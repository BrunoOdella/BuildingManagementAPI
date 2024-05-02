﻿using LogicInterface.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.In;

namespace BuildingManagementApi.Controllers
{
    [Route("api/v1/buildings/{buildingId}/maintenancestaff")]
    [ApiController]
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
        public IActionResult CreateMaintenanceStaff(Guid buildingId, [FromBody] CreateMaintenanceStaffRequest request)
        {
            string managerId = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            var maintenanceStaff = request.ToEntity(buildingId);
            var response = _maintenanceStaffLogic.AddMaintenanceStaff(managerId, maintenanceStaff);

            return CreatedAtAction(nameof(CreateMaintenanceStaff), new { id = response.ID }, response);
        }
    }
}
