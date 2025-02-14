using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpotifyTest1.Models;
using SpotifyTest1.Services;

namespace SpotifyTest1.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class UserController(IUserService userService, ITokenService tokenService) : ControllerBase
{
    private readonly ITokenService _tokenService = tokenService;
    private readonly IUserService _userService = userService;
    
    [HttpPost("login")] // route: /user/login
    [AllowAnonymous] // allows anyone to make this request
    public IActionResult Login(User user)
    {
        var token = _userService.Login(user);

        if (string.IsNullOrWhiteSpace(token))
        {
            return BadRequest(new { message = "Username or password is incorrect" });
        }
        
        return Ok(token);
    }

    [HttpGet]
    public IActionResult Get()
    {
        string token = Request.Headers.Authorization.ToString().Replace("Bearer ", "");
        
        ClaimsPrincipal principal = _tokenService.GetPrincipalFromExpiredToken(token);
        
        return Ok(principal.Identity.Name);
    }

    [HttpPost("signup")]
    [AllowAnonymous]
    public IActionResult Register(User user)
    {
        var registeredUser = _userService.SignUp(user);
        
        if (string.IsNullOrEmpty(registeredUser)) return Ok(new { message = "Username already exists." });
        
        return Ok(registeredUser);
    }
}