using BuildingManagementApi.Filters;
using BusinessLogic.Logics;
using Domain;
using LogicInterface.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.In;
using Models.Out;

namespace BuildingManagementApi.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(AuthenticationFilter))]
    public class RequestsController : ControllerBase
    {
        private readonly IRequestLogic _logic;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public RequestsController(IRequestLogic Ilogic, IHttpContextAccessor httpContextAccessor)
        {
            _logic = Ilogic;
            _httpContextAccessor = httpContextAccessor;

        }

        [HttpGet]
        public ObjectResult GetAllRequest([FromQuery] int? category)
        {
            var personID = new Guid(_httpContextAccessor.HttpContext.Items["userID"] as string);
            if (category is null)
            {
                // Si no se especifica una categoría, obtener todos los Request.
                return Ok(_logic.GetAllRequest(personID).Select(request => new RequestResponse(request)).ToList());
            }
            // Si se especifica una categoría, obtener solo los Request de esa categoría
            return Ok(_logic.GetAllRequest(personID, (int)category).Select(request => new RequestResponse(request)).ToList());
        }

        /*
        [HttpPut("{requestid}/activate")]
        public ObjectResult PutActivateRequest([FromRoute] string requestid, [FromBody] ActiveRequest activeRequest)
        {
            return Ok(new RequestResponse(_logic.ActivateRequest(new Guid(_httpContextAccessor.HttpContext.Items["userID"] as string), new Guid(requestid), activeRequest.StartTime)));
        }
        */

        [HttpPut("{requestid}/finished")]
        public ObjectResult PutFinishedRequest([FromRoute] string requestid, [FromBody] FinishedRequest finishedRequest)
        {
            var managerID = new Guid(_httpContextAccessor.HttpContext.Items["userID"] as string);
            return Ok(new RequestResponse(_logic.TerminateRequest(managerID, new Guid(requestid), finishedRequest.EndTime, finishedRequest.TotalCost)));
        }

        [HttpPut("{requestid}")]
        public ObjectResult PutMaintenancePersonRequest([FromRoute] string requestid, [FromBody] ActivateRequest ActivateRequest)
        {
            var staffID = new Guid(_httpContextAccessor.HttpContext.Items["userID"] as string);
            return Ok(new RequestResponse(_logic.ActivateRequest(staffID, new Guid(requestid), ActivateRequest.MaintenancePersonId, ActivateRequest.StartTime)));
        }

        [HttpPost]
        public ObjectResult PostRequest([FromBody] CreateRequestRequest createRequest)
        {
            var managerID = new Guid(_httpContextAccessor.HttpContext.Items["userID"] as string);
            return StatusCode(201, new CreateRequestResponse(_logic.CreateRequest(managerID, createRequest.ToEntity())));
        }

    }
}
