using BusinessLogic.Logics;
using LogicInterface.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.In;
using Models.Out;

namespace BuildingManagementApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class InvitationsController : ControllerBase
    {
        private readonly IInvitationLogic _invitationLogic;
        public InvitationsController(IInvitationLogic invitationLogic)
        {
            _invitationLogic = invitationLogic;
        }

        [HttpPost]
        public IActionResult CreateInvitation([FromBody] CreateInvitationRequest invitation)
        {
            InvitationResponse response = new InvitationResponse(_invitationLogic.CreateInvitation(invitation.ToEntity()));

            return StatusCode(201, response);
        }

        [HttpGet]
        public ObjectResult GetAllInvitations()
        {
            InvitationsResponse response = new InvitationsResponse(_invitationLogic.GetAllInvitations());
            return StatusCode(200, response);
        }
    }
}
