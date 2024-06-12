using ApiPortal_DataLake.Domain.Contracts;
using ApiPortal_DataLake.Domain.Models;
using ApiPortal_DataLake.Domain.Request;
using ApiPortal_DataLake.Domain.Response;
using ApiPortal_DataLake.Domain.Shared.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace ApiPortal_DataLake.Domain.Services
{
    public class AuthService: IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly DBContext _context;
        private readonly ILogger<AuthService> _logger;
        private readonly IJwt _jwt;

        public AuthService(
            IConfiguration configuration,
            DBContext context,
            ILogger<AuthService> logger,
            IJwt jwt
        )
        {
            this._configuration = configuration;
            this._context = context;
            this._logger = logger;
            this._jwt = jwt;
        }

        public async Task<GeneralResponse<JwtResponse>> GetToken(string tokenAD)
        {
            try
            {
                this._logger.LogInformation($"GetToken started: {tokenAD}");
                //var tokenAdValid = await this._jwt.ValidateTokenAD(tokenAD);
                //if (string.IsNullOrEmpty(tokenAdValid))
                //{
                //    this._logger.LogWarning($"GetToken : Token invalid {tokenAD}");
                //    return new GeneralResponse<JwtResponse>(HttpStatusCode.Unauthorized);
                //}

                var correo = this._jwt.ReadToken(tokenAD, "preferred_username");

                this._logger.LogWarning($"GetToken preferred_username: {correo}");

                var usuario = await this._context.Usuarios.Include("Roles.Rol").FirstOrDefaultAsync(u => u.Correo == correo);

                if (usuario == null)
                {
                    this._logger.LogWarning($"Usuario no encontrado : {correo}");
                    return new GeneralResponse<JwtResponse>(HttpStatusCode.BadRequest);
                }

                if (usuario.Estado == "0")
                {
                    this._logger.LogWarning($"Usuario inactivo : {usuario.Correo}");
                    return new GeneralResponse<JwtResponse>(HttpStatusCode.BadRequest);
                }

                var claims = new List<Claim>();

                claims.Add(new Claim("email", $"{usuario.Correo}"));
                claims.Add(new Claim("userName", $"{usuario.Correo}"));
                var nameRol = "";
                if (usuario.Roles != null)
                {
                    foreach (var rol in usuario.Roles)
                    {
                        claims.Add(new Claim(ClaimsConstant.Rol, rol.RolId.ToString()));
                        if (rol.Rol != null && !string.IsNullOrEmpty(rol.Rol.Nombre))
                        {
                            claims.Add(new Claim(ClaimsConstant.RolName, rol.Rol.Nombre));
                            nameRol = rol.Rol.Nombre;
                        }
                    }
                }

                var token = this._jwt.CreateToken(claims);
                var refreshToken = this._jwt.GenerateRefreshToken();

                this._logger.LogInformation($"GetToken token: {token}");
                this._logger.LogInformation($"GetToken refreshToken: {refreshToken}");

                var tokens = this._context.TokenSecurity.Where(t => t.UsuarioCreacion == correo);
                if (tokens != null && tokens.Any())
                {
                    this._context.TokenSecurity.RemoveRange(tokens);
                }
              
                var tokenSecurity = new TokenSecurity()
                {
                     
                    RefreshToken = refreshToken,
                    RefreshTokenExpiryTime = DateTime.Now.AddMinutes(int.Parse(this._configuration["Jwt:RefreshTokenExpiration"])),
                    UsuarioCreacion = correo
                };

                this._context.TokenSecurity.Add(tokenSecurity);

                await this._context.SaveChangesAsync();
                var users = this._context.Usuarios.ToList();            
                //.Include(u => u.Roles) // Carga la entidad Roles relacionada en Usuario
                //    .ThenInclude(ru => ru.Rol)// Carga la entidad Rol relacionada en RolUsuario
                //.ToList(); 
                return new GeneralResponse<JwtResponse>(HttpStatusCode.OK, new JwtResponse() { 
                    unidadNegocio = "", 
                    Rol= nameRol, 
                    Id = usuario.Id.ToString(), 
                    Token = token, 
                    RefreshToken = refreshToken,
                    user= null

                });
            }
            catch (Exception ex)
            {
                this._logger.LogError($"GetToken Error try catch: {JsonConvert.SerializeObject(ex)}");
                return new GeneralResponse<JwtResponse>(HttpStatusCode.InternalServerError);
            }
        }

        public async Task<GeneralResponse<JwtResponse>> GetRefreshToken(RefreshTokenRequest refreshTokenRequest)
        {
            try
            {
                var principal = this._jwt.GetPrincipalFromExpiredToken(refreshTokenRequest.AccessToken);
                if (principal == null)
                {
                    this._logger.LogWarning($"GetRefreshToken : Token invalid {refreshTokenRequest.AccessToken}");
                    return new GeneralResponse<JwtResponse>(HttpStatusCode.Conflict);
                }

                var tokenSecurity = this._context.TokenSecurity.FirstOrDefault(t => t.RefreshToken == refreshTokenRequest.RefreshToken);

                if (tokenSecurity == null || tokenSecurity.RefreshToken != refreshTokenRequest.RefreshToken || tokenSecurity.RefreshTokenExpiryTime <= DateTime.Now)
                {
                    this._logger.LogWarning($"GetRefreshToken : Invalid access token or refresh token {refreshTokenRequest.RefreshToken}");
                    return new GeneralResponse<JwtResponse>(HttpStatusCode.Conflict);
                }

                var correo = this._jwt.ReadToken(refreshTokenRequest.AccessToken, "email");

                var token = this._jwt.CreateToken(principal.Claims.Where(c => c.Type != "aud").ToList());
                var refreshToken = this._jwt.GenerateRefreshToken();

                this._context.TokenSecurity.Remove(tokenSecurity);

                var newTokenSecurity = new TokenSecurity()
                {
                    RefreshToken = refreshToken,
                    RefreshTokenExpiryTime = DateTime.Now.AddMinutes(int.Parse(this._configuration["Jwt:RefreshTokenExpiration"])),
                    UsuarioCreacion = correo
                };

                this._context.TokenSecurity.Add(newTokenSecurity);

                await this._context.SaveChangesAsync();

                return new GeneralResponse<JwtResponse>(HttpStatusCode.OK, new JwtResponse() { Token = token, RefreshToken = refreshToken });
            }
            catch (Exception ex)
            {
                this._logger.LogError($"GetRefreshToken Error try catch: {JsonConvert.SerializeObject(ex)}");
                return new GeneralResponse<JwtResponse>(HttpStatusCode.InternalServerError);
            }
        }
    }
}
