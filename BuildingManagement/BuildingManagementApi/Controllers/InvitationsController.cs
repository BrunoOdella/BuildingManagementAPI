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
    public class InvitationsController : ControllerBase
    {
        private readonly IInvitationLogic _invitationLogic;
        public InvitationsController(IInvitationLogic invitationLogic)
        {
            _invitationLogic = invitationLogic;
        }

        [HttpPost]
        [ServiceFilter(typeof(AuthenticationFilter))]
        public IActionResult CreateInvitation([FromBody] CreateInvitationRequest invitation)
        {
            InvitationResponse response = new InvitationResponse(_invitationLogic.CreateInvitation(invitation.ToEntity()));

            return StatusCode(201, response);
        }

        [HttpGet]
        [ServiceFilter(typeof(AuthenticationFilter))]
        public ObjectResult GetAllInvitations()
        {
            return Ok(_invitationLogic.GetAllInvitations().Select(invitation => new InvitationResponse(invitation)).ToList());
        }

        [HttpDelete("{InvitationId}")]
        [ServiceFilter(typeof(AuthenticationFilter))]
        public IActionResult DeleteInvitation([FromRoute] Guid InvitationId)
        {
            _invitationLogic.DeleteInvitation(InvitationId);
            return NoContent();
        }

        [HttpPut("{InvitationId}")]
        public IActionResult AcceptInvitation([FromRoute] Guid InvitationId, [FromBody] AcceptInvitationRequest acceptRequest)
        {
            var response = _invitationLogic.AcceptInvitation(InvitationId, acceptRequest.Email, acceptRequest.Password);
            return Ok(new InvitationResponse(response));
        }


    }
}
