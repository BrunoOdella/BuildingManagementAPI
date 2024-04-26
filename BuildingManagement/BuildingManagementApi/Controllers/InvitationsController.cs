using BusinessLogic.Logics;
using LogicInterface.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.In;
using Models.Out;

namespace BuildingManagementApi.Controllers
{
    [Route("api/[controller]")]
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
            CreateInvitationResponse response = new CreateInvitationResponse(_invitationLogic.CreateInvitation(invitation.ToEntity()));

            return StatusCode(201, response);
        }
    }
}
