using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using CRUDAPI.Models;
using CRUDAPI.Data;
using System.Diagnostics;


namespace CRUDAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtHelper _jwtHelper;

        public AuthController(IConfiguration configuration)
        {
            _jwtHelper = new JwtHelper(
                configuration["Jwt:SecretKey"],
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"]
            );
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            // Validate the user credentials (In a real scenario, you'd check a DB)
            if (model.Username == "test" && model.Password == "password") // Simple validation for demo
            {
                var token = _jwtHelper.GenerateToken(model.Username);
                return Ok(new { Token = token });
            }

            return Unauthorized("Invalid username or password");
        }
    }

}
