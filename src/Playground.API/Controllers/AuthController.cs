using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Serilog.Context;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using static Playground.API.Controllers.AuthController;

namespace Playground.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("Auth")]
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            ILogger<AuthController> logger)
        {
            _logger = logger;
        }

        public record AuthUser(string UserId, string UserName, string AccessGroup);

        [HttpPost("generate-token")]
        public IActionResult GenerateToken([FromBody] AuthUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var secretKey = GetUniqueKey(32);
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId", user.UserId),
                    new Claim("UserName", user.UserName),
                    new Claim("AccessGroup", user.AccessGroup)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            _logger.LogInformation($"[AuthController][GenerateToken] Token gerado com sucesso");

            return Ok(new { Token = tokenHandler.WriteToken(token) });
        }

        private static string GetUniqueKey(int size)
        {
            var secretKey = new byte[size];
            RandomNumberGenerator.Fill(secretKey);
            return Convert.ToBase64String(secretKey);
        }
    }
}
