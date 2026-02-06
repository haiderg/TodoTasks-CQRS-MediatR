using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using TodoTasks.Application.Common.Interfaces;

namespace TodoTasks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtTokenService _jwtTokenService;

        public AuthController(IJwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            // Example only — normally validate via Application service
            if (request.Email != "admin@test.com" || request.Password != "123")
                return Unauthorized();

            var token = _jwtTokenService.GenerateToken(
                Guid.NewGuid(),
                request.Email,
                "Admin");

            return Ok(new { token });
        }
    }
}
