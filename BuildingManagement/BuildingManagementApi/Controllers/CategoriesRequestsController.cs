using System.Data;
using BuildingManagementApi.Filters;
using LogicInterface.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.In;
using Models.Out;

namespace BuildingManagementApi.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(AuthenticationFilter))]
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
            CategoryResponse response = new CategoryResponse(_logic.CreateCategory(request.ToEntity()));

            return StatusCode(201, response);
        }
    }
}
