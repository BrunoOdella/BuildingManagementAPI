using BuildingManagementApi.Filters;
using Domain;
using LogicInterface.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.In;
using Models.Out;
using System.Net;

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
            var createdCompany = new ConstructionCompanyResponse(_constructionCompanyLogic.CreateConstructionCompany(constructionCompanyRequest.ToEntity(), ConsCompanyAdminId));
            return StatusCode(201, createdCompany);
        }

        [HttpPut]
        public IActionResult UpdateConstructionCompany(
            [FromBody] UpdateConstructionCompanyRequest updateConstructionCompanyRequest)
        {
            var updatedCompany = new ConstructionCompanyResponse(
                _constructionCompanyLogic.UpdateConstructionCompanyName(updateConstructionCompanyRequest.ToEntity(), updateConstructionCompanyRequest.ActualName));
            return StatusCode(200, updatedCompany);
        }
    }
}
