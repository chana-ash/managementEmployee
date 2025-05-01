using Manage.Api.Models;
using Manage.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Manage.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AuthController(IUserService userService, IConfiguration configuration)
        {
            _configuration = configuration;
            _userService = userService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
           
            var user = await _userService.AuthenticateAsync(loginModel.UserName, loginModel.Password);

            if (user == null)
                return Unauthorized();

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.Name),
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
    };

            
        


        // יצירת סיסמא מוצפנת
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JWT:Key")));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            // יצירת טוקן JWT
            var tokeOptions = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("JWT:Issuer"),
                audience: _configuration.GetValue<string>("JWT:Audience"),
                claims: claims,
                expires: DateTime.Now.AddMinutes(6),  // הגדרת זמן תפוגה
                signingCredentials: signinCredentials
            );

            // החזרת הטוקן ללקוח
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return Ok(new { Token = tokenString });
        }


    }


}

