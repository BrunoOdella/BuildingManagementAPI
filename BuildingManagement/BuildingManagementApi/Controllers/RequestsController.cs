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
    [Route("api/v1/[controller]")]
    [ApiController]
    //[ServiceFilter(typeof(AuthenticationFilter))]
    public class RequestsController : ControllerBase
    {
        private readonly IRequestLogic _logic;

        public RequestsController(IRequestLogic Ilogic)
        {
            _logic = Ilogic;
        }

        [HttpGet]
        public ObjectResult GetAllRequest([FromQuery] string? category)
        {
            if (string.IsNullOrEmpty(category))
            {
                // Si no se especifica una categoría, obtener todos los Request.
                return Ok(_logic.GetAllRequest().Select(request => new RequestResponse(request)).ToList());
            }
            // Si se especifica una categoría, obtener solo los Request de esa categoría
            return Ok(_logic.GetAllRequest(int.Parse(category)).Select(request => new RequestResponse(request)).ToList());
        }

        [HttpPut("{requestid}/activate")]
        public ObjectResult PutActivateRequest([FromRoute] string requestid, [FromBody] ActiveRequest activeRequest)
        {
            return Ok(new RequestResponse(_logic.ActivateRequest(new Guid(requestid), activeRequest.StartTime)));
        }

        [HttpPut("{requestid}/finished")]
        public ObjectResult PutFinishedRequest([FromRoute] string requestid, [FromBody] FinishedRequest finishedRequest)
        {
            return Ok(new RequestResponse(_logic.TerminateRequest(new Guid(requestid), finishedRequest.EndTime, finishedRequest.TotalCost)));
        }

        [HttpPut("{requestid}")]
        public ObjectResult PutMaintenancePersonRequest([FromRoute] string requestid, [FromBody] MaintenancePersonRequest maintenancePersonRequest)
        {
            return Ok(new RequestResponse(_logic.AsignMaintenancePerson(new Guid(requestid), maintenancePersonRequest.Id)));
        }

    }
}
