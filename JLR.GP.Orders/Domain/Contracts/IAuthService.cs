using ApiPortal_DataLake.Domain.Request;
using ApiPortal_DataLake.Domain.Response;

namespace ApiPortal_DataLake.Domain.Contracts
{
    public interface IAuthService
    {
        Task<GeneralResponse<JwtResponse>> GetToken(string token);
        Task<GeneralResponse<JwtResponse>> GetRefreshToken(RefreshTokenRequest refreshTokenRequest);
    }
}
