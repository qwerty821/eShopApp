using Identity.API.Models.DTO;
using Identity.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography;
using Identity.API.Contexts;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
         
        private readonly IConfiguration _configuration;
        private readonly IdentityDBContext _context;
        public AuthController(IConfiguration configuration, IdentityDBContext context)
        {
            _configuration = configuration;
            _context = context;
        }


        [HttpPost("login")]
        public async Task<IActionResult> UserLogin([FromBody] LoginDTO loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Email.ToLower() == loginDto.Username.ToLower());

            if (user == null)
            {
                user = await _context.Users
                    .Include(u => u.UserRoles)
                        .ThenInclude(ur => ur.Role)
                    .FirstOrDefaultAsync(u => u.Firstname.ToLower() == loginDto.Username.ToLower());
            }

            if (user == null)
            {
                return Unauthorized(new { code = 1, message = "Invalid credentials." });
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password);
            if (!isPasswordValid)
            {
                return Unauthorized(new { code = 1, message = "Invalid credentials." });
            }

            var tokenStr = GenerateJwtToken(user);

            var roles = user.UserRoles.Select(ur => ur.Role.Name).ToList();
            
            Response.Cookies.Append("token", tokenStr, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                //SameSite = SameSiteMode.Strict, // nu merge din alt domen 
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddHours(1),
                Path = "/"
            });

            var response = new
            {
                firstname = user.Firstname,
                lastname = user.Lastname,
                username = user.Username,
                email = user.Email,
                roles = roles,

            };

            return Ok(response);
        }

        // /api/auth/login-admin for admin panel
        [HttpPost("login-admin")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            Console.WriteLine($"logindata = {loginDto.Username} - {loginDto.Password}");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Email.ToLower() == loginDto.Username.ToLower());

            if (user == null)
            {
                user = await _context.Users
                    .Include(u => u.UserRoles)
                        .ThenInclude(ur => ur.Role)
                    .FirstOrDefaultAsync(u => u.Firstname.ToLower() == loginDto.Username.ToLower());
            }

            if (user == null)
            {
                return Unauthorized(new { code = 1, message = "Invalid credentials." });
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password);
            if (!isPasswordValid)
            {
                return Unauthorized(new { code = 1, message = "Invalid credentials." });
            }

            var tokenStr = GenerateJwtToken(user);

            var roles = user.UserRoles.Select(ur => ur.Role.Name).ToList();

            var response = new
            {
                code = 0,
                data = new
                {
                    id = user.Id,
                    username = user.Email,
                    realName = $"{user.Firstname} {user.Lastname}",
                    roles = roles,
                    homePath = "/workspace",
                    accessToken = tokenStr
                },
                error = "null",
                message = "ok",

            };

            return Ok(response);
        }


        private string GenerateJwtToken(User user, Client? client = null)
        {
            var signingKey = _context.SigningKeys.FirstOrDefault(k => k.IsActive);
            if (signingKey == null)
            {
                throw new Exception("No active signing key available.");
            }
            var privateKeyBytes = Convert.FromBase64String(signingKey.PrivateKey);
             
            var rsa = RSA.Create();
            
            rsa.ImportRSAPrivateKey(privateKeyBytes, out _);
           
            var rsaSecurityKey = new RsaSecurityKey(rsa)
            {
                KeyId = signingKey.KeyId
            };
            var creds = new SigningCredentials(rsaSecurityKey, SecurityAlgorithms.RsaSha256);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, user.Firstname),
                new Claim(ClaimTypes.NameIdentifier, user.Email),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("UserId", user.Id.ToString()),
                new Claim("RealName".ToString(), user.Lastname + " " + user.Firstname)
            };

            foreach (var userRole in user.UserRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole.Role.Name));
            }
             
            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],  
                audience: client?.ClientURL ?? "web-eshop",  
                claims: claims, 
                expires: DateTime.UtcNow.AddHours(1),  
                signingCredentials: creds  
            );
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.WriteToken(tokenDescriptor);
 
            return token;
        }

        [HttpGet("codes")]
        [Authorize]
        public IActionResult GetPermissionCodes()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var role = identity?.FindFirst(ClaimTypes.Role)?.Value;
            //var role = identity.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Role);
            if (role == "admin")
            {
                return Ok(new
                {
                    code = 0,
                    type = "success",
                    message = "",
                    result = new[] { "*" }
                });
            }
             
            var permissionCodes = new[] { "AC_100100", "AC_100110" };
            return Ok(new
            {
                code = 0,
                type = "success",
                message = "",
                result = permissionCodes
            });
        }
    }


}
