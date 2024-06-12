using System.Security.Claims;

namespace ApiPortal_DataLake.Domain.Contracts
{
    public interface IJwt
    {
        string CreateToken(List<Claim> claims);
        string ParseToken(string authorization);
        bool ValidateToken(string authorization);
        string ReadToken(string jwtInput, string key);
        IEnumerable<string> ReadsToken(string jwtInput, string key);
        Task<string?> ValidateTokenAD(string ADtoken);
        string GenerateRefreshToken();
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);
    }
}
