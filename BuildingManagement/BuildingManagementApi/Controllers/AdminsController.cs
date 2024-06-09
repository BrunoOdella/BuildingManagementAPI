using LogicInterface.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.In;
using Models.Out;


namespace BuildingManagementApi.Controllers
{
    [Route("api/v2/admins")]
    [ApiController]
    //[ServiceFilter(typeof(AuthenticationFilter))]
    public class AdminsController : ControllerBase
    {
        private readonly IAdminLogic _adminLogic;

        public AdminsController(IAdminLogic adminLogic)
        {
            _adminLogic = adminLogic;
        }

        [HttpPost]
        public IActionResult CreateAdmin([FromBody] CreateAdminRequest admin)
        {
            AdminResponse response = new AdminResponse(_adminLogic.CreateAdmin(admin.ToEntity()));

            return StatusCode(201, response);
        }
    }
}
