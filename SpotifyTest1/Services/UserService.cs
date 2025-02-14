using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.IdentityModel.Tokens;
using SpotifyTest1.Models;

namespace SpotifyTest1.Services;

public class UserService(ITokenService tokenService) : IUserService
{
    // hardcode new user for this example
    private List<User> _users = [new User { Username = "Admin", Password = "Password" }];
    
    private readonly ITokenService _tokenService = tokenService;

    public string Login(User user)
    {
        // Compare incoming user against existing users.
        // If credentials match, LoginUser will have 'some value' (what??) otherwise it will be null.
        var loginUser = _users.SingleOrDefault(u => u.Username == user.Username && u.Password == user.Password);
        
        if (loginUser == null) { return string.Empty; }
        
        return _tokenService.GenerateAccessToken(user.Username);   
    }

    public string SignUp(User user)
    {
        var userLookup = _users.SingleOrDefault(u => u.Username == user.Username);
        
        if (userLookup != null) { return string.Empty; }
        
        _users.Add(user);
        
        return user.Username;
    }
}