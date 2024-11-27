using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;

namespace TrisGPOI.Controllers.User.Controllers
{
    [ApiController]
    [Route("")]
    public class UsersController : ControllerBase
    {
        private readonly ICustomerManager _customerManager;
        public UsersController(ICustomerManager customerManager)
        {
            _customerManager = customerManager;
        }


        //Login
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(CustomerLoginRequest model)
        {
            try
            {
                var token = await _customerManager.LoginAsync(model.ToCoreCustomerLogin());
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
        [SwaggerResponse(StatusCodes.Status200OK, null, null)]
        [SwaggerResponse(StatusCodes.Status404NotFound, null, null)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, null, null)]
        [SwaggerResponse(StatusCodes.Status409Conflict, null, null)]
        public async Task<IActionResult> RegisterAsync(CustomerRegisterRequest model)
        {
            try
            {
                await _customerManager.RegisterAsync(model.ToCoreCustomerRegister());
                return Ok();
            }
            catch(WrongEmailOrPasswordException e)
            {
                return BadRequest(e.Message);
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
