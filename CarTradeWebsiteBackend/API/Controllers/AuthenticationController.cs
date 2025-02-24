﻿using CarTradeWebsite.DataAccess.Repositories.Interfaces;
using CarTradeWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CarTradeWebsite.API.Controllers
{
    public class AuthenticationController : ControllerBase
    {
        private IAuthenticationRepository _authenticationRepository;

        public AuthenticationController(IAuthenticationRepository searchRepository)
        {
            this._authenticationRepository = searchRepository;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthenticatedResponseModel>> Login([FromBody] UserLoginModel user)
        {
            AuthenticatedResponseModel isLoginSuccessfull = await this._authenticationRepository.Login(user);
            
            if (isLoginSuccessfull != null)
            {
                return Ok(isLoginSuccessfull);
            }
            
            return Unauthorized();
        }
    }
}
