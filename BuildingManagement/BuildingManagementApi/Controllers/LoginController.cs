﻿using LogicInterface.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.In;
using CustomExceptions;
using Microsoft.AspNetCore.Identity.Data;
using System.Security.Authentication;
using Models.Out;

namespace BuildingManagementApi.Controllers
{
    [Route("api/v2")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public LoginController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Models.In.LoginRequest loginRequest)
        {
            var authResult = _authenticationService.Authenticate(loginRequest.Email, loginRequest.Password);
            return Ok(new LoginResponse(authResult.UserID.ToString(), authResult.UserType));

        }
    }
}