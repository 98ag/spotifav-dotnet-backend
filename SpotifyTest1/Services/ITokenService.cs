using System.Security.Claims;

namespace SpotifyTest1.Services;

public interface ITokenService
{
    string GenerateAccessToken(string username);
    
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}