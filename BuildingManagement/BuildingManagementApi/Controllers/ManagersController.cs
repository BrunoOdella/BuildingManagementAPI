using BuildingManagementApi.Filters;
using LogicInterface.Interfaces.IManagerLogic;
using Microsoft.AspNetCore.Mvc;
using Models.Out;

namespace BuildingManagementApi.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(AuthenticationFilter))]
    public class ManagersController : ControllerBase
    {
        private readonly IManagerLogic _managerLogic;

        public ManagersController(IManagerLogic managerLogic)
        {
            _managerLogic = managerLogic;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var managers = _managerLogic.GetAll();
            var response = managers.Select(b => new ManagerResponse(b)).ToList();
            return Ok(response);
        }
    }
}
