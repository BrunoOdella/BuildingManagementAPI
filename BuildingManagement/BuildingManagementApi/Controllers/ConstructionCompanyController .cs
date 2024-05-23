using Domain;
using LogicInterface.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.In;
using Models.Out;

namespace BuildingManagementApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConstructionCompanyController : ControllerBase
    {
        private readonly IConstructionCompanyLogic _constructionCompanyLogic;

        public ConstructionCompanyController(IConstructionCompanyLogic constructionCompanyLogic)
        {
            _constructionCompanyLogic = constructionCompanyLogic;
        }

        [HttpPost]
        public IActionResult CreateConstructionCompany([FromBody] CreateConstructionCompanyRequest constructionCompanyRequest)
        {
            var createdCompany = new ConstructionCompanyResponse(_constructionCompanyLogic.CreateConstructionCompany(constructionCompanyRequest.ToEntity()));
            return StatusCode(201, createdCompany);
        }

        [HttpPut]
        public IActionResult UpdateConstructionCompany(
            [FromBody] UpdateConstructionCompanyRequest updateConstructionCompanyRequest)
        {
            var updatedCompany = new ConstructionCompanyResponse(
                _constructionCompanyLogic.UpdateConstructionCompany(updateConstructionCompanyRequest.ToEntity()));
            return StatusCode(200, updatedCompany);
        }
    }
}
