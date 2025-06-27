using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipePlatform.BLL.DTOs;
using RecipePlatform.BLL.Interfaces.Services;

namespace RecipePlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }




        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var tokenOrError = await _authService.RegisterAsync(dto);

            if (tokenOrError.StartsWith("Invalid") || tokenOrError.Contains("Password") || tokenOrError.Contains("failed"))
                return BadRequest(tokenOrError);

            return Ok(new { token = tokenOrError });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var tokenOrError = await _authService.LoginAsync(dto);
            if (tokenOrError == "Invalid email or password")
                return Unauthorized();


            // redirect if autheticated
            return Ok(new { token = tokenOrError });
        }

    }
}
