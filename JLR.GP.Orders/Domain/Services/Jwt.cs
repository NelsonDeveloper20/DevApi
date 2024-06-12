using ApiPortal_DataLake.Domain.Contracts;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ApiPortal_DataLake.Domain.Services
{
    public class Jwt: IJwt
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<Jwt> _logger;

        public Jwt(
            IConfiguration configuration, 
            ILogger<Jwt> logger
        )
        {
            this._configuration = configuration;
            this._logger = logger;
        }

        public string CreateToken(List<Claim> claims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this._configuration["Jwt:Secret"]));
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(int.Parse(this._configuration["Jwt:Expiration"])),
                Issuer = this._configuration["Jwt:Issuer"],
                Audience = this._configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public string ParseToken(string authorization)
        {
            string jwt;

            if (string.IsNullOrEmpty(authorization))
            {
                this._logger.LogError($"No se ha ingresado ningún token");
                return string.Empty;
            }

            try
            {
                string[] prases = authorization.Split(' ');
                jwt = prases[1];
                return jwt;
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Formato de autorización incorrecto: {ex.Message} {ex.StackTrace}");
                return string.Empty;
            }
        }

        public string ReadToken(string jwtInput, string key)
        {
            var jwtHandler = new JwtSecurityTokenHandler();

            if (!jwtHandler.CanReadToken(jwtInput)) throw new Exception("El token no tiene un formato JWT valido");

            var token = jwtHandler.ReadJwtToken(jwtInput);
            var value = token.Claims.First(c => c.Type == key).Value;

            return value;
        }

        public IEnumerable<string> ReadsToken(string jwtInput, string key)
        {
            var jwtHandler = new JwtSecurityTokenHandler();

            if (!jwtHandler.CanReadToken(jwtInput)) throw new Exception("El token no tiene un formato JWT valido");

            var token = jwtHandler.ReadJwtToken(jwtInput);
            var values = token.Claims.Where(c => c.Type == key).Select(c => c.Value);

            return values;
        }

        public bool ValidateToken(string authorization)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this._configuration["Jwt:Secret"]));
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(authorization, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = this._configuration["Jwt:Issuer"],
                    ValidAudience = this._configuration["Jwt:Audience"],
                    IssuerSigningKey = securityKey
                }, out SecurityToken validatedToken);
            }
            catch(Exception ex)
            {
                this._logger.LogError($"ValidateToken error: {ex.Message} {ex.StackTrace}");
                return false;
            }

            return true;
        }

        public async Task<string?> ValidateTokenAD(string ADtoken)
        {
            string tenant = _configuration["AzureAD:TenanId"];
            IConfigurationManager<OpenIdConnectConfiguration> configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>($"https://login.microsoftonline.com/{tenant}/v2.0/.well-known/openid-configuration", new OpenIdConnectConfigurationRetriever());
            OpenIdConnectConfiguration openIdConfig = await configurationManager.GetConfigurationAsync(CancellationToken.None);
            var idclient = _configuration["AzureAD:ClientId"].ToString();
            TokenValidationParameters validationParameters =
                new TokenValidationParameters
                {
                    ValidIssuer = $"https://login.microsoftonline.com/{tenant}/v2.0",
                    ValidAudience = _configuration["AzureAD:ClientId"].ToString(),
                    IssuerSigningKeys = openIdConfig.SigningKeys
                };

            SecurityToken validatedToken;
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            try
            {
                var user = handler.ValidateToken(ADtoken, validationParameters, out validatedToken);
                return validatedToken.ToString();
            }
            catch (Exception ex)
            {
                this._logger.LogError($"ValidateTokenAD: {ex.Message} {ex.StackTrace}");
                return null;
            }
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"])),
                    ValidateLifetime = false
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
                if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    this._logger.LogError($"GetPrincipalFromExpiredToken: Token invalid");
                    return null;
                }

                return principal;
            }
            catch (Exception ex)
            {
                this._logger.LogError($"GetPrincipalFromExpiredToken: {ex.Message} {ex.StackTrace}");
                return null;
            }
            

        }
    }
}
