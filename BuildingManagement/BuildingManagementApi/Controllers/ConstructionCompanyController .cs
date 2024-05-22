using Domain;
using LogicInterface.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.In;

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
            ConstructionCompany createdCompany = _constructionCompanyLogic.CreateConstructionCompany(constructionCompanyRequest.ToEntity());
            return StatusCode(201, createdCompany);
        }
    }
}
