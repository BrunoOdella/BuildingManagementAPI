using LogicInterface.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.In;
using Models.Out;


namespace BuildingManagementApi.Controllers
{
    [Route("api/admins")]
    [ApiController]
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
            CreateAdminResponse response = new CreateAdminResponse(_adminLogic.CreateAdmin(admin.ToEntity()));
            return Ok(response);
        }
    }
}
