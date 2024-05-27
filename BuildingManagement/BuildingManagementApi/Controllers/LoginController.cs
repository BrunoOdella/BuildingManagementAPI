using LogicInterface.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.In;
using CustomExceptions;
using Microsoft.AspNetCore.Identity.Data;
using System.Security.Authentication;

namespace BuildingManagementApi.Controllers
{
    [Route("api/v2/")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public LoginController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            var token = _authenticationService.Authenticate(loginRequest.Email, loginRequest.Password);
            return Ok(new { Token = token });

        }
    }
}
