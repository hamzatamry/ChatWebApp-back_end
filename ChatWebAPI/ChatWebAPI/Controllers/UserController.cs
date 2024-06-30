using ChatWeb.Api.Models;
using ChatWeb.Core.Interfaces.Services;
using ChatWeb.Core.Models;
using ChatWeb.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatWeb.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("")]
        public IActionResult GetAllUsers()
        {
            try
            {
                IEnumerable<User> users = _userService.GetAllUsers();

                //temporary solution
                //not a good solution i should retrieve only id and email in the first place from db
                var userBasicInfos = users.Select(u => new
                {
                    UserID = u.Id,
                    Email = u.Email
                });
                
                return Ok(userBasicInfos);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
