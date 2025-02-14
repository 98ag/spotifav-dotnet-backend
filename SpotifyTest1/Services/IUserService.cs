using SpotifyTest1.Models;

namespace SpotifyTest1.Services;

public interface IUserService 
{
    string Login(User user);
    
    string SignUp(User user);
}