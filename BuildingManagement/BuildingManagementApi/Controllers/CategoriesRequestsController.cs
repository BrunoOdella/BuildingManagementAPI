using System.Data;
using BuildingManagementApi.Filters;
using BusinessLogic.Logics;
using Domain;
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
        private readonly ICategoriesRequestsLogic _categoriesRequestsLogic;

        public CategoriesRequestsController(ICategoriesRequestsLogic categoriesRequestsLogic)
        {
            _categoriesRequestsLogic = categoriesRequestsLogic;
        }

        [HttpPost]
        public IActionResult CreateCategory([FromBody] CreateCategoryRequest request)
        {
            CategoryResponse response = new CategoryResponse(_categoriesRequestsLogic.CreateCategory(request.ToEntity()));

            return StatusCode(201, response);
        }

        [HttpGet]
        public IActionResult GetAllCategories()
        {
            var categories = _categoriesRequestsLogic.GetAllCategories();
            var response = categories.Select(b => new CategoryResponse(b)).ToList();
            return Ok(response);
        }

    }
}
