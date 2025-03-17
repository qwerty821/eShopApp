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
        // /api/Auth/Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            // doar pentru mai multe aplicatii (ex.: mobile sau daca api-ul va fi expus) 
            
            //var client = _context.Clients
            //    .FirstOrDefault(c => c.ClientId == loginDto.ClientId);
            //if (client == null)
            //{
            //    return Unauthorized("Invalid client credentials.");
            //}
           
            var user = await _context.Users
                .Include(u => u.UserRoles)  
                    .ThenInclude(ur => ur.Role)  
                .FirstOrDefaultAsync(u => u.Email.ToLower() == loginDto.Email.ToLower());
            
            if (user == null)
            {
                return Unauthorized("Invalid credentials.");
            }
            
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password);
            if (!isPasswordValid)
            {
                return Unauthorized("Invalid credentials.");
            }
            //  authentication is successful, generate a JWT token.
            var token = GenerateJwtToken(user);
            
            
            return Ok(new { Token = token });
        }
       
        private string GenerateJwtToken(User user, Client? client = null)
        {
            var signingKey = _context.SigningKeys.FirstOrDefault(k => k.IsActive);
            if (signingKey == null)
            {
                throw new Exception("No active signing key available.");
            }
            var privateKeyBytes = Convert.FromBase64String(signingKey.PrivateKey);
            // Create a new RSA instance for cryptographic operations
            var rsa = RSA.Create();
            // Import the RSA private key into the RSA instance
            rsa.ImportRSAPrivateKey(privateKeyBytes, out _);
            // Create a new RsaSecurityKey using the RSA instance
            var rsaSecurityKey = new RsaSecurityKey(rsa)
            {
                // Assign the Key ID to link the JWT with the correct public key
                KeyId = signingKey.KeyId
            };
            var creds = new SigningCredentials(rsaSecurityKey, SecurityAlgorithms.RsaSha256);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                // JWT ID (jti) claim with a unique identifier for the token
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, user.Firstname),
                new Claim(ClaimTypes.NameIdentifier, user.Email),
                new Claim(ClaimTypes.Email, user.Email)
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
    }
}
