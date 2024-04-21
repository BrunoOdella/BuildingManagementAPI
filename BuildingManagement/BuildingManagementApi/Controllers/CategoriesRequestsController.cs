using System.Data;
using LogicInterface.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.In;
using Models.Out;

namespace BuildingManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesRequestsController : ControllerBase
    {
        private readonly ICategoriesRequestsLogic _logic;

        public CategoriesRequestsController(ICategoriesRequestsLogic logic)
        {
            _logic = logic;
        }

        [HttpPost]
        public IActionResult CreateCategory([FromBody] CreateCategoryRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
