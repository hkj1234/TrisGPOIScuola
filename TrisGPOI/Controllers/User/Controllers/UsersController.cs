using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using TrisGPOI.Controllers.User.Entities;
using TrisGPOI.Core.User.Exceptions;
using TrisGPOI.Core.User.Interfaces;

namespace TrisGPOI.Controllers.User.Controllers
{
    [ApiController]
    [Route("")]
    public class UsersController : ControllerBase
    {
        private readonly IUserManager _userManager;
        public UsersController(IUserManager customerManager)
        {
            _userManager = customerManager;
        }


        //Login
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(UserLoginRequest model)
        {
            try
            {
                var token = await _userManager.LoginAsync(model.ToUserLogin());
                return Ok(new { token = token });
            }
            catch (WrongEmailOrPasswordException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return NotFound($"Resource not found {e.Message}");
            }
        }


        //Register
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync(UserRegisterRequest model)
        {
            try
            {
                await _userManager.RegisterAsync(model.ToUserRegister());
                return Ok();
            }
            catch (ExisitingEmailException e)
            {
                return Conflict(e.Message);
            }
            catch (Exception e)
            {
                return NotFound($"Resource not found {e.Message}");
            }
        }
    }
}
