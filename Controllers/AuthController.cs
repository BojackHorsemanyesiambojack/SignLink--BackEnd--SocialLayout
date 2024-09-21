using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignLinkAPI.Context;
using SignLinkAPI.Models.Auth;
using SignLinkAPI.Models.Tables.UserLayout;
using SignLinkAPI.Services;

namespace SignLinkAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly UserLayoutDb _Context;
        private readonly Auth _Service;

        public AuthController(UserLayoutDb context, Auth service)
        {
            _Context = context;
            _Service = service;
        }

        [HttpPost("Sign-in")]

        public async Task<ActionResult<UserAccountDto>> Authentication([FromBody] AuthenticationInput User)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var Response = await _Service.GetUserByAuthentication(User.UserEmail, User.UserPassword);

                return Ok(Response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Sign-up")]

        public async Task<ActionResult<UserAccountDto>> Register([FromBody] CreateAccountInput Input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var Response = await _Service.CreateUser(Input);

                if(Response == null)
                {
                    return NotFound(Response);
                }

                return Created("Created: ", Response);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest("Invalid Data: " + ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Unknowed error: " + ex.Message);
            }
        }

        [HttpPost ("Get-User")]

        public async Task<ActionResult<UserAccountDto>> GetUser([FromBody]string UserEmail)
        {
            var Result = await _Context.UserAccount.FirstOrDefaultAsync(u => u.UserEmail == UserEmail);
            return Ok(Result);
        }
    }
}
