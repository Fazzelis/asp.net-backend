using restApi.Dtos.User;
using restApi.Models;

namespace restApi.Services;

public interface IUserService
{
    public Dictionary<string, string>? CreateUser(User newUser);
    public string? LogIn(UserLogin userLogin);
    public User? LogInWithJwt(string token);
}