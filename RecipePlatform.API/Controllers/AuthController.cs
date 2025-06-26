using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipePlatform.BLL.DTOs;

namespace RecipePlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //create / Register api 
        [HttpPost("register")]
        public IActionResult Register(RegisterDto dto)
        {
            // Logic to register a new user
            // This could include saving user details to the database
            return Ok("User registered successfully");
        }



        //authenticate user / Login / verify user credentials
    }
}
