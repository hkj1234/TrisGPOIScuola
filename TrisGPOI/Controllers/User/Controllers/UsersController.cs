using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using TrisGPOI.Controllers.User.Entities;
using TrisGPOI.Core.OTP.Exceptions;
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
        public async Task<IActionResult> LoginAsync([FromBody]UserLoginRequest model)
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
            catch (AccountNotActivedException e)
            {
                return StatusCode(406, e.Message);
            }
            catch (Exception e)
            {
                return NotFound($"Resource not found {e.Message}");
            }
        }


        //Register
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] UserRegisterRequest model)
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


        //VerifyOTP
        [HttpPost("VerifyOTP")]
        public async Task<IActionResult> VerifyOTP([FromBody] VerifyOTPRequest model)
        {
            try
            {
                string token = await _userManager.VerifyOTP(model.OTP, model.email);
                return Ok(new { token = token });
            }
            catch (WrongEmailOrOTPExeption e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return NotFound($"Resource not found {e.Message}");
            }
        }


        //PasswordDimenticata
        [HttpPost("PasswordDimenticata")]
        public async Task<IActionResult> PasswordDimenticata([FromBody] PasswordDimenticataRequest model)
        {
            try
            {
                await _userManager.PasswordDimenticata(model.Email);
                return Ok();
            }
            catch(NotExisitingEmailException e)
            {
                return BadRequest(e.Message);
            }
            catch(Exception e)
            {
                return NotFound($"Resource not found {e.Message}");
            }
        }

        //LoginOTP
        [HttpPost("LoginOTP")]
        public async Task<IActionResult> LoginOTP([FromBody] PasswordDimenticataRequest model)
        {
            try
            {
                await _userManager.LoginOTP(model.Email);
                return Ok();
            }
            catch (WrongEmailOrOTPExeption e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return NotFound($"Resource not found {e.Message}");
            }
        }

        //ChangePassword
        [Authorize]
        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest model)
        {
            try
            {
                var email = User?.Identity?.Name;
                await _userManager.ChangeUserPassword(email, model.Password);
                return Ok();
            }
            catch (MalformedDataException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return NotFound($"Resource not found {e.Message}");
            }
        }

        //GetData
        [Authorize]
        [HttpGet("GetMyData")]
        public async Task<IActionResult> GetMyData()
        {
            try
            {
                var email = User?.Identity?.Name;
                var data = await _userManager.GetUserData(email);
                return Ok(data);
            }
            catch (NotExisitingEmailException e)
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
