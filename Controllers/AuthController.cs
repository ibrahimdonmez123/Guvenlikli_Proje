using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Guvenlikli_Proje;
using System.Security.Cryptography;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace Guvenlikli_Proje.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly byte[] _jwtSecretKey = new byte[16]; 

        public AuthController(UserService userService)
        {
            _userService = userService;

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(_jwtSecretKey);
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User loginUser)
        {
            var user = _userService.GetUserByUsernameAndPassword(loginUser.Username, loginUser.Password);

            if (user == null)
                return Unauthorized();

            var token = JwtHelper.GenerateJwtToken(_jwtSecretKey, "Issuer", "Audience", user);

            return Ok(new { Token = token });
        }
    }
}
