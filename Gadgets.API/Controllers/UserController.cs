using Gadgets.API.Data;
using Gadgets.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Gadgets.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private GadgetsDbContext context;
        public UserController(GadgetsDbContext context)
        {
            this.context = context;
        }

        [HttpPost("")]
        public async Task<User> Create([FromBody]User user)
        {
            return await context.Users.CreateAsync(user);
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody]User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            if (!await context.Users.Check(user))
            {
                return Unauthorized();
            }

            return Ok(GetToken());
        }

        private JWTToken GetToken()
        {
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.Configuration["JWT:Secret"]));
            var credentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);

            var jwtOptions = new JwtSecurityToken(
                issuer: ConfigurationManager.Configuration["JWT:ValidIssuer"],
                audience: ConfigurationManager.Configuration["JWT:ValidAudience"],
                claims: new List<Claim>(),
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);

            var tokenStr = new JwtSecurityTokenHandler().WriteToken(jwtOptions);

            return new JWTToken() { Token = tokenStr };
        }
    }
}
