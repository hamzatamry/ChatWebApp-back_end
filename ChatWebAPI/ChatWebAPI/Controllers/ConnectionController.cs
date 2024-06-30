using ChatWeb.Api.Models;
using ChatWeb.Core.Interfaces;
using ChatWeb.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChatWeb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectionController : ControllerBase
    {
        private readonly IConnectionService _connectionService;

        public ConnectionController(IConnectionService connectionService)
        {
            _connectionService = connectionService;
        }

        [HttpGet("")]
        public IActionResult GetAllConnections()
        {
            try
            {
                IEnumerable<Connection> connections = _connectionService.GetAllConnections();
                return Ok(connections);
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("add")]
        public IActionResult AddConnection([FromBody] ConnectionModel connectionModel)
        {
            try
            {
                _connectionService.AddConnection(connectionModel.UserId, connectionModel.ConnectionId);
                return Ok(new { message = "Connection created" });
            }
            catch(Exception ex)
            {
               return BadRequest(ex.Message);
            }
        }

        [HttpPost("update")]
        public IActionResult updateConnection([FromBody] ConnectionModel connectionModel)
        {
            return Ok("Connection updated");
        }

        [HttpPost("delete")]
        public IActionResult deleteConnection(string connectionId)
        {
            return null;
        }
    
    }
}
