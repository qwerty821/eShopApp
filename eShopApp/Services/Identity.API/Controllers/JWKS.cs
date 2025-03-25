using Identity.API.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace Identity.API.Controllers
{
    [Route(".well-known")]
    [ApiController]
    public class JWKSController : ControllerBase
    {
        private readonly IdentityDBContext _context;

        public JWKSController(IdentityDBContext context)
        {
            _context = context;
        }

        // GET requests at '/.well-known/jwks.json'
        [HttpGet("jwks.json")]
        public IActionResult GetJWKS()
        {
            var keys = _context.SigningKeys.Where(k => k.IsActive).ToList();

            var jwks = new
            {
                keys = keys.Select(k => new
                {
                    kty = "RSA",    // Key type (RSA)
                    use = "sig",    // Usage (sig for signature)
                    kid = k.KeyId,  // Key ID to identify the key
                    alg = "RS256",  // Algorithm (RS256 for RSA SHA-256)
                    n = Base64UrlEncoder.Encode(GetModulus(k.PublicKey)),  
                    e = Base64UrlEncoder.Encode(GetExponent(k.PublicKey))  
                })
            };

            return Ok(jwks);
        }

         
        private byte[] GetModulus(string publicKey)
        {
            var rsa = RSA.Create();

            rsa.ImportRSAPublicKey(Convert.FromBase64String(publicKey), out _);

            var parameters = rsa.ExportParameters(false);

            rsa.Dispose();
            if (parameters.Modulus == null)
            {
                throw new InvalidOperationException("RSA parameters are not valid.");
            }

            return parameters.Modulus;
        }

        private byte[] GetExponent(string publicKey)
        {
            var rsa = RSA.Create();

            rsa.ImportRSAPublicKey(Convert.FromBase64String(publicKey), out _);

            var parameters = rsa.ExportParameters(false);

            rsa.Dispose();

            if (parameters.Exponent == null)
            {
                throw new InvalidOperationException("RSA parameters are not valid.");
            }

            return parameters.Exponent;
        }
    }
}