using BuildingManagementApi.Filters;
using LogicInterface.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.In;
using Models.Out;

namespace BuildingManagementApi.Controllers
{
    [ApiController]
    [Route("api/v2/[controller]")]
    [ServiceFilter(typeof(AuthenticationFilter))]
    public class ConstructionCompanyController : ControllerBase
    {
        private readonly IConstructionCompanyLogic _constructionCompanyLogic;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ConstructionCompanyController(IConstructionCompanyLogic constructionCompanyLogic, IHttpContextAccessor httpContextAccessor)
        {
            _constructionCompanyLogic = constructionCompanyLogic;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        public IActionResult CreateConstructionCompany([FromBody] CreateConstructionCompanyRequest constructionCompanyRequest)
        {
            string ConsCompanyAdminId = _httpContextAccessor.HttpContext.Items["userID"] as string;
            ConstructionCompanyResponse createdCompany = new ConstructionCompanyResponse(_constructionCompanyLogic.CreateConstructionCompany(constructionCompanyRequest.ToEntity(), ConsCompanyAdminId));
            return StatusCode(201, createdCompany);
        }

        [HttpPut]
        public IActionResult UpdateConstructionCompany(
            [FromBody] UpdateConstructionCompanyRequest updateConstructionCompanyRequest)
        {
            string ConsCompanyAdminId = _httpContextAccessor.HttpContext.Items["userID"] as string;
            ConstructionCompanyResponse updatedCompany = new ConstructionCompanyResponse(
                _constructionCompanyLogic.UpdateConstructionCompanyName(updateConstructionCompanyRequest.ToEntity(), ConsCompanyAdminId));
            return StatusCode(200, updatedCompany);
        }
    }
}
