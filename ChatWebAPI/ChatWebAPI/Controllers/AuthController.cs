using ChatWeb.Api.Models;
using ChatWeb.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatWeb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authService;

        public AuthController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterModel model)
        {
            try
            {
                _authService.Register(model.Email, model.Password);
                return Ok("Registration succeeded");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            try
            {
                Tuple<int, string> userTuple = _authService.Login(model.Email, model.Password);

                int userId = userTuple.Item1;
                string tokenString = userTuple.Item2;

                return Ok(new { UserId = userId, Token = tokenString });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
